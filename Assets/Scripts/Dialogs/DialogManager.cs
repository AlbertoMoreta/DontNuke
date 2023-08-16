using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public enum DIALOG_BOX_POSITIONS {
    LEFT = -1,
    RIGHT = 1
}

public class DialogManager : MonoBehaviour {
    
    public GameObject dialogBox;
    public float dialogBoxOffset = 1f;
    public float dialogBoxHeight = 1f;

    private TextMeshProUGUI _subsTextBox;
    
    private Transform _originalParent;

    private DialogCollection _dialogCollection;
    private AudioSource audioSource;
    private GameObject _background;
    private Dialog _currentDialog;
    private Coroutine _subtitlesCoroutine;
    private float typeDelay = 0.05f;

    private List<Subtitle> currentDialog;
    private int currentDialogLine = 0;

    public static DialogManager Instance {
        get; private set;
    }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        _subsTextBox = GameObject.Find("Subs").GetComponent<TextMeshProUGUI>();
        _background = GameObject.Find("Background");
        _originalParent = dialogBox.transform.parent;
        dialogBox.SetActive(false);

        // Load dialogs from Resources/dialogs.json
        var dialogsFile = Resources.Load("dialogs") as TextAsset;
        Debug.Log("dialogsFile: " + dialogsFile);
        if (dialogsFile != null) {
            _dialogCollection = JsonUtility.FromJson<DialogCollection>(dialogsFile.text);
        } else {
            Debug.LogWarning(dialogsFile + " file does not exists.");
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void Clear() {
        _subsTextBox.text = "";
    }

    // Play dialog: DialogManager.Instance.SendMessage("StartDialog", "dialog_key");
    public void StartDialog(string dialogKey) {
        if(!audioSource.isPlaying && !dialogBox.activeSelf){
            _currentDialog = _dialogCollection.dialogLines.First(dialog => dialog.key == dialogKey);
            // PlayAudio(_currentDialog.audioPath);
            _subtitlesCoroutine = StartCoroutine(DisplaySubtitles(_currentDialog.subtitles));
        }
    }

    public bool HasNextSubtitle() {
        return currentDialogLine <= (currentDialog.Count - 1);
    }

    private void PlayAudio(string audioName) {
        var audioPath = "Sounds/Dialogs/" + audioName;
        var audioClip = Resources.Load<AudioClip>(audioPath);
        audioSource.PlayOneShot(audioClip);
    }

    private IEnumerator DisplaySubtitles(List<Subtitle> subtitles) {
        currentDialog = subtitles;
        currentDialogLine = 0;

        dialogBox.SetActive(true);

        StartCoroutine(DisplayNextSubtitle());

        yield return null;

    }

    public IEnumerator DisplayNextSubtitle() {
        var sub = currentDialog[currentDialogLine];
        Debug.Log("DISPLAYING NEXT SUBTITLE LINE:" + sub);
        // var character = GameObject.Find(sub.characterName);
        // dialogBox.transform.parent = character.transform.parent;
        // float xOffset = 0;
        // Debug.Log("Position: " + sub.position);
        // switch(sub.position) {
        //     case DIALOG_BOX_POSITIONS.RIGHT: {
        //         xOffset = -dialogBoxOffset;
        //         _background.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        //         break;
        //     }
        //     case DIALOG_BOX_POSITIONS.LEFT:{
        //         xOffset = dialogBoxOffset;
        //         _background.transform.localRotation *= Quaternion.AngleAxis(180, Vector3.forward);
        //         break;
        //     }
        // }
        // dialogBox.transform.position = character.transform.position + new Vector3(xOffset, dialogBoxHeight, 0);
        foreach (char c in sub.text) {
            _subsTextBox.text += c;
            yield return new WaitForSeconds(typeDelay);
        }

        currentDialogLine++;
    }

    public void StopSubtitles(){
        Debug.Log("Stopping subtitles");
        if(_subsTextBox != null) { _subsTextBox.text = ""; }
        if(_background != null) {
            _background.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }   
        dialogBox.transform.parent = _originalParent;
        if(dialogBox != null){
            dialogBox.SetActive(false);
        }
    }

    public void StopDialog() {
        if(dialogBox != null && dialogBox.activeSelf) {
            if(audioSource != null && audioSource.isPlaying){
                audioSource.Stop();
            }
            if(_currentDialog != null){
                StopCoroutine(_subtitlesCoroutine);
                _currentDialog = null;
            }
            StopSubtitles();
        }
        
    }
}

