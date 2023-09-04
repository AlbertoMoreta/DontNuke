using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;
using static MessageBoxManager;

public class FinalSceneManager : MonoBehaviour {

    public GameObject rambo;
    public GameObject bomba;
    public GameObject resplandorBomba;
    public GameObject presi;
    public Image cursor;
    private Sprite crosshair1;
    private Sprite crosshair2;
    public GameObject brokenScreen;

    void Start() {
        crosshair1 = Resources.Load<Sprite>("Cursor_Normal");
        crosshair2 = Resources.Load<Sprite>("Cursor_Shot");
    }
    

    void OnMouseDown(){
        StartCoroutine(StartAnimation());

    }

    void OnMouseEnter(){
        cursor.sprite = crosshair2;
    }

    void OnMouseExit() {
        cursor.sprite = crosshair1;

    }

    IEnumerator StartAnimation() {
        var presiAnimator = presi.GetComponent<Animator>();
        var ramboAnimator = rambo.GetComponent<Animator>();
        var bombaRenderer = bomba.GetComponent<SpriteRenderer>();
        var bombaAnimator = bomba.GetComponent<Animator>();
        var brokenScreenRenderer = brokenScreen.GetComponent<SpriteRenderer>();
        var brokenScreenAudioSource = brokenScreen.GetComponent<AudioSource>();
        var resplandorBombaRender = resplandorBomba.GetComponent<SpriteRenderer>();

        presiAnimator.enabled = true;
        ramboAnimator.enabled = true;

        yield return new WaitForSeconds(5);

        resplandorBombaRender.enabled = true;
        yield return new WaitForSeconds(0.2f);
        resplandorBombaRender.enabled = false;

        bombaRenderer.enabled = true;
        bombaAnimator.enabled = true;

        yield return new WaitForSeconds(3f);

        brokenScreenRenderer.enabled = true;
        brokenScreenAudioSource.Play();
    
    }

    void Finish() {
        uint error =  0x00000000;
        DisplayMessage("ERROR", new LocalizedString("UI", "disk_not_formated").GetLocalizedString(), error, MESSAGE_TYPE.ERROR);
        DisplayMessage("ERROR", new LocalizedString("UI", "thanks_for_playing").GetLocalizedString(), error, MESSAGE_TYPE.ERROR);
        Application.Quit();
    }
}
