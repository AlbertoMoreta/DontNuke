using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class MessageBoxManager : MonoBehaviour {

    IEnumerator SetLocale(int _localeID) {
        yield return LocalizationSettings.InitializationOperation;
    }

    const uint MB_ICONERROR = 0x00000010;
    const uint MB_CANCELTRYCONTINUE = 0x00000006;
    const uint MB_OK = 0x00000000;
    const uint MB_YESNO = 0x00000004;
    const uint MB_RETRYCANCEL = 0x00000005;
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

    public static void DisplayMessage(string header, string message, uint flags, MESSAGE_TYPE type = MESSAGE_TYPE.DEFAULT) {

        uint messageType = 0;
        switch(type) {
            case MESSAGE_TYPE.ERROR: {
                messageType = MB_ICONERROR;
                break;
            }
        }

        int msgboxID = MessageBox(new IntPtr(0), message, header, messageType | flags);

        switch (msgboxID) {
            case CANCEL: break;
            case TRYAGAIN: break;
            case CONTINUE: break;
            default: break;
        }

        return;
    }


    void Start() {
        var error = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("UI", "error").Result;
        var alert_message_1 = new LocalizedString("UI", "alert_message_1").GetLocalizedString();
        var alert_message_2 = new LocalizedString("UI", "alert_message_2").GetLocalizedString();
        var alert_message_3 = new LocalizedString("UI", "alert_message_3").GetLocalizedString();
        var alert_message_4 = new LocalizedString("UI", "alert_message_4").GetLocalizedString();
        var alert_message_5 = new LocalizedString("UI", "alert_message_5").GetLocalizedString();

        DisplayMessage(error, alert_message_1, MB_CANCELTRYCONTINUE, MESSAGE_TYPE.ERROR);
        DisplayMessage(error, alert_message_2, MB_OK, MESSAGE_TYPE.ERROR);
        DisplayMessage(error, alert_message_3, MB_OK, MESSAGE_TYPE.ERROR);
        DisplayMessage(error, alert_message_4, MB_YESNO, MESSAGE_TYPE.ERROR);
        DisplayMessage(error, alert_message_5, MB_RETRYCANCEL, MESSAGE_TYPE.ERROR);

    }
}
