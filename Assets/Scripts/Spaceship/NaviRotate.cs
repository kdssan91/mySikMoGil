using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaviRotate : MonoBehaviour
{
    public float rotateSpeed = 20f;
    private void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Time.time * rotateSpeed );
    }
}
 