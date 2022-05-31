using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    int stageNum = 0;
    
    int[,] map;
    int[] stagePerMapSize = new int[3] { 7, 8, 9 };
    int[] stageSum = new int[3] { 12, 14, 17 };

    int[] tempMapArray;
    
    // Start is called before the first frame update
    void Start()
    {
        map = new int[stagePerMapSize[stageNum], 3];
        MapBuilding();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MapBuilding()
    {
        //Initialize
        System.Array.Clear(map, 0, map.Length);

        //Temp Map Array
        tempMapArray = new int[stagePerMapSize[stageNum] - 2];
        int tempMapSum = 0;
        int beforeMapNum = 0; // Before Random Num
        float meanSum = stageSum[stageNum] / stagePerMapSize[stageNum];

        //First Value In
        tempMapArray[0] = Random.Range(0, 3) + 1;
        tempMapSum = tempMapArray[0];
        beforeMapNum = tempMapArray[0];

        //After Value In
        for (int i = 1; i < tempMapArray.Length; i++){
            int tempValue = Random.Range(0,3) + 1;
            
            //Current sum is more than mean sum
            if( (tempMapSum + tempValue) > meanSum * (i + 1) )
            {
                if (tempValue == 3)
                {
                    tempValue = beforeMapNum;
                }
                else if(tempValue == 2 && (beforeMapNum == 3 || beforeMapNum == 1))
                {
                    tempValue = 1;
                }
            }
            else
            {
                if(tempValue == 2)
                {
                    tempValue = 3;
                }
                else if(tempValue == 1)
                {
                    if (beforeMapNum == 2)
                        tempValue = 2;
                    else tempValue = 3;
                }

            }

            tempMapArray[i] = tempValue;
            beforeMapNum = tempValue;
        }
    }
}
