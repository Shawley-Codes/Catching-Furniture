using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {
    public GameObject overlay;
    public GameObject victoryScreen;
    public Text count;
    public Text objective;
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
        count.text = collected.ToString();
    }

    public void setObjective(GameObject newItem)
    {
        objective.text = newItem.name;
    }
}
