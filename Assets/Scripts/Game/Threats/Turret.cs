using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Turret : MonoBehaviour, IOnEventCallback
{
    [Header ("Base:")] 
    [SerializeField] private Transform tower;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform rangeTriger;

    [Header("Bullet:")]
    [SerializeField] private Transform bulletPrefab;

    [Header("Laser:")]
    [SerializeField] private bool useLaser = false;
    

    private CreateLevel level;
    private Transform field;    
    private bool isActive = false;
    private Transform currentPlayer;
    private RaiseEventOptions options;
    private SendOptions sendOptions;
    private bool scanTheArea = false;
    private double lastTickTime;    
    private Transform bulletsContainer;
   

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log((int) Mathf.Ceil(179.26f) + " / " + Mathf.Ceil(179.56f));
        level = new CreateLevel();
        field = GameObject.Find("NewField").transform;
        Player.onPlayerDeath += GameOver;        

        options = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        sendOptions = new SendOptions { Reliability = true };

        lastTickTime = PhotonNetwork.Time;        
        bulletsContainer = GameObject.Find("Bullets").transform;      
    }

    private void GameOver(Transform player)
    {
        Player.onPlayerDeath -= GameOver;
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        if (!useLaser)
        {
            if (isActive && currentPlayer != null)
            {
                tower.LookAt(currentPlayer.GetChild(3));
                if (PhotonNetwork.Time > lastTickTime + 2 && PhotonNetwork.IsMasterClient)
                {

                    PhotonNetwork.RaiseEvent(12, false, options, sendOptions);
                    Fire();

                    lastTickTime = PhotonNetwork.Time;
                }
            }
        }            
    }
    
    private byte CurrentPurpose(Transform player) {
        return (byte)player.GetComponent<PhotonView>().Owner.ActorNumber;
    }   

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "Player") {
            ChangeTurretState(true, other.transform);             
        }        
    }

    private byte FindTurret(Transform turret) {
        byte res = 0;
        for (int i = 0; i < turret.parent.childCount; i++)
        {
            res = (byte)i;
            if (turret == turret.parent.GetChild(i)) break;
        }
        return res;
    }

    private void OnTriggerStay(Collider other)
    {
        if (scanTheArea && other.gameObject.tag == "Player")
        {
            ChangeTurretState(true, other.transform);
            scanTheArea = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        if (other.gameObject.tag == "Player")
        {
            ChangeTurretState(false, null);
            scanTheArea = true;
        }
    }

    private void ChangeTurretState(bool state, Transform player) {
        isActive = state;
        currentPlayer = player;        
        PhotonNetwork.RaiseEvent(10, isActive, options, sendOptions);
    }

    private void Fire() {        
        Transform bullet = Instantiate(bulletPrefab, tower.GetChild(3).position, Quaternion.identity).transform;
        Bullet bulScript = bullet.GetComponent<Bullet>();        
        if(bulScript != null) bulScript.Seek(tower.GetChild(4));
    }
   
    public void OnEvent(EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case 10: 
                isActive = (bool)photonEvent.CustomData;                
                break;
            case 11:
                object[] data = (object[])photonEvent.CustomData;
                foreach (var player in LevelController.allPlayers)
                {
                    if (player.GetComponent<PhotonView>().Owner.ActorNumber == (byte)data[2])
                    {
                        transform.parent.GetChild((byte)data[1]).GetChild(0).LookAt(player);
                    }
                }
                break;
            case 12:
                Fire();
                break;                
            default:
                break;
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }    
}
