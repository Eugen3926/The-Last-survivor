using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyUiController : MonoBehaviour
{
    [SerializeField] private InputField playerName;
    [SerializeField] private Text placeHolder;
    [SerializeField] private GameObject popUp;
    [SerializeField] private Button readyButton;
    [SerializeField] private GameObject statusField;

    public delegate void ButtonAction(string playerName);
    public static event ButtonAction OnReadyButtonDown;

    // Start is called before the first frame update
    void Start()
    {
        playerName.text = PhotonNetwork.NickName;
    }

    public void onReadyButtonDown()
    {
        if (!PhotonNetwork.IsConnectedAndReady) return;
        if (playerName.text.Length < 5)
        {
            ErorMessage();
        }
        else
        {
            OnReadyButtonDown?.Invoke(playerName.text);
            statusField.SetActive(true);
        }        
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
