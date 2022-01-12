using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneController : MonoBehaviour
{
    public Image background;
    public Image checkmark;
    public Text runeName;

    Toggle toggle;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(toggle);
        });
    }
    void ToggleValueChanged(Toggle change)
    {
        print($"{change.name} now is " + toggle.isOn);
    }
}
