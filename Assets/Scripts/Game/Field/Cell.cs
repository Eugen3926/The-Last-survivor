using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Cell : MonoBehaviour
{
    private MeshRenderer _meshUp;
    private MeshRenderer _meshDown;
    private MeshCollider _collider;

    private bool playerOnCell = false;
    
    private void Start()
    {
        _meshUp = this.transform.GetComponent<MeshRenderer>();
        _meshDown = this.transform.parent.GetChild(0).GetComponent<MeshRenderer>();
        _collider = this.transform.GetComponent<MeshCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.parent.tag != "Wall")
        {
            transform.parent.tag = "notEmptyCell";
            if (transform.parent.localScale.x == 1f)
            {
                transform.parent.localScale = new Vector3(0.98f, 1f, 0.98f);
                StartCoroutine(Tremble(other.gameObject));
            }
        }                 
    }

    private void OnTriggerExit(Collider other)
    {
        if (transform.parent.tag != "Wall")
            transform.parent.tag = "emptyCell";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (transform.parent.tag != "Wall")
            transform.parent.tag = "notEmptyCell";
        if (collision.gameObject.tag == "Player")        
            playerOnCell = true;
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (transform.parent.tag != "Wall")
            transform.parent.tag = "emptyCell";

        if (collision.gameObject.tag == "Player")        
            playerOnCell = false;
        
    }

    IEnumerator Tremble(GameObject collapse)
    {
        Vector3 val = new Vector3(0.04f, 0f, 0.04f);
        for (int i = 1; i <= 40; i++)
        {
            if (i % 2 == 0) { transform.parent.position += val; }
            else transform.parent.position -= val;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        TrembleComplete(collapse);
    }

    private void TrembleComplete(GameObject collapse) {       
        transform.parent.DOScale(0.05f, 0.5f).OnComplete(() => ScaleComplete(collapse));
    }

    public void ScaleComplete(GameObject collapse) {        
        _meshUp.enabled = false;
        _meshDown.enabled = false;
        _collider.enabled = false;
        
        if (playerOnCell)
        {            
            Rigidbody hero = LevelController.heroTransform.GetComponent<Rigidbody>();
            hero.constraints = RigidbodyConstraints.None;            
        }
        StartCoroutine(Respawn(collapse));
    }

    IEnumerator Respawn(GameObject collapse)
    {
        yield return new WaitForSecondsRealtime(2f);
        _meshUp.enabled = true;
        _meshDown.enabled = true;
        _collider.enabled = true;
        transform.parent.DOScale(1f, 0.5f).OnComplete(() => RespawnComplete(collapse));
    }

    public void RespawnComplete(GameObject collapse)
    {
        collapse.SetActive(false);
    }
}
