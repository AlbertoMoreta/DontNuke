using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    public AudioSource disparo;
    public AudioSource caida;
    public AudioSource explosion;
    public AudioSource boton;
    public GameObject sonidoDisparo;
    public GameObject sonidoCaida;
    public GameObject sonidoExplosion;
    public GameObject sonidoBoton;

    // Start is called before the first frame update
    void Start()
    {
        disparo = GetComponent<AudioSource>();
        caida = GetComponent<AudioSource>();
        explosion = GetComponent<AudioSource>();
        boton = GetComponent<AudioSource>();



    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void play_disparo()
    {
        disparo.Play();
       
    }
    public void play_caida()
    {
        
        caida.Play();
        
    }
    public void play_explosion()
    {
        
        explosion.Play();
       
    }
    public void play_boton()
    {
        
        boton.Play();
    }
}
