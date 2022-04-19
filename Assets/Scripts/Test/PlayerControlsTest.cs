using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControlsTest : MonoBehaviour
{
    public static Transform heroTransform;
    private PhotonView photonView;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        heroTransform = this.transform;
        //player = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetKey(KeyCode.LeftArrow)) heroTransform.Translate(-Time.deltaTime * 5, 0f, 0f);
        if (Input.GetKey(KeyCode.RightArrow)) heroTransform.Translate(Time.deltaTime * 5, 0f, 0f);
    }
}
