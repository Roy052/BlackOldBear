using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject boxPrefab;
    public GameObject boxPrefab1;
    public GameObject boxPrefab2;
    public GameObject boxPrefab3;
    public GameObject boxPrefab4;
    public GameObject boxPrefab5;

    public bool miniGameEnd = false;

    private float currTime;
    private int WolfNum = 0;
    private Transform[] targets = new Transform[6];
    private float[,] spawnRange = new float[2, 2] { { -9f, 9f }, { -4.5f, 4.5f } };

    // Start is called before the first frame update
    void Start()
    {
        targets[0] = boxPrefab.GetComponent<Transform>();
        targets[1] = boxPrefab1.GetComponent<Transform>();
        targets[2] = boxPrefab2.GetComponent<Transform>();
        targets[3] = boxPrefab3.GetComponent<Transform>();
        targets[4] = boxPrefab4.GetComponent<Transform>();
        targets[5] = boxPrefab5.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if(miniGameEnd == false)
            currTime += Time.deltaTime;

        if(currTime > 0.5)
        {
            float newX = Random.Range(spawnRange[0, 0], spawnRange[0, 1]),
                newY = Random.Range(spawnRange[1, 0], spawnRange[1, 1]),
                newZ = -1;
            targets[WolfNum].position = new Vector3(newX, newY, newZ);

            currTime = 0;

            WolfNum += 1;
            WolfNum = WolfNum % 6;
        }


    }

    private void Awake()
    {



    }
}
