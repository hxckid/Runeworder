using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rune", menuName = "Scriptables/Rune", order = 1)]
public class Rune_SO : ScriptableObject
{
    [Header("Basic Info")]
    public RunesEn runeType;
    public Sprite runeIcon;
    
    [Header("Bonuses")]
    [TextArea(3, 5)]
    public string weaponBonus;
    [TextArea(3, 5)]
    public string armorBonus;
    [TextArea(3, 5)]
    public string helmetBonus;
    [TextArea(3, 5)]
    public string shieldBonus;
    
    [Header("Level")]
    public int runeLevel;
    
    [Header("Crafting Recipe")]
    public List<RunesEn> craftingRunes = new List<RunesEn>();
    public List<Gem_SO> craftingGems = new List<Gem_SO>();
    
    private void OnValidate()
    {
        // Автоматически устанавливаем спрайт руны при создании или изменении типа
        var runesSprites = FindObjectOfType<UIController>()?.runesSprites;
        if (runesSprites != null && runesSprites.sprites != null)
        {
            int runeIndex = (int)runeType;
            if (runeIndex < runesSprites.sprites.Count)
            {
                runeIcon = runesSprites.sprites[runeIndex];
            }
        }
        
        // Автоматически переименовываем файл при изменении типа руны
        string newName = runeType.ToString();
        
        // Переименовываем только если имя файла не совпадает с новым именем
        // и если это не "New Rune" (файл только что создан)
        if (name != newName)
        {
            name = newName;
            
            // Переименовываем файл через AssetDatabase
            #if UNITY_EDITOR
            string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this);
            string directory = System.IO.Path.GetDirectoryName(assetPath);
            string extension = System.IO.Path.GetExtension(assetPath);
            string newPath = System.IO.Path.Combine(directory, newName + extension);
            
            if (assetPath != newPath)
            {
                UnityEditor.AssetDatabase.RenameAsset(assetPath, newName);
                UnityEditor.AssetDatabase.SaveAssets();
            }
            #endif
        }
    }
} 