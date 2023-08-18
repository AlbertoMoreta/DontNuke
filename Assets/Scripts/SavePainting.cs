using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SavePainting : MonoBehaviour {

    private bool isSaved = false;
    
    public static SavePainting Instance {
        get; private set;
    }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public bool IsSaved() {
        return isSaved;
    }


    public void Save() {
        var painting = Resources.Load<Sprite>("Paint");
        Debug.Log("Painting: " + painting);
        String directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar + "test";
        String filePath = directoryPath + Path.DirectorySeparatorChar + "verification_image.jpg";
        // Create directory on the desktop
        Directory.CreateDirectory(directoryPath);
        byte[] spriteData = painting.texture.EncodeToJPG();
        File.WriteAllBytes(filePath, spriteData);
        isSaved = true;
    }
}
