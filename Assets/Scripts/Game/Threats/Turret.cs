using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Photon.Pun;

public class Turret : MonoBehaviour
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
            StartCoroutine(Fire());
        }        
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
}
