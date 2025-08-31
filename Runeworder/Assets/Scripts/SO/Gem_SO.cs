using System;
using System.Collections.Generic;
using UnityEngine;

public enum GemType
{
    Amethyst,
    Diamond,
    Emerald,
    Ruby,
    Sapphire,
    Topaz,
    Skull
}

public enum GemQuality
{
    Chipped,
    Flawed,
    Normal,
    Flawless,
    Perfect
}

[CreateAssetMenu(fileName = "New Gem", menuName = "Scriptables/Gem", order = 3)]
public class Gem_SO : ScriptableObject
{
    [Header("Gem Info")]
    public GemType gemType;
    public GemQuality gemQuality;
    public Sprite gemSprite;
    
    private void OnValidate()
    {
        // Автоматически переименовываем файл при изменении типа или качества
        string newName = $"{gemQuality}_{gemType}";
        
        // Переименовываем только если имя файла не совпадает с новым именем
        // и если это не "New Gem" (файл только что создан)
        if (name != newName && name != "New Gem 1")
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