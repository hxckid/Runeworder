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
    public Dropdown typeDropdown;

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

                    typeDropdown.options.Clear();
                    typeDropdown.captionText.text = "All Runewords";
                    typeDropdown.options.Add(new Dropdown.OptionData("All Runewords"));
                    typeDropdown.options.Add(new Dropdown.OptionData("All Weapons"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Amazon Spears"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Armors"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Axes"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Claws"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Clubs"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Daggers"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Hammers"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Helms"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Maces"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Melee Weapons"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Missile Weapons"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Polearms"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Scepters"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Spears"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Staves (Not Orbs)"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Swords"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Wands"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Shields"));
                    break;
                case Languages.Ru:
                    foreach (var rune in runesUI)
                    {
                        Text text = rune.GetComponentInChildren<Text>();
                        text.text = Enum.GetName(typeof(RunesRu), runesUI.IndexOf(rune));
                    }

                    typeDropdown.options.Clear();
                    typeDropdown.captionText.text = "Все Рунворды";
                    typeDropdown.options.Add(new Dropdown.OptionData("Все Рунворды"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Все Оружие"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Копья Амазонки"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Броня"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Топоры"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Когти"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Дубины"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Кинжалы"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Молоты"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Шлемы"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Булавы"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Оружие ближнего боя"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Оружие дальнего боя"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Древковое оружие"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Скипетры"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Копья"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Двуручные Посохи (Не Сферы)"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Мечи"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Жезлы Некроманта"));
                    typeDropdown.options.Add(new Dropdown.OptionData("Щиты"));
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