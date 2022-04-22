using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Loading : MonoBehaviourPunCallbacks
{
    [SerializeField] string region;
    [SerializeField] Text percentProgress;
    [SerializeField] Image bar;
    [SerializeField] private byte maxPlayers = 4;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(1000, 9999);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Player name: " + PhotonNetwork.NickName);

        PhotonNetwork.ConnectToRegion(region);

    }

    private void Update()
    {
        if (bar.fillAmount >= 1f && PhotonNetwork.IsConnected) PhotonNetwork.LoadLevel("Lobby");
        
        bar.fillAmount += Random.Range(0.001f, 0.01f);
        percentProgress.text = Mathf.RoundToInt(bar.fillAmount*100) + "%";
    }

    public override void OnConnectedToMaster()
    {
        /*bar.fillAmount = 1f;
        percentProgress.text = Mathf.RoundToInt(bar.fillAmount * 100) + "%";
        Debug.Log("Connected to: " + PhotonNetwork.CloudRegion);
        */
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected");
    }
}
