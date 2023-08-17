using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour {

    public float speed = 5f;
    public bool shouldMove = true;
    
    Animator animator;
    SpriteRenderer spriteRenderer;

    void Start() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if(Input.GetKey(KeyCode.W)) {
            if (shouldMove) {
                transform.position += Vector3.up * speed * Time.deltaTime;
            }
            animator.SetBool("Forward", false);
            animator.SetBool("Backwards", true);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            spriteRenderer.sortingLayerName = "XRay";
        } else if(Input.GetKey(KeyCode.A)) {
            if (shouldMove) {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            animator.SetBool("Forward", false);
            animator.SetBool("Backwards", false);
            animator.SetBool("Left", true);
            animator.SetBool("Right", false);
            spriteRenderer.sortingLayerName = "Default";
        } else if(Input.GetKey(KeyCode.S)) {
            if (shouldMove) {
                transform.position += Vector3.down * speed * Time.deltaTime;
            }
            animator.SetBool("Forward", true);
            animator.SetBool("Backwards", false);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            spriteRenderer.sortingLayerName = "Default";
        } else if(Input.GetKey(KeyCode.D)) {
            if (shouldMove) {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            animator.SetBool("Forward", false);
            animator.SetBool("Backwards", false);
            animator.SetBool("Left", false);
            animator.SetBool("Right", true);
            spriteRenderer.sortingLayerName = "Default";
        } else {
            animator.SetBool("Forward", false);
            animator.SetBool("Backwards", false);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
        }
    }
}
