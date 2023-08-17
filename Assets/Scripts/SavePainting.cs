using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SavePainting : MonoBehaviour {
    // Start is called before the first frame update
    public void Save() {
        var painting = Resources.Load<Sprite>("Paint");
        Debug.Log("Painting: " + painting);
        String directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar + "test";
        String filePath = directoryPath + Path.DirectorySeparatorChar + "yourbeautifuldrawing.jpg";
        // Create directory on the desktop
        Directory.CreateDirectory(directoryPath);
        byte[] spriteData = painting.texture.EncodeToJPG();
        File.WriteAllBytes(filePath, spriteData);
    }
}
