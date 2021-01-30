using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetColorPicker : MonoBehaviour {
  public float sizePerButton = 50;
  public RectTransform pickerBar;
  public GameObject optionTemplate;
  public GameObject itemsContainer;

  void AddColor(int id) {
    var option = Instantiate(optionTemplate, itemsContainer.transform);
    var comp = option.GetComponent<MagnetColorPickerOption>();
    comp.colorId = id;
    if (id == -1) {
      comp.image.color = Color.black;
    } else {
      comp.image.color = LevelManager.instance.gameColors[id].solidMaterial.color;
    }
    comp.text.text = "" + (id + 1);
    if (id == -1) comp.text.text = "~";
  }
  void Start() {
    for (int id = -1; id < LevelManager.instance.gameColors.Length; ++id) {
      AddColor(id);
    }
    optionTemplate.SetActive(false);
    var d = pickerBar.sizeDelta;
    d.x = sizePerButton * LevelManager.instance.gameColors.Length;
    pickerBar.sizeDelta = d;
  }
}
