using UnityEngine;
using UnityEditor;
using System.IO;

public class ResizeTexturesToMultipleOf4 : EditorWindow
{
    [MenuItem("Tools/Resize Textures to Multiple of 4")]
    public static void ShowWindow()
    {
        GetWindow<ResizeTexturesToMultipleOf4>("Resize Textures");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Resize All Textures to Multiple of 4"))
        {
            ResizeAllTextures();
        }
    }

    private static void ResizeAllTextures()
    {
        string[] textureGuids = AssetDatabase.FindAssets("t:Texture2D");
        foreach (string guid in textureGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

            if (texture != null)
            {
                int newWidth = Mathf.CeilToInt(texture.width / 4f) * 4;
                int newHeight = Mathf.CeilToInt(texture.height / 4f) * 4;

                if (newWidth != texture.width || newHeight != texture.height)
                {
                    Debug.Log($"Resizing {path} from {texture.width}x{texture.height} to {newWidth}x{newHeight}");

                    Texture2D resizedTexture = ResizeTexture(texture, newWidth, newHeight);
                    SaveResizedTexture(resizedTexture, path);
                }
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("All textures resized to multiples of 4!");
    }

    private static Texture2D ResizeTexture(Texture2D original, int width, int height)
    {
        RenderTexture rt = RenderTexture.GetTemporary(width, height);
        RenderTexture.active = rt;

        Graphics.Blit(original, rt);

        Texture2D resized = new Texture2D(width, height, original.format, false);
        resized.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        resized.Apply();

        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);

        return resized;
    }

    private static void SaveResizedTexture(Texture2D texture, string path)
    {
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(path, bytes);
        AssetDatabase.ImportAsset(path);
    }
}