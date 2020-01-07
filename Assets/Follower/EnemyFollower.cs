using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollower : MonoBehaviour
{
    [SerializeField] AudioClip DestroyAudio;
    GameObject target;
    Vector2 targetPos;
    AudioSource audioSource;
    float speed = 2f;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public void Initialize(float speed_p){
        speed = speed_p;
    }
    void Update()
    {
        if(target != null){ 
            targetPos = target.transform.position;  
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        float angle = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);         
    }

    public void Die(){
            GameManager.instance.GenerateDestroyEffect(transform.position);
            if(DestroyAudio != null){
                AudioSource.PlayClipAtPoint(DestroyAudio, transform.position);
            }
            Destroy(gameObject, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.CompareTag("Bullet")){
            Die();
        }
    }
}
