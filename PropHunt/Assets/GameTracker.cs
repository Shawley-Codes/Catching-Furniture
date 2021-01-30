using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTracker : MonoBehaviour
{
    //obtain list of collectables to get max goals and track progression
    List<GameObject> collectablesList = new List<GameObject>();
    int MaxCollectables = 0;
    public int Collected = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Collected);
    }
}
