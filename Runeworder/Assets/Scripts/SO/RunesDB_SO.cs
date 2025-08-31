using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Runes Database", menuName = "Scriptables/Runes Database", order = 2)]
public class RunesDB_SO : ScriptableObject
{
    public List<Rune_SO> runes = new List<Rune_SO>();
    
    public Rune_SO GetRune(RunesEn runeType)
    {
        foreach (var rune in runes)
        {
            if (rune.runeType == runeType)
                return rune;
        }
        return null;
    }
} 