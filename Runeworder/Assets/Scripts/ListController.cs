using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ListController : MonoBehaviour
{
    public Runeword_SO runeword;
    public Image background;
    public Text runewordName;
    public List<Text> runes;
    public Text reqLevel;
    public Text type;
    public Button btn;

    public Canvas parent;
    public GameObject tooltip;
    public GameObject tooltipRune;

    Color ressurectedColor;

    private void Awake()
    {
        btn.onClick.AddListener(ShowTooltip);
        parent = FindFirstObjectByType<Canvas>();
        ressurectedColor = new Color32(236,140,24,255);
    }

    private void ShowTooltip()
    {
        AppManager.instance.gameState = GameState.Tooltip;
        var inst = Instantiate(tooltip, parent.transform);
        TooltipController tc = inst.GetComponent<TooltipController>();
        Text[] txts = tc.GetComponentsInChildren<Text>();
        for (int i = 0; i < runeword.sprites.Count; i++)
        {
            tooltipRune.GetComponent<Image>().sprite = runeword.sprites[i];
            Instantiate(tooltipRune, tc.runeIcons.transform);
        }
        tc.rwName.text = runeword.runewordName;
        tc.rwSeq.text = runeword.runesSequence;
        tc.rwStats.text = runeword.statsDesc;
        tc.rwType.text = runeword.subType;
        tc.rwLevel.text = $"Required level: {runeword.reqLevel}";
        if (runeword.gameVersion.Contains("Resurrected"))
            tc.rwLadder.text = runeword.isLadder ? $"Ladder Item: Yes, {runeword.gameVersion}" : "Ladder Item: No";
        else
            tc.rwLadder.text = runeword.isLadder ? "Ladder Item: Yes, Diablo 2 (LoD)" : "Ladder Item: No";


        tc.rwClass.text = runeword.classItem != Classes.Any ? $"Class specified: {runeword.classItem}" : "Class specified: Any";
        if (runeword.gameVersion.Contains("1.10") || runeword.gameVersion.Contains("1.11"))
            tc.rwVersion.text = $"Game version: {runeword.gameVersion} + (LoD)";
        else
            tc.rwVersion.text = $"Game Version: {runeword.gameVersion}";

        tc.rwVersion.color = Color.white;
        switch (runeword.gameVersion)
        {
            case "Resurrected 2.4":
            case "Resurrected 2.6":
                tc.rwVersion.color = ressurectedColor;
                break;
            case "1.10":
            case "1.11":
                tc.rwVersion.color = new Color32(222, 222, 150, 255);
                break;
        }
        tc.rwItem.text = $"{runeword.recomendedItems}";

        if (AppManager.instance.currentLanguage == Languages.Ru)
        {
            for (int i = 0; i < 33; i++)
            {
                tc.rwSeq.text = tc.rwSeq.text.Replace($"{Enum.GetName(typeof(RunesEn), i)}", $"{Enum.GetName(typeof(RunesRu), i)}");
            }
            int sockets = int.Parse(tc.rwType.text[0].ToString());

            switch (sockets)
            {
                case 2:
                case 3:
                case 4:
                    if (tc.rwType.text.Contains("Socket"))
                        tc.rwType.text = tc.rwType.text.Replace("Socket", "������:");
                    break;
                case 5:
                case 6:
                    if (tc.rwType.text.Contains("Socket"))
                        tc.rwType.text = tc.rwType.text.Replace("Socket", "�����:");
                    break;

            }

            if (tc.rwType.text.Contains("Armor"))
                tc.rwType.text = tc.rwType.text.Replace("Armor", "�����");
            if (tc.rwType.text.Contains("Helms"))
                tc.rwType.text = tc.rwType.text.Replace("Helms", "�����");
            if (tc.rwType.text.Contains("Shields"))
                tc.rwType.text = tc.rwType.text.Replace("Shields", "����");
            if (tc.rwType.text.Contains("Amazon Spears"))
                tc.rwType.text = tc.rwType.text.Replace("Amazon Spears", "����� ��������");
            if (tc.rwType.text.Contains("Axes"))
                tc.rwType.text = tc.rwType.text.Replace("Axes", "������");
            if (tc.rwType.text.Contains("Claws"))
                tc.rwType.text = tc.rwType.text.Replace("Claws", "�����");
            if (tc.rwType.text.Contains("Clubs"))
                tc.rwType.text = tc.rwType.text.Replace("Clubs", "�������");
            if (tc.rwType.text.Contains("Daggers"))
                tc.rwType.text = tc.rwType.text.Replace("Daggers", "�������");
            if (tc.rwType.text.Contains("Hammers"))
                tc.rwType.text = tc.rwType.text.Replace("Hammers", "������");
            if (tc.rwType.text.Contains("Maces"))
                tc.rwType.text = tc.rwType.text.Replace("Maces", "������");
            if (tc.rwType.text.Contains("Melee Weapons"))
                tc.rwType.text = tc.rwType.text.Replace("Melee Weapons", "������ �������� ���");
            if (tc.rwType.text.Contains("Missile Weapons"))
                tc.rwType.text = tc.rwType.text.Replace("Missile Weapons", "������ �������� ���");
            if (tc.rwType.text.Contains("Polearms"))
                tc.rwType.text = tc.rwType.text.Replace("Polearms", "��������� ������");
            if (tc.rwType.text.Contains("Scepters"))
                tc.rwType.text = tc.rwType.text.Replace("Scepters", "��������");
            if (tc.rwType.text.Contains("Spears"))
                tc.rwType.text = tc.rwType.text.Replace("Spears", "�����");
            if (tc.rwType.text.Contains("Staves"))
                tc.rwType.text = tc.rwType.text.Replace("Staves", "��������� ������");
            if (tc.rwType.text.Contains("Swords"))
                tc.rwType.text = tc.rwType.text.Replace("Swords", "����");
            if (tc.rwType.text.Contains("Wands"))
                tc.rwType.text = tc.rwType.text.Replace("Wands", "����� ����������");
            if (tc.rwType.text.Contains("Weapons"))
                tc.rwType.text = tc.rwType.text.Replace("Weapons", "����� ������");

            tc.rwLevel.text = $"��������� �������: {runeword.reqLevel}";
            if (runeword.gameVersion.Contains("Resurrected"))
                tc.rwLadder.text = runeword.isLadder ? $"������: ��, {runeword.gameVersion}" : "������: ���";
            else
                tc.rwLadder.text = runeword.isLadder ? "������: ��, Diablo 2 (LoD)" : "������: ���";

            tc.rwClass.text = runeword.classItem != Classes.Any ? $"�����: {runeword.classItem}" : "�����: �����";
            switch (tc.rwClass.text)
            {
                case "�����: Amazon":
                    tc.rwClass.text = tc.rwClass.text.Replace("Amazon", "��������");
                    break;
                case "�����: Assassin":
                    tc.rwClass.text = tc.rwClass.text.Replace("Assassin", "������");
                    break;
                case "�����: Barbarian":
                    tc.rwClass.text = tc.rwClass.text.Replace("Barbarian", "������");
                    break;
                case "�����: Druid":
                    tc.rwClass.text = tc.rwClass.text.Replace("Druid", "�����");
                    break;
                case "�����: Necromancer":
                    tc.rwClass.text = tc.rwClass.text.Replace("Necromancer", "���������");
                    break;
                case "�����: Paladin":
                    tc.rwClass.text = tc.rwClass.text.Replace("Paladin", "�������");
                    break;
                case "�����: Sorceress":
                    tc.rwClass.text = tc.rwClass.text.Replace("Sorceress", "����������");
                    break;
            }

            if (runeword.gameVersion.Contains("1.10") || runeword.gameVersion.Contains("1.11"))
                tc.rwVersion.text = $"������ ����: {runeword.gameVersion} + (LoD)";
            else
                tc.rwVersion.text = $"������ ����: {runeword.gameVersion}";
            tc.bestItemLabel.text = "������ ������� ��������:";
        }
    }
}
