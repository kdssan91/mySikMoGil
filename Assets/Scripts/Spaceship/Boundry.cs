using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundry : MonoBehaviour
{
    public static Boundry instance;
    public bool isPlayerInShip = true; 
    private void Awake()
    {
        instance = this; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInShip = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInShip = false; 
        }
    }
}
