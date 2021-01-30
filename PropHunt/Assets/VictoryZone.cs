using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryZone : MonoBehaviour {
  void OnTriggerEnter(Collider collider) {
    Debug.Log("Collide", collider.gameObject);
    if (collider.gameObject.tag == "Player") {
      UiManager.instance.ShowVictory();
    }
  }
}
