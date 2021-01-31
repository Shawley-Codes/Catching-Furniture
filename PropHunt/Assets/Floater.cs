using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour {
  Rigidbody rb;
  void Awake() {
    rb = GetComponent<Rigidbody>();
  }
  void SetLayerRecursively(GameObject obj, int newLayer) {
    if (null == obj) {
      return;
    }

    obj.layer = newLayer;

    foreach (Transform child in obj.transform) {
      if (null == child) {
        continue;
      }
      SetLayerRecursively(child.gameObject, newLayer);
    }
  }
  void FixedUpdate() {
    SetLayerRecursively(gameObject, 0);
    if (rb.velocity.magnitude > 1) SetLayerRecursively(gameObject, 8);
  }
}
