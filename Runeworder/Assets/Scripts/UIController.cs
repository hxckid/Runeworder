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
    public Dropdown socketsDropdown;
    public Dropdown typeDropdown;
    public GameObject donationPanel;

    [SerializeField] List<GameObject> runesUI;

    private void Start()
    {
        AppManager.OnLanguageChanged += Localize;
        AppManager.OnLanguageChanged += ShowDonationPanel;

        runesUI = new List<GameObject>();
        InitRunes();
        ShowDonationPanel(AppManager.instance.currentLanguage, string.Empty);
    }

    private void ShowDonationPanel(Languages languages, string ver)
    {
        if (languages == Languages.Ru)
            donationPanel.SetActive(true);
        else
            donationPanel.SetActive(false);
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
        Localize(AppManager.instance.currentLanguage, "Resurrected");
    }

    public void Localize(Languages lang, string ver)
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

                    socketsDropdown.options.Clear();
                    socketsDropdown.captionText.text = "All";
                    socketsDropdown.options.Add(new Dropdown.OptionData("All"));
                    socketsDropdown.options.Add(new Dropdown.OptionData("6"));
                    socketsDropdown.options.Add(new Dropdown.OptionData("5"));
                    socketsDropdown.options.Add(new Dropdown.OptionData("4"));
                    socketsDropdown.options.Add(new Dropdown.OptionData("3"));
                    socketsDropdown.options.Add(new Dropdown.OptionData("2"));

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

                    socketsDropdown.options.Clear();
                    socketsDropdown.captionText.text = "���";
                    socketsDropdown.options.Add(new Dropdown.OptionData("���"));
                    socketsDropdown.options.Add(new Dropdown.OptionData("6"));
                    socketsDropdown.options.Add(new Dropdown.OptionData("5"));
                    socketsDropdown.options.Add(new Dropdown.OptionData("4"));
                    socketsDropdown.options.Add(new Dropdown.OptionData("3"));
                    socketsDropdown.options.Add(new Dropdown.OptionData("2"));

                    typeDropdown.options.Clear();
                    typeDropdown.captionText.text = "��� ��������";
                    typeDropdown.options.Add(new Dropdown.OptionData("��� ��������"));
                    typeDropdown.options.Add(new Dropdown.OptionData("��� ������"));
                    typeDropdown.options.Add(new Dropdown.OptionData("����� ��������"));
                    typeDropdown.options.Add(new Dropdown.OptionData("�����"));
                    typeDropdown.options.Add(new Dropdown.OptionData("������"));
                    typeDropdown.options.Add(new Dropdown.OptionData("�����"));
                    typeDropdown.options.Add(new Dropdown.OptionData("������"));
                    typeDropdown.options.Add(new Dropdown.OptionData("�������"));
                    typeDropdown.options.Add(new Dropdown.OptionData("������"));
                    typeDropdown.options.Add(new Dropdown.OptionData("�����"));
                    typeDropdown.options.Add(new Dropdown.OptionData("������"));
                    typeDropdown.options.Add(new Dropdown.OptionData("������ �������� ���"));
                    typeDropdown.options.Add(new Dropdown.OptionData("������ �������� ���"));
                    typeDropdown.options.Add(new Dropdown.OptionData("��������� ������"));
                    typeDropdown.options.Add(new Dropdown.OptionData("��������"));
                    typeDropdown.options.Add(new Dropdown.OptionData("�����"));
                    typeDropdown.options.Add(new Dropdown.OptionData("��������� ������ (�� �����)"));
                    typeDropdown.options.Add(new Dropdown.OptionData("����"));
                    typeDropdown.options.Add(new Dropdown.OptionData("����� ����������"));
                    typeDropdown.options.Add(new Dropdown.OptionData("����"));
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
    ��, ���, ���, ���, ��, ��, ���, ���, ���, ���, ���,
    ���, ����, ���, ���, ��, ���, ��, ���, ���, ���, ��,
    ���, ���, ���, ����, ��, ��, ���, ���, ���, ���, ���
}