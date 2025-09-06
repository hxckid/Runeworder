using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SideMenuController : MonoBehaviour, IPointerClickHandler
{
    [Header("Menu Settings")]
    [SerializeField] private RectTransform sideMenuPanel;
    [SerializeField] private Button burgerButton;
    [SerializeField] private Button[] menuButtons;
    
    [Header("Animation Settings")]
    [SerializeField] private float animationDuration = 0.3f;
    [SerializeField] private AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    [Header("Menu Position")]
    [SerializeField] private float hiddenXPosition = -300f;
    [SerializeField] private float shownXPosition = 0f;
    
    private bool isMenuOpen = false;
    private bool isAnimating = false;
    private Coroutine currentAnimation;
    
    private void Start()
    {
        // Инициализация позиции меню (скрыто)
        if (sideMenuPanel != null)
        {
            Vector3 hiddenPosition = sideMenuPanel.anchoredPosition;
            hiddenPosition.x = hiddenXPosition;
            sideMenuPanel.anchoredPosition = hiddenPosition;
        }
        
        // Подписка на кнопку бургера
        if (burgerButton != null)
        {
            burgerButton.onClick.AddListener(ToggleMenu);
        }
        
        // Подписка на кнопки меню
        foreach (Button button in menuButtons)
        {
            if (button != null)
            {
                button.onClick.AddListener(CloseMenu);
            }
        }
    }
    
    private void OnDestroy()
    {
        // Отписка от событий
        if (burgerButton != null)
        {
            burgerButton.onClick.RemoveListener(ToggleMenu);
        }
        
        foreach (Button button in menuButtons)
        {
            if (button != null)
            {
                button.onClick.RemoveListener(CloseMenu);
            }
        }
    }
    
    public void ToggleMenu()
    {
        if (isAnimating) return;
        
        if (isMenuOpen)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }
    
    public void OpenMenu()
    {
        if (isAnimating || isMenuOpen) return;
        
        isMenuOpen = true;
        StartAnimation(shownXPosition);
    }
    
    public void CloseMenu()
    {
        if (isAnimating || !isMenuOpen) return;
        
        isMenuOpen = false;
        StartAnimation(hiddenXPosition);
    }
    
    private void StartAnimation(float targetX)
    {
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        
        currentAnimation = StartCoroutine(AnimateMenu(targetX));
    }
    
    private IEnumerator AnimateMenu(float targetX)
    {
        isAnimating = true;
        
        Vector2 startPosition = sideMenuPanel.anchoredPosition;
        Vector2 targetPosition = new Vector2(targetX, startPosition.y);
        
        float elapsedTime = 0f;
        
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / animationDuration;
            float curveValue = animationCurve.Evaluate(progress);
            
            sideMenuPanel.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, curveValue);
            
            yield return null;
        }
        
        sideMenuPanel.anchoredPosition = targetPosition;
        isAnimating = false;
        currentAnimation = null;
    }
    
    // Обработка клика по свободной области для закрытия меню
    public void OnPointerClick(PointerEventData eventData)
    {
        // Проверяем, что клик был не по самому меню и не по кнопке бургера
        if (isMenuOpen && !isAnimating)
        {
            if (eventData.pointerCurrentRaycast.gameObject == gameObject)
            {
                CloseMenu();
            }
        }
    }
    
    // Метод для проверки, открыто ли меню (может быть полезен для других скриптов)
    public bool IsMenuOpen()
    {
        return isMenuOpen;
    }
    
    // Метод для принудительного закрытия меню (например, при смене экрана)
    public void ForceCloseMenu()
    {
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
            currentAnimation = null;
        }
        
        isMenuOpen = false;
        isAnimating = false;
        
        Vector2 hiddenPosition = sideMenuPanel.anchoredPosition;
        hiddenPosition.x = hiddenXPosition;
        sideMenuPanel.anchoredPosition = hiddenPosition;
    }
}
