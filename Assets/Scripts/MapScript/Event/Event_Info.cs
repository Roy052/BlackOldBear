using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Info
{
    readonly public string[,] eventDescription = {
        {
            "There is a shooting range in the middle of the road.\nI tried to pass by, but there was an eye-catching phrase.\n\"A huge reward of 10 won\".\nShall we try shooting?",
            "A broken racetrack appeared before my eyes.\nOne of the foxes is running away from me.\nShould I catch a fox?",
            "I'm looking at the cards on the table.\nThere is a reward in the card, but there is also a trap.\nShould I pick a card?",
            "Someone appears if I gives money to him,\nHe will give his material.\nShould I pay for the material?",
            "An old hooded old woman talks to me.\n\"If you give me blood, I'll give you money.\"\nShould I give you blood and get money?"
        },
        {
            "길 중간에 사격장이 있다.\n지나치려 했으나 눈길을 끄는 문구가 있었다.\n\"10원으로 엄청난 보상을\"\n사격에 도전할까?",
            "부서진 경마장이 눈 앞에 나타났다.\n여우 중 한 명이 나를 피해 도망치고 있다.\n여우를 잡아야할까?",
            "테이블 위에 놓인 카드들을 보고 있다.\n카드 속에는 보상이 있지만 함정도 있다.\n카드를 뽑을까?",
            "누군가가 나타나 자신에게 돈을 주면\n재료를 선물해준다고 한다. \n돈을 주고 재료를 받을까?",
            "두건을 쓴 늙은 노파가 나에게 말을 건다. \n \"피를 주면 돈을 주마\" 피를 주고 돈을 받을까?"
        }
    };
    readonly public string[,,] eventChoice = { 
        {
            { "Try shooting", "Pass" },
            { "Catch it", "Ignore"},
            { "Pick a card", "Pass"},
            { "Give money", "Ignore" },
            { "Give blood", "Ignore" } 
        },
        {
            { "도전한다", "지나간다" },
            { "잡는다", "무시한다"},
            { "뽑는다", "뽑지 않는다"},
            { "돈을 준다", "무시한다" },
            { "피를 준다", "무시한다" }
        }
    };
    readonly public int[] eventConditionType = { 1, 0, 0, 1, 4 }; //0 : 없음, 1 : 돈, 2 : 대거, 3 : 가죽, 4 : 체력
    readonly public int[] eventConditionValue = { 10, 0, 0, 5, 20};
    
}
