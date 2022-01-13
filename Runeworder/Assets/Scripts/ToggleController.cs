using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public delegate void ToggleHandler(Toggle toggle, bool isOn);
    public static event ToggleHandler OnToggleChanged;

    Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged();
        });
    }

    private void ToggleValueChanged()
    {
        OnToggleChanged?.Invoke(toggle, toggle.isOn);
    }
}
