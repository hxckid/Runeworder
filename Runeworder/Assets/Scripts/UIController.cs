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
    public UserRunes_SO userRunes;

    [SerializeField] List<GameObject> runesUI;

    private void Start()
    {
        runesUI = new List<GameObject>();
        InitRunes();
        AppManager.OnLanguageChanged += Localize;
    }

    void InitRunes()
    {
        for (int i = 0; i <= 32; i++)
        {
            Sprite sprite = runesSprites.sprites[i];
            runeUIPrefab.name = name;
            runeUIPrefab.GetComponent<RuneController>().background.sprite = sprite;
            runeUIPrefab.GetComponent<RuneController>().checkmark.sprite = sprite;
            runeUIPrefab.GetComponent<RuneController>().runeName.text = name;
            runeUIPrefab.GetComponent<RuneController>().rune = (RunesEn)i;
            runeUIPrefab.GetComponent<Toggle>().isOn = false;
            var runeUI = Instantiate(runeUIPrefab, runesPanel.transform);
            runeUI.name = name;
            runesUI.Add(runeUI);
            foreach (var rune in userRunes.hasRunes)
            {
                if (rune == runeUI.GetComponent<RuneController>().rune)
                {
                    runeUI.GetComponent<Toggle>().isOn = true;
                }
            }
        }
        Localize(AppManager.instance.currentLanguage);
    }

    public void Localize(Languages lang)
    {
        if (runesUI.Count > 0)
        {
            switch (lang)
            {
                case Languages.En:
                    foreach (var rune in runesUI)
                    {
                        Text text = rune.GetComponentInChildren<Text>();
                        text.text = Enum.GetName(typeof(RunesEn), runesUI.IndexOf(rune));
                    }
                    break;
                case Languages.Ru:
                    foreach (var rune in runesUI)
                    {
                        Text text = rune.GetComponentInChildren<Text>();
                        text.text = Enum.GetName(typeof(RunesRu), runesUI.IndexOf(rune));
                    }
                    break;
            }
        }
    }

    public void UncheckRunes()
    {
        foreach (var rune in runesUI)
        {
            rune.GetComponent<Toggle>().isOn = false;
        }
    }
}

public enum RunesEn 
{
    El, Eld, Tir, Nef, Eth, Ith, Tal, Ral, Ort, Thul, Amn,
    Sol, Shael, Dol, Hel, Io, Lum, Ko, Fal, Lem, Pul, Um,
    Mal, Ist, Gul, Vex, Ohm, Lo, Sur, Ber, Jah, Cham, Zod 
}

public enum RunesRu
{
    Эл, Элд, Тир, Неф, Эт, Ит, Тал, Рал, Орт, Тул, Амн,
    Сол, Шаэл, Дол, Хел, Ио, Лум, Ко, Фал, Лем, Пул, Ум,
    Мал, Ист, Гул, Векс, Ом, Ло, Сур, Бер, Джа, Чам, Зод
}