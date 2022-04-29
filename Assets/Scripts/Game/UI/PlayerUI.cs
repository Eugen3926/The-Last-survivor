using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerUI : MonoBehaviour
{     
    // Start is called before the first frame update
    void Start()
    {
        foreach (var player in PhotonNetwork.PhotonViewCollection)
        {

            player.gameObject.transform.GetChild(6).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = player.Owner.NickName;            
        }        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        
    }
}
