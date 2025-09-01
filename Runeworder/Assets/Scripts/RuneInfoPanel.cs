using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RuneInfoPanel : MonoBehaviour
{
    [Header("UI Elements")]
    public Image runeIcon;
    public TextMeshProUGUI runeName;
    public TextMeshProUGUI runeLevel;
    public TextMeshProUGUI runeRecipe;
    public Transform recipePanel; // Панель Recipe с дочерним элементом Icons
    public TextMeshProUGUI weaponBonus;
    public TextMeshProUGUI armorBonus;
    public TextMeshProUGUI helmetBonus;
    public TextMeshProUGUI shieldBonus;
    public TextMeshProUGUI socketableItems;
    public Button closeButton;
    
    [Header("Databases")]
    public RunesDB_SO runesDBEng;
    public RunesDB_SO runesDBRus;
    
    [Header("Fonts")]
    public TMP_FontAsset blizzardFont;
    
    private TMP_FontAsset GetBlizzardFont()
    {
        // Пытаемся получить шрифт из существующих UI элементов
        if (blizzardFont != null) return blizzardFont;
        
        // Если не назначен в инспекторе, берем из существующих элементов
        if (runeName != null && runeName.font != null) return runeName.font;
        if (runeLevel != null && runeLevel.font != null) return runeLevel.font;
        if (runeRecipe != null && runeRecipe.font != null) return runeRecipe.font;
        
        // Загружаем шрифт по GUID
        string fontPath = "Assets/Fonts/Blizzard SDF.asset";
        TMP_FontAsset font = Resources.Load<TMP_FontAsset>(fontPath);
        if (font != null) return font;
        
        // Последняя попытка - загружаем через AssetDatabase
        #if UNITY_EDITOR
        font = UnityEditor.AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(fontPath);
        if (font != null) return font;
        #endif
        
        Debug.LogWarning("Не удалось найти шрифт Blizzard, используется шрифт по умолчанию");
        return null;
    }
    
    private RunesDB_SO currentDB;
    
    void Start()
    {
        // Подписываемся на изменение языка
        AppManager.OnLanguageChanged += OnLanguageChanged;
        
        // Находим UI элементы по именам в префабе
        FindUIElements();
        
        // Настраиваем кнопку закрытия
        SetupCloseButton();
    }
    
    void FindUIElements()
    {
        // Находим UI элементы по именам в префабе
        runeIcon = transform.Find("RuneIcon")?.GetComponent<Image>();
        runeName = transform.Find("runeName")?.GetComponent<TextMeshProUGUI>();
        runeLevel = transform.Find("runeLevel")?.GetComponent<TextMeshProUGUI>();
        runeRecipe = transform.Find("Rune Recipe")?.GetComponent<TextMeshProUGUI>();
        recipePanel = transform.Find("Recipe");
        weaponBonus = transform.Find("weaponBonus")?.GetComponent<TextMeshProUGUI>();
        armorBonus = transform.Find("armorBonus")?.GetComponent<TextMeshProUGUI>();
        helmetBonus = transform.Find("helmetBonus")?.GetComponent<TextMeshProUGUI>();
        shieldBonus = transform.Find("shieldBonus")?.GetComponent<TextMeshProUGUI>();
        socketableItems = transform.Find("SocketableItems")?.GetComponent<TextMeshProUGUI>();
        closeButton = transform.Find("CloseBtn")?.GetComponent<Button>();
        
        // Проверяем, что все элементы найдены
        if (runeIcon == null) Debug.LogWarning("RuneIcon не найден в префабе");
        if (runeName == null) Debug.LogWarning("runeName не найден в префабе");
        if (runeLevel == null) Debug.LogWarning("runeLevel не найден в префабе");
        if (runeRecipe == null) Debug.LogWarning("Rune Recipe не найден в префабе");
        if (recipePanel == null) Debug.LogWarning("Recipe панель не найдена в префабе");
        if (weaponBonus == null) Debug.LogWarning("weaponBonus не найден в префабе");
        if (armorBonus == null) Debug.LogWarning("armorBonus не найден в префабе");
        if (helmetBonus == null) Debug.LogWarning("helmetBonus не найден в префабе");
        if (shieldBonus == null) Debug.LogWarning("shieldBonus не найден в префабе");
        if (socketableItems == null) Debug.LogWarning("SocketableItems не найден в префабе");
        if (closeButton == null) Debug.LogWarning("CloseBtn не найден в префабе");
    }
    
    void SetupCloseButton()
    {
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(HidePanel);
        }
    }
    
    void OnLanguageChanged(Languages language, string version)
    {
        currentDB = (language == Languages.En) ? runesDBEng : runesDBRus;
    }
    
    void Update()
    {
        // Закрываем панель по нажатию Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HidePanel();
        }
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
        if (runeIcon != null) runeIcon.sprite = rune.runeIcon;
        if (runeName != null) runeName.text = GetLocalizedRuneName(rune.runeType);
        if (runeLevel != null) runeLevel.text = GetLocalizedLevelText(rune.runeLevel);
        
        // Устанавливаем заголовок рецепта
        if (runeRecipe != null)
        {
            runeRecipe.text = GetLocalizedRecipeText();
        }
        
        // Заполняем панель рецепта
        PopulateRecipePanel(rune);
        
        // Заполняем бонусы с префиксами типов
        if (weaponBonus != null) weaponBonus.text = GetLocalizedWeaponBonusPrefix() + rune.weaponBonus;
        if (armorBonus != null) armorBonus.text = GetLocalizedArmorBonusPrefix() + rune.armorBonus;
        if (helmetBonus != null) helmetBonus.text = GetLocalizedHelmetBonusPrefix() + rune.helmetBonus;
        if (shieldBonus != null) shieldBonus.text = GetLocalizedShieldBonusPrefix() + rune.shieldBonus;
        
        // Заполняем информацию о вставке в гнезда
        if (socketableItems != null) socketableItems.text = GetLocalizedSocketableItemsText();
        
        // Показываем панель
        ShowPanel();
    }
    
    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }
    
    public void HidePanel()
    {
        // Уничтожаем панель при закрытии
        Destroy(gameObject);
    }
    
    private string GetLocalizedRuneName(RunesEn runeType)
    {
        if (AppManager.instance.currentLanguage == Languages.En)
        {
            return runeType.ToString();
        }
        else
        {
            // Используем существующий enum RunesRu для русских названий
            int runeIndex = (int)runeType;
            return Enum.GetName(typeof(RunesRu), runeIndex);
        }
    }
    
    private string GetLocalizedLevelText(int level)
    {
        if (AppManager.instance.currentLanguage == Languages.En)
        {
            return $"Required Level: {level}";
        }
        else
        {
            return $"Требуемый уровень: {level}";
        }
    }
    
    private string GetLocalizedRecipeText()
    {
        if (AppManager.instance.currentLanguage == Languages.En)
        {
            return "Rune Recipe: ";
        }
        else
        {
            return "Рецепт руны: ";
        }
    }
    
    private string GetLocalizedUnknownGemText()
    {
        if (AppManager.instance.currentLanguage == Languages.En)
        {
            return "Unknown Gem";
        }
        else
        {
            return "Неизвестный камень";
        }
    }
    
    private string GetLocalizedCannotCraftText()
    {
        if (AppManager.instance.currentLanguage == Languages.En)
        {
            return "Cannot be crafted";
        }
        else
        {
            return "Нельзя создать";
        }
    }
    
    private string GetLocalizedSocketableItemsText()
    {
        if (AppManager.instance.currentLanguage == Languages.En)
        {
            return "Socketable Items: ";
        }
        else
        {
            return "Вставляемые предметы: ";
        }
    }
    
    private string GetLocalizedWeaponBonusPrefix()
    {
        if (AppManager.instance.currentLanguage == Languages.En)
        {
            return "Weapons: ";
        }
        else
        {
            return "Оружие: ";
        }
    }
    
    private string GetLocalizedArmorBonusPrefix()
    {
        if (AppManager.instance.currentLanguage == Languages.En)
        {
            return "Armor: ";
        }
        else
        {
            return "Доспехи: ";
        }
    }
    
    private string GetLocalizedHelmetBonusPrefix()
    {
        if (AppManager.instance.currentLanguage == Languages.En)
        {
            return "Helmet: ";
        }
        else
        {
            return "Шлем: ";
        }
    }
    
    private string GetLocalizedShieldBonusPrefix()
    {
        if (AppManager.instance.currentLanguage == Languages.En)
        {
            return "Shield: ";
        }
        else
        {
            return "Щит: ";
        }
    }
    
    private void PopulateRecipePanel(Rune_SO rune)
    {
        if (recipePanel == null) return;
        
        // Находим панель Icons внутри Recipe
        Transform iconsPanel = recipePanel.Find("Icons");
        if (iconsPanel == null) return;
        
        // Очищаем существующие элементы
        foreach (Transform child in iconsPanel)
        {
            Destroy(child.gameObject);
        }
        
        // Проверяем, есть ли ингредиенты для крафта
        bool hasIngredients = rune.craftingRunes.Count > 0 || rune.craftingGems.Count > 0;
        
        if (!hasIngredients)
        {
            // Создаем текст "Нельзя создать"
            CreateTextElement(iconsPanel, GetLocalizedCannotCraftText());
            return;
        }
        
        // Добавляем руны
        for (int i = 0; i < rune.craftingRunes.Count; i++)
        {
            CreateRuneElement(iconsPanel, rune.craftingRunes[i]);
        }
        
        // Добавляем драгоценные камни
        for (int i = 0; i < rune.craftingGems.Count; i++)
        {
            CreateGemElement(iconsPanel, rune.craftingGems[i]);
        }
    }
    
    private void CreateTextElement(Transform parent, string text)
    {
        GameObject textObj = new GameObject("TextElement");
        textObj.transform.SetParent(parent, false);
        
        TextMeshProUGUI textComponent = textObj.AddComponent<TextMeshProUGUI>();
        textComponent.text = text;
        textComponent.font = GetBlizzardFont();
        textComponent.fontSize = 55f; // Размер шрифта для "Нельзя создать"
        textComponent.color = Color.white;
        textComponent.alignment = TextAlignmentOptions.Center;
        
        RectTransform rectTransform = textObj.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(300, 50); // Увеличиваем ширину для лучшего размещения текста
    }
    
    private void CreateRuneElement(Transform parent, RunesEn runeType)
    {
        GameObject runeObj = new GameObject($"Rune_{runeType}");
        runeObj.transform.SetParent(parent, false);
        
        // Создаем контейнер для иконки и текста
        RectTransform containerRect = runeObj.AddComponent<RectTransform>();
        containerRect.sizeDelta = new Vector2(100, 100);
        
        // Создаем иконку руны
        GameObject iconObj = new GameObject("Icon");
        iconObj.transform.SetParent(runeObj.transform, false);
        
        Image iconImage = iconObj.AddComponent<Image>();
        Rune_SO runeData = currentDB.GetRune(runeType);
        if (runeData != null)
        {
            iconImage.sprite = runeData.runeIcon;
        }
        
        RectTransform iconRect = iconObj.GetComponent<RectTransform>();
        iconRect.anchorMin = new Vector2(0.5f, 0.5f);
        iconRect.anchorMax = new Vector2(0.5f, 0.5f);
        iconRect.sizeDelta = new Vector2(100, 100);
        iconRect.anchoredPosition = new Vector2(0, 0);
        
        // Создаем текст с названием руны
        GameObject textObj = new GameObject("Name");
        textObj.transform.SetParent(runeObj.transform, false);
        
        TextMeshProUGUI textComponent = textObj.AddComponent<TextMeshProUGUI>();
        textComponent.text = GetLocalizedRuneName(runeType);
        textComponent.font = GetBlizzardFont();
        textComponent.fontSize = 62.4f;
        textComponent.color = Color.white;
        textComponent.alignment = TextAlignmentOptions.Center;
        
        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0.5f, 0.5f);
        textRect.anchorMax = new Vector2(0.5f, 0.5f);
        textRect.sizeDelta = new Vector2(200, 50);
        textRect.anchoredPosition = new Vector2(0, -97.8f);
    }
    
    private void CreateGemElement(Transform parent, Gem_SO gem)
    {
        GameObject gemObj = new GameObject($"Gem_{gem?.name ?? "Unknown"}");
        gemObj.transform.SetParent(parent, false);
        
        // Создаем контейнер для иконки и текста
        RectTransform containerRect = gemObj.AddComponent<RectTransform>();
        containerRect.sizeDelta = new Vector2(100, 100);
        
        // Создаем иконку камня
        GameObject iconObj = new GameObject("Icon");
        iconObj.transform.SetParent(gemObj.transform, false);
        
        Image iconImage = iconObj.AddComponent<Image>();
        if (gem != null)
        {
            iconImage.sprite = gem.gemSprite;
        }
        
        RectTransform iconRect = iconObj.GetComponent<RectTransform>();
        iconRect.anchorMin = new Vector2(0.5f, 0.5f);
        iconRect.anchorMax = new Vector2(0.5f, 0.5f);
        iconRect.sizeDelta = new Vector2(100, 100);
        iconRect.anchoredPosition = new Vector2(0, 0);
        
        // Создаем текст с названием камня
        GameObject textObj = new GameObject("Name");
        textObj.transform.SetParent(gemObj.transform, false);
        
        TextMeshProUGUI textComponent = textObj.AddComponent<TextMeshProUGUI>();
        textComponent.text = GetGemDisplayName(gem);
        textComponent.font = GetBlizzardFont();
        textComponent.fontSize = 45f; // Размер шрифта для названий камней
        textComponent.enableAutoSizing = true; // Включаем AutoSize
        textComponent.fontSizeMin = 40f; // Минимальный размер шрифта
        textComponent.fontSizeMax = 72f; // Максимальный размер шрифта
        textComponent.color = Color.white;
        textComponent.alignment = TextAlignmentOptions.Center;
        
        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0.5f, 0.5f);
        textRect.anchorMax = new Vector2(0.5f, 0.5f);
        textRect.sizeDelta = new Vector2(365, 135); // Новые размеры для названий камней
        textRect.anchoredPosition = new Vector2(0, -97.8f);
    }
    
    private string GetGemDisplayName(Gem_SO gem)
    {
        if (gem == null) return GetLocalizedUnknownGemText();
        
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
    
    void OnDestroy()
    {
        // Отписываемся от событий
        AppManager.OnLanguageChanged -= OnLanguageChanged;
    }
} 