using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainTest : MonoBehaviourPunCallbacks
{
    public Transform playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f, 0.35f, 0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
