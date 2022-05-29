using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircleLine : MonoBehaviour
{
    public int segments;
    public float xradius;
    public float yradius;
    public float baseAngle; // 시작 앵글
    public Color lineColor;
    public Color hideColor; // 투명 색깔
    public List<DrawCircleLine> subLines; // 회색 비트들 저장용. LineManageScript에서 꺼내서 씀
    public bool hided = false;
    LineRenderer line;

    /// <summary>
    /// 분당 박자 수 = BPM
    /// 초당 박자 수 = BPM / 60
    /// 초당 노트 이동 거리 = noteSpeed
    /// 박자간 거리 = noteSpeed / (BPM / 60)
    /// </summary>
    
    // Start is called before the first frame update
    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public void CreatePoints()
    {
        line.positionCount = segments + 1;
        line.startColor = lineColor;
        line.endColor = lineColor;
        line.startWidth = 0.03f;
        line.endWidth = 0.03f;
        line.useWorldSpace = false;

        float x;
        float y;
        float z = 0f;
        float angle = baseAngle;
        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Cos(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Sin(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, z));
            angle += (360f / segments);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 다시 보이게 할 때는 그냥 circleReload할때 보이게 됨
    public void colorHide()
    {
        line.startColor = hideColor;
        line.endColor = hideColor;
    }

    public void lineRemove()
    {
        foreach (DrawCircleLine lineObj in subLines)
        {
            Destroy(lineObj.gameObject);
        }
        Destroy(this.gameObject);
    }
}
