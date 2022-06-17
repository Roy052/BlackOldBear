using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory_Info : MonoBehaviour
{
    public int[] rarityArray = {1, 3 };
    public int[] typeArray = {0, 1 };
    
    public string[] nameArray = 
        { "핫 초코 밀크 머그컵", 
        "일그러진 큐브" };
    public string[] descriptionArray = 
        { "전투가 끝날 때, 체력을 2 회복한다.", 
        "받는 데미지를 10% 늘리고 주는 데미지를 20% 늘린다." };
    public string[] additionalTextArray = 
        { "따뜻한 핫초코나 한사발 하지 그래?", 
        "큐브는 열에 취약하다"};
}
