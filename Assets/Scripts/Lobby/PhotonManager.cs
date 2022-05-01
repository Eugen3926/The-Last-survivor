using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private byte maxPlayers = 4;
    [SerializeField] string region;
    [SerializeField] Text infoField;


    private bool isReadyToPlay = false;

    private void Awake()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(1000, 9999);
    }
    private void Start()
    {
        infoField.text = "Connection...";
        LobbyUiController.OnReadyButtonDown += QuickMatch;
        
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(region);
    }

    private void Update()
    {
        if(!isReadyToPlay) return;
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2) SceneManager.LoadScene("Main");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to " + PhotonNetwork.CloudRegion);
        infoField.text = "Connected to " + PhotonNetwork.CloudRegion + " region";
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
        infoField.text = "Room " + PhotonNetwork.CurrentRoom.Name + " created";
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room wasn't created");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined to " + PhotonNetwork.CurrentRoom.Name + " room");
        infoField.text = "Waiting for another players...";
        isReadyToPlay = true;
    }
}
