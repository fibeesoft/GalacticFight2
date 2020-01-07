using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer :MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;
    float moveSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = vel * moveSpeed;
        Destroy(gameObject, 3f);
    }

    public void Initialize(Vector2 vel_p, float bulletSpeed_p){
        vel = vel_p.normalized;
        moveSpeed = bulletSpeed_p;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Obstacle") || other.transform.CompareTag("Enemy")){
            Destroy(gameObject,0f);
            GameManager.instance.GenerateHitEffect(transform.position);
        }

    }
}
