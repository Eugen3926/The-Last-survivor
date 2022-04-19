using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Turret : MonoBehaviour
{
    public Transform tower;
    public Transform rangeTriger;
    public Transform bulletPrefab;
    public Vector2Int angleRotation;

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
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            //tower.LookAt(LevelController.heroTransform);            
        }               
        
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
