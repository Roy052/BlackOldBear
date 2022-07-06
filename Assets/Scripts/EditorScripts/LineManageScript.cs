using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LineManageScript : MonoBehaviour
{
    public GameObject circleObj, lineObj, wolfObj, sliderObj, sideMenuObj, musicObj, canvas;
    public TMP_Text nodeText;
    SideMenu sideMenuScript;

    public int segments; // 원 그리는데 들어가는 직선 갯수
    public float baseAngle; // 시작 앵글
    public float baseOffset; // 시작 시간 오프셋
    public float BPM;
    public int beat; // 한 박자를 몇 비트로 쪼갤 건지
    public float noteSpeed; // 초당 노트 이동 거리
    public float musicSpeed; // 노래 재생 속도
    public float boundary; // 곰 주변 비어있는 공간 크기
    [HideInInspector] public int wolfType = 0; // 울프 종류
    [SerializeField] Color judgeColor; // 판정라인 색깔
    [SerializeField] Color majorBeatColor; // 박자 색깔
    [SerializeField] Color minorBeatColor; // 비트 색깔
    float screenWidth = 10f; // 화면 크기. 화면에 표시할 박자 갯수 세는데 사용

    DrawCircleLine judgeCircleLine = null;
    List<DrawCircleLine> circleList = new(); // 박자 라인 저장하는 리스트
    List<DrawLine> lineList = new(); // 방사형 선 저장하는 리스트
    List<TMP_Text> nodeTextList = new(); // 라인 번호 저장하는 리스트
    List<WolfScript> wolfList = new();
    MusicManage musicScript;

    [HideInInspector] public float musicLength = 1f;
    [HideInInspector] public float currentPos = 0f;
    [HideInInspector] public bool playState = false;
    bool movingSlider = false;
    float gap; // 박자간 거리
    float subGap; // 박자 속의 비트 간 거리
    int visibleBeat; // 화면에 표시되는 박자 갯수

    /// <summary>
    /// 분당 박자 수 = BPM
    /// 초당 박자 수 = BPM / 60
    /// 초당 노트 이동 거리 = noteSpeed
    /// 박자간 거리 = noteSpeed / (BPM / 60)
    /// </summary>


    // Start is called before the first frame update
    void Start()
    {
        sideMenuScript = sideMenuObj.GetComponent<SideMenu>();
        musicScript = musicObj.GetComponent<MusicManage>();

        GameObject inst = Instantiate(circleObj);
        judgeCircleLine = inst.GetComponent<DrawCircleLine>();

        gapRenew(); // gap, subGap, visibleBeat 초기화
        circleCheck(); // 원형 라인 생성
        lineCheck(); // 방사형 라인 생성
        nodeTextCheck(); // 라인 번호 생성
        circleReload(); // 원형 라인 위치 맞추기
        lineReload(); // 방사형 라인 위치 맞추기
        nodeTextReload(); // 라인 번호 위치 맞추기
    }

    void gapRenew()
    {
        gap = noteSpeed / (BPM / 60); // 박자간 거리
        subGap = gap / beat; // 박자 속의 비트 간 거리
        visibleBeat = (int)(screenWidth / gap) + 1; // 화면에 표시되는 박자 갯수
    }

    void nodeTextCheck()
    {
        int count = nodeTextList.Count;
        // 원형 선 갯수만큼 필요한 텍스트 갯수를 맞춤
        if (count > visibleBeat)
        {
            for (int i = visibleBeat; i < count; i++)
            {
                Destroy(nodeTextList[i]);
            }
            nodeTextList = nodeTextList.GetRange(0, visibleBeat);
        }
        else if (count < visibleBeat)
        {
            for (int i = count; i < visibleBeat; i++)
            {
                TMP_Text inst = Instantiate(nodeText);
                inst.transform.SetParent(canvas.transform);
                nodeTextList.Add(inst);
            }
        }
    }

    void circleCheck()
    {

        // 필요한 흰색 원형 라인의 갯수를 맞춤
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

        // 필요한 회색 원형 라인의 갯수를 맞춤
        // 회색 라인의 갯수는 beat - 1개
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
        // 방사형 선의 갯수를 맞춤
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

    void nodeTextReload()
    {
        // 지금까지의 거리를 gqp으로 나누고 남은 자투리
        float fullDist = (currentPos - baseOffset / 1000) * noteSpeed;
        float dist = fullDist % gap;
        float count = (int)(fullDist / gap);

        for (int i = 0; i < visibleBeat; i++)
        {
            float xpos = boundary + gap * i - dist;
            if (xpos < boundary)
            {
                nodeTextList[i].SetText("");
            }
            else
            {
                nodeTextList[i].SetText("" + (i + count));
                Vector3 pos = new Vector3(xpos + 0.3f, 0.3f, -5f);
                Vector3 newPos = Camera.main.WorldToScreenPoint(pos);
                nodeTextList[i].transform.position = newPos;
            }
        }
    }

    public void circleReload()
    {
        float dist = ((currentPos - baseOffset / 1000) * noteSpeed) % gap;

        // 판정 라인
        if (judgeCircleLine)
        {
            DrawCircleLine judgeScript = judgeCircleLine;
            judgeScript.segments = segments;
            judgeScript.xradius = boundary;
            judgeScript.yradius = boundary;
            judgeScript.baseAngle = baseAngle;
            judgeScript.lineColor = judgeColor;
            judgeScript.CreatePoints(0.07f, -0.1f);
        }

        for (int i = 0; i < visibleBeat; i++)
        {
            // 박자 라인 생성
            DrawCircleLine instScript = circleList[i];
            instScript.segments = segments;
            instScript.xradius = boundary + gap * i - dist;
            instScript.yradius = boundary + gap * i - dist;
            instScript.baseAngle = baseAngle;
            instScript.lineColor = majorBeatColor;
            instScript.CreatePoints();

            if (instScript.xradius < boundary)
            {
                instScript.colorHide(); // 숨기기
            }

            // 비트 라인 생성
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
                    subInstScript.colorHide(); // 숨기기
                }
            }
        }
        nodeTextReload();
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

    // 늑대들의 위치를 재조정
    void wolfReload()
    {
        foreach (WolfScript wolf in wolfList)
        {
            float wolfAngle = wolf.angle;
            float beatPos = wolf.node + ((float)wolf.beat / wolf.fullBeat);
            float dist = boundary + (gap * beatPos) - ((currentPos - baseOffset / 1000) * noteSpeed);

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

    // Update is called once per frame
    void Update()
    {
        if (playState)
        {
            currentPos = musicScript.mSource.time;
            setSlider();
            circleReload();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                // 마우스 위치
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ray2D ray = new Ray2D(pos, Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                // 슬라이더 바 조절일 경우
                if (pos.y > 4.2 && pos.y < 4.8)
                {
                    // 슬라이더 위치는 -8 ~ 8, 총 길이 16
                    movingSlider = true;
                }

                // 늑대 생성일 경우
                else
                {
                    if (hit.collider != null && hit.transform.gameObject.name == "LineManager")
                    {
                        Vector2 myPos = new Vector2(transform.position.x + 1, transform.position.y);
                        Vector2 mousePos = new Vector2(pos.x, pos.y);
                        float mouseAngle = Vector2.Angle(myPos, mousePos);
                        if (pos.y < myPos.y)
                        {
                            mouseAngle = 360 - mouseAngle;
                        }

                        // 늑대 각도 계산
                        float angleNum = Mathf.Round((mouseAngle - baseAngle) / (360f / segments));
                        float wolfAngle = angleNum * (360f / segments) + baseAngle;

                        // 늑대 위치 계산
                        myPos = new Vector2(transform.position.x, transform.position.y);
                        float scrollDist = (currentPos - baseOffset / 1000) * noteSpeed;
                        float nearestBoundary = Mathf.Ceil(scrollDist / subGap) * subGap
                                                - scrollDist + boundary;

                        float mouseDist = Vector2.Distance(myPos, mousePos);

                        if (mouseDist > nearestBoundary - (subGap / 2))
                        {
                            float dist = mouseDist - boundary + scrollDist;
                            int beatCount = (int)(Mathf.Round(dist / subGap));
                            float beatPos = beatCount / beat + (float)(beatCount % beat) / beat;
                            float wolfx = Mathf.Cos(Mathf.Deg2Rad * wolfAngle) * (boundary + gap * beatPos - scrollDist);
                            float wolfy = Mathf.Sin(Mathf.Deg2Rad * wolfAngle) * (boundary + gap * beatPos - scrollDist);

                            GameObject inst = Instantiate(wolfObj);
                            WolfScript instScript = inst.GetComponent<WolfScript>();
                            instScript.node = beatCount / beat;
                            instScript.fullBeat = beat;
                            instScript.beat = beatCount % beat;
                            instScript.angle = wolfAngle;
                            instScript.type = wolfType;
                            instScript.lineManagerScript = this;
                            inst.transform.position = new Vector3(wolfx, wolfy, -1 + wolfy / 5);
                            wolfList.Add(instScript);
                        }
                    }
                }
            }
            if (Input.GetMouseButton(0))
            {
                if (movingSlider)
                {
                    // 마우스 위치
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
                    Debug.Log(currentPos + " / " + musicLength);

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

            // 마우스 휠 조작
            float wheelInput = Input.GetAxis("Mouse ScrollWheel");
            if (wheelInput < 0)
            {
                currentPos += 0.2f;
                if (currentPos > musicLength)
                    currentPos = musicLength;

                sliderObj.transform.position = new Vector3(currentPos / musicLength * 16 - 8, 4.5f, -2.1f);

                Debug.Log(currentPos + " / " + musicLength);
                circleReload();
            }
            else if (wheelInput > 0)
            {
                currentPos -= 0.2f;
                if (currentPos < 0)
                    currentPos = 0;

                sliderObj.transform.position = new Vector3(currentPos / musicLength * 16 - 8, 4.5f, -2.1f);

                Debug.Log(currentPos + " / " + musicLength);
                circleReload();
            }
        }
    }

    void setSlider()
    {
        float fullTime = 0f;
        if (musicScript.mSource.clip)
        {
            fullTime = musicScript.mSource.clip.length;

            if (currentPos < 0)
            {
                sliderObj.transform.position = new Vector3(-8f, 4.5f, -2.1f);
            }
            else if (currentPos > fullTime)
            {
                sliderObj.transform.position = new Vector3(8f, 4.5f, -2.1f);
            }
            else
            {
                sliderObj.transform.position = new Vector3(currentPos / fullTime * 16 - 8, 4.5f, -2.1f);
            }
        }
    }

    public void wolfRemove(WolfScript instScript)
    {
        wolfList.Remove(instScript);
    }

    public void saveData(string filename, string bgm, int diff)
    {
        SaveScript.saveData(filename, BPM, bgm, beat, segments, baseAngle, baseOffset, diff, noteSpeed, wolfList);
    }

    public PatternData loadData(string filename)
    {
        PatternData pData = SaveScript.loadData(filename);
        BPM = pData.BPM;
        beat = pData.beat;
        segments = pData.segments;
        baseAngle = pData.baseAngle;
        baseOffset = pData.baseOffset;
        noteSpeed = pData.speed;

        // 생성되어 있던 늑대들 삭제
        foreach (WolfScript wolf in wolfList)
        {
            Destroy(wolf.gameObject);
        }
        wolfList = new();

        // 늑대 불러오기
        foreach (WolfData wolf in pData.wolfs)
        {
            GameObject inst = Instantiate(wolfObj);
            WolfScript instScript = inst.GetComponent<WolfScript>();
            instScript.node = wolf.node;
            instScript.fullBeat = wolf.fullBeat;
            instScript.beat = wolf.beat;
            instScript.angle = wolf.angle;
            instScript.type = wolf.type;
            instScript.lineManagerScript = this;
            wolfList.Add(instScript);
        }

        gapRenew(); // gap, subGap, visibleBeat 초기화
        circleCheck(); // 원형 라인 생성
        lineCheck(); // 방사형 라인 생성
        circleReload(); // 원형 라인 위치 맞추기, 늑대 위치 조정
        lineReload(); // 방사형 라인 위치 맞추기

        return pData;
    }

    public void setBPM(float _bpm)
    {
        BPM = _bpm;
        gapRenew();
        circleCheck();
        circleReload();
    }

    public void setBeat(int _beat)
    {
        beat = _beat;
        gapRenew();
        circleCheck();
        circleReload();
    }

    public void setSegments(int _seg)
    {
        segments = _seg;
        lineCheck();
        circleReload();
        lineReload();
    }

    public void setBaseOffset(float _bo)
    {
        baseOffset = _bo;
        circleReload();
    }

    public void setBaseAngle(float _ao)
    {
        baseAngle = _ao;
        lineCheck();
        circleReload();
        lineReload();
    }

    public void setWolfSpeed(float _speed)
    {
        noteSpeed = _speed;
        gapRenew();
        circleCheck();
        circleReload();
    }

    public void setMusicSpeed(float _speed)
    {
        musicSpeed = _speed;
        musicScript.mSource.pitch = _speed;
    }

    public void musicPlay()
    {
        playState = true;
    }

    public void musicPause()
    {
        playState = false;
    }

    public void musicStop()
    {
        playState = false;
        currentPos = 0;
        setSlider();
        circleReload();
    }
}
