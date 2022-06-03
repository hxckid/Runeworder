using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class RunewordsController : MonoBehaviour
{
    public UserRunes_SO userRunes;
    public RunewordsDB_SO runewordsDBEng;
    public RunewordsDB_SO runewordsDBRus;
    public RunewordsDB_SO workflowDB;
    public GameObject runewordPrefab;
    public GameObject uiParent;
    public Text status;

    List<GameObject> runewordsToShow;
    string lastPressed = string.Empty;
    bool toggle = false;
    RunewordsDB_SO currentDB;

    private void Start()
    {
        runewordsToShow = new List<GameObject>();
        runewordsToShow.Clear();
        AppManager.OnLanguageChanged += InitDB;
        InitDB(AppManager.instance.currentLanguage);
        lastPressed = "All";
    }

    private void InitDB(Languages lang)
    {
        switch (lang)
        {
            case Languages.En:
                currentDB = runewordsDBEng;
                break;
            case Languages.Ru:
                currentDB = runewordsDBRus;
                break;
        }
    }

    public void SearchByName(TMP_InputField runewordName)
    {
        //if (runewordName.text.Length == 0)
        //    return;

        bool found = false;
        
        ClearWorkflowDB();

        foreach (var rw in currentDB.runewords)
        {
            if (rw.runewordName.ToUpper().Contains(runewordName.text.ToUpper()))
            {
                workflowDB.runewords.Add(rw);
                status.text = $"{runewordName.text} ({workflowDB.runewords.Count}):";
                found = true;
            }
        }

        if (!found)
        {
            if (AppManager.instance.currentLanguage == Languages.En)
                status.text = $"{runewordName.text} not found!";
            else
                status.text = $"{runewordName.text} не найден!";
        }
        else
        {
            FillRunewordList();
        }
    }

    public void FilterRunewords(string type)
    {
        ClearWorkflowDB();

        switch (type)
        {
            case "Armors":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.runewordType == RunewordType.Armor)
                    {
                        workflowDB.runewords.Add(rw);
                    }
                }
                break;
            case "Helms":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.runewordType == RunewordType.Helms)
                    {
                        workflowDB.runewords.Add(rw);
                    }
                }
                break;
            case "Shields":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.runewordType == RunewordType.Shields)
                    {
                        workflowDB.runewords.Add(rw);
                    }
                }
                break;
            case "All Runewords":
                foreach (var rw in currentDB.runewords)
                {
                    workflowDB.runewords.Add(rw);
                }
                break;
            case "Resurrected (Patch 2.4)":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.gameVersion == "Resurrected")
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "Amazon Spears":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.subType.Contains("Amazon") || rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile"))
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "Axes":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.subType.Contains("Axes") || rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile"))
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "Claws":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.subType.Contains("Claws") && rw.runes.Count <= 3 || rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile") && rw.runes.Count <= 3)
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "Clubs":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.subType.Contains("Clubs") && rw.runes.Count <= 3 || rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile") && rw.runes.Count <= 3)
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "Daggers":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.subType.Contains("Daggers") && rw.runes.Count <= 3 || rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile") && rw.runes.Count <= 3)
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "Hammers":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.subType.Contains("Hammers") || rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile"))
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "Maces":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.subType.Contains("Maces") && rw.runes.Count <= 5 || rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile") && rw.runes.Count <= 5)
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "Melee Weapons":
                foreach (var rw in currentDB.runewords)
                {
                    if (!rw.subType.Contains("Missile") && !rw.subType.Contains("Armor") && !rw.subType.Contains("Helms") && !rw.subType.Contains("Shields"))
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "Missile Weapons":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.subType.Contains("Missile") || rw.subType.Contains("Weapons") && !rw.subType.Contains("Melee"))
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "Polearms":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.subType.Contains("Polearms") || rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile"))
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "Scepters":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.subType.Contains("Scepters") && rw.runes.Count <= 5 || rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile") && rw.runes.Count <= 5)
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "Spears":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.subType.Contains("Spears") || rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile"))
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "Staves (Not Orbs)":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.subType.Contains("Staves") || rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile"))
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "Swords":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.subType.Contains("Swords") || rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile"))
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "Wands":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.subType.Contains("Wands") && rw.runes.Count <= 2 || rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile") && rw.runes.Count <= 2)
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "All Weapons":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.runewordType == RunewordType.Weapons)
                        workflowDB.runewords.Add(rw);
                }
                break;
        }
        
        lastPressed = type;
        if (AppManager.instance.currentLanguage == Languages.En)
            status.text = lastPressed;
        else
            Localize(type);
        
        FillRunewordList();
    }

    private void ClearWorkflowDB()
    {
        workflowDB.runewords.Clear();
    }

    private void ClearRunewordList()
    {
        if (runewordsToShow.Count > 0)
        {
            foreach (var rw in runewordsToShow)
            {
                Destroy(rw);
            }
            runewordsToShow.Clear();
        }
    }

    private void FillRunewordList()
    {
        ClearRunewordList();
        int odd = 1;
        foreach (var rw in workflowDB.runewords)
        {
            rw.hasRunes = 0;
            ListController lc = runewordPrefab.GetComponent<ListController>();

            if (odd % 2 == 0)
                lc.background.color = Color.red;
            else
                lc.background.color = Color.white;
            odd++;

            lc.runewordName.text = rw.runewordName;
            foreach (var rune in lc.runes)
            {
                rune.text = "";
            }
            for (int i = 0; i < rw.runes.Count; i++)
            {
                switch (AppManager.instance.currentLanguage)
                {
                    case Languages.En:
                        lc.runes[i].text = rw.runes[i].ToString();
                        lc.reqLevel.text = $"Level: {rw.reqLevel}";
                        lc.type.text = $"{rw.runewordType}";
                        break;
                    case Languages.Ru:
                        RunesRu rus = (RunesRu)rw.runes[i];
                        lc.runes[i].text = rus.ToString();
                        lc.reqLevel.text = $"Уровень: {rw.reqLevel}";
                        switch (rw.runewordType)
                        {
                            case RunewordType.Weapons:
                                lc.type.text = $"Оружие";
                                break;
                            case RunewordType.Armor:
                                lc.type.text = $"Броня";
                                break;
                            case RunewordType.Helms:
                                lc.type.text = $"Шлемы";
                                break;
                            case RunewordType.Shields:
                                lc.type.text = $"Щиты";
                                break;
                        }
                        break;
                }

                lc.runes[i].color = Color.gray;
                foreach (var rune in userRunes.hasRunes)
                {
                    if (rune == rw.runes[i])
                    {
                        lc.runes[i].color = Color.green;
                        rw.hasRunes++;
                    }
                }

                lc.runeword = rw;
            }
            GameObject inst = Instantiate(runewordPrefab, uiParent.transform);
            runewordsToShow.Add(inst);
        }
    }

    private void SortRunewordsBy(string type)
    {
        if (toggle)
        {
            switch (type)
            {
                case "Name":
                    currentDB.runewords.Sort((b, a) => a.runewordName.CompareTo(b.runewordName));
                    break;
                case "Runes":
                    currentDB.runewords.Sort((a, b) =>
                    {
                        int result = a.hasRunes.CompareTo(b.hasRunes);
                        return result == 0 ? a.runewordName.CompareTo(b.runewordName) : result;
                    });
                    break;
                case "Level":
                    currentDB.runewords.Sort((a, b) =>
                    {
                        int result = a.reqLevel.CompareTo(b.reqLevel);
                        return result == 0 ? a.runewordName.CompareTo(b.runewordName) : result;
                    });
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case "Name":
                    currentDB.runewords.Sort((a, b) => a.runewordName.CompareTo(b.runewordName));
                    break;
                case "Runes":
                    currentDB.runewords.Sort((b, a) =>
                    {
                        int result = a.hasRunes.CompareTo(b.hasRunes);
                        return result == 0 ? b.runewordName.CompareTo(a.runewordName) : result;
                    });
                    break;
                case "Level":
                    currentDB.runewords.Sort((b, a) =>
                    {
                        int result = a.reqLevel.CompareTo(b.reqLevel);
                        return result == 0 ? b.runewordName.CompareTo(a.runewordName) : result;
                    });
                    break;
            }
        }
        toggle = !toggle;
        FilterRunewords(lastPressed);
    }

    private void Localize(string type)
    {
        switch (type)
        {
            case "Armors":
                status.text = "Броня";
                break;
            case "Helms":
                status.text = "Шлемы";
                break;
            case "Shields":
                status.text = "Щиты";
                break;
            case "All Runewords":
                status.text = "Все Рунворды";
                break;
            case "Resurrected (Patch 2.4)":
                status.text = "Resurrected (Патч 2.4)";
                break;
            case "Amazon Spears":
                status.text = "Копья Амазонки";
                break;
            case "Axes":
                status.text = "Топоры";
                break;
            case "Claws":
                status.text = "Когти";
                break;
            case "Clubs":
                status.text = "Дубины";
                break;
            case "Daggers":
                status.text = "Кинжалы";
                break;
            case "Hammers":
                status.text = "Молоты";
                break;
            case "Maces":
                status.text = "Булавы";
                break;
            case "Melee Weapons":
                status.text = "Оружие ближнего боя";
                break;
            case "Missile Weapons":
                status.text = "Оружие дальнего боя";
                break;
            case "Polearms":
                status.text = "Древковое оружие";
                break;
            case "Scepters":
                status.text = "Скипетры";
                break;
            case "Spears":
                status.text = "Копья";
                break;
            case "Staves (Not Orbs)":
                status.text = "Двуручные Посохи (Не Сферы)";
                break;
            case "Swords":
                status.text = "Мечи";
                break;
            case "Wands":
                status.text = "Жезлы Некроманта";
                break;
            case "All Weapons":
                status.text = "Все оружие";
                break;
        }
    }
}