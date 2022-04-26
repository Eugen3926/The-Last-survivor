using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Player : MonoBehaviour, IOnEventCallback
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
        /*if (photonView.IsMine)
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
        }      */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Selected")
        {
            RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(1, photonView.Owner.ActorNumber, options, sendOptions);
            this.transform.GetChild(2).gameObject.SetActive(false);
        }        
    }

    public void OnEvent(EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case 1:                
                if (LevelController.allPlayers.Count > 1)
                {
                    foreach (var player in LevelController.allPlayers)
                    {
                        if (player.GetComponent<PhotonView>().Owner.ActorNumber == (int)photonEvent.CustomData)
                        {
                            player.GetChild(2).gameObject.SetActive(false);
                        }
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

    private void MovePlayer(float joyX, float joyZ) {
        Debug.Log("Move");
        if (!photonView.IsMine) return;         
        player.Move(joyX, joyZ, hero);
    }
}
