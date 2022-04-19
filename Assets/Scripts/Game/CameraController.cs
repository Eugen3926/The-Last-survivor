using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CameraController : MonoBehaviour
{
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = LevelController.heroTransform.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            transform.position = new Vector3(LevelController.heroTransform.position.x, transform.position.y, LevelController.heroTransform.position.z - 23.2f);
        }        
    }
}
