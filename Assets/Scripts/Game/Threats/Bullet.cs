using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class Bullet : MonoBehaviour
{
    public static event onCollisionEvent onBulletHit;
    public delegate void onCollisionEvent(PhotonView player, float damage);

   
    private Vector3 target;
    private float speed = 10f;    

    public void Seek(Transform _target) {
        target = new Vector3(_target.position.x, _target.position.y, _target.position.z);
    }
    

    private void Start()
    {        
        Player.onPlayerDeath += GameOver;        
    }

    private void GameOver(Transform player)
    {
        /*if (this.gameObject != null)
        {
            Destroy(this.gameObject);
        }      */  
    }

    private void FixedUpdate()
    {       
        BulletFly();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (this.gameObject != null)
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            PhotonView colPlayer = collision.transform.GetComponent<PhotonView>();            
            onBulletHit?.Invoke(colPlayer, 0.15f);
        }
    }    

    private void BulletFly() {
        if (target == null) {
            Destroy(this.gameObject);
            return;
        }

        Vector3 dir = target - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            Destroy(this.gameObject);
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);        
    }
}
