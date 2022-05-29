using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManageScript : MonoBehaviour
{
    public GameObject circleObj, lineObj, wolfObj;

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
    List<wolfScript> wolfList = new();


    /// <summary>
    /// �д� ���� �� = BPM
    /// �ʴ� ���� �� = BPM / 60
    /// �ʴ� ��Ʈ �̵� �Ÿ� = noteSpeed
    /// ���ڰ� �Ÿ� = noteSpeed / (BPM / 60)
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        float gap = noteSpeed / (BPM / 60); // ���ڰ� �Ÿ�
        float visibleBeat = screenWidth / gap + 1; // ȭ�鿡 ǥ�õǴ� ���� ����
        float subGap = gap / beat; // ���� ���� ��Ʈ �� �Ÿ�
        for (int i = 0; i < visibleBeat; i++)
        {
            // ���� ���� ����
            GameObject inst = Instantiate(circleObj);
            DrawCircleLine instScript = inst.GetComponent<DrawCircleLine>();
            instScript.segments = segments;
            instScript.xradius = boundary + gap * i;
            instScript.yradius = boundary + gap * i;
            instScript.baseAngle = baseAngle;
            instScript.lineColor = majorBeatColor;

            List<DrawCircleLine> subLineList = new();

            // ��Ʈ ���� ����
            for (int j = 1; j <= beat + 1; j++)
            {
                GameObject subInst = Instantiate(circleObj);
                DrawCircleLine subInstScript = subInst.GetComponent<DrawCircleLine>();
                subInstScript.segments = segments;
                subInstScript.xradius = boundary + gap * i + subGap * j;
                subInstScript.yradius = boundary + gap * i + subGap * j;
                subInstScript.baseAngle = baseAngle;
                subInstScript.lineColor = minorBeatColor;
                subLineList.Add(subInstScript);
            }

            instScript.subLines = subLineList;
            circleList.Add(instScript);
        }

        for (int i = 0; i < (segments + 1); i++)
        {
            GameObject inst = Instantiate(lineObj);
            DrawLine instScript = inst.GetComponent<DrawLine>();
            instScript.lineColor = majorBeatColor;
            instScript.xstart = transform.position.x;
            instScript.ystart = transform.position.y;
            instScript.zstart = transform.position.z;
            instScript.angle = baseAngle + (360f / segments) * i;
            lineList.Add(instScript);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 ��ġ
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPos = new Vector2(transform.position.x + 1, transform.position.y);
            Vector2 mousePos = new Vector2(pos.x, pos.y);
            float mouseAngle = Vector2.Angle(myPos, mousePos);
            if (pos.y < myPos.y)
            {
                mouseAngle = 360 - mouseAngle;
            }

            // ���� ���� ���
            float gap = noteSpeed / (BPM / 60); // ���ڰ� �Ÿ�
            float subGap = gap / beat; // ���� ���� ��Ʈ �� �Ÿ�
            float angleNum = Mathf.Round((mouseAngle - baseAngle) / (360f / segments));
            float wolfAngle = angleNum * (360f / segments) + baseAngle;
            Debug.Log("WolfAngle : " + wolfAngle);

            // ���� ��ġ ���
            myPos = new Vector2(transform.position.x, transform.position.y);
            float dist = Vector2.Distance(myPos, mousePos) - boundary;
            int distNum = (int)(Mathf.Round(dist / subGap));
            int nodeNum = distNum / beat;
            int beatNum = distNum % beat;
            float wolfx = Mathf.Cos(Mathf.Deg2Rad * wolfAngle) * (boundary + gap * (nodeNum + (float)beatNum / beat));
            float wolfy = Mathf.Sin(Mathf.Deg2Rad * wolfAngle) * (boundary + gap * (nodeNum + (float)beatNum / beat));

            if (distNum >= 0)
            {
                GameObject inst = Instantiate(wolfObj);
                wolfScript instScript = inst.GetComponent<wolfScript>();
                instScript.nodeNum = nodeNum;
                instScript.beatNum = beatNum;
                instScript.angle = wolfAngle;
                instScript.lineManagerScript = this;
                inst.transform.position = new Vector3(wolfx, wolfy, -1 + wolfy / 5);
                wolfList.Add(instScript);
            }
        }
    }

    public void wolfRemove(wolfScript instScript)
    {
        wolfList.Remove(instScript);
        Destroy(instScript.gameObject);
    }
}
