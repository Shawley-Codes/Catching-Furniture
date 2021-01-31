﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
  List<List<int>> availableSingleColors = new List<List<int>>();
  int nextSingleColor = 0;
  List<List<int>> availableDualColors = new List<List<int>>();
  int nextDualColor = 0;
  List<List<int>> availableTripleColors = new List<List<int>>();
  int nextTripleColor = 0;

  void Awake() {
    instance = this;
    currentLevel = Instantiate(templatePrefab);
    templatePrefab.SetActive(false);
  }

  void Shuffle<T>(List<T> v) {
    for (int i = 0; i < v.Count; i++) {
      T temp = v[i];
      int j = Random.Range(i, v.Count);
      v[i] = v[j];
      v[j] = temp;
    }
  }

  public void RestartLevel() {
    var oldLevel = currentLevel;
    currentLevel = Instantiate(templatePrefab);
    currentLevel.SetActive(true);
    Destroy(oldLevel);
    UiManager.instance.victoryScreen.SetActive(false);
    nextSingleColor = 0;
    availableSingleColors.Clear();
    nextDualColor = 0;
    availableDualColors.Clear();
    nextTripleColor = 0;
    availableTripleColors.Clear();
    for (int i = 0; i < gameColors.Length; ++i) {
      var single = new List<int>();
      single.Add(i);
      availableSingleColors.Add(single);
      for (int j = i + 1; j < gameColors.Length; ++j) {
        var dual = new List<int>();
        dual.Add(i);
        dual.Add(j);
        availableDualColors.Add(dual);
        for (int k = j + 1; k < gameColors.Length; ++k) {
          var triple = new List<int>();
          triple.Add(i);
          triple.Add(j);
          triple.Add(k);
          availableTripleColors.Add(triple);
        }
      }
    }
    // Shuffle(availableSingleColors);
    // Shuffle(availableDualColors);
  }
  
  public List<int> AssignSingleColor() {
    if (nextSingleColor >= availableSingleColors.Count) return null;
    var col = availableSingleColors[nextSingleColor];
    nextSingleColor++;
    return col;
  }

  
  public List<int> AssignDualColor() {
    if (nextDualColor >= availableDualColors.Count) return null;
    var col = availableDualColors[nextDualColor];
    nextDualColor++;
    return col;
  }

  public List<int> AssignTripleColor() {
    if (nextTripleColor >= availableTripleColors.Count) return null;
    var col = availableTripleColors[nextTripleColor];
    nextTripleColor++;
    return col;
  }

  // Returns null if not possible.
  public List<int> AssignColor(int count) {
    if (count == 1) return AssignSingleColor();
    if (count == 2) return AssignDualColor();
    if (count == 3) return AssignTripleColor();
    return null;
  }
}
