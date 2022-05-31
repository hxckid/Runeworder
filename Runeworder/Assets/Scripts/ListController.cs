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
        parent = FindObjectOfType<Canvas>();
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
        tc.rwStats.resizeTextForBestFit = true;
        tc.rwStats.resizeTextMaxSize = 55;
        tc.rwStats.resizeTextMinSize = 45;
        tc.rwType.text = runeword.subType;
        tc.rwLevel.text = $"Required level: {runeword.reqLevel}";
        if (runeword.gameVersion != "Ressurected")
            tc.rwLadder.text = runeword.isLadder ? "Ladder Item: Diablo 2 (LoD)" : "Ladder Item: No";
        else
            tc.rwLadder.text = runeword.isLadder ? "Ladder Item: Ressurected" : "Ladder Item: No";
        tc.rwClass.text = runeword.classItem != Classes.Any ? $"Class specified: {runeword.classItem}" : "Class specified: Any";
        if (runeword.gameVersion == "1.10" || runeword.gameVersion == "1.11")
            tc.rwVersion.text = $"Game version: {runeword.gameVersion}+ (LoD)";
        else
            tc.rwVersion.text = $"Game Version: Resurrected";

        tc.rwVersion.color = Color.white;
        switch (runeword.gameVersion)
        {
            case "Ressurected":
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

            tc.rwStats.resizeTextForBestFit = true;
            tc.rwStats.resizeTextMaxSize = 45;
            tc.rwStats.resizeTextMinSize = 40;

            foreach (var txt in txts)
            {
                txt.font = AppManager.instance.cyrillic;
            }
            int sockets = int.Parse(tc.rwType.text[0].ToString());

            switch (sockets)
            {
                case 2:
                case 3:
                case 4:
                    if (tc.rwType.text.Contains("Socket"))
                        tc.rwType.text = tc.rwType.text.Replace("Socket", "Гнезда:");
                    break;
                case 5:
                case 6:
                    if (tc.rwType.text.Contains("Socket"))
                        tc.rwType.text = tc.rwType.text.Replace("Socket", "Гнезд:");
                    break;

            }

            if (tc.rwType.text.Contains("BodyArmor"))
                tc.rwType.text = tc.rwType.text.Replace("BodyArmor", "Броня");
            if (tc.rwType.text.Contains("Helms"))
                tc.rwType.text = tc.rwType.text.Replace("Helms", "Шлемы");
            if (tc.rwType.text.Contains("Shields"))
                tc.rwType.text = tc.rwType.text.Replace("Shields", "Щиты");
            if (tc.rwType.text.Contains("Amazon Spears"))
                tc.rwType.text = tc.rwType.text.Replace("Amazon Spears", "Копья Амазонки");
            if (tc.rwType.text.Contains("Axes"))
                tc.rwType.text = tc.rwType.text.Replace("Axes", "Топоры");
            if (tc.rwType.text.Contains("Claws"))
                tc.rwType.text = tc.rwType.text.Replace("Claws", "Когти");
            if (tc.rwType.text.Contains("Clubs"))
                tc.rwType.text = tc.rwType.text.Replace("Clubs", "Дубинки");
            if (tc.rwType.text.Contains("Daggers"))
                tc.rwType.text = tc.rwType.text.Replace("Daggers", "Кинжалы");
            if (tc.rwType.text.Contains("Hammers"))
                tc.rwType.text = tc.rwType.text.Replace("Hammers", "Молоты");
            if (tc.rwType.text.Contains("Maces"))
                tc.rwType.text = tc.rwType.text.Replace("Maces", "Булавы");
            if (tc.rwType.text.Contains("Melee Weapons"))
                tc.rwType.text = tc.rwType.text.Replace("Melee Weapons", "Оружие ближнего боя");
            if (tc.rwType.text.Contains("Missile Weapons"))
                tc.rwType.text = tc.rwType.text.Replace("Missile Weapons", "Оружие дальнего боя");
            if (tc.rwType.text.Contains("Polearms"))
                tc.rwType.text = tc.rwType.text.Replace("Polearms", "Древковое Оружие");
            if (tc.rwType.text.Contains("Scepters"))
                tc.rwType.text = tc.rwType.text.Replace("Scepters", "Скипетры");
            if (tc.rwType.text.Contains("Spears"))
                tc.rwType.text = tc.rwType.text.Replace("Spears", "Копья");
            if (tc.rwType.text.Contains("Staves"))
                tc.rwType.text = tc.rwType.text.Replace("Staves", "Двуручные Посохи");
            if (tc.rwType.text.Contains("Swords"))
                tc.rwType.text = tc.rwType.text.Replace("Swords", "Мечи");
            if (tc.rwType.text.Contains("Wands"))
                tc.rwType.text = tc.rwType.text.Replace("Wands", "Жезлы Некроманта");
            if (tc.rwType.text.Contains("Weapons"))
                tc.rwType.text = tc.rwType.text.Replace("Weapons", "Любое оружие");

            txts[0].fontSize = 90; //Name
            txts[1].fontSize = 58; //RuneSeq
            //txts[2].fontSize = 40; //Stats
            txts[3].resizeTextForBestFit = true; //Type
            txts[3].resizeTextMaxSize = 45; //Type
            txts[3].resizeTextMinSize = 40; //Type
            txts[4].fontSize = 46; //Best Item Label
            txts[5].fontSize = 40; //Best Item Field
            txts[6].fontSize = 46; //Lvl
            txts[7].fontSize = 40; //Ladder
            txts[8].fontSize = 40; //Class
            txts[9].fontSize = 40; //Version
            txts[10].fontSize = 56; //X

            tc.rwLevel.text = $"Требуемый уровень: {runeword.reqLevel}";
            if (runeword.gameVersion != "Ressurected")
                tc.rwLadder.text = runeword.isLadder ? "Ладдер: Diablo 2 LoD " : "Ладдерный Предмет: Нет";
            else
                tc.rwLadder.text = runeword.isLadder ? "Ладдер: Ressurected" : "Ладдерный Предмет: Нет";

            tc.rwClass.text = runeword.classItem != Classes.Any ? $"Класс: {runeword.classItem}" : "Класс: Любой";
            switch (tc.rwClass.text)
            {
                case "Класс: Amazon":
                    tc.rwClass.text = tc.rwClass.text.Replace("Amazon", "Амазонка");
                    break;
                case "Класс: Assassin":
                    tc.rwClass.text = tc.rwClass.text.Replace("Assassin", "Убийца");
                    break;
                case "Класс: Barbarian":
                    tc.rwClass.text = tc.rwClass.text.Replace("Barbarian", "Варвар");
                    break;
                case "Класс: Druid":
                    tc.rwClass.text = tc.rwClass.text.Replace("Druid", "Друид");
                    break;
                case "Класс: Necromancer":
                    tc.rwClass.text = tc.rwClass.text.Replace("Necromancer", "Некромант");
                    break;
                case "Класс: Paladin":
                    tc.rwClass.text = tc.rwClass.text.Replace("Paladin", "Паладин");
                    break;
                case "Класс: Sorceress":
                    tc.rwClass.text = tc.rwClass.text.Replace("Sorceress", "Волшебница");
                    break;
            }
            
            if (runeword.gameVersion == "1.10" || runeword.gameVersion == "1.11")
                tc.rwVersion.text = $"Версия игры: {runeword.gameVersion}+ (LoD)";
            else
                tc.rwVersion.text = $"Версия игры: Любая";
            tc.bestItemLabel.text = "Лучшие базовые предметы:";
        }
    }
}
