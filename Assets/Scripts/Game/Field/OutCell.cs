using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutCell : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "notIntersection")
        {
            other.transform.tag = "isIntersection";
        }       
    }
}
