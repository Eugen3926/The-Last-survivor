using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUiController : MonoBehaviour
{
    [SerializeField] private InputField playerName;
    [SerializeField] private Text placeHolder;
    [SerializeField] private GameObject popUp;
    [SerializeField] private Button readyButton;

    public delegate void ButtonAction(string playerName);
    public static event ButtonAction OnReadyButtonDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onReadyButtonDown()
    {
        if (playerName.text.Length < 5)
        {
            ErorMessage();
        }
        else
        {
            OnReadyButtonDown?.Invoke(playerName.text);            
        }
        //QuickMatch();
    }

    private void ErorMessage()
    {
        readyButton.interactable = false;
        popUp.SetActive(true);
    }

    public void ErorButton()
    {
        playerName.text = "";
        popUp.SetActive(false);
        readyButton.interactable = true;
    }
}
