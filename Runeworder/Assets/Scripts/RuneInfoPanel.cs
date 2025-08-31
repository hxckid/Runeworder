using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RuneInfoPanel : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject panel;
    public Image runeIcon;
    public TextMeshProUGUI runeName;
    public TextMeshProUGUI runeLevel;
    public TextMeshProUGUI craftingRecipe;
    public TextMeshProUGUI runeBonuses;
    
    [Header("Databases")]
    public RunesDB_SO runesDBEng;
    public RunesDB_SO runesDBRus;
    
    private RunesDB_SO currentDB;
    
    void Start()
    {
        // Подписываемся на изменение языка
        AppManager.OnLanguageChanged += OnLanguageChanged;
        
        // Скрываем панель при старте
        HidePanel();
    }
    
    void OnLanguageChanged(Languages language, string version)
    {
        currentDB = (language == Languages.En) ? runesDBEng : runesDBRus;
    }
    
    public void ShowRuneInfo(RunesEn runeType)
    {
        if (currentDB == null)
        {
            currentDB = (AppManager.instance.currentLanguage == Languages.En) ? runesDBEng : runesDBRus;
        }
        
        Rune_SO rune = currentDB.GetRune(runeType);
        if (rune == null)
        {
            Debug.LogWarning($"Rune {runeType} not found in database!");
            return;
        }
        
        // Заполняем UI данными руны
        runeIcon.sprite = rune.runeIcon;
        runeName.text = rune.runeType.ToString();
        runeLevel.text = $"Level: {rune.runeLevel}";
        
        // Формируем рецепт крафта
        string recipeText = "Crafting Recipe: ";
        bool hasIngredients = false;
        
        // Добавляем руны
        for (int i = 0; i < rune.craftingRunes.Count; i++)
        {
            if (hasIngredients) recipeText += " + ";
            recipeText += rune.craftingRunes[i].ToString();
            hasIngredients = true;
        }
        
        // Добавляем драгоценные камни
        for (int i = 0; i < rune.craftingGems.Count; i++)
        {
            if (hasIngredients) recipeText += " + ";
            if (rune.craftingGems[i] != null)
            {
                string gemName = GetGemDisplayName(rune.craftingGems[i]);
                recipeText += gemName;
            }
            else
            {
                recipeText += "Unknown Gem";
            }
            hasIngredients = true;
        }
        
        if (!hasIngredients)
        {
            recipeText += "Cannot be crafted";
        }
        craftingRecipe.text = recipeText;
        
        // Заполняем бонусы
        runeBonuses.text = rune.runeBonuses;
        
        // Показываем панель
        ShowPanel();
    }
    
    public void ShowPanel()
    {
        panel.SetActive(true);
    }
    
    public void HidePanel()
    {
        panel.SetActive(false);
    }
    
    private string GetGemDisplayName(Gem_SO gem)
    {
        if (gem == null) return "Unknown Gem";
        
        string qualityName = GetGemQualityTranslation(gem.gemQuality);
        string typeName = GetGemTypeTranslation(gem.gemType);
        
        return $"{qualityName} {typeName}";
    }
    
    private string GetGemQualityTranslation(GemQuality quality)
    {
        if (AppManager.instance.currentLanguage == Languages.En)
        {
            return quality.ToString();
        }
        else
        {
            switch (quality)
            {
                case GemQuality.Chipped: return "Осколок";
                case GemQuality.Flawed: return "Трещина";
                case GemQuality.Normal: return "Обычный";
                case GemQuality.Flawless: return "Безупречный";
                case GemQuality.Perfect: return "Совершенный";
                default: return quality.ToString();
            }
        }
    }
    
    private string GetGemTypeTranslation(GemType type)
    {
        if (AppManager.instance.currentLanguage == Languages.En)
        {
            return type.ToString();
        }
        else
        {
            switch (type)
            {
                case GemType.Amethyst: return "Аметист";
                case GemType.Diamond: return "Алмаз";
                case GemType.Emerald: return "Изумруд";
                case GemType.Ruby: return "Рубин";
                case GemType.Sapphire: return "Сапфир";
                case GemType.Topaz: return "Топаз";
                case GemType.Skull: return "Череп";
                default: return type.ToString();
            }
        }
    }
} 