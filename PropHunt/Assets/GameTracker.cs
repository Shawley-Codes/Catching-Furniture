using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTracker : MonoBehaviour
{
    //obtain list of collectables to get max goals and track progression
    List<GameObject> collectables = new List<GameObject>();
    int MaxCollectables = 0;
    public int Collected = 0;

    // Start is called before the first frame update
    void Start()
    {
        //yoinked but failed somehow
        for (int i = 0; i < transform.childCount; ++i)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            //i dont know why obj is failing here.

            //if (obj.FindWithTag("Collectable"))) collectables.Add(obj);
            //if (obj.tag == "Collectable")) collectables.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Collected);
    }
}
