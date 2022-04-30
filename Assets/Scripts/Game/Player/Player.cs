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
        LaserBeamer.onRayHit += GetDamage;

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
                object[] data = (object[]) photonEvent.CustomData;
                foreach (var player in PhotonNetwork.PhotonViewCollection)
                {
                    if (player.Owner.ActorNumber == (int) data[0]) {
                        player.transform.GetChild(5).GetChild(1).GetComponent<Image>().fillAmount = (float) data[1];
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

    private void GetDamage(PhotonView currentPlayer, float damageValue) {       

        if (photonView.Owner.ActorNumber == currentPlayer.Owner.ActorNumber)
        {
            Debug.Log("Damage");
            Image healthBar = currentPlayer.transform.GetChild(5).GetChild(1).GetComponent<Image>();
            healthBar.fillAmount -= damageValue;            
            PhotonNetwork.RaiseEvent(1, new object[] { currentPlayer.Owner.ActorNumber, healthBar.fillAmount }, options, sendOptions);            
        }
    }
}
