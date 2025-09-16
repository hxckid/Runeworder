using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RuneTooltipController : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject runeInfoPanel;
    public Image runeIcon;
    public TextMeshProUGUI runeName;
    public TextMeshProUGUI weaponsBonus;
    public TextMeshProUGUI armorsBonus;
    public TextMeshProUGUI helmsBonus;
    public TextMeshProUGUI shieldsBonus;
    public TextMeshProUGUI runeLevel;
    public TextMeshProUGUI runeRecipeLabel;
    public TextMeshProUGUI cantBeCrafted;
    public GameObject runeFirst;
    public GameObject runeSecond;
    public GameObject runeThird;
    public GameObject gem;
    public GameObject langImage;

    [Header("Databases")]
    public RunesDB_SO runesDBEng;
    public RunesDB_SO runesDBRus;

    private RunesDB_SO currentDB;
    private List<GameObject> runesInRecipe = new List<GameObject>();
    private Image runeFirstImage, runeSecondImage, runeThirdImage, gemImage;
    private TextMeshProUGUI runeFirstText, runeSecondText, runeThirdText, gemText;

    [Header("Language Flip Animation")]
    [SerializeField] private float langFlipDuration = 0.25f;
    [SerializeField] private AnimationCurve langFlipCurve = null;
    private Coroutine langFlipCoroutine;

    public enum RotationAxis { X, Y, Z }
    [Header("Axis Settings")]
    [SerializeField] private RotationAxis langFlipAxis = RotationAxis.Z;

    private void Start()
    {
        // Подписываемся на изменение языка
        AppManager.OnLanguageChanged += OnLanguageChanged;
        RuneController.OnLongTapRune += FillRuneInfo;
        currentDB = (AppManager.instance.currentLanguage == Languages.En) ? runesDBEng : runesDBRus;

        runesInRecipe.Add(runeFirst);
        runesInRecipe.Add(runeSecond);
        runesInRecipe.Add(runeThird);

        runeFirstImage = runeFirst.GetComponentInChildren<Image>(true);
        runeSecondImage = runeSecond.GetComponentInChildren<Image>(true);
        runeThirdImage = runeThird.GetComponentInChildren<Image>(true);
        gemImage = gem.GetComponentInChildren<Image>(true);

        runeFirstText = runeFirst.GetComponentInChildren<TextMeshProUGUI>(true);
        runeSecondText = runeSecond.GetComponentInChildren<TextMeshProUGUI>(true);
        runeThirdText = runeThird.GetComponentInChildren<TextMeshProUGUI>(true);
        gemText = gem.GetComponentInChildren<TextMeshProUGUI>(true);

        // Инициализируем кривую по умолчанию, если не задана в инспекторе
        if (langFlipCurve == null)
        {
            langFlipCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        }

        // Устанавливаем начальный поворот языка без анимации
        InitializeLangImageRotation();
    }

    void OnLanguageChanged(Languages language, string version)
    {
        currentDB = (language == Languages.En) ? runesDBEng : runesDBRus;
        RotateLangImage();
    }

    private void RotateLangImage()
    {
        if (langImage == null) return;
        RectTransform rt = langImage.transform as RectTransform;
        if (rt == null) return;

        float targetAngle = (AppManager.instance.currentLanguage == Languages.Ru) ? 180f : 0f;
        float currentAngle = GetAxisAngle(rt, langFlipAxis);

        if (langFlipCoroutine != null)
        {
            StopCoroutine(langFlipCoroutine);
        }
        langFlipCoroutine = StartCoroutine(AnimateLangFlip(rt, currentAngle, targetAngle, langFlipDuration, langFlipAxis));
    }

    private void InitializeLangImageRotation()
    {
        if (langImage == null) return;
        RectTransform rt = langImage.transform as RectTransform;
        if (rt == null) return;
        float targetAngle = (AppManager.instance.currentLanguage == Languages.Ru) ? 180f : 0f;
        SetAxisAngle(rt, langFlipAxis, targetAngle);
    }

    private IEnumerator AnimateLangFlip(RectTransform rectTransform, float startAngle, float endAngle, float duration, RotationAxis axis)
    {
        float elapsed = 0f;
        float delta = Mathf.DeltaAngle(startAngle, endAngle);
        float baseAngle = startAngle;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float curvedT = langFlipCurve.Evaluate(t);
            float angle = baseAngle + delta * curvedT;
            SetAxisAngle(rectTransform, axis, angle);
            yield return null;
        }
        SetAxisAngle(rectTransform, axis, endAngle);
        langFlipCoroutine = null;
    }

    private static float GetAxisAngle(RectTransform rectTransform, RotationAxis axis)
    {
        switch (axis)
        {
            case RotationAxis.X: return rectTransform.localEulerAngles.x;
            case RotationAxis.Y: return rectTransform.localEulerAngles.y;
            default: return rectTransform.localEulerAngles.z;
        }
    }

    private static void SetAxisAngle(RectTransform rectTransform, RotationAxis axis, float angle)
    {
        Vector3 eulers = rectTransform.localEulerAngles;
        switch (axis)
        {
            case RotationAxis.X: eulers.x = angle; break;
            case RotationAxis.Y: eulers.y = angle; break;
            default: eulers.z = angle; break;
        }
        rectTransform.localEulerAngles = eulers;
    }

    void FillRuneInfo(RunesEn runeType)
    {
        runeInfoPanel.SetActive(true);
        Rune_SO rune = currentDB.GetRune(runeType);

        // Заполняем UI данными руны
        if (runeIcon != null) runeIcon.sprite = rune.runeIcon;
        if (runeName != null) runeName.text = (AppManager.instance.currentLanguage == Languages.En) ? $"Rune {runeType}" : $"Руна {Enum.GetName(typeof(RunesRu), (int)runeType).ToString()}";
        if (weaponsBonus != null) weaponsBonus.text = (AppManager.instance.currentLanguage == Languages.En) ? $"Weapons: {rune.weaponBonus}" : $"Оружие: {rune.weaponBonus}";
        if (armorsBonus != null) armorsBonus.text = (AppManager.instance.currentLanguage == Languages.En) ? $"  Armor: {rune.armorBonus}" : $"Броня: {rune.armorBonus}";
        if (helmsBonus != null) helmsBonus.text = (AppManager.instance.currentLanguage == Languages.En) ? $"  Helms: {rune.helmetBonus}" : $"Шлемы: {rune.helmetBonus}";
        if (shieldsBonus != null) shieldsBonus.text = (AppManager.instance.currentLanguage == Languages.En) ? $"Shields: {rune.shieldBonus}" : $" Щиты: {rune.shieldBonus}";
        if (runeLevel != null) runeLevel.text = (AppManager.instance.currentLanguage == Languages.En) ? $"Required Level: {rune.runeLevel.ToString()}" : $"Требуемый уровень: {rune.runeLevel.ToString()}";
        if (runeRecipeLabel != null) runeRecipeLabel.text = (AppManager.instance.currentLanguage == Languages.En) ? $"Rune Recipe" : $"Рецепт Руны";

        if (rune.craftingRunes.Count < 2)
        {
            foreach (var runeInRecipe in runesInRecipe) 
            {
                runeInRecipe.SetActive(false);
            }
            gem.SetActive(false);
            cantBeCrafted.gameObject.SetActive(true);
            cantBeCrafted.text = (AppManager.instance.currentLanguage == Languages.En) ? $"Cannot Be Crafted" : $"Руну Нельзя Создать";
        }
        else
        {
            foreach (var runeInRecipe in runesInRecipe)
            {
                runeInRecipe.SetActive(true);
            }
            cantBeCrafted.gameObject.SetActive(false);
            runeThird.SetActive(rune.craftingRunes.Count > 2);
            gem.SetActive(rune.craftingGems.Count > 0);

            // Runes (first two required)
            runeFirstImage.sprite = currentDB.GetRune(rune.craftingRunes[0]).runeIcon;
            runeSecondImage.sprite = currentDB.GetRune(rune.craftingRunes[1]).runeIcon;

            runeFirstText.text = (AppManager.instance.currentLanguage == Languages.En) ? $"Rune {rune.craftingRunes[0]}" : $"Руна {Enum.GetName(typeof(RunesRu), (int)rune.craftingRunes[0])}";
            runeSecondText.text = (AppManager.instance.currentLanguage == Languages.En) ? $"Rune {rune.craftingRunes[1]}" : $"Руна {Enum.GetName(typeof(RunesRu), (int)rune.craftingRunes[1])}";

            // Optional 3rd rune
            if (rune.craftingRunes.Count == 3)
            {
                runeThirdImage.sprite = currentDB.GetRune(rune.craftingRunes[2]).runeIcon;
                runeThirdText.text = (AppManager.instance.currentLanguage == Languages.En) ? $"Rune {rune.craftingRunes[2]}" : $"Руна {Enum.GetName(typeof(RunesRu), (int)rune.craftingRunes[2])}";
            }

            // Optional gem (0 or 1)
            if (rune.craftingGems.Count == 1)
            {
                var gemForCraft = rune.craftingGems[0]; 
                gemImage.sprite = gemForCraft.gemSprite;
                gemText.text = GetGemLocalized(gemForCraft);
            }
        }
    }

    string GetGemLocalized(Gem_SO gem_SO)
    {
        string gemType, gemQuality;

        if (AppManager.instance.currentLanguage == Languages.Ru)
        {
            gemQuality = gem_SO.gemQuality switch
            {
                GemQuality.Chipped => "Надколотый",
                GemQuality.Flawed => "Мутный",
                GemQuality.Normal => "",
                GemQuality.Flawless => "Безупречный",
                GemQuality.Perfect => "Идеальный",
                _ => gem_SO.gemQuality.ToString()
            };

            gemType = gem_SO.gemType switch
            {
                GemType.Amethyst => "Аметист",
                GemType.Diamond => "Бриллиант",
                GemType.Emerald => "Изумруд",
                GemType.Ruby => "Рубин",
                GemType.Sapphire => "Сапфир",
                GemType.Topaz => "Топаз",
                GemType.Skull => "Череп",
                _ => gem_SO.gemType.ToString()
            };

            return gem_SO.gemQuality == GemQuality.Normal ? gemType : $"{gemQuality} {gemType}";
        }
        else
            return gem_SO.gemQuality == GemQuality.Normal ? gem_SO.gemType.ToString() : $"{gem_SO.gemQuality} {gem_SO.gemType}";
    }

    private void OnDestroy()
    {
        AppManager.OnLanguageChanged -= OnLanguageChanged;
        RuneController.OnLongTapRune -= FillRuneInfo;
    }
}
