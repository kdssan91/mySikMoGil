using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BagController : MonoBehaviour
{
    public Transform target;
    public Transform player; 
    public static BagController instance;

    private void Awake()
    {
        instance = this;
    }

    public void FlyToPlayer()
    {
        transform.position = target.position;
        transform.LookAt(player); 
    }
}
