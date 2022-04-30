using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamer : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem impactEffect;
    [SerializeField] private Transform laserPurpose;
    [SerializeField] private float distance;
    [SerializeField] private bool horizontalMove = false;


    private Vector3 startPosition;
    private Vector3 direct;

    public static event onRayastHitEvent onRayHit;
    public delegate void onRayastHitEvent(PhotonView player, float damage);
    // Start is called before the first frame update

    private void OnEnable()
    {
        startPosition = transform.position;        
    }
    void Start()
    {
        direct = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {        
        BeamerMove();
        LaserShoot();       
    }

    private void BeamerMove()
    {
        if (horizontalMove)
        {
            if (Mathf.Abs(transform.position.x - startPosition.x) >= distance)
            {
                startPosition = transform.position;
                direct *= -1;
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            if (Mathf.Abs(transform.position.z - startPosition.z) >= distance)
            {
                startPosition = transform.position;
                direct *= -1;
                this.gameObject.SetActive(false);
            }
        }

        transform.Translate(direct * Time.deltaTime, Space.Self);
    }

    private void LaserShoot()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        RaycastHit hit;
        Vector3 dir = firePoint.position - laserPurpose.position;
        if (Physics.Raycast(firePoint.position, -dir.normalized, out hit))
        {
            if (hit.collider)
            {
                Vector3 seekPosition = new Vector3(hit.point.x, firePoint.position.y, hit.point.z);
                lineRenderer.SetPosition(1, seekPosition);
                impactEffect.gameObject.SetActive(true);
                impactEffect.transform.position = seekPosition;
                if (hit.collider.gameObject.tag == "Player") {
                    PhotonView colPlayer = hit.collider.transform.GetComponent<PhotonView>();
                    onRayHit?.Invoke(colPlayer, 0.01f);
                }
            }
        }
        else
        {
            lineRenderer.SetPosition(1, laserPurpose.position);
            impactEffect.gameObject.SetActive(false);
        }
    }
}
