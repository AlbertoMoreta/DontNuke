using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("On trigger enter");
        StartCoroutine(FoundHole());
    }

    IEnumerator FoundHole() {
        var light = GameObject.Find("Light");
        light.SetActive(false);
        var hole = GameObject.Find("Hole");
        hole.GetComponent<SpriteMask>().enabled = true;
        var holeCollider = hole.GetComponent<PolygonCollider2D>();
        shouldMove = false;
        DialogManager.Instance.StartDialog("12-found-hole"); 
        yield return new WaitWhile(() => DialogManager.Instance.IsDialogPlaying());
        foundHole = true;
        animator.SetBool("Jump", true);
    }

    void OnTriggerExit2D(Collider2D col) {
        Debug.Log("On trigger exit");
        canJump = true;
    }

    IEnumerator EnterHole(){
        var holeFront = GameObject.Find("HoleFront");
        holeFront.GetComponent<SpriteRenderer>().enabled = true;
        rigidBody.gravityScale = gravity;
        yield return new WaitForSeconds(5);
        DialogManager.Instance.StartDialog("13-fix-hole");
    }


}
