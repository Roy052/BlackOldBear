using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MapManager : MonoBehaviour
{
    int stageNum = 0;
    
    int[,] map;
    int[] stagePerMapSize = new int[3] { 7, 8, 9 };
    int[] stageSum = new int[3] { 12, 14, 16 };

    int[] tempMapArray;

    // 0 : Empty, 1 : Not contain, 2 : Start, 3 : Shop,
    // 4 : Bonfire, 5 : Random Event, 6 : Chest, 7 : Enemy
    // 8 : Mid Boss, 9 : Boss
    int[,] mapContentsRatio = { {1,1,2,1,7,0 },{ 1, 2, 3, 1, 9, 1 }, { 1, 2, 3, 2, 10, 1 } }; 
    List<int> mapContentQueue = new List<int>();
    public GameObject node;
    public GameObject[] mapIcons;

    void Start()
    {
        map = new int[stagePerMapSize[stageNum], 3];
        MapArrayBuilding();
        MapImageBuilding();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MapArrayBuilding()
    {
        //Initialize
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
                map[i + 1, 0] = 1; map[i + 1, 1] = 1; map[i + 1, 2] = 1;
            }
            else if (tempMapArray[i] == 2)
            {
                tempMapApperance = UnityEngine.Random.Range(0, 2);
                for (int j = 0; j < 2; j++)
                {
                    map[i + 1, mapApperance_two[tempMapApperance, j]] = 1;
                }
            }
            else
            {
                map[i + 1, 1] = 1;
            }
        }

        

        //Map Apperance To Map Content
        map[0, 1] = 2; // Start
        map[stagePerMapSize[stageNum] - 1, 1] = 9; // Boss
        map[stagePerMapSize[stageNum] - 2, 1] = 4; // Bonfire

        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < mapContentsRatio[stageNum,i]; j++)
            {
                mapContentQueue.Add(i + 3);
            }
        }

        var mixedMapContentQueue = mapContentQueue.OrderBy(a => Guid.NewGuid()).ToList();

        int count = 0;
        for(int i = 1; i < stagePerMapSize[stageNum] - 1; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(map[i, j] == 1)
                {
                    map[i,j] = mixedMapContentQueue[count++];
                }
            }
        }

        string ForDebug = "";
        for (int i = 0; i < stagePerMapSize[stageNum]; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                ForDebug += map[i,j] + ", ";
            }
            ForDebug += "\n";
        }

        Debug.Log(ForDebug);
    }

    void MapImageBuilding()
    {
        float[] position = { 2.5f, 0, -2.5f };
        for(int i = 0; i < stagePerMapSize[stageNum]; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(map[i,j] != 0)
                Instantiate(node, new Vector3(-7.9f + (15.8f * ((float)i / 7)), position[j]), Quaternion.identity);
                Instantiate(mapIcons[map[i,j]], new Vector3(-7.9f + (15.8f * ((float)i / 7)), position[j]), Quaternion.identity);
            }
        }
    }
}
