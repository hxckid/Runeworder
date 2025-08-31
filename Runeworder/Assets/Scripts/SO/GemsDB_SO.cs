using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gems Database", menuName = "Scriptables/Gems Database", order = 4)]
public class GemsDB_SO : ScriptableObject
{
    public List<Gem_SO> gems = new List<Gem_SO>();
    
    public Gem_SO GetGem(GemType gemType, GemQuality gemQuality)
    {
        foreach (var gem in gems)
        {
            if (gem.gemType == gemType && gem.gemQuality == gemQuality)
                return gem;
        }
        return null;
    }
} 