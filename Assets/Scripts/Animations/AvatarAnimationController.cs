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

    public GameObject alberto;
    public GameObject mario;

    public Avatar GetAvatar(string name) {
        return name.ToLower() switch {
            "alberto" => Avatar.ALBERTO,
            "mario" => Avatar.MARIO,
            _ => throw new System.NotImplementedException(),
        };
    }

    private GameObject GetGameObject(Avatar avatar) {
        return avatar switch {
            Avatar.ALBERTO => alberto,
            Avatar.MARIO => mario,
            _ => null,
        };
    }

    private Animator GetAnimator(GameObject gameObject) {
        return gameObject.GetComponent<Animator>();
    }

    private SpriteRenderer GetRenderer(GameObject gameObject) {
        return gameObject.GetComponent<SpriteRenderer>();
    }

    public void Appear(Avatar avatar) {
        var gameObject = GetGameObject(avatar);
        GetRenderer(gameObject).enabled = true;
        GetAnimator(gameObject).SetBool("isVisible", true);
    }

    public void Disappear(Avatar avatar) {
        var gameObject = GetGameObject(avatar);
        GetAnimator(gameObject).SetBool("isVisible", false); 
        StartCoroutine(HideSprite(gameObject));
    }

    private IEnumerator HideSprite(GameObject gameObject) {
        yield return new WaitForSeconds(4);
        GetRenderer(gameObject).enabled = false;
    }

    public void StartTalking(Avatar avatar) {
        var gameObject = GetGameObject(avatar);
        var animator = GetAnimator(gameObject);
        animator.SetBool("isTalking", true);
    }

    public void StopTalking(Avatar avatar) {
        var gameObject = GetGameObject(avatar);
        var animator = GetAnimator(gameObject);
        animator.SetBool("isTalking", false);
        Debug.Log("IsTAlking = false");
    }
}
