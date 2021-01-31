using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UiManager : MonoBehaviour {
    public GameObject overlay;
    public GameObject victoryScreen;
    public TMP_Text count;
    public TMP_Text objective;
    public static UiManager instance;

    void Awake() {
        instance = this;
        overlay.SetActive(true);
        victoryScreen.SetActive(false);
    }

    public void ShowVictory() {
        victoryScreen.SetActive(true);
    }

    public void UpdateCount(int collected){
        //change tmp to provided int
        count.text = collected.ToString();
    }

    public void setObjective(String newObj)
    {
        //change tmp to provided string
        objective.text = newObj;
    }
}
