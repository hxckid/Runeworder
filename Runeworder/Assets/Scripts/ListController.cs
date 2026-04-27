using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ListController : MonoBehaviour, IPoolableUI
{
    public Runeword_SO runeword;
    public Image background;
    public Text runewordName;
    public List<Text> runes;
    public Text reqLevel;
    public Text type;
    public Button btn;
    public Image star;

    public Canvas parent;
    public GameObject tooltip;
    public GameObject tooltipRune;

    Color ressurectedColor;
    Color rotwcolor;

    private void Awake()
    {
        btn.onClick.AddListener(ShowTooltip);
        parent = FindFirstObjectByType<Canvas>();
        ressurectedColor = new Color32(236, 140, 24, 255);
        rotwcolor = new Color32(209, 11, 160, 255);

        if (tooltip != null)
        {
            UIObjectPool.Instance.Prewarm(tooltip, 1, parent != null ? parent.transform : null);
        }
        if (tooltipRune != null)
        {
            UIObjectPool.Instance.Prewarm(tooltipRune, 8, parent != null ? parent.transform : null);
        }
    }

    public void UpdateStarVisibility()
    {
        if (star != null && runeword != null)
        {
            bool isFavorite = AppManager.instance.IsRunewordFavorite(runeword.name);
            star.gameObject.SetActive(isFavorite);
        }
    }

    private void ShowTooltip()
    {
        AppManager.instance.gameState = GameState.Tooltip;
        var inst = UIObjectPool.Instance.Get(tooltip, parent.transform);
        TooltipController tc = inst.GetComponent<TooltipController>();
        tc.ClearRuneIcons();
        var tooltipRunes = new List<GameObject>(runeword.sprites.Count);

        for (int i = 0; i < runeword.sprites.Count; i++)
        {
            var runeIcon = UIObjectPool.Instance.Get(tooltipRune, tc.runeIcons.transform);
            var runeImage = runeIcon.GetComponent<Image>();
            if (runeImage != null)
            {
                runeImage.sprite = runeword.sprites[i];
                runeImage.color = Color.white;
            }

            tooltipRunes.Add(runeIcon);
        }

        tc.SetActiveIcons(tooltipRunes);
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
            case "Reign Of The Warlock":
                tc.rwVersion.color = rotwcolor;
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
                        tc.rwType.text = tc.rwType.text.Replace("Socket", "Гнезда:");
                    break;
                case 5:
                case 6:
                    if (tc.rwType.text.Contains("Socket"))
                        tc.rwType.text = tc.rwType.text.Replace("Socket", "Гнезд:");
                    break;

            }

            if (tc.rwType.text.Contains("Armor"))
                tc.rwType.text = tc.rwType.text.Replace("Armor", "Броня");
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
                tc.rwType.text = tc.rwType.text.Replace("Clubs", "Дубины");
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
                tc.rwType.text = tc.rwType.text.Replace("Polearms", "Древковое оружие");
            if (tc.rwType.text.Contains("Scepters"))
                tc.rwType.text = tc.rwType.text.Replace("Scepters", "Скипетры");
            if (tc.rwType.text.Contains("Spears"))
                tc.rwType.text = tc.rwType.text.Replace("Spears", "Копья");
            if (tc.rwType.text.Contains("Staves"))
                tc.rwType.text = tc.rwType.text.Replace("Staves", "Посохи");
            if (tc.rwType.text.Contains("Swords"))
                tc.rwType.text = tc.rwType.text.Replace("Swords", "Мечи");
            if (tc.rwType.text.Contains("Wands"))
                tc.rwType.text = tc.rwType.text.Replace("Wands", "Жезлы");
            if (tc.rwType.text.Contains("Weapons"))
                tc.rwType.text = tc.rwType.text.Replace("Weapons", "Оружие");

            tc.rwLevel.text = $"Требуемый уровень: {runeword.reqLevel}";
            if (runeword.gameVersion.Contains("Resurrected"))
                tc.rwLadder.text = runeword.isLadder ? $"Ладдер: Да, {runeword.gameVersion}" : "Ладдер: Нет";
            else
                tc.rwLadder.text = runeword.isLadder ? "Ладдер: Да, Diablo 2 (LoD)" : "Ладдер: Нет";

            tc.rwClass.text = runeword.classItem != Classes.Any ? $"Класс: {runeword.classItem}" : "Класс: Любой";
            switch (tc.rwClass.text)
            {
                case "Класс: Amazon":
                    tc.rwClass.text = tc.rwClass.text.Replace("Amazon", "Амазонка");
                    break;
                case "Класс: Assassin":
                    tc.rwClass.text = tc.rwClass.text.Replace("Assassin", "Ассасин");
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
                case "Класс: Warlock":
                    tc.rwClass.text = tc.rwClass.text.Replace("Warlock", "Чернокнижник");
                    break;
            }

            if (runeword.gameVersion.Contains("1.10") || runeword.gameVersion.Contains("1.11"))
                tc.rwVersion.text = $"Версия игры: {runeword.gameVersion} + (LoD)";
            else
                tc.rwVersion.text = $"Версия игры: {runeword.gameVersion}";
            tc.bestItemLabel.text = "Лучшие базовые предметы:";
        }

        // Initialize star toggle state
        tc.InitializeStarToggle(runeword);
    }

    public void OnBeforeGetFromPool()
    {
        background.color = Color.white;
        runeword = null;

        if (runewordName != null) runewordName.text = string.Empty;
        if (reqLevel != null) reqLevel.text = string.Empty;
        if (type != null) type.text = string.Empty;

        if (star != null)
        {
            star.gameObject.SetActive(false);
        }

        foreach (var rune in runes)
        {
            if (rune == null)
            {
                continue;
            }

            rune.text = string.Empty;
            rune.color = Color.white;
        }
    }

    public void OnBeforeReleaseToPool()
    {
        runeword = null;
        if (star != null)
        {
            star.gameObject.SetActive(false);
        }
    }
}