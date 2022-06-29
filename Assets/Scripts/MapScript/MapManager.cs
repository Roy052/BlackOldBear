using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    int stageNum;
    
    int[,] map;
    bool[,] visitableMap;
    int[] stagePerMapSize = new int[3] { 7, 7, 7 };
    int[] stageSum = new int[3] { 12, 12, 12 };

    int[] tempMapArray;

    float[] nodePositions = new float[2] { -6.4f, 15.8f };

    // 0 : Empty, 1 : Not contain, 2 : Start, 3 : Shop,
    // 4 : Bonfire, 5 : Random Event, 6 : Chest, 7 : Enemy
    // 8 : Mid Boss, 9 : Boss
    int[,] mapContentsRatio = { {1,1,2,1,7,0 },{ 1, 2, 3, 1, 9, 1 }, { 1, 2, 3, 2, 10, 1 } }; 
    List<int> mapContentQueue = new List<int>();
    MapRecorder mapRecorder;

    //Fade
    FadeManager fadeManager;

    //public
    public GameObject node;
    public GameObject[] mapIcons;

    public GameObject nextStageButton;
    public GameObject mapBackground;
    public Sprite[] mapBackgroundSprites;


    void Start()
    {
        fadeManager = GameObject.FindGameObjectWithTag("FadeManager").GetComponent<FadeManager>();

        stageNum = GameObject.Find("GameManager").GetComponent<GameManager>().stageNum;
        mapBackground.GetComponent<SpriteRenderer>().sprite = mapBackgroundSprites[stageNum];

        mapRecorder = GameObject.Find("GameManager").GetComponent<MapRecorder>();
        if (mapRecorder.recorded == false)
        {
            MapArrayBuilding();
            VisitableMapArrayBuilding();

            mapRecorder.RecordMap(map);
            mapRecorder.RecordVisitableMap(visitableMap);

            mapRecorder.currentPosition.y = 0;
            mapRecorder.currentPosition.x = 1;
        }
        else
        {
            map = mapRecorder.ReturnMap();
            visitableMap = mapRecorder.ReturnVisitableMap();
        }
            
        MapImageBuilding();

        if(map[1, stagePerMapSize[stageNum]-1] != 1)
        {
            nextStageButton.SetActive(false);
        }

        StartCoroutine(fadeManager.FadeIn(GameManager.FadeTimeGap));
    }


    void MapArrayBuilding()
    {
        //Initialize
        map = new int[3, stagePerMapSize[stageNum]];
        System.Array.Clear(map, 0, map.Length);

        //Temp Map Array
        tempMapArray = new int[stagePerMapSize[stageNum] - 2];
        int tempMapSum = 0;
        float meanSum = stageSum[stageNum] / (float) stagePerMapSize[stageNum];
        Debug.Log(meanSum);

        //First Value In
        tempMapArray[0] = UnityEngine.Random.Range(0, 3) + 1;
        tempMapSum = tempMapArray[0];

        //After Value In
        for (int i = 1; i < tempMapArray.Length; i++){
            int tempValue = UnityEngine.Random.Range(0,3) + 1;
            
            //Current sum is more than mean sum
            if( (tempMapSum + tempValue) > meanSum * (i + 1) )
            {
                if (tempValue != 1)
                    tempValue -= 1;
            }
            else
            {
                if (tempValue != 3)
                    tempValue += 1;
            }

            tempMapArray[i] = tempValue;
            tempMapSum += tempValue;
        }

        //tempMapArray fit in stageSum
        if (stageSum[stageNum] > tempMapSum)
        {
            for (int i = 0; i < stageSum[stageNum] - tempMapSum; i++)
            {
                for(int j = 0; j < tempMapArray.Length ; j++)
                {
                    if(tempMapArray[j] == 3) continue;
                    else
                    {
                        tempMapArray[j]++;
                        break;
                    }
                }
            }

        }
        else if(stageSum[stageNum] < tempMapSum)
        {
            for (int i = 0; i < tempMapSum - stageSum[stageNum] ; i++)
            {
                for (int j = tempMapArray.Length - 1; j > 0; j--)
                {
                    if (tempMapArray[j] == 1) continue;
                    else
                    {
                        tempMapArray[j]--;
                        break;
                    }
                }
            } 
        }

        //Number To Map Apperance
        int[,] mapApperance_two = { { 0, 1 }, { 1, 2 }};

        int tempMapApperance = -1;
        for (int i = 0; i < tempMapArray.Length; i++)
        {
            //Full
            if (tempMapArray[i] == 3)
            {
                map[0, i + 1] = 1; map[1, i + 1] = 1; map[2, i + 1] = 1;
            }
            else if (tempMapArray[i] == 2)
            {
                tempMapApperance = UnityEngine.Random.Range(0, 2);
                for (int j = 0; j < 2; j++)
                {
                    map[mapApperance_two[tempMapApperance, j], i + 1] = 1;
                }
            }
            else
            {
                map[1, i + 1] = 1;
            }
        }

        //Map Apperance To Map Content
        map[1, 0] = 2; // Start
        map[1, stagePerMapSize[stageNum] - 1] = 9; // Boss
        map[1, stagePerMapSize[stageNum] - 2] = 4; // Bonfire

        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < mapContentsRatio[stageNum,i]; j++)
            {
                mapContentQueue.Add(i + 3);
            }
        }

        var mixedMapContentQueue = mapContentQueue.OrderBy(a => Guid.NewGuid()).ToList();

        int count = 0;
        for(int i = 0; i < 3 ; i++)
        {
            for(int j = 1; j < stagePerMapSize[stageNum] - 1; j++)
            {
                if(map[i, j] == 1)
                {
                    map[i,j] = mixedMapContentQueue[count++];
                }
            }
        }

        string ForDebug = "map\n";
        for (int i = 0; i < 3; i++)
        {
            for(int j = 0; j < stagePerMapSize[stageNum]; j++)
            {
                ForDebug += map[i,j] + ", ";
            }
            ForDebug += "\n";
        }

        Debug.Log(ForDebug);

        
    }

    void VisitableMapArrayBuilding()
    {
        visitableMap = new bool[3, stagePerMapSize[stageNum]];
        System.Array.Clear(visitableMap, 0, visitableMap.Length);
        visitableMap[1, 0] = true;

        string ForDebug = "visitablemap\n";
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < stagePerMapSize[stageNum]; j++)
            {
                ForDebug += visitableMap[i, j] + ", ";
            }
            ForDebug += "\n";
        }

        Debug.Log(ForDebug);
    }

        void MapImageBuilding()
    {
        float[] position = { 2.5f, 0, -2.5f };
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < stagePerMapSize[stageNum]; j++)
            {
                if(map[i,j] != 0)
                {
                    float positionX = nodePositions[0] + (nodePositions[1] * ((float)j / 7));
                    Instantiate(node, new Vector3(positionX, position[i]), Quaternion.identity);
                    GameObject temp = Instantiate(mapIcons[map[i, j]], 
                        new Vector3(positionX, position[i]), Quaternion.identity);
                    temp.GetComponent<MapIcon>().position = new Vector2(j, i);
                    if (visitableMap[i, j] == false) temp.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<MapIcon>().sealedIcon;
                }
            }
        }
    }

    public bool Visitable(int x, int y)
    {
        return visitableMap[y, x] && map[y,x] != 1;
    }


    public void SceneMovement(int num, Vector2 position)
    {
        int sceneNum = SceneManager.GetSceneByName("MapScene").buildIndex - 1;
        mapRecorder.currentPosition = position;
        Debug.Log(sceneNum);
        Debug.Log("y : " + (int) position.y + ", x : " + (int) position.x);
        
        //Used node
        if (num != 3) map[(int) position.y, (int) position.x] = 1;

        //Visitable
        visitableMap[(int)position.y, (int)position.x] = true;
        //Top
        if ((int)position.y != 0 && map[(int)position.y - 1, (int)position.x] != 0) 
            visitableMap[(int)position.y - 1, (int)position.x] = true;
        //Bottom
        if ((int)position.y != 2 && map[(int)position.y + 1, (int)position.x] != 0) 
            visitableMap[(int)position.y + 1, (int)position.x] = true;
        //Left
        if ((int)position.x != 0 && map[(int)position.y, (int)position.x - 1] != 0) 
            visitableMap[(int)position.y, (int)position.x - 1] = true;
        // Right
        if ((int)position.x != stagePerMapSize[stageNum] - 1 
            && map[(int)position.y, (int)position.x + 1] != 0) 
            visitableMap[(int)position.y, (int)position.x + 1] = true;

        //Record
        mapRecorder.RecordMap(map);
        mapRecorder.RecordVisitableMap(visitableMap);

        StartCoroutine(LoadSceneWithTerm(GameManager.FadeTimeGap, sceneNum, num));
    }

    IEnumerator LoadSceneWithTerm(float term, int sceneNum, int num)
    {
        StartCoroutine(fadeManager.FadeOut(term));
        yield return new WaitForSeconds(term);
        SceneManager.LoadScene(sceneNum + num);
    }

    public void NextStage()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().stageNum++;
        mapRecorder.recorded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
