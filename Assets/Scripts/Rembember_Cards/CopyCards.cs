using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;

public class CopyCards : MonoBehaviour
{
    public GameObject Card_Front;
    public GameObject Card_Back;
    public Camera Camera;
    public Sprite[] cardImages;
    public Sprite frontImage;
    public TextMeshProUGUI clockText;

    int i, j, k;
    int count = 0;
    int First_Choice = -999;
    GameObject Before_GameObject;
    Vector3 MousePosition;

    int Left = 36;
    float timeLimit = 30;
    
    List<GameObject> id_front = new List<GameObject>();
    List<int> backNumber = new List<int>() { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17, 18, 18 };
    List<int> mixed_backNumber = new List<int>();
    bool gameEnd = false;


    // Start is called before the first frame update
    void Start()
    {

        mixed_backNumber = backNumber.OrderBy(a => Guid.NewGuid()).ToList(); 

        for (i = -8; i <= 8; i = i + 2)
        {
            for (j = -3; j <= 4; j = j + 2)
            {
                count += 1;
                GameObject input_Front = Instantiate(Card_Front, new Vector3(i, j, -1), Quaternion.identity);

                input_Front.name = count.ToString();

                id_front.Add(input_Front);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        timeLimit -= Time.deltaTime;
        string timeText = (Mathf.Round(timeLimit * 10) / 10).ToString();
        clockText.text = timeText;


        if (Left == 0 || timeLimit <= 0)
        {
            gameEnd = true;
        }

        if (gameEnd == false && Input.GetMouseButtonDown(0))
        {
            MousePosition = Input.mousePosition;
            MousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, 15f);

            if (hit)
            {
                int now_id = Convert.ToInt32(hit.transform.gameObject.name.ToString());

                if(First_Choice < 0){
                    First_Choice = mixed_backNumber[now_id-1];
                    Before_GameObject = hit.transform.gameObject;
                    Before_GameObject.GetComponent<SpriteRenderer>().sprite = cardImages[FindCardSpriteNum(First_Choice)];
                }
                else
                {
                    if(FindCardSpriteNum(First_Choice) == FindCardSpriteNum(mixed_backNumber[now_id-1]) && hit.transform.gameObject != Before_GameObject)
                    {

                        Debug.Log("correct");
                        hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite
                            = cardImages[FindCardSpriteNum(mixed_backNumber[now_id - 1])];
                        First_Choice = -1;
                        Left = Left - 2;
                        hit.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        Before_GameObject.GetComponent<BoxCollider2D>().enabled = false;
                        
                    }
                    else if(First_Choice == mixed_backNumber[now_id-1] && hit.transform.gameObject == Before_GameObject){
                        Debug.Log("click same cards");
                        StartCoroutine(WaitAndFlip(Before_GameObject, null));
                        First_Choice = -1;
                    }
                    else
                    {
                        hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite 
                            = cardImages[FindCardSpriteNum(mixed_backNumber[now_id - 1])];
                        StartCoroutine(WaitAndFlip(Before_GameObject, hit.transform.gameObject));
                        First_Choice = -1;
                    }
                }
            }
        }
    }

    public int FindCardSpriteNum(int num)
    {
        if (num <= 3) // Money +  [1,2,3]
        {
            return 0;
        }
        else if (num <= 5) // Money - [4,5]
        {
            return 1;
        }
        else if (num <= 7) // Dagger [6,7]
        {
            return 2;
        }
        else if (num <= 9) // Leather [8,9]
        {
            return 3;
        }
        else if (num <= 10) // DamageUP [10]
        {
            return 4;
        }
        else if (num <= 11) // ArmorUP [11]
        {
            return 5;
        }
        else if (num <= 14) // Time + [12,13,14]
        {
            return 6;
        }
        else if (num <= 15) // Time - [15]
        {
            return 7;
        }
        else if (num <= 17) // Heal + [16,17]
        {
            return 8;
        }
        else // Heal - [18]
        {
            return 9;
        }
    }

    IEnumerator WaitAndFlip(GameObject first, GameObject second)
    {
        yield return new WaitForSeconds(0.5f);
        if(first != null)
            first.GetComponent<SpriteRenderer>().sprite = frontImage;
        if(second != null)
            second.GetComponent<SpriteRenderer>().sprite = frontImage;
    }
}
