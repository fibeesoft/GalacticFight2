using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBase : MonoBehaviour
{
    Transform target;
    Vector2 targetPos;

    [SerializeField] GameObject BulletPrefab;
    [SerializeField] Transform aimPos;
    [SerializeField] [Range(1,10)] int NoOfBulletsInSeries = 5;
    [SerializeField] [Range(0.2f,1f)] float delayBetweenBullets = 0.3f;
    [SerializeField] [Range(0.5f,5f)] float delayBetweenSeries = 1f;

    void Start()
    {
        target  = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if(target != null){ 
            targetPos = target.transform.position - transform.position;  
        }
        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);   
        transform.rotation *= Quaternion.Euler( new Vector3(1f,1f,1.1f));     
    }

    IEnumerator Shoot(){
        Vector3 dirChange = new Vector3(0.2f,0f,0f);
        
        for(int i = 0; i < NoOfBulletsInSeries; i++){
            Vector3 bulletDir = aimPos.transform.position - transform.position;
            GameObject g = Instantiate(BulletPrefab, aimPos.transform.position, Quaternion.identity);
            g.GetComponent<CanonBullet>().Initialize(bulletDir);
            bulletDir += dirChange;
            yield return new WaitForSeconds(delayBetweenBullets);
        }
        yield return new WaitForSeconds(delayBetweenSeries);
        StartCoroutine(Shoot());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Player")){
            StartCoroutine(Shoot());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.transform.CompareTag("Player")){
            StopAllCoroutines();
        }        
    }
}
