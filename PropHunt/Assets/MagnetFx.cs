using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetFx : MonoBehaviour {
  public GameObject effect;
  public ParticleSystem charge;
  public ParticleSystem glow;
  void Update() {
    effect.SetActive(Magnet.instance?.currentColor != -1);
    if (effect.activeInHierarchy && Magnet.instance != null &&
        LevelManager.instance != null && Magnet.instance.currentColor != -1) {
      var col = LevelManager.instance.gameColors[Magnet.instance.currentColor];
      
      foreach (var ps in charge.GetComponentsInChildren<ParticleSystem>()) {
        var main = ps.main;
        main.startColor = col.solidMaterial.color;
        //var keys = ps.colorOverLifetime.color.gradient.colorKeys;
        //for (int i = 0; i < keys.Length; ++i) {
        //  keys[i].color = col.solidMaterial.color;
        //  Debug.Log("i => " + keys[i].color);
        //}
      }
    }
  }
}
