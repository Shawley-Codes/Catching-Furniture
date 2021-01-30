using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
  public GameObject player;
  Vector3 prevPos;

  void Start() {
    prevPos = player.transform.position;
  }

  void FixedUpdate() {
    var curPos = player.transform.position;
    var delta = curPos - prevPos;
    transform.position += delta;
    prevPos = curPos;
  }
}
