using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{    
    public Text scoreField;
    public Image gameOverPanel;
    public Text gameOverScoreField;
    public GameObject joystic;

    public Text infoField;
    private int cadr = 0;

    private void Start()
    {
        Bonus.onBonusCollision += UIUpdate;
        Player.onPlayerDeath += GameOver;
    }

    private void Update()
    {
        cadr++;
        scoreField.text = LevelController.playerScore.ToString();
        if (cadr%313 == 0)
        {
            //infoField.text += " / " + LevelController.heroTransform.position.x;
        }
        
    }

    private void UIUpdate(string name) {
        /*if (name == "Star(Clone)")
        {
            scoreField.text = LevelController.playerScore.ToString();
        }   */     
    }

    private void GameOver(Transform player)
    {
        joystic.SetActive(false);
        scoreField.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(true);
        gameOverScoreField.text = LevelController.playerScore.ToString();
    }

    public void onRetryButton() {
        LevelController.playerScore = 0;
        SceneManager.LoadScene(0);
    }
}
