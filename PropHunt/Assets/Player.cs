using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  static public GameObject instance;

  void Awake() {
    instance = gameObject;
  }
}
