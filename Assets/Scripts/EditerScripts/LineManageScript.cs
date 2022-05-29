using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManageScript : MonoBehaviour
{
    public GameObject circleObj, lineObj, wolfObj;

    public int segments; // 원 그리는데 들어가는 직선 갯수
    public float baseAngle; // 시작 앵글
    public float BPM;
    public int beat; // 한 박자를 몇 비트로 쪼갤 건지
    public float noteSpeed; // 초당 노트 이동 거리
    public float boundary; // 곰 주변 비어있는 공간 크기
    public Color majorBeatColor; // 박자 색깔
    public Color minorBeatColor; // 비트 색깔
    float screenWidth = 10f; // 화면 크기. 화면에 표시할 박자 갯수 세는데 사용
    List<DrawCircleLine> circleList = new(); // 박자 라인 저장하는 리스트
    List<DrawLine> lineList = new(); // 방사형 선 저장하는 리스트
    List<wolfScript> wolfList = new();


    /// <summary>
    /// 분당 박자 수 = BPM
    /// 초당 박자 수 = BPM / 60
    /// 초당 노트 이동 거리 = noteSpeed
    /// 박자간 거리 = noteSpeed / (BPM / 60)
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        float gap = noteSpeed / (BPM / 60); // 박자간 거리
        float visibleBeat = screenWidth / gap + 1; // 화면에 표시되는 박자 갯수
        float subGap = gap / beat; // 박자 속의 비트 간 거리
        for (int i = 0; i < visibleBeat; i++)
        {
            // 박자 라인 생성
            GameObject inst = Instantiate(circleObj);
            DrawCircleLine instScript = inst.GetComponent<DrawCircleLine>();
            instScript.segments = segments;
            instScript.xradius = boundary + gap * i;
            instScript.yradius = boundary + gap * i;
            instScript.baseAngle = baseAngle;
            instScript.lineColor = majorBeatColor;

            List<DrawCircleLine> subLineList = new();

            // 비트 라인 생성
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
            // 마우스 위치
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPos = new Vector2(transform.position.x + 1, transform.position.y);
            Vector2 mousePos = new Vector2(pos.x, pos.y);
            float mouseAngle = Vector2.Angle(myPos, mousePos);
            if (pos.y < myPos.y)
            {
                mouseAngle = 360 - mouseAngle;
            }

            // 늑대 각도 계산
            float gap = noteSpeed / (BPM / 60); // 박자간 거리
            float subGap = gap / beat; // 박자 속의 비트 간 거리
            float angleNum = Mathf.Round((mouseAngle - baseAngle) / (360f / segments));
            float wolfAngle = angleNum * (360f / segments) + baseAngle;
            Debug.Log("WolfAngle : " + wolfAngle);

            // 늑대 위치 계산
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
