using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertTimer : MonoBehaviour {

    private float timeLeft = 3600;
    private bool countDownRunning = false;
    public TMP_Text alertText;
    AudioSource audioSource;

    public static AlertTimer Instance {
        get; private set;
    }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void Display() {
        GetComponent<SpriteRenderer>().enabled = true;
        countDownRunning = true;
        audioSource.Play();
    }

    public void ReduceHalf() {
        timeLeft /= 2;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update() {
        if (countDownRunning) {
            if(timeLeft > 0) {
                timeLeft -= Time.deltaTime;
                var minutes = Mathf.FloorToInt(timeLeft / 60);
                var seconds = Mathf.FloorToInt(timeLeft % 60);
                alertText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
            } else {
                // explode
                timeLeft = 0;
                countDownRunning = false;
            }
        }
        
    }
}
