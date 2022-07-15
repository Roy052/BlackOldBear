using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    int stageNum;
    
    int[,] map;
    bool[,] visitableMap;
    readonly int[] stagePerMapSize = new int[3] { 7, 7, 7 };
    readonly int[] stageSum = new int[3] { 12, 12, 12 };

    int[] tempMapArray;

    readonly float[] nodePositions = new float[2] { -6.4f, 15.8f };

    readonly string[,] mapTextHeadArray = { 
        {"Introduction", "Mid Section", "Ending", "End of Ending" },
        { "도입부", "중간부", "결말부", "결말의 끝" } 
    };
    readonly string[,] mapTextMainArray = {
        { "Cave", "Forest", "Temple", "??"},
        { "동굴", "숲", "사원", "??" }
    };

    // 0 : Empty, 1 : Not contain, 2 : Start, 3 : Shop,
    // 4 : Bonfire, 5 : Random Event, 6 : Chest, 7 : Enemy
    // 8 : Mid Boss, 9 : Boss
    readonly int[,] mapContentsRatio = { {1,1,2,1,7,0 },{ 1, 2, 3, 1, 9, 1 }, { 1, 2, 3, 2, 10, 1 } }; 
    List<int> mapContentQueue = new List<int>();
    MapRecorder mapRecorder;

    //Fade
    FadeManager fadeManager;

    GameObject gameManagerObject;
    int languageType = 0;

    //public
    public GameObject node;
    public GameObject[] mapIcons;

    public GameObject nextStageButton;
    public GameObject mapBackground;
    public Sprite[] mapBackgroundSprites;

    public Text mapHeadText, mapHeadShadowText, mapText, mapTextShadow;
    public GameObject mapTextBackground;

   

    void Start()
    {
        fadeManager = GameObject.FindGameObjectWithTag("FadeManager").GetComponent<FadeManager>();

        gameManagerObject = GameObject.Find("GameManager");
        stageNum = gameManagerObject.GetComponent<GameManager>().stageNum;
        mapBackground.GetComponent<SpriteRenderer>().sprite = mapBackgroundSprites[stageNum];

        languageType = gameManagerObject.GetComponent<GameManager>().languageType;

        //Map 생성 파트
        mapRecorder = gameManagerObject.GetComponent<MapRecorder>();

        //기록 없음
        if (mapRecorder.recorded == false)
        {
            //Text 파트
            StartCoroutine(MapTextONandOFF());

            MapArrayBuilding();
            VisitableMapArrayBuilding();

            mapRecorder.RecordMap(map);
            mapRecorder.RecordVisitableMap(visitableMap);

            mapRecorder.currentPosition.y = 0;
            mapRecorder.currentPosition.x = 1;
        }
        //기록 있음
        else
        {
            mapText.text = "";
            mapTextShadow.text = "";
            map = mapRecorder.ReturnMap();
            visitableMap = mapRecorder.ReturnVisitableMap();
        }
            
        StartCoroutine(MapImageBuilding());

        

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

        /*string ForDebug = "visitablemap\n";
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < stagePerMapSize[stageNum]; j++)
            {
                ForDebug += visitableMap[i, j] + ", ";
            }
            ForDebug += "\n";
        }

        Debug.Log(ForDebug);*/
        //visitableMap[1, stagePerMapSize[stageNum] - 1] = true;
    }

        IEnumerator MapImageBuilding()
    {
        float[] position = { 2.5f, 0, -2.5f };

        if (map[1, 0] == 2)
            yield return new WaitForSeconds(1);
        for (int j = 0; j < stagePerMapSize[stageNum]; j++) 
        {
            for (int i = 0; i < 3; i++)
            {
                if(map[i,j] != 0)
                {
                    float positionX = nodePositions[0] + (nodePositions[1] * ((float)j / 7));
                    GameObject tempNode = Instantiate(node, new Vector3(positionX, position[i]), Quaternion.identity);

                    GameObject temp = Instantiate(mapIcons[map[i, j]], 
                        new Vector3(positionX, position[i]), Quaternion.identity);
                    temp.GetComponent<MapIcon>().position = new Vector2(j, i);
                    if (visitableMap[i, j] == false && map[i,j] != 9) temp.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<MapIcon>().sealedIcon[stageNum];

                    if(visitableMap[1,1] == false)
                    {
                        //Fading
                        Color tempColor = new Color(1, 1, 1, 0);
                        tempNode.GetComponent<SpriteRenderer>().color = tempColor;
                        StartCoroutine(FadeManager.FadeOut(tempNode.GetComponent<SpriteRenderer>(), 1));

                        //Fading
                        Color tempColor1 = new Color(1, 1, 1, 0);
                        temp.GetComponent<SpriteRenderer>().color = tempColor1;
                        StartCoroutine(FadeManager.FadeOut(temp.GetComponent<SpriteRenderer>(), 1));
                    }
                }
                if (visitableMap[1, 1] == false)
                    yield return new WaitForSeconds(0.3f);
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

    IEnumerator MapTextONandOFF()
    {
        yield return new WaitForSeconds(0.3f);

        mapHeadText.text = mapTextHeadArray[languageType ,stageNum];
        mapHeadShadowText.text = mapTextHeadArray[languageType ,stageNum];

        mapText.text = mapTextMainArray[languageType ,stageNum];
        mapTextShadow.text = mapTextMainArray[languageType, stageNum];

        Color color = new Color(1, 1, 1, 0);
        Color color1 = new Color(0.25f, 0.25f, 0.25f, 0);

        StartCoroutine(FadeManager.FadeOut(mapTextBackground.GetComponent<SpriteRenderer>(), 1));
        while(color.a < 1)
        {
            mapHeadText.color = color;
            mapHeadShadowText.color = color1;
            mapText.color = color;
            mapTextShadow.color = color1;

            color.a += Time.fixedDeltaTime;
            color1.a += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(6.2f);

        StartCoroutine(FadeManager.FadeIn(mapTextBackground.GetComponent<SpriteRenderer>(), 1));
        while (color.a > 0)
        {
            mapHeadText.color = color;
            mapHeadShadowText.color = color1;

            mapText.color = color;
            mapTextShadow.color = color1;

            color.a -= Time.fixedDeltaTime;
            color1.a -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
