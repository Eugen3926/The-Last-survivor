using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{    
    [SerializeField] Text percentProgress;
    [SerializeField] Image bar;     

    private void Update()
    {
        if (bar.fillAmount >= 1f) SceneManager.LoadScene("Lobby");
        
        bar.fillAmount += Random.Range(0.001f, 0.01f);
        percentProgress.text = Mathf.RoundToInt(bar.fillAmount*100) + "%";
    }    
}
