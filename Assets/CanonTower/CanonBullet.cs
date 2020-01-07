using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBullet : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;
    float moveSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 16f;
        rb.velocity = vel * moveSpeed;
        Destroy(gameObject, 3f);
    }

    public void Initialize(Vector2 vel_p){
        vel = vel_p.normalized;
    }
}
