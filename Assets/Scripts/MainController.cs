using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// This script assumes that it it a layout with its children being UI panels

public class MainController : MonoBehaviour
{

  public ButtonDisplayTextPanel buttonDisplayTextPanelPrefab;
  public AxisDisplayTextPanel axisDisplayTextPanelPrefab;
  private Dictionary<KeyCode, ButtonDisplayTextPanel> buttonTextPanelsByKeyCode = new Dictionary<KeyCode, ButtonDisplayTextPanel>();
  private Dictionary<JoystickAxisName, AxisDisplayTextPanel> axisTextPanelsByJoystickAxisName = new Dictionary<JoystickAxisName, AxisDisplayTextPanel>();

  internal class JoystickAxisName {
    public string name { get; private set; }

    private JoystickAxisName() { }
    public JoystickAxisName(string name)
    {
      this.name = name;
    }

    public override int GetHashCode()
    {
      return name.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (obj is JoystickAxisName) {
        return ((JoystickAxisName)obj).name == name;
      }
      return base.Equals(obj);
    }
  }

  internal class KeyCodeName
  {
    public string name { get; private set; }

    private KeyCodeName() { }
    public KeyCodeName(string name)
    {
      this.name = name;
    }

    public override int GetHashCode()
    {
      return name.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (obj is KeyCodeName) {
        return ((KeyCodeName)obj).name == name;
      }
      return base.Equals(obj);
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
      }
      else if (buttonTextPanelsByKeyCode.ContainsKey(keyCode))
      {
        buttonTextPanelsByKeyCode[keyCode].SetInactive();
      }
    }

    foreach(JoystickAxisName joystickAxisName in getAllAxisCombinations()) {
      if (Input.GetAxisRaw(joystickAxisName.name) != 0.0) {
        if (!axisTextPanelsByJoystickAxisName.ContainsKey(joystickAxisName))
        {
          axisTextPanelsByJoystickAxisName[joystickAxisName] = Instantiate(axisDisplayTextPanelPrefab, transform);
          SortTextPanels();
        }
        axisTextPanelsByJoystickAxisName[joystickAxisName].SetText(joystickAxisName, Input.GetAxisRaw(joystickAxisName.name));
        axisTextPanelsByJoystickAxisName[joystickAxisName].SetActive();
      }
      else if (axisTextPanelsByJoystickAxisName.ContainsKey(joystickAxisName))
      {
        axisTextPanelsByJoystickAxisName[joystickAxisName].SetInactive();
      }
    }


  }


  private void SortTextPanels()
  {
    int counter = 0;
    foreach (KeyCode keyCode in buttonTextPanelsByKeyCode.Keys.ToList().OrderBy((KeyCode keyCode) => ((int)keyCode)))
    {
      buttonTextPanelsByKeyCode[keyCode].transform.SetSiblingIndex(counter);
      counter++;
    }
  }


  private List<JoystickAxisName> getAllAxisCombinations()
  {
    List<JoystickAxisName> joystickAxisNames = new List<JoystickAxisName>();
    foreach (int joystick in new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })
    {
      foreach (int axis in new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 })
      {
        joystickAxisNames.Add(new JoystickAxisName("joystick " + joystick + " axis " + axis));
      }
    }
    return joystickAxisNames;
  }
}
