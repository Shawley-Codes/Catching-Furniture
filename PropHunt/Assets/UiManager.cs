﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UiManager : MonoBehaviour {
    public GameObject overlay;
    public GameObject victoryScreen;
    public GameObject pauseMenu;
    public TMP_Text count;
    public TMP_Text objective;
    public static UiManager instance;

    void Awake() {
        instance = this;
        overlay.SetActive(true);
        victoryScreen.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void ShowVictory() {
        victoryScreen.SetActive(true);
    }

    public void UpdateCount(int collected, int total){
      count.text = collected + " / " + total;
    }

    public void setObjective(String newObj)
    {
        //change tmp to provided string
        objective.text = newObj;
    }


    //check for puase button
    void Update()
    {

        //click escape to change time scale
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            else if (Time.timeScale == 0)
            {
                Debug.Log("high");
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
        }
    }


}
