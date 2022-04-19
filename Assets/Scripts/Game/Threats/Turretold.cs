using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Turretold : MonoBehaviour
{
    public Transform startPosition;
    public Transform[] ammunition;
    TurretController trCont;

    private List<Transform> turRot;
    // Start is called before the first frame update
    void Start()
    {
        trCont = new TurretController();
        trCont.TurretUprise(this.transform);
        TurretRotation();
        StartCoroutine(startFire());
        
        this.transform.GetChild(1).DOLookAt(new Vector3(turRot[turRot.Count - 1].position.x, this.transform.GetChild(1).position.y, this.transform.GetChild(1).position.z), 5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator startFire()
    {        
        yield return new WaitForSecondsRealtime(2f);
        while (true)
        {
            switch (this.tag)
            {
                case "BulletTurret":
                    BulletFire(ammunition[0]);                    
                    break;
                default:
                    break;
            }
            yield return new WaitForSecondsRealtime(0.3f);
        }
    }

    private void TurretRotation()
    {
        turRot = new List<Transform>();
        for (int i = 0; i < this.transform.GetChild(0).childCount; i++)
        {
            if (this.transform.GetChild(0).GetChild(i).tag == "isIntersection")
            {
                turRot.Add(this.transform.GetChild(0).GetChild(i));                
            }
        }        
    }

    private void BulletFire(Transform bulletPrefub)
    {
        Transform bullet = Instantiate(bulletPrefub, startPosition.position, Quaternion.identity);
        bullet.SetParent(this.transform.GetChild(1).GetChild(2));        
    }
}
