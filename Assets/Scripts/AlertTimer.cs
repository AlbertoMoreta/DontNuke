using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertTimer : MonoBehaviour {

    private float timeLeft = 3600;
    private bool countDownRunning = false;
    private TMP_Text alertText;

    // Start is called before the first frame update
    void Start() {
        alertText = GetComponent<TMP_Text>();
        countDownRunning = true;
    }

    // Update is called once per frame
    void Update() {
        if (countDownRunning) {
            if(timeLeft > 0) {
                timeLeft -= Time.deltaTime;
                var minutes = Mathf.FloorToInt(timeLeft / 60);
                var seconds = Mathf.FloorToInt(timeLeft % 60);
                alertText.text = minutes + ":" + seconds;
            } else {
                // explode
                timeLeft = 0;
                countDownRunning = false;
            }
        }
        
    }
}
