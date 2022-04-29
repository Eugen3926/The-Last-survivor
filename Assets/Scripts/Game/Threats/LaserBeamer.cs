using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamer : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem impactEffect;
    [SerializeField] private Transform laserPurpose;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        RaycastHit hit;
        Vector3 dir = firePoint.position - laserPurpose.position;
        if (Physics.Raycast(transform.position, -dir.normalized, out hit))
        {
            if (hit.collider)
            {
                Vector3 seekPosition = new Vector3(hit.point.x, firePoint.position.y, hit.point.z);
                lineRenderer.SetPosition(1, seekPosition);
                impactEffect.gameObject.SetActive(true);
                impactEffect.transform.position = seekPosition;
            }
        }
        else
        {
            lineRenderer.SetPosition(1, -dir.normalized * 300);
            impactEffect.gameObject.SetActive(false);
        }
    }
}
