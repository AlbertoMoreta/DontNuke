using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    private AvatarAnimationController animationController;

    void Start() {
        animationController = AvatarAnimationController.Instance;
    }

    void StopTalking(){
        Debug.Log("Stop Talking");
        animationController.StopTalking(animationController.GetAvatar(gameObject.name));
    }
}
