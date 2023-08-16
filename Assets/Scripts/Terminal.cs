
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Terminal : MonoBehaviour {

    public TMP_InputField inputField;
    public TMP_Text content;

    // Start is called before the first frame update
    void Start() {
        PrintComputerInfo();
        inputField.onSubmit.AddListener(command => {
            SendCommand(command);
        });
        
    }

    private void SendCommand(string command) {
        inputField.text = "";
        content.text += "\n$ " + command;
        ProcessCommand(command);
        PrintComputerInfo();
        inputField.ActivateInputField();
    }
 
    private void ProcessCommand(string command) {
        switch(command.ToLower()) {
            case "help": PrintHelp(); break;
        }
    }

    private void PrintHelp() {
        content.text += "\n";
        content.text += "Este es un mensaje de ayuda";
    }

    private void PrintComputerInfo() {
        string username = Environment.UserName;
        string deviceName = SystemInfo.deviceName;
        content.text += "\n\n";
        content.text += "<color=#00FF00>" + username + "@" + deviceName + "</color>";
    }

}
