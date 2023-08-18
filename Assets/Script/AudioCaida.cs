using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCaida : MonoBehaviour
{
    public AudioSource caida;
    public GameObject Muerte;
    public GameObject Boton;
    

    // Start is called before the first frame update
    void Start()
    {
        caida = GetComponent<AudioSource>();
        



    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void play_Caida()
    {
        caida.Play();

    }
    public void play_Muerte()
    {
        Instantiate(Muerte);

    }
    public void play_Boton()
    {
        Instantiate(Boton);

    }
}
