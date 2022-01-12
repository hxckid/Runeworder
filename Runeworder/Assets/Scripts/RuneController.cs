using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneController : MonoBehaviour
{
    public Image background;
    public Image checkmark;
    public Text runeName;
    public Runes rune;

    public delegate void RuneHandler(Runes rune, bool isOn);
    public static event RuneHandler OnRuneToggleChanged;

    Toggle toggle;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged();
        });
    }
    void ToggleValueChanged()
    {
        OnRuneToggleChanged?.Invoke(rune, toggle.isOn);
    }
}
