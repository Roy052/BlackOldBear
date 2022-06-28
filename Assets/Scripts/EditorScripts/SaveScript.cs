using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// 인게임에서 데이터 호출 방법
/// 1. PatternData2 객체를 선언.
/// - PatternData2 pData
/// 2. 데이터 로드 함수를 호출.
/// - pData = SaveScript.loadData2(filename)
/// - filename은 확장자 뺴고. "Electronic_2.json"이면 SaveScript.loadData2("Electronic_2");
/// 3. 패턴데이터에서 울프 데이터 추출
/// - List<WolfData2> wData = PatternData2.wolfs;
/// </summary>

[Serializable]
public class WolfData
{
    public int node;
    public int fullBeat;
    public int beat;
    public float angle;
}

[Serializable]
public class PatternData
{
    public float BPM;
    public string BGM;
    public int beat;
    public int segments;
    public float baseAngle;
    public float baseOffset;
    public int difficult;
    public float speed;
    public List<WolfData> wolfs;
}

// 인게임에서 쓰이는 간략화된 데이터
public class WolfData2
{
    public float time;
    public float angle;
}

public class PatternData2
{
    public string BGM;
    public float duration;
    public int difficult;
    public List<WolfData2> wolfs;
}


public class SaveScript
{
    static public void saveData(string filename, float _bpm, string _bgm, int _beat, int _seg, float _ba, float _bo, int _diff, float _speed, List<WolfScript> _WSs)
    {
        PatternData pData = new();

        pData.BPM = _bpm;
        pData.BGM = _bgm;
        pData.beat = _beat;
        pData.segments = _seg;
        pData.baseAngle = _ba;
        pData.baseOffset = _bo;
        pData.difficult = _diff;
        pData.speed = _speed;

        pData.wolfs = new();

        // wolfScript List 정렬
        _WSs.Sort(delegate (WolfScript A, WolfScript B)
        {
            if (A.node < B.node) return -1;
            else if (A.node > B.node) return 1;
            else
            {
                if (A.beat * B.fullBeat < B.beat * A.fullBeat) return -1; // 비트를 정수로 비교
                else if (A.beat * B.fullBeat > B.beat * A.fullBeat) return 1;
                else return 0;
            }
        });

        foreach (WolfScript ws in _WSs)
        {
            WolfData wolf = new();
            wolf.node = ws.node;
            wolf.fullBeat = ws.fullBeat;
            wolf.beat = ws.beat;
            wolf.angle = ws.angle;
            pData.wolfs.Add(wolf);
        }

        DataSave(pData, filename + ".json");
    }

    static public PatternData loadData(string filename)
    {
        PatternData pData = DataLoad(filename + ".json");
        return pData;

    }

    static public PatternData2 loadData2(string filename)
    {
        PatternData pData = DataLoad(filename + ".json");

        PatternData2 newData = new();
        newData.BGM = pData.BGM;
        AudioClip bgmfile = Resources.Load("EditorResource/" + newData.BGM) as AudioClip;
        newData.duration = bgmfile.length;
        newData.difficult = pData.difficult;
        newData.wolfs = new();

        foreach (WolfData wolf in pData.wolfs)
        {
            WolfData2 wolf2 = new();
            float beatPos = wolf.node + ((float)wolf.beat / wolf.fullBeat);
            wolf2.time = beatPos / (pData.BPM / 60) + (pData.baseOffset / 1000);
            wolf2.angle = wolf.angle;
            newData.wolfs.Add(wolf2);
        }

        return newData;
    }

    static public void DataSave(PatternData data, string _fileName)
    {
        try
        {
            string json = JsonUtility.ToJson(data, true);

            if (json.Equals("{}"))
            {
                Debug.Log("json null");
                return;
            }
            string path = "./Assets/Patterns/" + _fileName;
            File.WriteAllText(path, json);

            Debug.Log(json);
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("The file was not found:" + e.Message);
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.Log("The directory was not found: " + e.Message);
        }
        catch (IOException e)
        {
            Debug.Log("The file could not be opened:" + e.Message);
        }
    }

    static public PatternData DataLoad(string _fileName)
    {
        try
        {
            string path = "./Assets/Patterns/" + _fileName;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Debug.Log(json);
                PatternData t = JsonUtility.FromJson<PatternData>(json);
                return t;
            }
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("The file was not found:" + e.Message);
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.Log("The directory was not found: " + e.Message);
        }
        catch (IOException e)
        {
            Debug.Log("The file could not be opened:" + e.Message);
        }
        return default;
    }

    static public float wolfDist(WolfData wolf, float BPM, float baseOffset, float speed)
    {
        float beatPos = wolf.node + ((float)wolf.beat / wolf.fullBeat);
        float time = beatPos / (BPM / 60) + (baseOffset / 1000);
        return time * speed;
    }
}
