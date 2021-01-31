using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetizable : MonoBehaviour {
  List<int> colors;

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

  // Start is called before the first frame update
  void Start() {
    var renderers = new List<MeshRenderer>();

    foreach (var renderer in GetComponentsInChildren<MeshRenderer>()) {
      renderers.Add(renderer);
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
}
