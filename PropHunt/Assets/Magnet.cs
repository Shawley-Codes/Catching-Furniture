using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {
  public static Magnet instance;
  public float strength = 1000;
  public float yOffset = 2f;
  public int currentColor = -1;
  public AudioSource magnetSound;
  public GameObject player;

  float origVolume;
  float stopAfter = 0.2f;

  List<Magnetizable> magnetizables = new List<Magnetizable>();
  // List<GameObject> blues = new List<GameObject>();
  
  void Awake() {
    instance = this;
  }

  void Start() {
    origVolume = magnetSound.volume;
    for (int i = 0; i < transform.childCount; ++i) {
      GameObject obj = transform.GetChild(i).gameObject;
      var mag = obj.GetComponent<Magnetizable>();
      if (mag != null) magnetizables.Add(mag);
    }
  }

  public void SetColor(int id) {
    currentColor = id;
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
    foreach (var mag in magnetizables) {
      GameObject obj = mag.gameObject;
      obj.GetComponent<Rigidbody>().useGravity = true;
      obj.GetComponent<Rigidbody>().isKinematic = false;
      SetLayerRecursively(obj, 0);
    }

    List<GameObject> toPull = new List<GameObject>();
    foreach (var mag in magnetizables) {
      if (mag.HasColor(currentColor)) toPull.Add(mag.gameObject);
    }

    if (toPull.Count == 0) {
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
      // obj.layer = 8;
      SetLayerRecursively(obj, 8);
      var rb = obj.GetComponent<Rigidbody>();
      rb.isKinematic = false;
      rb.useGravity = false;
      var source = player.transform.position + yOffset * Vector3.up;
      var target = obj.GetComponent<Magnetizable>().ClosestPoint(source);
      var dir = (source - target).normalized;
      float dist = (source - target).magnitude;
      if (dist < 3) {
        // rb.isKinematic = true;
      } else {
        
      }
      rb.AddForce(dir * strength, ForceMode.Acceleration);
    }
  }
}