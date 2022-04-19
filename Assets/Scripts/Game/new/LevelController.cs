using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LevelController : MonoBehaviourPunCallbacks
{
    public Transform field;
    public Transform playerPrefab;
    public Transform SpawnPoints;
    public Transform collapses;
    public Transform[] turretPrefabs;
    public Transform[] bonusPrefabs;
    public Transform bonuses;
       
         
    private CreateLevel level;    
    private List<Transform> emptyCells;

    public static int playerScore = 0;
    public static Transform heroTransform;
    public static Transform currentCell;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; 
        level = new CreateLevel();
        Player.onPlayerDeath += GameOver;

        SpawnPlayer();
        //StartCoroutine(collapseCreation());
        //StartCoroutine(BonusCreation());
        //StartCoroutine(ScoreCount());
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
        heroTransform = PhotonNetwork.Instantiate(playerPrefab.name, SpawnPoints.GetChild(Random.Range(0, SpawnPoints.childCount)).position, Quaternion.identity).transform;        
        Transform players = GameObject.Find("Players").transform;        
        //hero.SetParent(players);        
    }


}
