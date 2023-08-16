using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class MessageBoxManager : MonoBehaviour {

    IEnumerator SetLocale(int _localeID) {
        yield return LocalizationSettings.InitializationOperation;
    }

    const uint MB_ICONERROR = 0x00000010;
    const int OK = 1;
    const int CANCEL = 2;
    const int ABORT = 3;
    const int RETRY = 4;
    const int IGNORE = 5;
    const int YES = 6;
    const int NO = 7;
    const int TRYAGAIN = 10;
    const int CONTINUE = 11;

    public enum MESSAGE_TYPE {
        DEFAULT,
        ERROR,
    }


    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    public static void DisplayMessage(string header, string message, MESSAGE_TYPE type = MESSAGE_TYPE.DEFAULT) {

        uint messageType = 0;
        switch(type) {
            case MESSAGE_TYPE.ERROR: {
                messageType = MB_ICONERROR;
                break;
            }
        }

        int msgboxID = MessageBox(new IntPtr(0), message, header, messageType);

        switch (msgboxID) {
            case CANCEL: break;
            case TRYAGAIN: break;
            case CONTINUE: break;
            default: break;
        }

        return;
    }


    void Start() {
        var error = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("UI", "ERROR").Result;

        DisplayMessage(error, "Mira que te hemos dicho que no pulses el botón.", MessageBoxManager.MESSAGE_TYPE.ERROR);

    }
}
