using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Laser : MonoBehaviour
{
    ParticleSystem particleSystem;
    Transform particles;
    // Start is called before the first frame update
    void Start()
    {
        particles = transform.GetChild(0);
        particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
        particleSystem.Stop();
        //transform.parent.DOMoveZ(-12f, 10f);
        transform.DOScaleY(14f, 0.2f);
        transform.DOLocalMoveX(13.7f, 0.2f);
        /*transform.localScale = new Vector3(transform.localScale.x, 14f, transform.localScale.z);
        transform.localPosition = new Vector3(13.7f, transform.localPosition.y, transform.localPosition.z);*/
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        float scaleY = Mathf.Abs(transform.parent.position.x - collision.transform.position.x) / 2;
        float posX = transform.parent.position.x + scaleY / 2;
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
        transform.localPosition = new Vector3(scaleY, transform.localPosition.y, transform.localPosition.z);
        particles.position = new Vector3(collision.transform.position.x-collision.transform.localScale.x/2, particles.position.y, particles.position.z);
        particleSystem.Play();
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        particleSystem.Stop();
        transform.localScale = new Vector3(transform.localScale.x, 14f, transform.localScale.z);
        transform.localPosition = new Vector3(13.7f, transform.localPosition.y, transform.localPosition.z);
    }*/

    private void OnTriggerEnter(Collider collision)
    {
        float scaleY = Mathf.Abs(transform.parent.position.x - collision.transform.position.x) / 2;
        float posX = transform.parent.position.x + scaleY / 2;
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
        transform.localPosition = new Vector3(scaleY, transform.localPosition.y, transform.localPosition.z);
        particles.position = new Vector3(collision.transform.position.x - collision.transform.localScale.x / 2, particles.position.y, particles.position.z);
        particleSystem.Play();
    }

    private void OnTriggerStay(Collider collision)
    {
        float scaleY = Mathf.Abs(transform.parent.position.x - collision.transform.position.x) / 2;
        float posX = transform.parent.position.x + scaleY / 2;
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
        transform.localPosition = new Vector3(scaleY, transform.localPosition.y, transform.localPosition.z);
        particles.position = new Vector3(collision.transform.position.x - collision.transform.localScale.x / 2, particles.position.y, particles.position.z);
    }

    private void OnTriggerExit(Collider collision)
    {
        particleSystem.Stop();
        transform.localScale = new Vector3(transform.localScale.x, 14f, transform.localScale.z);
        transform.localPosition = new Vector3(13.7f, transform.localPosition.y, transform.localPosition.z);
    }
}
