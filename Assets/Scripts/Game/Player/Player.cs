using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image armorBar;
    [SerializeField] private Image armorBarBackGround;
    [SerializeField] private Rigidbody hero;
    [SerializeField] private PhotonView photonView;
    private LevelController lc;

    public static event onChangeStateEvent onPlayerDeath;
    public delegate void onChangeStateEvent();

    private PlayerController player;    

    // Start is called before the first frame update
    void Start()
    { 
        lc = FindObjectOfType<LevelController>();
        lc.AddPlayer(this.transform);
        player = new PlayerController();
        JoystickController.onTouchDownEvent += MovePlayer;
        Bullet.onBulletHit += player.Damage;
        Bonus.onBonusCollision += player.BonusEffect;
        Debug.Log("ActorNumber " + GetComponent<PhotonView>().Owner.ActorNumber);
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
        /*if (collision.gameObject.transform.parent.tag == "emptyCell") {
            currentCell = collision.gameObject.transform.parent;                       
        }*/
    }

    private void MovePlayer(float joyX, float joyZ) {
        Debug.Log("Move");
        if (!photonView.IsMine) return;         
        player.Move(joyX, joyZ, hero);
    }
}
