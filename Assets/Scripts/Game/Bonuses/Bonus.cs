using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Bonus : MonoBehaviour
{
    private float range = 0.15f;
    private float startPosition;
    private float startRotation;

    public static event onCollision onBonusCollision;
    public delegate void onCollision(string name);
    private void Start()
    {
        Player.onPlayerDeath += GameOver;
        startPosition = transform.position.y;
        startRotation = transform.rotation.y;
        transform.DOMoveY(transform.position.y + range, 1f).OnComplete(() => Move());
        transform.DORotate(new Vector3(0f, startRotation + 360f, 0f), 5f, RotateMode.FastBeyond360).OnComplete(() => Rotate());
    }

    private void GameOver(Transform player)
    {
        DOTween.PauseAll();
    }

    private void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {        
        DOTween.Pause(this.transform);
        onBonusCollision?.Invoke(transform.name);
        Destroy(this.gameObject);        
    }

    private void Move() {
        range *= -1;
        startPosition = transform.position.y;
        transform.DOMoveY(transform.position.y + range, 1f).OnComplete(() => Move());
    }

    private void Rotate()
    {        
        startRotation = transform.rotation.y;
        transform.DORotate(new Vector3(0f, startRotation + 360f, 0f), 5f, RotateMode.FastBeyond360).OnComplete(() => Rotate());
    }
}
