using System.Collections;
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
  public int Collected = 0;
  public int numToCollect = 3;
  HashSet<GameObject> inZone = new HashSet<GameObject>();

  void Awake() {
    instance = this;
    templatePrefab.SetActive(false);
    RestartLevel();
        // currentLevel = Instantiate(templatePrefab
    // setNewCollectable();
    }

  //check is object has tag
  private void OnTriggerEnter(Collider collider) {
    Debug.Log("Enter", collider.gameObject);
    if (collider.gameObject.GetComponent<Magnetizable>() != null) {
      inZone.Add(collider.gameObject);
    }
    //Debug.Log("Collide", collider.gameObject);
    ////if item is a collectable, increment and set new collectable.
    //if (collider.gameObject.tag == "Collectable")
    //{
    //   Collected++;
    //   setNewCollectable();
    //   //TBD: needs to set ui with collectable count
    //}
    ////win if 3 collectables arem collected
    //if (Collected >= 3)
    //{
    //   UiManager.instance.ShowVictory();
    //}
  }

  //check is object has tag
  private void OnTriggerExit(Collider collider) {
    Debug.Log("Exit", collider.gameObject);
    if (collider.gameObject.GetComponent<Magnetizable>() != null) {
      inZone.Remove(collider.gameObject);
    }
  }
    //called when a new collectable object needs to be set
    //TBD: needs to also set ui with new object
  public void SetCollectibles() {
    var magnetizables = new List<GameObject>();
    foreach (var mag in Magnet.instance.gameObject.GetComponentsInChildren<Magnetizable>()) {
      magnetizables.Add(mag.gameObject);
    }
    Debug.Log("Total: " + magnetizables.Count + " magnetizables");
    Shuffle(magnetizables);
    for (int i = 0; i < numToCollect && i < magnetizables.Count; ++i) {
      // var taggable = magnetizables[i].GetComponent<Tags>();
      // if (taggable != null) { taggable.setCurrentCollectable(); }
    }
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
    if (oldLevel != null) Destroy(oldLevel);
    UiManager.instance?.victoryScreen.SetActive(false);
    inZone.Clear();
    SetCollectibles();
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
