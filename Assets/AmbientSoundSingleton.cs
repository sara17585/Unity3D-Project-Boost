using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundSingleton : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
