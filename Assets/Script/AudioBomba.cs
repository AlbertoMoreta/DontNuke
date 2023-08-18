using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBomba : MonoBehaviour
{
    
    public GameObject bomba;
    

    // Start is called before the first frame update
    void Start()
    {
        
        


  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void play_bomba()
    {
        Instantiate(bomba);

    }
}
