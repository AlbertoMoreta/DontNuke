using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.UI;

public class FinalSceneManager : MonoBehaviour {

    public GameObject rambo;
    public GameObject bomba;
    public GameObject resplandorBomba;
    public GameObject presi;
    public Image cursor;
    private Sprite crosshair1;
    private Sprite crosshair2;
    public SpriteRenderer brokenScreen;

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

        brokenScreen.enabled = true;
        yield return new WaitForSeconds(1f);
        MessageBoxManager.DisplayMessage("ERROR", "El disco duro no se ha podido formatear", 0x00000000);
        Application.Quit();
    }
}
