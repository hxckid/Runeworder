using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunewordsController : MonoBehaviour
{
    public UserRunes_SO userRunes;
    public RunewordsDB_SO runewordsDB;

    private void Start()
    {
        RuneController.OnRuneToggleChanged += RuneStateChanged;
        ToggleController.OnToggleChanged += TypeStateChanged;
    }

    private void TypeStateChanged(Toggle toggle, bool isOn)
    {
        
    }

    private void RuneStateChanged(Runes rune, bool isOn)
    {
        
    }
}
