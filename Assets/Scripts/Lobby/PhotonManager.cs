using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string region;
    [SerializeField] Text playerInfo;
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

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to: " + PhotonNetwork.CloudRegion);
        QuickMatch();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected");
    }

    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;        
        PhotonNetwork.CreateRoom(null, roomOptions, null);        
    }

    private void QuickMatch()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();        
    }
    
    /*public void CreateRoomButton() {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        //PhotonNetwork.CreateRoom(roomName.text, roomOptions, TypedLobby.Default);
    }

    public void JoinOrCreateRandomRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        
        PhotonNetwork.JoinRandomOrCreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2});
        //PhotonNetwork.CreateRoom(roomName.text, roomOptions, TypedLobby.Default);
    }*/

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
        playerInfo.text = PhotonNetwork.NickName;
        PhotonNetwork.LoadLevel("Main");
    }
}
