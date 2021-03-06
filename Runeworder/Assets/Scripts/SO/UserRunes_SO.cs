using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "User Runes DB", menuName = "Scriptables/Add User Runes DB", order = 2)]
public class UserRunes_SO : ScriptableObject
{
    public List<RunesEn> hasRunes;

    private void OnEnable()
    {
        RuneController.OnRuneToggleChanged += ToggleHandler;
    }

    private void OnDisable()
    {
        RuneController.OnRuneToggleChanged -= ToggleHandler;
    }

    private void ToggleHandler(RunesEn rune, bool isOn)
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
