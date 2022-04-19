using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class Bullet : MonoBehaviour
{
    public static event onCollisionEvent onBulletHit;
    public delegate void onCollisionEvent(Image healthBar, Image armorBar);

    private void Start()
    {
        Player.onPlayerDeath += GameOver;
    }

    private void GameOver()
    {
        /*if (this.gameObject != null)
        {
            Destroy(this.gameObject);
        }      */  
    }

    private void FixedUpdate()
    {
        if (this.gameObject != null) {
            transform.localPosition += new Vector3(0f, -0.01f, 0f);
            if (transform.localPosition.y > 10f)
            {
                Destroy(this.gameObject);
            }
        }            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.gameObject != null) {
            Destroy(this.gameObject);
        }
        
        if (collision.gameObject.tag == "Player")
        {
            onBulletHit?.Invoke(collision.gameObject.transform.parent.GetChild(1).GetChild(1).GetComponent<Image>(), collision.gameObject.transform.parent.GetChild(1).GetChild(3).GetComponent<Image>());
        }
    }
}
