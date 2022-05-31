using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Runeword", menuName = "Scriptables/Add Runeword", order = 3)]
public class Runeword_SO : ScriptableObject
{
    public string runewordName;
    public string runesSequence;
    public List<RunesEn> runes;
    public List<Sprite> sprites;
    public RunewordType runewordType = RunewordType.Weapons;
    public RunewordWeaponBases[] weaponBases;
    [TextArea] public string subType;
    [Range(13,69)]public int reqLevel;
    [TextArea(minLines:6, maxLines: 20)] public string statsDesc;
    public bool isLadder;
    public Classes classItem;
    public string gameVersion = "Ressurected";
    [SerializeField] RunesSprites_SO runesSprites;
    public int hasRunes;
    [TextArea] public string recomendedItems;

    private void OnValidate()
    {
        sprites.Clear();
        string seq = string.Empty;
        if (runesSprites == null)
        {
            Debug.LogWarning("Не назначен RunesSpritesDB");
        }
        else
        {
            for (int i = 0; i < runes.Count; i++)
            {
                seq += runes[i].ToString();
                sprites.Add(runesSprites.sprites[(int)runes[i]]);
            }
        }
        runesSequence = $"'{seq}'";
        
        if (runewordType != RunewordType.Weapons && runewordType != RunewordType.Shields)
            subType = ($"{runes.Count} Socket {runewordType}");
        if (runewordType == RunewordType.Weapons)
        {
            string bases = string.Empty;
            foreach (var item in weaponBases)
            {
                switch (item)
                {
                    case RunewordWeaponBases.AmazonSpears:
                        bases += "Amazon Spears, ";
                        break;
                    case RunewordWeaponBases.MeleeWeapons:
                        bases += "Melee Weapons, ";
                        break;
                    case RunewordWeaponBases.MissileWeapons:
                        bases += "Missile Weapons, ";
                        break;
                    default:
                        bases += item.ToString() + ", ";
                        break;
                }
            }
            bases = bases.Remove(bases.Length - 1);
            bases = bases.Remove(bases.Length - 1);
            subType = ($"{runes.Count} Socket {bases}");
        }
    }
}

public enum RunewordType { Weapons, BodyArmor, Helms, Shields, All }
public enum RunewordWeaponBases { AmazonSpears, Axes, Claws, Clubs, Daggers, Hammers, Maces, MeleeWeapons, MissileWeapons, Polearms, Scepters, Spears, Staves, Swords, Wands, Weapons }
public enum Classes { Any, Amazon, Assassin, Barbarian, Druid, Necromancer, Paladin, Sorceress }