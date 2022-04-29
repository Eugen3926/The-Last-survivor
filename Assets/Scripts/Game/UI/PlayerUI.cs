using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerUI : MonoBehaviour
{     
    private Color myNickNameColor = new Color(0, 168, 191, 255);
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var player in PhotonNetwork.PhotonViewCollection)
        {
            TextMeshProUGUI nickName = player.transform.GetChild(6).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            nickName.text = player.Owner.NickName;
            if (player.IsMine) nickName.color = myNickNameColor;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        
    }
}
