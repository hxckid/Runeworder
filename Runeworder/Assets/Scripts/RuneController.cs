using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RuneController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image background;
    public Image checkmark;
    public Text runeName;
    public RunesEn rune;

    public delegate void RuneHandler(RunesEn rune, bool isOn);
    public static event RuneHandler OnRuneToggleChanged;

    Toggle toggle;
    private float longPressTime = 0.5f;
    private float pressStartTime;
    private bool isLongPress = false;
    private bool isPressed = false;
    private bool toggleStateBeforePress = false;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged();
        });
    }

    void Update()
    {
        if (isPressed && !isLongPress)
        {
            if (Time.time - pressStartTime >= longPressTime)
            {
                isLongPress = true;
                OnLongPress();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressStartTime = Time.time;
        isPressed = true;
        isLongPress = false;
        toggleStateBeforePress = toggle.isOn;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        if (isLongPress)
        {
            // Если был длинный тап, отменяем клик
            eventData.eligibleForClick = false;
        }
        isLongPress = false;
    }

    void ToggleValueChanged()
    {
        // Не срабатываем если это был длинный тап
        if (!isLongPress)
        {
            OnRuneToggleChanged?.Invoke(rune, toggle.isOn);
        }
    }

    void OnLongPress()
    {
        // Метод для длинного тапа - показываем информацию о руне
        var runeInfoPanel = FindObjectOfType<RuneInfoPanel>();
        if (runeInfoPanel != null)
        {
            runeInfoPanel.ShowRuneInfo(rune);
        }
    }
}
