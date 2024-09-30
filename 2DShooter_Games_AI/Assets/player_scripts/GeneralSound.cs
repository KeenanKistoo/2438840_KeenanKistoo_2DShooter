using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSound : MonoBehaviour
{
    public AudioSource _audioSource;

    public AudioClip _normalClip;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = this.gameObject.GetComponent<AudioSource>();

        _audioSource.clip = _normalClip;
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
