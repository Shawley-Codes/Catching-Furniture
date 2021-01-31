using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetizable : MonoBehaviour {
  public MonoBehaviour outlinable;
  public List<int> colors;
  bool outline = false;
  bool initialized = false;

  public Vector3 ClosestPoint(Vector3 source) {
    Vector3 sum = Vector3.zero; ;
    int cnt = 0;
    foreach (var coll in GetComponentsInChildren<Collider>()) {
      sum += coll.ClosestPoint(source);
      ++cnt;
    }
    if (cnt == 0) return source;
    return sum / cnt;
  }
  public Vector3 Center() {
    Vector3 sum = Vector3.zero;
    int cnt = 0;
    foreach (var coll in GetComponentsInChildren<Collider>()) {
      sum += coll.bounds.center;
      ++cnt;
    }
    if (cnt == 0) return Vector3.zero;
    return sum / cnt;
  }
  //[ExecuteInEditMode]
  //void Awake() {
  //  foreach (var renderer in GetComponentsInChildren<MeshRenderer>()) {
  //    renderer.gameObject.AddComponent<EPOOutline.Outlinable>();
  //  }
  //}

  // Start is called before the first frame update
  void Start() {
    Init();
  }

  public void Init() {
    if (initialized) return;
    initialized = true;
    var renderers = new List<MeshRenderer>();

    foreach (var renderer in GetComponentsInChildren<MeshRenderer>()) {
      renderers.Add(renderer);
      var o = renderer.gameObject.AddComponent<EPOOutline.Outlinable>();
      o.OutlineTargets.Add(new EPOOutline.OutlineTarget(renderer));
      o.enabled = outline;
    }
    colors = LevelManager.instance.AssignColor(renderers.Count);
    if (colors == null || colors.Count != renderers.Count) {
      gameObject.SetActive(false);
      return;
    }
    for (int i = 0; i < renderers.Count; ++i) {
      renderers[i].material = LevelManager.instance.gameColors[colors[i]].solidMaterial;
    }
  }
  public bool HasColor(int id) {
    if (colors == null) return false;
    foreach (var c in colors) if (id == c) return true;
    return false;
  }

  public void SetHighlight(bool on) {
    outline = on;
    foreach (var o in GetComponentsInChildren<EPOOutline.Outlinable>()) {
      o.enabled = on;
    }
  }
}
