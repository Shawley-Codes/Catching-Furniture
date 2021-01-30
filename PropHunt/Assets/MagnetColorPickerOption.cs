using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetColorPickerOption : MonoBehaviour {
  public int colorId = -1;
  public Image image;
  public Image border;
  public TMPro.TextMeshProUGUI text;
  public Toggle toggle;

  public bool IsSelected() {
    return Magnet.instance?.currentColor == colorId;
  }

  public void Pick(bool on) {
    if (on) Magnet.instance?.SetColor(colorId);
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.Alpha1 + colorId) ||
        (colorId == -1 && Input.GetKeyDown(KeyCode.BackQuote))) {
      if (toggle.isOn) {
        Magnet.instance?.SetColor(-1);
      } else {
        toggle.isOn = true;
      }
    }
    if (IsSelected()) {
      toggle.Select();
      toggle.isOn = true;
      // border.color = Color.yellow;
    } else {
      // border.color = Color.black;
    }
  }
}
