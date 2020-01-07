using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneShooter : MonoBehaviour
{
    [SerializeField] GameObject StonePrefab;
    [SerializeField] [Range(1f,4f)] float stoneShootingDelay = 2f;

    IEnumerator Shoot(){
        if(StonePrefab != null){
            GameObject g = Instantiate(StonePrefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(stoneShootingDelay);
            StartCoroutine(Shoot());
        }
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
