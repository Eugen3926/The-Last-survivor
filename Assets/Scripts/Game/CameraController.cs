using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CameraController : MonoBehaviour
{
    Transform heroTransform;
    // Start is called before the first frame update
    void Start()
    {
        heroTransform = GameObject.Find("Player").transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {        
        transform.position = new Vector3(heroTransform.position.x, transform.position.y, heroTransform.position.z - 23.2f);        
    }
}
