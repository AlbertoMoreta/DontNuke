using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Walk : MonoBehaviour {

    public float gravity = 5f;
    public float speed = 5f;
    public bool shouldMove = true;
    public bool shouldAnimate = true;
    private bool foundHole = false;
    private bool canJump = false;

    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidBody;
    public GameObject insideTheFall;

    void Start() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if(Input.GetKey(KeyCode.W)) {
            if (shouldMove) {
                transform.position += Vector3.up * speed * Time.deltaTime;
            }
                
            if (shouldAnimate) {
                animator.SetBool("Forward", false);
                animator.SetBool("Backwards", true);
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
                spriteRenderer.sortingLayerName = "XRay";
            }
        } else if(Input.GetKey(KeyCode.A)) {
            if (shouldMove) {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
                
            if (shouldAnimate) {
                animator.SetBool("Forward", false);
                animator.SetBool("Backwards", false);
                animator.SetBool("Left", true);
                animator.SetBool("Right", false);
                spriteRenderer.sortingLayerName = "Default";   
            }
        } else if(Input.GetKey(KeyCode.S)) {
            if (shouldMove) {
                transform.position += Vector3.down * speed * Time.deltaTime;
            }
                
            if (shouldAnimate) {
                animator.SetBool("Forward", true);
                animator.SetBool("Backwards", false);
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
                spriteRenderer.sortingLayerName = "Default";
            }
        } else if(Input.GetKey(KeyCode.D)) {
            if (shouldMove) {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
                
            if (shouldAnimate) {
                animator.SetBool("Forward", false);
                animator.SetBool("Backwards", false);
                animator.SetBool("Left", false);
                animator.SetBool("Right", true);
                spriteRenderer.sortingLayerName = "Default";
            }
        } else {
            animator.SetBool("Forward", false);
            animator.SetBool("Backwards", false);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        
        Debug.Log(col.name);
        if (col.name == "Hole") {
            Debug.Log("On trigger enter");
            StartCoroutine(FoundHole());
        } else if (col.name == "Food"){
            DialogManager.Instance.StartDialog("14-found-food");
        } else if (col.name == "Folder"){
            DialogManager.Instance.StartDialog("15-found-folder");
        } else if (col.name == "MonkeyHead") {
            Debug.Log("Monkey");
            System.Random rnd = new System.Random();
            int dialogNumber = rnd.Next(3);
            Debug.Log("Dialog Number: " + dialogNumber);
            switch(dialogNumber){
                case 0: DialogManager.Instance.StartDialog("16-found-monkey-1"); break;
                case 1: DialogManager.Instance.StartDialog("16-found-monkey-2"); break;
                case 2: DialogManager.Instance.StartDialog("16-found-monkey-3"); break;
            }
        }
    }

    IEnumerator FoundHole() {
        var light = GameObject.Find("Light");
        light.SetActive(false);
        var hole = GameObject.Find("Hole");
        hole.GetComponent<SpriteMask>().enabled = true;
        var holeCollider = hole.GetComponent<PolygonCollider2D>();
        shouldMove = false;
        shouldAnimate = false;
        DialogManager.Instance.StartDialog("12-found-hole"); 
        yield return new WaitWhile(() => DialogManager.Instance.IsDialogPlaying());
        foundHole = true;
        animator.SetBool("Jump", true);
    }

    void OnTriggerExit2D(Collider2D col) {
        Debug.Log("On trigger exit");
        if (col.name == "Hole") {
            canJump = true;
        }
    }

    IEnumerator EnterHole(){
        var holeFront = GameObject.Find("HoleFront");
        holeFront.GetComponent<SpriteRenderer>().enabled = true;
        rigidBody.gravityScale = gravity;
        insideTheFall.SetActive(true);
        yield return new WaitForSeconds(1f);
        holeFront.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(18.5f);
        insideTheFall.SetActive(false);
        DialogManager.Instance.StartDialog("13-fix-hole");
        yield return new WaitWhile(() => DialogManager.Instance.IsDialogPlaying());
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MenuGood");  
    }


}
