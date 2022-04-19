using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviour
{   
    
    public Image healthBar;
    public Image armorBar;
    public Image armorBarBackGround;

    public static event onChangeStateEvent onPlayerDeath;
    public delegate void onChangeStateEvent();

    PlayerController player;

    private PhotonView photonView;

    private void Awake()
    {
        photonView = transform.GetComponent<PhotonView>();        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        player = new PlayerController();
        JoystickController.onTouchDownEvent += player.Move;
        Bullet.onBulletHit += player.Damage;
        Bonus.onBonusCollision += player.BonusEffect;
    }

    
    private void Update()
    {
        if (photonView.IsMine)
        {
            if (armorBar.fillAmount <= 0)
            {
                armorBarBackGround.gameObject.SetActive(false);
                armorBar.gameObject.SetActive(false);
            }
            if (healthBar.fillAmount <= 0)
            {
                onPlayerDeath?.Invoke();
                Destroy(this.transform.parent.gameObject);
            }
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.transform.parent.tag == "emptyCell") {
            //currentCell = collision.gameObject.transform.parent;                       
        }
    }   

}
