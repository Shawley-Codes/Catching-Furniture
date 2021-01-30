using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {
  public float strength = 1000;
  public AudioSource magnetSound;
  public GameObject player;

  float origVolume;
  float stopAfter = 0.2f;

  List<GameObject> reds = new List<GameObject>();
  List<GameObject> blues = new List<GameObject>();
  
  void Start() {
    origVolume = magnetSound.volume;
    for (int i = 0; i < transform.childCount; ++i) {
      GameObject obj = transform.GetChild(i).gameObject;
      if (obj.name.StartsWith("Blue")) blues.Add(obj);
      if (obj.name.StartsWith("Red")) reds.Add(obj);
    }
  }

  void FixedUpdate() {
    foreach (var obj in reds) {
      obj.GetComponent<Rigidbody>().useGravity = true;
      obj.GetComponent<Rigidbody>().isKinematic = false;
      obj.layer = 0;
    }
    foreach (var obj in blues) {
      obj.GetComponent<Rigidbody>().useGravity = true;
      obj.GetComponent<Rigidbody>().isKinematic = false;
      obj.layer = 0;
    }

    List<GameObject> toPull = null;
    if (Input.GetKey(KeyCode.Alpha1)) toPull = reds;
    if (Input.GetKey(KeyCode.Alpha2)) toPull = blues;

    if (toPull == null) {
      stopAfter -= Time.fixedDeltaTime;
      magnetSound.volume *= 0.9f;
      if (stopAfter < 0) magnetSound.Stop();
      return;
    }
    if (magnetSound.volume < origVolume) {
      magnetSound.volume = origVolume;
    }
    if (!magnetSound.isPlaying) {
      magnetSound.Play();
    }
    stopAfter = 0.2f;
    foreach (GameObject obj in toPull) {
      obj.layer = 8;
      var rb = obj.GetComponent<Rigidbody>();
      rb.isKinematic = false;
      rb.useGravity = false;
      var source = player.transform.position + Vector3.up;
      var dir = (source - obj.transform.position).normalized;
      float dist = (source - obj.transform.position).magnitude;
      if (dist < 3) {
        // rb.isKinematic = true;
      } else {
        
      }
      rb.AddForce(dir * strength, ForceMode.Acceleration);
    }
  }
}