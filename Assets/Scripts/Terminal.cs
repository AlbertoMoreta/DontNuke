
using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Terminal : MonoBehaviour {

    public TMP_InputField inputField;
    public TMP_Text content;
    public GameObject paint;
    public GameObject albertoFlashlight;
    private bool inputCode = false;

    // Start is called before the first frame update
    void Start() {
        PrintComputerInfo();
        inputField.onSubmit.AddListener(command => {
            SendCommand(command);
        });
        
    }

    private void PrintComputerInfo() {
        string username = Environment.UserName;
        string deviceName = SystemInfo.deviceName;
        content.text += "\n\n";
        content.text += "<color=#00FF00>" + username + "@" + deviceName + "</color>";
    }

    private void SendCommand(string command) {
        inputField.text = "";
        content.text += "\n$ " + command;
        ProcessCommand(command);
        inputField.ActivateInputField();
    }
 
    private void ProcessCommand(string command) {
        if(inputCode){
            switch(command.ToLower()) {
                case "0000": StartCoroutine(CorrectCode()); PrintComputerInfo(); break;
                case "1990": StartCoroutine(FormatDisk()); break;
                case "1234": StartCoroutine(NotSexyEnough()); break;
                default: NonExistentCode(); break;
            }
        } else {
            switch(command.ToLower()) {
                case "ayuda":
                case "help": PrintHelp(); break;
                case "arreglar_juego": 
                case "repair_game": RepairGame(); return; 
                case "mas_ayuda":
                case "more_help": StartCoroutine(MoreHelp()); break; 
                case "paint": StartCoroutine(OpenPaint()); break;
            }
            
            PrintComputerInfo();
        }
    }

    private void PrintHelp() {
        content.text += "\n";
        content.text += "==== Invocando al programador ====";
        AvatarAnimationController.Instance.Appear(AvatarAnimationController.Avatar.ALBERTO);
        DialogManager.Instance.StartDialog("1-alberto-appear");
    }

    private void RepairGame() {
        content.text += "\n";
        content.text += "Por favor, introduzca el código de reparación:";
        inputCode = true;
        DialogManager.Instance.StartDialog("2-input-wrong-code");
    }

    private IEnumerator OpenPaint(){
        paint.SetActive(true);
        DialogManager.Instance.StartDialog("8-paint");
        yield return new WaitUntil(() => SavePainting.Instance.IsSaved());
        paint.SetActive(false);
        DialogManager.Instance.StartDialog("9-email");
        content.text += "\nIntroduzca código de confirmación:";
        inputCode = true;
    }

    private IEnumerator CorrectCode() {
        content.text += "\n";
        content.text += "===== CLAVE MAESTRA CORRECTA =====\n";
        yield return new WaitForSeconds(3);
        content.text += "\n";
        content.text += "FileNotFoundException: Could not find file \"Assets/Textures/MrPresident.png\".";
        content.text += "UnityEngine.GUI.DrawTexture (Rect position, Texture image, ScaleMode scaleMode, Boolean alphaBlend, Single imageAspect, Color color, Single borderWidth, Single borderRadius) (at <c95232c9c3b24b6592841f15a5ca524e>:0)";
        content.text += "DancingPresident.Start () (at Assets/Scripts/DancingPresident.cs:15)\n";
        content.text += "Completa la imagen para continuar.";
        yield return new WaitForSeconds(2);
        DialogManager.Instance.StartDialog("7-captcha");
        inputCode = false;
    }

    private IEnumerator FormatDisk() {
        content.text += "\n";
        content.text += "===== INICIANDO FORMATEO DE DISCO =====\n";
        content.text += "Por favor, no toque nada hasta que el proceso termine.";
        inputCode = false;
        AlertTimer.Instance.Display();
        DialogManager.Instance.StartDialog("3-format-disk");
        yield return new WaitWhile(() => DialogManager.Instance.IsDialogPlaying());
        Debug.Log("Dialog finished playing");
        AvatarAnimationController.Instance.Disappear(AvatarAnimationController.Avatar.ALBERTO);
        yield return new WaitForSeconds(5f);
        AvatarAnimationController.Instance.Appear(AvatarAnimationController.Avatar.ALBERTO);
        yield return new WaitForSeconds(1f);
        DialogManager.Instance.StartDialog("4-still-there");
    }

    private void NonExistentCode() {
        content.text += "\n";
        content.text += "¡CÓDIGO INCORRECTO!\n";
        content.text += "Por favor, pruebe de nuevo:";
    }

    private IEnumerator MoreHelp() {
        AvatarAnimationController.Instance.Appear(AvatarAnimationController.Avatar.MARIO);
        DialogManager.Instance.StartDialog("5-mario-appears");
        content.text += "\n";
        content.text += "==== Invocando al diseñador ====\n";
        content.text += "La memoria RAM se ha reducido a la mitad.\n";
        yield return new WaitWhile(() => DialogManager.Instance.IsDialogPlaying());
        AlertTimer.Instance.ReduceHalf();
        DialogManager.Instance.StartDialog("6-alert-time");
        content.text += "\nIntroduce clave maestra:";
        SaveCodeImage();
        inputCode = true;
    }

    private IEnumerator NotSexyEnough() {
        content.text += "\n";
        content.text += "====== ERROR ======\n";
        content.text += "El presidente no es lo suficientemente sexy.\n";
        AlertTimer.Instance.ReduceHalf();
        yield return new WaitForSeconds(2);
        DialogManager.Instance.StartDialog("10-alberto-mad");
        AvatarAnimationController.Instance.Disappear(AvatarAnimationController.Avatar.ALBERTO);
        yield return new WaitForSeconds(1);
        albertoFlashlight.SetActive(true);
        DialogManager.Instance.StartDialog("11-find-hole");
        yield return new WaitWhile(() => DialogManager.Instance.IsDialogPlaying());
    }

    private void SaveCodeImage() {
        var texture = Resources.Load<Texture2D>("DontNuke_Code");
        Texture2D decopmpresseTex = DeCompress(texture);
        Debug.Log("painting: " + decopmpresseTex);
        String filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar + "DontNuke_Code.png";
        Debug.Log("FILEPATH: " + filePath);
        byte[] spriteData = decopmpresseTex.EncodeToPNG();
        File.WriteAllBytes(filePath, spriteData);
        Debug.Log("Saved");
    }

    public static Texture2D DeCompress(Texture2D source) {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }

}
