
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Terminal : MonoBehaviour {

    public TMP_InputField inputField;
    public TMP_Text content;
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
                case "0000": CorrectCode(); break;
                case "1990": FormatDisk(); break;
                default: NonExistentCode(); break;
            }
        } else {
            switch(command.ToLower()) {
                case "ayuda":
                case "help": PrintHelp(); break;
                case "arreglar_juego": 
                case "repair_game": RepairGame(); return; 
                case "mas ayuda": MoreHelp(); return; 
            }
            
            PrintComputerInfo();
        }
    }

    private void PrintHelp() {
        content.text += "\n";
        content.text += "Este es un mensaje de ayuda";
        AvatarAnimationController.Instance.Appear(AvatarAnimationController.Avatar.ALBERTO);
        DialogManager.Instance.StartDialog("1-alberto-appear");
    }

    private void RepairGame() {
        StartCoroutine(Wait(3));
        content.text += "\n";
        content.text += "Por favor, introduzca el código de verificación:";
        inputCode = true;
        DialogManager.Instance.StartDialog("2-input-wrong-code");
    }

    private void CorrectCode() {
        inputCode = false;
        // DialogManager.Instance.StopSubtitles();
        // DialogManager.Instance.StartDialog("2-input-wrong-code");
    }

    private void FormatDisk() {
        content.text += "\n";
        content.text += "===== INICIANDO FORMATEO DE DISCO =====\n";
        content.text += "Por favor, no toque nada hasta que el proceso termine.";
        inputCode = false;
        AlertTimer.Instance.Display();
        DialogManager.Instance.StartDialog("3-format-disk");
        AvatarAnimationController.Instance.Disappear(AvatarAnimationController.Avatar.ALBERTO);
        StartCoroutine(Wait(5));
        AvatarAnimationController.Instance.Appear(AvatarAnimationController.Avatar.ALBERTO);
        DialogManager.Instance.StartDialog("4-still-there");
    }

    private IEnumerator Wait(float seconds) {
        yield return new WaitForSeconds(5);
    }

    private void NonExistentCode() {
        content.text += "\n";
        content.text += "¡CÓDIGO INCORRECTO!\n";
        content.text += "Por favor, introduzca el código de verificación:";
    }

    private void MoreHelp() {
        AvatarAnimationController.Instance.Appear(AvatarAnimationController.Avatar.MARIO);
    }

}
