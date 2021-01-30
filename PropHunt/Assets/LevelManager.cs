using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameColor {
  public Material solidMaterial;
  public Material transparentMaterial;
  public string name;
}

public class LevelManager : MonoBehaviour {
  public static LevelManager instance;
  public GameObject templatePrefab;
  GameObject currentLevel;
  public GameColor[] gameColors;

  void Awake() {
    instance = this;
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
