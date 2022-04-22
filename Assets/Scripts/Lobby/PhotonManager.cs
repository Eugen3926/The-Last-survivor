using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private byte maxPlayers = 4;

    private void Start()
    {
        LobbyUiController.OnReadyButtonDown += QuickMatch;
    }

    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;        
        PhotonNetwork.CreateRoom(null, roomOptions, null);        
    }

    private void QuickMatch(string playerName)
    {
        LobbyUiController.OnReadyButtonDown -= QuickMatch;
        PhotonNetwork.NickName = playerName;        
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();        
    }  

    public override void OnCreatedRoom()
    {
        Debug.Log("Room " + PhotonNetwork.CurrentRoom.Name + " created");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room wasn't created");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined to " + PhotonNetwork.CurrentRoom.Name + " room");        
        PhotonNetwork.LoadLevel("Main");
    }
}
