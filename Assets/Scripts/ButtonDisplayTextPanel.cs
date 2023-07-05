using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisplayTextPanel : MonoBehaviour
{

  public TMPro.TMP_Text text;
  private Image image;

  void Awake()
  {
    image = GetComponent<Image>();
  }


  internal void SetActive()
  {
    text.color = new Color(0, 0, 0, 100);
    image.color = new Color(255, 255, 255, 100);
  }

  internal void SetInactive()
  {
    text.color = new Color(50, 50, 50, 100);
    image.color = new Color(100, 100, 100, 100);
  }

  internal void SetText(MainController.KeyCodeName keyCodeName)
  {
    text.text = keyCodeName.name;
  }
}
