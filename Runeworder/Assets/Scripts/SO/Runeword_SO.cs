using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Runeword", menuName = "Scriptables/Add Runeword", order = 3)]
public class Runeword_SO : ScriptableObject
{
    public string runewordName;
    public string runesSequence;
    public List<Runes> runes;
    public List<Sprite> sprites;
    public RunewordType runewordType = RunewordType.Weapons;
    [TextArea] public string subType;
    [Range(13,69)]public int reqLevel;
    [TextArea(minLines:6, maxLines: 20)] public string statsDesc;
    public bool isLadder;
    public Classes classItem;
    public string gameVersion = "Original Rune Words";
    [SerializeField] RunesSprites_SO runesSprites;
    public int hasRunes;
    [TextArea] public string recomendedItems;

    private void OnValidate()
    {
        sprites.Clear();
        string seq = string.Empty;
        for (int i = 0; i < runes.Count; i++)
        {
            seq += runes[i].ToString();
            sprites.Add(runesSprites.sprites[(int)runes[i]]);
        }
        runesSequence = $"'{seq}'";
        
        if (runewordType != RunewordType.Weapons && runewordType != RunewordType.Shields)
            subType = ($"{runes.Count} Socket {runewordType}");
    }
}

public enum RunewordType { Weapons, BodyArmor, Helms, Shields, All }
public enum Classes { Any, Amazon, Assassin, Barbarian, Druid, Necromancer, Paladin, Sorceress }