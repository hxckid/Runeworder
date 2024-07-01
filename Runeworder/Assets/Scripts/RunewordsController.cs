using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Xml.Serialization;
using System.IO;

public class RunewordsController : MonoBehaviour
{
    public Text langText;
    public Dropdown verDrop;
    public UserRunes_SO userRunes;
    public RunewordsDB_SO runewordsDBEng;
    public RunewordsDB_SO runewordsDBRus;
    public RunewordsDB_SO d2LodDBEng;
    public RunewordsDB_SO d2LodDBRus;
    public RunewordsDB_SO workflowDB;
    public RunewordsDB_SO customSearchDB;
    public GameObject runewordPrefab;
    public GameObject uiParent;
    public Text status;
    public Color defaultRuneColor;
    public Dropdown socketsDropdown;
    public Dropdown typeDropdown;
    public List<GameObject> runewordsToShow;
    public Toggle allRunewordsToggle;
    public Toggle nonLadderToggle;
    public Toggle ladderToggle;
    
    string lastPressed = string.Empty;
    bool toggle = false;
    RunewordsDB_SO currentDB;
    int lastSocketSearch;
    string lastTypeSearch;

    private void Start()
    {
        runewordsToShow = new List<GameObject>();
        runewordsToShow.Clear();
        AppManager.OnLanguageChanged += InitDB;
        InitDB(AppManager.instance.currentLanguage, "Resurrected");
        lastPressed = "All";
        lastSocketSearch = 0;
        lastTypeSearch = "None";

        //string filePath = Application.dataPath + "/Runewords.xml";
        //RunewordSerializer serializer = new RunewordSerializer();
        //serializer.SaveRunewordsToXml(runewordsDBRus.runewords, filePath);
        //Debug.Log($"Saved here: {filePath}");
    }

    public void ChangeGameVersion()
    {
        Languages currentLanguage = langText.text == "RUS" ? Languages.En : Languages.Ru;
        string currentVersion = verDrop.options[verDrop.value].text;
        InitDB(currentLanguage, currentVersion);
    }

    private void InitDB(Languages lang, string ver)
    {
        switch (lang)
        {
            case Languages.En:
                if (ver == "Resurrected")
                    currentDB = runewordsDBEng;
                else
                    currentDB = d2LodDBEng;
                break;
            case Languages.Ru:
                if (ver == "Resurrected")
                    currentDB = runewordsDBRus;
                else
                    currentDB = d2LodDBRus;
                break;
        }
    }

    public void FilterLast()
    {
        string res = lastPressed == "All" ? "All Runewords" : lastPressed;
        FilterRunewords(res);
    }

    public void SearchByName(TMP_InputField runewordName)
    {
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
            FillRunewordList(workflowDB);
        }
    }

    public void CustomSearch()
    {
        ClearCustomSearchDB();
        string numOfSockets = socketsDropdown.options[socketsDropdown.value].text;

        if (numOfSockets == "All" || numOfSockets == "Все")
            lastSocketSearch = 0;
        else
            lastSocketSearch = Int32.Parse(numOfSockets); 

        lastTypeSearch = typeDropdown.options[typeDropdown.value].text;

        lastTypeSearch = lastTypeSearch == "Все Рунворды" ? "All Runewords" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Все Оружие" ? "All Weapons" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Копья Амазонки" ? "Amazon Spears" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Броня" ? "Armors" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Топоры" ? "Axes" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Когти" ? "Claws" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Дубины" ? "Clubs" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Кинжалы" ? "Daggers" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Молоты" ? "Hammers" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Шлемы" ? "Helms" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Булавы" ? "Maces" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Оружие ближнего боя" ? "Melee Weapons" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Оружие дальнего боя" ? "Missile Weapons" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Древковое оружие" ? "Polearms" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Скипетры" ? "Scepters" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Копья" ? "Spears" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Двуручные Посохи (Не Сферы)" ? "Staves (Not Orbs)" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Мечи" ? "Swords" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Жезлы Некроманта" ? "Wands" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "Щиты" ? "Shields" : lastTypeSearch;
        
        FilterRunewords(lastTypeSearch);

        if (lastSocketSearch != 0)
        {
            foreach (var rw in workflowDB.runewords)
            {
                if (rw.runes.Count == lastSocketSearch && (allRunewordsToggle.isOn || (!rw.isLadder && nonLadderToggle.isOn) || (rw.isLadder && ladderToggle.isOn)))
                    customSearchDB.runewords.Add(rw);
            }
        }
        else
        {
            foreach (var rw in workflowDB.runewords)
            {
                if ((allRunewordsToggle.isOn || (!rw.isLadder && nonLadderToggle.isOn) || (rw.isLadder && ladderToggle.isOn)))
                    customSearchDB.runewords.Add(rw);
            }
        }

        ClearWorkflowDB();
        foreach (var rw in customSearchDB.runewords)
            workflowDB.runewords.Add(rw);
        
        string ladderText = string.Empty;
        string socketText = lastSocketSearch == 0 ? "2-6" : lastSocketSearch.ToString();
        string typeText = AppManager.instance.currentLanguage == Languages.En ? lastTypeSearch : typeDropdown.options[typeDropdown.value].text;
        switch (AppManager.instance.currentLanguage)
        {
            case Languages.En:
                ladderText = allRunewordsToggle.isOn ? "Both" : nonLadderToggle.isOn ? "Non-Ladder Only" : "Ladder Only";
                break;
            case Languages.Ru:
                ladderText = allRunewordsToggle.isOn ? "Оба" : nonLadderToggle.isOn ? "Не-Ладдер" : "Ладдер";
                break;
        }

        status.text = $"({workflowDB.runewords.Count}) {typeText}/{socketText}*/{ladderText}";

        FillRunewordList(customSearchDB);
    }

    public void FilterRunewords(string type)
    {
        ClearWorkflowDB();

        var filters = new Dictionary<string, Func<Runeword_SO, bool>>()
        {
            { "Armors", rw => rw.runewordType == RunewordType.Armor },
            { "Helms", rw => rw.runewordType == RunewordType.Helms },
            { "Shields", rw => rw.runewordType == RunewordType.Shields },
            { "All Runewords", rw => true },
            { "2.6", rw => rw.gameVersion == "Resurrected 2.6" },
            { "2.4", rw => rw.gameVersion == "Resurrected 2.4" },
            { "1.11", rw => rw.gameVersion == "1.11" },
            { "1.10", rw => rw.gameVersion == "1.10" },
            { "Original", rw => rw.gameVersion == "Original Rune Words" },
            { "Amazon Spears", rw => rw.subType.Contains("Amazon") || (rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile")) },
            { "Axes", rw => rw.subType.Contains("Axes") || (rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile")) },
            { "Claws", rw => (rw.subType.Contains("Claws") || (rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile"))) && rw.runes.Count <= 3 },
            { "Clubs", rw => (rw.subType.Contains("Clubs") || (rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile"))) && rw.runes.Count <= 3 },
            { "Daggers", rw => (rw.subType.Contains("Daggers") || (rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile"))) && rw.runes.Count <= 3 },
            { "Hammers", rw => rw.subType.Contains("Hammers") || (rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile")) },
            { "Maces", rw => (rw.subType.Contains("Maces") || (rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile"))) && rw.runes.Count <= 5 },
            { "Melee Weapons", rw => !rw.subType.Contains("Missile") && !rw.subType.Contains("Armor") && !rw.subType.Contains("Helms") && !rw.subType.Contains("Shields") },
            { "Missile Weapons", rw => rw.subType.Contains("Missile") || (rw.subType.Contains("Weapons") && !rw.subType.Contains("Melee")) },
            { "Polearms", rw => rw.subType.Contains("Polearms") || (rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile")) },
            { "Scepters", rw => (rw.subType.Contains("Scepters") || (rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile"))) && rw.runes.Count <= 5 },
            { "Spears", rw => rw.subType.Contains("Spears") || (rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile")) },
            { "Staves (Not Orbs)", rw => (rw.subType.Contains("Staves") || rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile")) },
            { "Swords", rw => (rw.subType.Contains("Swords") || rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile")) },
            { "Wands", rw => (rw.subType.Contains("Wands") && rw.runes.Count <= 2 || rw.subType.Contains("Weapons") && !rw.subType.Contains("Missile") && rw.runes.Count <= 2) },
            { "All Weapons", rw => (rw.runewordType == RunewordType.Weapons) }
        };

        if (filters.TryGetValue(type, out var filter))
        {
            workflowDB.runewords.AddRange(currentDB.runewords.Where(filter));
        }

        lastPressed = type;
        if (AppManager.instance.currentLanguage == Languages.En)
            status.text = $"({workflowDB.runewords.Count}) {lastPressed}";
        else
            Localize(type);
        
        FillRunewordList(workflowDB);
    }
    
    private void FillRunewordList(RunewordsDB_SO dbToFillFrom)
    {
        ClearRunewordsList();
        int odd = 1;
        foreach (var rw in dbToFillFrom.runewords)
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

                lc.runes[i].color = defaultRuneColor;
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
                    workflowDB.runewords.Sort((b, a) => a.runewordName.CompareTo(b.runewordName));
                    break;
                case "Runes":
                    workflowDB.runewords.Sort((a, b) =>
                    {
                        int result = a.hasRunes.CompareTo(b.hasRunes);
                        return result == 0 ? a.runes.Count.CompareTo(b.runes.Count) : result;
                    });
                    break;
                case "Level":
                    workflowDB.runewords.Sort((a, b) =>
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
                    workflowDB.runewords.Sort((a, b) => a.runewordName.CompareTo(b.runewordName));
                    break;
                case "Runes":
                    workflowDB.runewords.Sort((b, a) =>
                    {
                        int result = a.hasRunes.CompareTo(b.hasRunes);
                        return result == 0 ? a.runes.Count.CompareTo(b.runes.Count) : result;
                    });
                    break;
                case "Level":
                    workflowDB.runewords.Sort((b, a) =>
                    {
                        int result = a.reqLevel.CompareTo(b.reqLevel);
                        return result == 0 ? b.runewordName.CompareTo(a.runewordName) : result;
                    });
                    break;
            }
        }
        toggle = !toggle;
        FillRunewordList(workflowDB);
    }
    
    private void ClearWorkflowDB()
    {
        workflowDB.runewords.Clear();
    }

    private void ClearCustomSearchDB()
    {
        customSearchDB.runewords.Clear();
    }

    private void ClearRunewordsList()
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

    private void Localize(string type)
    {
        status.text = $"({workflowDB.runewords.Count}) ";
        switch (type)
        {
            case "Armors": status.text += "Броня"; break;
            case "Helms": status.text += "Шлемы"; break;
            case "Shields": status.text += "Щиты"; break;
            case "All Runewords": status.text += "Все Рунворды"; break;
            case "2.4": status.text += "Патч 2.4"; break;
            case "2.6": status.text += "Патч 2.6"; break;
            case "1.11": status.text += "Повелитель Разрушений 1.11 и старше"; break;
            case "1.10": status.text += "Повелитель Разрушений 1.10 и старше"; break;
            case "Original": status.text += "Оригинальные Рунворды (до 1.10)"; break;
            case "Amazon Spears": status.text += "Копья Амазонки"; break;
            case "Axes": status.text += "Топоры"; break;
            case "Claws": status.text += "Когти"; break;
            case "Clubs": status.text += "Дубины"; break;
            case "Daggers": status.text += "Кинжалы"; break;
            case "Hammers": status.text += "Молоты"; break;
            case "Maces": status.text += "Булавы"; break;
            case "Melee Weapons": status.text += "Оружие ближнего боя"; break;
            case "Missile Weapons": status.text += "Оружие дальнего боя"; break;
            case "Polearms": status.text += "Древковое оружие"; break;
            case "Scepters": status.text += "Скипетры"; break;
            case "Spears": status.text += "Копья"; break;
            case "Staves (Not Orbs)": status.text += "Двуручные Посохи (Не Сферы)"; break;
            case "Swords": status.text += "Мечи"; break;
            case "Wands": status.text += "Жезлы Некроманта"; break;
            case "All Weapons": status.text += "Все оружие"; break;
        }
    }

}

public class RunewordSerializer
{
    public void SaveRunewordsToXml(List<Runeword_SO> runewords, string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Runeword_SO>));
        FileStream stream = new FileStream(filePath, FileMode.Create);
        serializer.Serialize(stream, runewords);
        stream.Close();
    }
}