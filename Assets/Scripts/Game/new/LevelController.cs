using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class LevelController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public Transform field;
    public Transform playerPrefab;
    public Transform SpawnPoints;
    public Transform collapses;
    public Transform[] turretPrefabs;
    public Transform[] bonusPrefabs;
    public Transform bonuses;
    public Transform playerContainer;
    public Transform mainCamera;
         
    private CreateLevel level;    
    private List<Transform> emptyCells;
    private double lastTickTime = 0;

    public static int playerScore = 0;
    public static List<Transform> allPlayers;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; 
        level = new CreateLevel();
        allPlayers = new List<Transform>();
        Player.onPlayerDeath += GameOver;

        SpawnPlayer();
        //StartCoroutine(collapseCreation());
        //StartCoroutine(BonusCreation());
        //StartCoroutine(ScoreCount());
    }

    private void Update()
    {
        /*if (PhotonNetwork.Time > lastTickTime + 5 && PhotonNetwork.IsMasterClient)
        {
            Debug.Log("One");
            RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.All};
            SendOptions sendOptions = new SendOptions { Reliability = true};
            PhotonNetwork.RaiseEvent(42, true, options, sendOptions);
        }*/
    }

    private void GameOver()
    {
        StopAllCoroutines();        
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
            case 42:
                /*List<Transform> list = (List<Transform>) photonEvent.CustomData;
                list[list.Count-1].GetComponent<Animator>().enabled = false;*/
                if (allPlayers.Count > 1)
                {
                    foreach (var player in allPlayers)
                    {
                        if (player.GetComponent<PhotonView>().Owner.ActorNumber>1) {
                            player.GetChild(2).gameObject.SetActive(false);
                        }
                        
                    }
                    Debug.Log("Oppa");
                }
                lastTickTime = PhotonNetwork.Time;
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
