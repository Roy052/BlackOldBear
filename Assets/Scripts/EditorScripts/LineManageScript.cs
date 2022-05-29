using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManageScript : MonoBehaviour
{
    public GameObject circleObj, lineObj, wolfObj, sliderObj;

    public int segments; // �� �׸��µ� ���� ���� ����
    public float baseAngle; // ���� �ޱ�
    public float BPM;
    public int beat; // �� ���ڸ� �� ��Ʈ�� �ɰ� ����
    public float noteSpeed; // �ʴ� ��Ʈ �̵� �Ÿ�
    public float boundary; // �� �ֺ� ����ִ� ���� ũ��
    public Color majorBeatColor; // ���� ����
    public Color minorBeatColor; // ��Ʈ ����
    float screenWidth = 10f; // ȭ�� ũ��. ȭ�鿡 ǥ���� ���� ���� ���µ� ���
    List<DrawCircleLine> circleList = new(); // ���� ���� �����ϴ� ����Ʈ
    List<DrawLine> lineList = new(); // ����� �� �����ϴ� ����Ʈ
    List<WolfScript> wolfList = new();

    public float musicLength = 1f;
    float currentPos = 0f;
    bool movingSlider = false;
    float gap; // ���ڰ� �Ÿ�
    float subGap; // ���� ���� ��Ʈ �� �Ÿ�
    int visibleBeat; // ȭ�鿡 ǥ�õǴ� ���� ����

    /// <summary>
    /// �д� ���� �� = BPM
    /// �ʴ� ���� �� = BPM / 60
    /// �ʴ� ��Ʈ �̵� �Ÿ� = noteSpeed
    /// ���ڰ� �Ÿ� = noteSpeed / (BPM / 60)
    /// </summary>
    
    void gapRenew()
    {
        gap = noteSpeed / (BPM / 60); // ���ڰ� �Ÿ�
        subGap = gap / beat; // ���� ���� ��Ʈ �� �Ÿ�
        visibleBeat = (int)(screenWidth / gap) + 1; // ȭ�鿡 ǥ�õǴ� ���� ����
    }

    void circleCheck()
    {
        // �ʿ��� ��� ���� ������ ������ ����
        if (circleList.Count > visibleBeat)
        {
            for (int i = visibleBeat; i < circleList.Count; i++)
            {
                circleList[i].lineRemove();
            }
            circleList = circleList.GetRange(0, visibleBeat);
        }
        else if (circleList.Count < visibleBeat)
        {
            for (int i = circleList.Count; i < visibleBeat; i++)
            {
                GameObject inst = Instantiate(circleObj);
                DrawCircleLine instScript = inst.GetComponent<DrawCircleLine>();
                instScript.subLines = new();
                circleList.Add(instScript);
            }
        }

        // �ʿ��� ȸ�� ���� ������ ������ ����
        // ȸ�� ������ ������ beat - 1��
        for (int i = 0; i < visibleBeat; i++)
        {
            if (circleList[i].subLines.Count > beat - 1)
            {
                for (int j = beat - 1; j < circleList[i].subLines.Count; j++)
                {
                    circleList[i].subLines[j].lineRemove();
                }
                circleList[i].subLines = circleList[i].subLines.GetRange(0, beat - 1);
            }
            else if (circleList[i].subLines.Count < beat - 1)
            {
                for (int j = circleList[i].subLines.Count; j < beat - 1; j++)
                {
                    GameObject subInst = Instantiate(circleObj);
                    DrawCircleLine subInstScript = subInst.GetComponent<DrawCircleLine>();
                    circleList[i].subLines.Add(subInstScript);
                }
            }
        }
    }

    void lineCheck()
    {
        // ����� ���� ������ ����
        if (lineList.Count > segments)
        {
            for (int i = segments; i < lineList.Count; i++)
            {
                Destroy(lineList[i].gameObject);
            }
            lineList = lineList.GetRange(0, segments);
        }
        else if (lineList.Count < segments)
        {
            for (int i = lineList.Count; i < segments; i++)
            {
                GameObject inst = Instantiate(lineObj);
                DrawLine instScript = inst.GetComponent<DrawLine>();
                lineList.Add(instScript);
            }
        }
    }

    void circleReload()
    {
        float dist = (currentPos * noteSpeed) % gap;

        for (int i = 0; i < visibleBeat; i++)
        {
            // ���� ���� ����
            DrawCircleLine instScript = circleList[i];
            instScript.segments = segments;
            instScript.xradius = boundary + gap * i - dist;
            instScript.yradius = boundary + gap * i - dist;
            instScript.baseAngle = baseAngle;
            instScript.lineColor = majorBeatColor;
            instScript.CreatePoints();

            if (instScript.xradius < boundary)
            {
                instScript.colorHide(); // �����
            }

            // ��Ʈ ���� ����
            for (int j = 0; j < beat - 1; j++)
            {
                DrawCircleLine subInstScript = instScript.subLines[j];
                subInstScript.segments = segments;
                subInstScript.xradius = boundary + gap * i + subGap * (j + 1) - dist;
                subInstScript.yradius = boundary + gap * i + subGap * (j + 1) - dist;
                subInstScript.baseAngle = baseAngle;
                subInstScript.lineColor = minorBeatColor;
                subInstScript.CreatePoints();

                if (subInstScript.xradius < boundary)
                {
                    subInstScript.colorHide(); // �����
                }
            }
        }

        wolfReload();
    }

    void lineReload()
    {
        for (int i = 0; i < segments; i++)
        {
            DrawLine instScript = lineList[i];
            instScript.lineColor = majorBeatColor;
            instScript.xstart = transform.position.x;
            instScript.ystart = transform.position.y;
            instScript.zstart = transform.position.z;
            instScript.angle = baseAngle + (360f / segments) * i;
            instScript.CreatePoints();
        }
    }

    void wolfReload()
    {
        foreach (WolfScript wolf in wolfList)
        {
            float wolfAngle = wolf.angle;
            float dist = boundary + (gap * wolf.beat) - (currentPos * noteSpeed);

            if (dist < boundary)
            {
                wolf.transform.position = new Vector3(15, 10, -1);
            }
            else
            {
                float wolfx = Mathf.Cos(Mathf.Deg2Rad * wolfAngle) * dist;
                float wolfy = Mathf.Sin(Mathf.Deg2Rad * wolfAngle) * dist;
                wolf.transform.position = new Vector3(wolfx, wolfy, -1 + wolfy / 5);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gapRenew(); // gap, subGap, visibleBeat �ʱ�ȭ
        circleCheck(); // ���� ���� ����
        circleReload(); // ����� ���� ����
        lineCheck(); // ���� ���� ��ġ ���߱�
        lineReload(); // ����� ���� ��ġ ���߱�
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 ��ġ
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // �����̴� �� ������ ���
            if (pos.y > 4.2 && pos.y < 4.8)
            {
                // �����̴� ��ġ�� -8 ~ 8, �� ���� 16
                movingSlider = true;
            }

            // ���� ������ ���
            else
            {
                Vector2 myPos = new Vector2(transform.position.x + 1, transform.position.y);
                Vector2 mousePos = new Vector2(pos.x, pos.y);
                float mouseAngle = Vector2.Angle(myPos, mousePos);
                if (pos.y < myPos.y)
                {
                    mouseAngle = 360 - mouseAngle;
                }

                // ���� ���� ���
                float angleNum = Mathf.Round((mouseAngle - baseAngle) / (360f / segments));
                float wolfAngle = angleNum * (360f / segments) + baseAngle;

                // ���� ��ġ ���
                myPos = new Vector2(transform.position.x, transform.position.y);
                float scrollDist = currentPos * noteSpeed;
                float nearestBoundary = Mathf.Ceil(currentPos * noteSpeed / subGap) * subGap
                                        - scrollDist + boundary;
                Debug.Log("nearest : " + nearestBoundary);
                float mouseDist = Vector2.Distance(myPos, mousePos);

                if (mouseDist > nearestBoundary - (subGap / 2))
                {
                    float dist = mouseDist - boundary + scrollDist;
                    int beatNum = (int)(Mathf.Round(dist / subGap));
                    float beatPos = beatNum / beat + (float)(beatNum % beat) / beat;
                    float wolfx = Mathf.Cos(Mathf.Deg2Rad * wolfAngle) * (boundary + gap * beatPos - scrollDist);
                    float wolfy = Mathf.Sin(Mathf.Deg2Rad * wolfAngle) * (boundary + gap * beatPos - scrollDist);

                    GameObject inst = Instantiate(wolfObj);
                    WolfScript instScript = inst.GetComponent<WolfScript>();
                    instScript.beat = beatPos;
                    instScript.angle = wolfAngle;
                    instScript.lineManagerScript = this;
                    inst.transform.position = new Vector3(wolfx, wolfy, -1 + wolfy / 5);
                    wolfList.Add(instScript);
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (movingSlider)
            {
                // ���콺 ��ġ
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (pos.x < -8)
                {
                    sliderObj.transform.position = new Vector3(-8f, 4.5f, -2.1f);
                    currentPos = 0;
                }
                else if (pos.x > 8)
                {
                    sliderObj.transform.position = new Vector3(8f, 4.5f, -2.1f);
                    currentPos = musicLength;
                }
                else
                {
                    sliderObj.transform.position = new Vector3(pos.x, 4.5f, -2.1f);
                    currentPos = musicLength * ((pos.x + 8) / 16);
                }
                Debug.Log(currentPos);

                circleReload();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (movingSlider)
            {
                movingSlider = false;
            }
        }

        // ���콺 �� ����
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0)
        {
            currentPos += 0.2f;
            if (currentPos > musicLength)
                currentPos = musicLength;

            sliderObj.transform.position = new Vector3(currentPos / musicLength * 16 - 8, 4.5f, -2.1f);

            circleReload();
            Debug.Log(currentPos);
        }
        else if (wheelInput < 0)
        {
            currentPos -= 0.2f;
            if (currentPos < 0)
                currentPos = 0;

            sliderObj.transform.position = new Vector3(currentPos / musicLength * 16 - 8, 4.5f, -2.1f);

            circleReload();
            Debug.Log(currentPos);
        }
    }

    public void wolfRemove(WolfScript instScript)
    {
        wolfList.Remove(instScript);
        Destroy(instScript.gameObject);
    }
}
