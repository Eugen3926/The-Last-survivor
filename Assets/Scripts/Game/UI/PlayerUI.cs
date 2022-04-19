using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.position = new Vector3(LevelController.heroTransform.position.x, transform.position.y, LevelController.heroTransform.position.z);
    }
}
