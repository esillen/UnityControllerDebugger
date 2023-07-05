using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// This script assumes that it it a layout with its children being UI panels

public class MainController : MonoBehaviour
{

  public ButtonDisplayTextPanel buttonDisplayTextPanelPrefab;
  private Dictionary<KeyCode, ButtonDisplayTextPanel> buttonTextPanelsByKeyCode = new Dictionary<KeyCode, ButtonDisplayTextPanel>();

  internal class KeyCodeName
  {
    public string name { get; private set; }

    private KeyCodeName() { }
    public KeyCodeName(string name)
    {
      this.name = name;
    }
  }

  void Update()
  {
    foreach ((KeyCodeName keyCodeName, KeyCode keyCode) in Enum.GetValues(typeof(KeyCode))
              .Cast<KeyCode>()
              .Select(keyCode => (new KeyCodeName(Enum.GetName(typeof(KeyCode), keyCode)), keyCode)))
    {

      if (Input.GetKey(keyCode))
      {
        if (!buttonTextPanelsByKeyCode.ContainsKey(keyCode))
        {
          buttonTextPanelsByKeyCode[keyCode] = Instantiate(buttonDisplayTextPanelPrefab, transform);
          buttonTextPanelsByKeyCode[keyCode].SetText(keyCodeName);
          SortTextPanels();
        }
        buttonTextPanelsByKeyCode[keyCode].SetActive();
      } else if (buttonTextPanelsByKeyCode.ContainsKey(keyCode)) {
        buttonTextPanelsByKeyCode[keyCode].SetInactive();
      }
    }
  }

  private void SortTextPanels()
  {
    int counter = 0;
    foreach (KeyCode keyCode in buttonTextPanelsByKeyCode.Keys.ToList().OrderBy((KeyCode keyCode) => ((int)keyCode))) {
      buttonTextPanelsByKeyCode[keyCode].transform.SetSiblingIndex(counter);
      counter++;
    }
  }
}
