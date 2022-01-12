using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject runesPanel;
    public GameObject runeUIPrefab;
    public RunesSprites_SO runesSprites;

    private void Start()
    {
        for (int i = 0; i <= 32; i++)
        {
            string name = Enum.GetName(typeof(Runes), i);
            Sprite sprite = runesSprites.sprites[i];
            
            runeUIPrefab.name = name;
            runeUIPrefab.GetComponent<RuneController>().background.sprite = sprite;
            runeUIPrefab.GetComponent<RuneController>().checkmark.sprite = sprite;
            runeUIPrefab.GetComponent<RuneController>().runeName.text = name;
            Instantiate(runeUIPrefab, runesPanel.transform);
        }
    }
}

public enum Runes { El, Eld, Tir, Nef, Eth, Ith, Tal, Ral, Ort, Thul, Amn,
                    Sol, Shael, Dol, Hel, Io, Lum, Ko, Fal, Lem, Pul, Um,
                    Mal, Ist, Gul, Vex, Ohm, Lo, Sur, Ber, Jah, Cham, Zod}