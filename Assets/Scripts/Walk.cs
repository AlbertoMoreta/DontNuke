using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour {
    
    Animator animator;
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.W)) {
            animator.SetBool("Forward", false);
            animator.SetBool("Backwards", true);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
        } else if(Input.GetKeyDown(KeyCode.A)) {
            animator.SetBool("Forward", false);
            animator.SetBool("Backwards", false);
            animator.SetBool("Left", true);
            animator.SetBool("Right", false);
        } else if(Input.GetKeyDown(KeyCode.S)) {
            animator.SetBool("Forward", true);
            animator.SetBool("Backwards", false);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            
        } else if(Input.GetKeyDown(KeyCode.D)) {
            animator.SetBool("Forward", false);
            animator.SetBool("Backwards", false);
            animator.SetBool("Left", false);
            animator.SetBool("Right", true);
        } else {
            animator.SetBool("Forward", false);
            animator.SetBool("Backwards", false);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
        }
    }
}
