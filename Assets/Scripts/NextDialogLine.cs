using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextDialogLine : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) {
        var dialogManager = DialogManager.Instance;
        if (dialogManager.HasNextSubtitle()){
            dialogManager.Clear();
            StartCoroutine(dialogManager.DisplayNextSubtitle());
        } else {
            dialogManager.StopSubtitles();
        }
        
    }
}
