using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRambo : MonoBehaviour
{
    public AudioSource disparo;
    public GameObject DontPressTheButton;
    public GameObject sorpesa;

    // Start is called before the first frame update
    void Start()
    {
        disparo = GetComponent<AudioSource>();
        



    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void play_disparo()
    {
        disparo.Play();

    }
    public void play_DontPressTheButton()
    {
        Instantiate(DontPressTheButton);

    }
    public void play_sorpresa()
    {
        Instantiate(sorpesa);

    }
}
