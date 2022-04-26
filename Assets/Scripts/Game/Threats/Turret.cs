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
    [SerializeField] private Transform tower;
    [SerializeField] private Transform rangeTriger;
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private PhotonView photonView;

    private CreateLevel level;
    private Transform field;    
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        level = new CreateLevel();
        field = GameObject.Find("NewField").transform;
        Player.onPlayerDeath += GameOver;

        
    }

    private void GameOver()
    {
        Player.onPlayerDeath -= GameOver;
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            
            //tower.LookAt(nearestPlayer());            
        }               
        
    }

    private Transform nearestPlayer()
    {
        float distance = Vector3.Distance(this.transform.position, LevelController.allPlayers[0].position);
        Transform nearestPlayer = null;
        foreach (var player in LevelController.allPlayers)
        {      
            float d = Vector3.Distance(this.transform.position, player.position);
            if (d <= distance) 
            { 
                distance = d;
                nearestPlayer = player;                
            }
        }
        return nearestPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "Player") {
            isActive = true;
            byte turNumb = FindTurret(this.transform);
            byte playerNumb = (byte)other.GetComponent<PhotonView>().Owner.ActorNumber;
            RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(11, new object[] { isActive, turNumb, playerNumb }, options, sendOptions);
            tower.LookAt(other.transform);
            //StartCoroutine(Fire());
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

    private void OnTriggerExit(Collider other)
    {        
        if (other.gameObject.tag == "Player")
        {
            isActive = false;
            StopCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        while (isActive)
        {
            yield return new WaitForSecondsRealtime(1f);
            Transform bullet = Instantiate(bulletPrefab, tower.GetChild(3).position, Quaternion.identity);
            bullet.SetParent(tower.GetChild(3));           
        }
    }

    public void OnEvent(EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case 10:
                
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
