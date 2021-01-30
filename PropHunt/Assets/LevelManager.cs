using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
  public GameObject templatePrefab;
  GameObject currentLevel;

  void Awake() {
    currentLevel = Instantiate(templatePrefab);
    templatePrefab.SetActive(false);
  }

  public void RestartLevel() {
    var oldLevel = currentLevel;
    currentLevel = Instantiate(templatePrefab);
    currentLevel.SetActive(true);
    Destroy(oldLevel);
    UiManager.instance.victoryScreen.SetActive(false);
  }
}
