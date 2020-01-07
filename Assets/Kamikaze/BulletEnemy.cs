using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;
    void Start()
    {
        vel = new Vector2(-12f,0f);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = vel;
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Obstacle") || other.transform.CompareTag("Player")){
            GameManager.instance.GenerateHitEffect(transform.position);
            Destroy(gameObject,0f);
        }

    }
}
