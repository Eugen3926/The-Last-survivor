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
    private Rigidbody hero;

    private void Awake()
    {
        photonView = transform.GetComponent<PhotonView>();
        hero = transform.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {        
        player = new PlayerController();
        JoystickController.onTouchDownEvent += MovePlayer;
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

    private void MovePlayer(float joyX, float joyZ) {
        if (!photonView.IsMine) return;         
        player.Move(joyX, joyZ, hero);
    }
}
