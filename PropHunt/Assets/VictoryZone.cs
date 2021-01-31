using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryZone : MonoBehaviour {
  void OnTriggerEnter(Collider collider) {
    if (collider.gameObject.tag == "Player") {
      UiManager.instance.ShowVictory();
    }
  }
}
