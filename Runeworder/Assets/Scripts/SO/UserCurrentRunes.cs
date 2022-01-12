using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "User Runes DB", menuName = "User Runes DB", order = 2)]
public class UserCurrentRunes : ScriptableObject
{
    public List<Runes> hasRunes;

    private void OnEnable()
    {
        RuneController.OnRuneToggleChanged += ToggleHandler;
    }

    private void OnDisable()
    {
        RuneController.OnRuneToggleChanged -= ToggleHandler;
    }

    private void ToggleHandler(Runes rune, bool isOn)
    {
        if (isOn)
        {
            hasRunes.Add(rune);
        }
        else
        {
            hasRunes.Remove(rune);
        }
    }
}
