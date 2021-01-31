using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class GameColor {
  public Material solidMaterial;
  public Material transparentMaterial;
  public string name;
}

public class LevelManager : MonoBehaviour {
  public static LevelManager instance;
  public GameObject templatePrefab;
  public GameObject soulPrefab;
  public AudioSource collectSound;
  GameObject currentLevel;
  public GameColor[] gameColors;
  List<List<int>> availableSingleColors = new List<List<int>>();
  int nextSingleColor = 0;
  List<List<int>> availableDualColors = new List<List<int>>();
  int nextDualColor = 0;
  List<List<int>> availableTripleColors = new List<List<int>>();
  int nextTripleColor = 0;
  public int collected = 0;
  public int desiredNumToCollect = 3;
  public GameObject[] toCollect;
  HashSet<GameObject> inZone = new HashSet<GameObject>();
  GameObject targetObject = null;

  void Awake() {
    instance = this;
    templatePrefab.SetActive(false);
    // currentLevel = Instantiate(templatePrefab
    // setNewCollectable();
    RestartLevel();
    Debug.Log("LevelManager Awake");
  }

  void Start() {

  }

  public bool cheatNext = false;
  void CheckObjectives() {
    UiManager.instance?.UpdateCount(collected, toCollect.Length);
    if (targetObject == null) return;
    if (inZone.Contains(targetObject) || cheatNext) {
      if (inZone.Count == 1 || cheatNext) {
        if (targetObject.GetComponent<Rigidbody>().velocity.magnitude < 0.1 || cheatNext) {
          if (soulPrefab != null) {
            var soul = Instantiate(
              soulPrefab,
              targetObject.GetComponent<Magnetizable>().Center(),
              Quaternion.identity);
            Destroy(soul, 3f);
          }
          collected++;
          collectSound.Play();
          // Debug.Log("Collected " + collected + " / " + toCollect.Length);
          UiManager.instance.UpdateCount(collected, toCollect.Length);
          if (collected >= toCollect.Length) {
            UiManager.instance.setObjective("You Won");
            UiManager.instance.ShowVictory();
          }
          NextCollectible();
        } else {
          // Debug.Log("Release highlighted object");
          UiManager.instance.setObjective("Let the " + targetObject.name + " settle on the floor");
        }
      } else {
        UiManager.instance.setObjective("Remove other objects from the designated area");
        // Debug.Log("Push non-highlighted objects outside the room.");
      }
    } else {
      
      UiManager.instance.setObjective("Bring the " + targetObject.name + " to the designated area");
      // Debug.Log("Push highlighted object into the room.");
    }
    cheatNext = false;
  }

  void FixedUpdate() {
    CheckObjectives();
  }

  Magnetizable GetMagnetizable(GameObject obj) {
    var mag = obj.GetComponent<Magnetizable>();
    if (mag != null) return mag;
    if (obj.transform.parent == null) return null;
    return GetMagnetizable(obj.transform.parent.gameObject);
  }
  //check is object has tag
  private void OnTriggerEnter(Collider collider) {
    // Debug.Log("Enter", collider.gameObject);
    var mag = GetMagnetizable(collider.gameObject);
    if (mag != null) inZone.Add(mag.gameObject);
    CheckObjectives();
  }

  //check is object has tag
  private void OnTriggerExit(Collider collider) {
    // Debug.Log("Exit", collider.gameObject);
    var mag = GetMagnetizable(collider.gameObject);
    if (mag != null) inZone.Remove(mag.gameObject);
    CheckObjectives();
  }

  public void NextCollectible() {
    targetObject?.GetComponent<Magnetizable>()?.SetHighlight(false);
    if (collected < toCollect.Length) {
      targetObject = toCollect[collected];
    } else {
      targetObject = null;
    }
    targetObject?.GetComponent<Magnetizable>()?.SetHighlight(true);
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
    var magnetizables = new List<GameObject>();
    var children = new List<Magnetizable>();
    foreach (var c in Magnet.instance.gameObject.GetComponentsInChildren<Magnetizable>()) {
      children.Add(c);
    }
    Shuffle(children);
    foreach (var mag in children) {
      mag.Init();
      if (mag.gameObject.activeInHierarchy) {
        magnetizables.Add(mag.gameObject);
      }
    }
    Debug.Log("Total: " + magnetizables.Count + " magnetizables");
    Shuffle(magnetizables);
    int cnt = Math.Min(desiredNumToCollect, magnetizables.Count);
    toCollect = new GameObject[cnt];
    for (int i = 0; i < cnt; ++i) {
      toCollect[i] = magnetizables[i];
    }
    collected = 0;
    NextCollectible();
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
