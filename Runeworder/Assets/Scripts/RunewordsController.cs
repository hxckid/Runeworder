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
    public UserRunes_SO userRunes;
    public RunewordsDB_SO runewordsDBEng;
    public RunewordsDB_SO runewordsDBRus;
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
        InitDB(AppManager.instance.currentLanguage);
        lastPressed = "All";
        lastSocketSearch = 0;
        lastTypeSearch = "None";

        //string filePath = Application.dataPath + "/Runewords.xml";
        //RunewordSerializer serializer = new RunewordSerializer();
        //serializer.SaveRunewordsToXml(runewordsDBRus.runewords, filePath);
        //Debug.Log($"Saved here: {filePath}");
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
                status.text = $"{runewordName.text} �� ������!";
        }
        else
        {
            FillRunewordList(workflowDB);
        }
    }

    public void CustomSearch()
    {
        ClearCustomSearchDB();
        lastSocketSearch = Int32.Parse(socketsDropdown.options[socketsDropdown.value].text); 
        lastTypeSearch = typeDropdown.options[typeDropdown.value].text;

        lastTypeSearch = lastTypeSearch == "��� ��������" ? "All Runewords" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "��� ������" ? "All Weapons" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "����� ��������" ? "Amazon Spears" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "�����" ? "Armors" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "������" ? "Axes" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "�����" ? "Claws" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "������" ? "Clubs" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "�������" ? "Daggers" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "������" ? "Hammers" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "�����" ? "Helms" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "������" ? "Maces" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "������ �������� ���" ? "Melee Weapons" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "������ �������� ���" ? "Missile Weapons" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "��������� ������" ? "Polearms" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "��������" ? "Scepters" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "�����" ? "Spears" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "��������� ������ (�� �����)" ? "Staves (Not Orbs)" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "����" ? "Swords" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "����� ����������" ? "Wands" : lastTypeSearch;
        lastTypeSearch = lastTypeSearch == "����" ? "Shields" : lastTypeSearch;
        
        FilterRunewords(lastTypeSearch);

        if (allRunewordsToggle.isOn)
        {
            foreach (var rw in workflowDB.runewords)
            {
                if (rw.runes.Count == lastSocketSearch)
                    customSearchDB.runewords.Add(rw);
            }
        }
        
        if (nonLadderToggle.isOn)
        {
            foreach (var rw in workflowDB.runewords)
            {
                if (rw.runes.Count == lastSocketSearch && !rw.isLadder)
                    customSearchDB.runewords.Add(rw);
            }
        }

        if (ladderToggle.isOn)
        {
            foreach (var rw in workflowDB.runewords)
            {
                if (rw.runes.Count == lastSocketSearch && rw.isLadder)
                    customSearchDB.runewords.Add(rw);
            }
        }

        ClearWorkflowDB();
        foreach (var rw in customSearchDB.runewords)
        {
            workflowDB.runewords.Add(rw);
        }

        FillRunewordList(customSearchDB);
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
            case "Patch":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.gameVersion == "Resurrected 2.6")
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
                        lc.reqLevel.text = $"�������: {rw.reqLevel}";
                        switch (rw.runewordType)
                        {
                            case RunewordType.Weapons:
                                lc.type.text = $"������";
                                break;
                            case RunewordType.Armor:
                                lc.type.text = $"�����";
                                break;
                            case RunewordType.Helms:
                                lc.type.text = $"�����";
                                break;
                            case RunewordType.Shields:
                                lc.type.text = $"����";
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
        switch (type)
        {
            case "Armors":
                status.text = "�����";
                break;
            case "Helms":
                status.text = "�����";
                break;
            case "Shields":
                status.text = "����";
                break;
            case "All Runewords":
                status.text = "��� ��������";
                break;
            case "Patch":
                status.text = "���� 2.6";
                break;
            case "Amazon Spears":
                status.text = "����� ��������";
                break;
            case "Axes":
                status.text = "������";
                break;
            case "Claws":
                status.text = "�����";
                break;
            case "Clubs":
                status.text = "������";
                break;
            case "Daggers":
                status.text = "�������";
                break;
            case "Hammers":
                status.text = "������";
                break;
            case "Maces":
                status.text = "������";
                break;
            case "Melee Weapons":
                status.text = "������ �������� ���";
                break;
            case "Missile Weapons":
                status.text = "������ �������� ���";
                break;
            case "Polearms":
                status.text = "��������� ������";
                break;
            case "Scepters":
                status.text = "��������";
                break;
            case "Spears":
                status.text = "�����";
                break;
            case "Staves (Not Orbs)":
                status.text = "��������� ������ (�� �����)";
                break;
            case "Swords":
                status.text = "����";
                break;
            case "Wands":
                status.text = "����� ����������";
                break;
            case "All Weapons":
                status.text = "��� ������";
                break;
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