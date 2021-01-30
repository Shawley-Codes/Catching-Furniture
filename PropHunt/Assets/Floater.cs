using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour {
  Rigidbody rb;
  void Awake() {
    rb = GetComponent<Rigidbody>();
  }

  void FixedUpdate() {
    gameObject.layer = 0;
    if (rb.velocity.magnitude > 1) gameObject.layer = 8;
  }
}
