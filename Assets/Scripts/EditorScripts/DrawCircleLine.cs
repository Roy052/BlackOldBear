using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircleLine : MonoBehaviour
{
    public int segments;
    public float xradius;
    public float yradius;
    public float baseAngle; // ���� �ޱ�
    public Color lineColor;
    public Color hideColor; // ���� ����
    public List<DrawCircleLine> subLines; // ȸ�� ��Ʈ�� �����. LineManageScript���� ������ ��
    public bool hided = false;
    LineRenderer line;

    /// <summary>
    /// �д� ���� �� = BPM
    /// �ʴ� ���� �� = BPM / 60
    /// �ʴ� ��Ʈ �̵� �Ÿ� = noteSpeed
    /// ���ڰ� �Ÿ� = noteSpeed / (BPM / 60)
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

    // �ٽ� ���̰� �� ���� �׳� circleReload�Ҷ� ���̰� ��
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
