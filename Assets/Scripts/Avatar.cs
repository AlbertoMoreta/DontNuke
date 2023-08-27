using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    private AvatarAnimationController animationController;
    private AudioSource audioSource;

    void Start() {
        animationController = AvatarAnimationController.Instance;
        audioSource = GetComponent<AudioSource>();
    }

    void StopTalking(){
        Debug.Log("Stop Talking");
        animationController.StopTalking(animationController.GetAvatar(gameObject.name));
    }

    void PlayAppearingSound() {
        audioSource.Play();
    }

    void OnMouseDown(){
        Debug.Log("repeat line button clicked");
        DialogManager.Instance.DisplayCurrentSubtitle();
        
    }


}
