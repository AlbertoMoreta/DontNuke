using UnityEngine;

public class NextDialogLine : MonoBehaviour {

    void OnMouseDown(){
        Debug.Log("next line button clicked");
        var dialogManager = DialogManager.Instance;
        if (dialogManager.HasNextSubtitle()){
            dialogManager.Clear();
            dialogManager.DisplayNextSubtitle();
        } else {
            dialogManager.StopDialog();
        }
    }
}
