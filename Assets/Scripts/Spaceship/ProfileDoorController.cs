using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileDoorController : MonoBehaviour
{
    Animator anim;
    AudioSource audioSource;
    public AudioClip openSound; 
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Open");
            audioSource.PlayOneShot(openSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Close");
            audioSource.PlayOneShot(openSound);
        }
    }
}
