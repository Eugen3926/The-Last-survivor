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
    
    public static event onChangeStateEvent onPlayerDeath;
    public delegate void onChangeStateEvent(Transform player);

    private PlayerController player;
    private RaiseEventOptions options;
    private SendOptions sendOptions;    

    // Start is called before the first frame update
    void Start()
    {        
        player = new PlayerController();
        JoystickController.onTouchDownEvent += MovePlayer;
        Bullet.onBulletHit += GetDamage;
        Bonus.onBonusCollision += player.BonusEffect;

        options = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        sendOptions = new SendOptions { Reliability = true };
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
                onPlayerDeath?.Invoke(this.transform);                
                PhotonNetwork.LeaveRoom();
            }
        }      
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    public void OnEvent(EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case 1:
                
                foreach (var p in LevelController.allPlayers)
                {
                    if (p.GetComponent<PhotonView>().Owner.ActorNumber == (byte) photonEvent.CustomData)
                    {
                        p.GetChild(5).GetChild(1).GetComponent<Image>().fillAmount -= 0.15f;
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
        if (!photonView.IsMine) return;         
        player.Move(joyX, joyZ, hero);
    }

    private void GetDamage(Transform currentPlayer) {
        /*byte playerNumb = (byte)currentPlayer.GetComponent<PhotonView>().Owner.ActorNumber;
        PhotonNetwork.RaiseEvent(1, playerNumb, options, sendOptions);*/
        //player.Damage(currentPlayer);
        if (currentPlayer.GetComponent<PhotonView>().IsMine) {
            currentPlayer.GetChild(5).GetChild(1).GetComponent<Image>().fillAmount -= 0.15f;
            //PhotonNetwork.RaiseEvent(1, (byte) photonView.Owner.ActorNumber, options, sendOptions);
        }
        
    }
}
