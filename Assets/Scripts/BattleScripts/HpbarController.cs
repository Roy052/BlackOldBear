using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpbarController : MonoBehaviour
{
    StatusManager sm;
    Slider Hpbar;
    int Maxhp;
    int hp;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("GameManager").GetComponent<StatusManager>();
        Hpbar = this.GetComponent<Slider>();

        Maxhp = sm.GetMaxhealth();
        
        Debug.Log(Hpbar.maxValue);
    }

    // Update is called once per frame
    void Update()
    {
        hp = sm.GetHealth();
        Hpbar.value = hp;
        // Debug.Log(hp);
        if (Hpbar.value <= 0)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);
    }
}
