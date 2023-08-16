using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MessageBoxManager : MonoBehaviour {
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


    [DllImport("user32.dll")]
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
        }
    }
}
