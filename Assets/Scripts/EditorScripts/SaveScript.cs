using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

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
