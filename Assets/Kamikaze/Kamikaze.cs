using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    [SerializeField] GameObject BulletEnemyPrefab;
    [SerializeField] GameObject aim;
    [SerializeField] AudioClip ShootBulletAudio;
    [SerializeField] AudioClip DestroyAudio;
    Rigidbody2D rb;
    AudioSource audioSource;
    Vector2 vel;
    void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        vel = new Vector2(-3f, 0f);
        rb.velocity = vel;
        InvokeRepeating("Shoot", 1f, 1f);
        Destroy(gameObject, 15f);
    }

    void Shoot(){
        GameObject g = Instantiate(BulletEnemyPrefab, aim.transform.position, Quaternion.identity);
        if(ShootBulletAudio != null){
            audioSource.clip = ShootBulletAudio;
            audioSource.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Obstacle")){
            Die();
        }
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

        if(other.gameObject.CompareTag("Obstacle")){
            if(transform.position.y <= 0){
                vel = new Vector2(-3f, 1.5f);
            }else{
                vel = new Vector2(-3f, -1.5f);
            }
            rb.velocity = vel;
        }

        if(other.gameObject.CompareTag("leftBarrier")){
            GetComponent<CircleCollider2D>().isTrigger = true;
            Destroy(gameObject, 3f);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Obstacle")){
            rb.velocity = new Vector2(-3, 0f);
        }
    }

}
