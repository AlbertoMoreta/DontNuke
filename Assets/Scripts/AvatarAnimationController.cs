using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarAnimationController : MonoBehaviour {

    public static AvatarAnimationController Instance {
        get; private set;
    }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public enum Avatar {
        ALBERTO,
        MARIO
    }

    public Animator alberto;
    public Animator mario;

    public Avatar GetAvatar(string name) {
        return name.ToLower() switch {
            "alberto" => Avatar.ALBERTO,
            "mario" => Avatar.MARIO,
            _ => throw new System.NotImplementedException(),
        };
    }

    private Animator GetAnimator(Avatar avatar) {
        return avatar switch {
            Avatar.ALBERTO => alberto,
            Avatar.MARIO => mario,
            _ => null,
        };
    }

    public void Appear(Avatar avatar) {
        GetAnimator(avatar).SetBool("isVisible", true);
    }

    public void Disappear(Avatar avatar) {
        GetAnimator(avatar).SetBool("isVisible", false); 
    }

    public void StartTalking(Avatar avatar) {
        GetAnimator(avatar).SetBool("isTalking", true);
    }

    public void StopTalking(Avatar avatar) {
        GetAnimator(avatar).SetBool("isTalking", false);
    }
}
