using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using DG.Tweening;

public class LevelController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField] private Transform field;
    [SerializeField] private Transform playerPrefab;
    [SerializeField] private Transform SpawnPoints;
    [SerializeField] private Transform collapses;
    [SerializeField] private Transform[] turretPrefabs;
    [SerializeField] private Transform[] bonusPrefabs;
    [SerializeField] private Transform bonuses;
    [SerializeField] private Transform playerContainer;
    [SerializeField] private Transform mainCamera;

    [SerializeField] private Transform lasers;

    private CreateLevel level;    
    private List<Transform> emptyCells;    
    private double lastLaserMoveTime;
    private RaiseEventOptions options;
    private SendOptions sendOptions;

    public static int playerScore = 0;
    public static List<Transform> allPlayers;

    // Start is called before the first frame update
    private void Awake()
    {
        Application.targetFrameRate = 60;        
        allPlayers = new List<Transform>();       

        SpawnPlayer();
    }

    void Start()
    {       
        level = new CreateLevel();        
        Player.onPlayerDeath += GameOver;
        lastLaserMoveTime = PhotonNetwork.Time;

        options = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        sendOptions = new SendOptions { Reliability = true };

        //StartCoroutine(collapseCreation());
        //StartCoroutine(BonusCreation());
        //StartCoroutine(ScoreCount());
    }

    private void Update()
    {
        /*if (PhotonNetwork.Time > lastTickTime + 5 && PhotonNetwork.IsMasterClient)
        {
            Debug.Log("One");
            
            PhotonNetwork.RaiseEvent(42, true, options, sendOptions);
        }*/

        if (PhotonNetwork.Time > lastLaserMoveTime + 10 && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.RaiseEvent(31, false, options, sendOptions);
            LaserBeamer();

            lastLaserMoveTime = PhotonNetwork.Time;
        }
    }

    private void LaserBeamer()
    {
        //lasers.GetChild(Random.Range(0, lasers.childCount)).gameObject.SetActive(true);
        lasers.GetChild(1).gameObject.SetActive(true);
    }

    private void GameOver(Transform player)
    {
        allPlayers.Remove(player);
        //StopAllCoroutines();        
    }

    /*private void FieldGeneration(Transform cellPrefab, Vector2 fieldSize, string cellType)
    {
        for (int i = 0; i < fieldSize.x*fieldSize.y; i++)
        {
            Transform cell = Instantiate(cellPrefab);
            cell.SetParent(field);
        }

        level.CreateField(field, fieldSize, cellType);       
    }*/

    IEnumerator collapseCreation()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(2f);
            emptyCells = level.GetEmptyCells(field);
            level.CreateCollapse(collapses, emptyCells);
        }
    }

    IEnumerator BonusCreation()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(5f);
            emptyCells = level.GetEmptyCells(field);
            Transform bonus = Instantiate(bonusPrefabs[Random.Range(0, bonusPrefabs.Length)]);
            bonus.SetParent(bonuses);
            level.CreateBonus(bonus, emptyCells);
        }
    }

    IEnumerator ScoreCount()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(5f);
            playerScore += Random.Range(1,7); 
        }
    }

    private void SpawnPlayer()
    {        
        Transform hero = PhotonNetwork.Instantiate(playerPrefab.name, SpawnPoints.GetChild(Random.Range(0, SpawnPoints.childCount)).position, Quaternion.identity).transform;
        allPlayers.Add(hero);
        hero.SetParent(playerContainer);        
    }

    public void AddPlayer(Transform player)
    {
        allPlayers.Add(player);
    }

    public void OnEvent(EventData photonEvent)
    {       
        switch (photonEvent.Code)
        {
            case 31:
                LaserBeamer();
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
