using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] Transform aim;
    [SerializeField] AudioClip ShootBulletAudio;
    [SerializeField] bool isUndestructable = false;
    [SerializeField] [Range(4,10)] float moveSpeed = 7f;
    [SerializeField] [Range(10,50)] float bulletSpeed = 16f;
    Rigidbody2D rb;
    float moveX, moveY;
    Vector2 playerVelocity;
    AudioSource audioSource;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(!GameManager.instance.IsGameOver){
            GetUserInput();
        }
    }
    void FixedUpdate() {
        if(!GameManager.instance.IsGameOver){
            Move();
        }
    }

    void Move(){
        playerVelocity = new Vector2(moveX, moveY).normalized;
        playerVelocity *= moveSpeed;
        rb.velocity = playerVelocity;

        if (playerVelocity != Vector2.zero) {
            float angle = Mathf.Atan2(playerVelocity.y, playerVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    void GetUserInput(){
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("Fire1")){
            Attack();
        }
    }

    void Die(){
        GameManager.instance.IsGameOver = true;
        GameManager.instance.GenerateDestroyEffect(transform.position);
        Destroy(gameObject,0f);
    }

    void Attack(){
        if(BulletPrefab != null){
            GameObject g = Instantiate(BulletPrefab, aim.transform.position, Quaternion.identity);
            g.GetComponent<BulletPlayer>().Initialize(aim.right, bulletSpeed);
            if(ShootBulletAudio != null){
                audioSource.clip = ShootBulletAudio;
                audioSource.Play();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("BulletEnemy")){
            if(!isUndestructable){
                Die();
                GameManager.instance.GameOver();
            }
        }
        if(other.gameObject.CompareTag("rightBarrier")){
            GameManager.instance.WinTheGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!isUndestructable){
            if(other.gameObject.CompareTag("BulletEnemy") || other.gameObject.CompareTag("CanonBullet") || other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Enemy")){
                Die();
                GameManager.instance.GameOver();
            }        
        }
    }
}
