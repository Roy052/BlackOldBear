using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject boxPrefab;
    private float currTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;

        if(currTime > 0.5)
        {
            float newX = Random.Range(-15f, 15f), newY = Random.Range(-10f, 10f), newZ = Random.Range(-10f, 10f);
            GameObject add_object = Instantiate(boxPrefab, new Vector3(newX, newY, newZ), Quaternion.identity);

            Destroy(add_object, 0.5f);
            currTime = 0;
        }
    }

    private void Awake()
    {



    }
}
