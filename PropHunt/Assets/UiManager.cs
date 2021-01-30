using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour {
  public GameObject overlay;
  public GameObject victoryScreen;
  public static UiManager instance;

  void Awake() {
    instance = this;
    overlay.SetActive(true);
    victoryScreen.SetActive(false);
  }

  public void ShowVictory() {
    victoryScreen.SetActive(true);
  }
}
