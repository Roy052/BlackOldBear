using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Info
{
    readonly public string[] eventDescription = {
        "길 중간에 사격장이 있다. 지나치려 했으나 눈길을 끄는 문구가 있었다. \"10원으로 엄청난 보상을\" 도전할까?",
        "부서진 경마장이 눈 앞에 나타났다. 여우 중 한 명이 나를 피해 도망치고 있다. 잡을까?",
        "누군가가 나타나 자신에게 돈을 주면 재료를 선물해준다고 한다. 돈을 줄까?",
        ""
    };
    readonly public string[,] eventChoice = { 
        { "도전한다", "지나간다" },
        { "잡는다", "무시한다"},
        { "돈을 준다", "무시한다" },
        { "", "" } };
    readonly public int[] eventConditionType = { 1, 0, 1, 0 };
    readonly public int[] eventConditionValue = { 10, 0, 5, 0};
    
}
