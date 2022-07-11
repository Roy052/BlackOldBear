using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory_Info
{
    public int[] rarityArray = { 1, 2, 3, 1, 2, 3, 2, 1, 1, 1 };
    public int[] typeArray = { 0, 1, 2, 0, 1, 2, 0, 1, 2, 0 };
    public int[] whereArray = { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 };

    public string[,] nameArray = {
        { "Hot Choco Milk Mug",
        "Distorted Cup",
        "Toy Hammer",
        "Old Cellphone",
        "Peanut Bar",
        "Can Food",
        "Floppy Disk",
        "Old Phone",
        "Toy Knife",
        "Broken Shooting Sign"},

        { "핫 초코 밀크 머그컵",
        "일그러진 큐브",
        "장난감 망치",
        "오래된 휴대폰",
        "땅콩바",
        "캔 음식",
        "플로피디스크",
        "오래된 전화기",
        "장난감 칼",
        "부서진 사격표지판"}
    };
    public string[,] descriptionArray = {
        { "After battle ends, Get a 2 heal",
        "Reduce the probability of a strong enemy by 10%.",
        "The scope of judgment is increased by 30%.",
        "The scope of judgment is increased by 10%.",
        "Max health is increased by 10.",
        "Max health is increased by 20.",
        "Gain 300 gold",
        "Gain 3 leather",
        "The scope of judgment is increased by 50%.",
        "Reduce the probability of a strong enemy by 50%."},

        { "전투가 끝날 때, 체력을 2 회복한다.",
        "강한 적이 나올 확률을 10% 줄인다.",
        "판정이 30% 좋아진다.",
        "판정이 10% 좋아진다.",
        "최대 체력을 10 증가시킨다.",
        "최대 체력을 20 증가시킨다.",
        "골드를 300 획득한다.",
        "가죽을 3개 획득한다.",
        "판정이 50% 좋아진다.",
        "강한 적이 나올 확률을 50% 줄인다."}
    };

    public string[,] additionalTextArray = {
        { "Why not enjoy some warm hot choco instead?",
        "Cube is vulnerable to heat",
        "Thank you don don",
        "Talk Play Love",
        "Penuuuuuut",
        "Can can play \"Can Can\"!",
        "The origin of save button",
        "Only 19.99$",
        "Grab the knife.",
        "Who broke this sign?"},

        { "따뜻한 핫초코나 한사발 하지 그래?",
        "큐브는 열에 취약하다",
        "감사합니다! 돈돈",
        "Talk Play Love",
        "피너어어엇",
        "고추참치 고추참치 참치.. 참치..",
        "저장버튼의 근원",
        "단돈 39800원",
        "그래 좋아 까짓껏 칼춤 한번 춰주지",
        "누가 이걸 부러뜨린걸까요?"}
    };
}
