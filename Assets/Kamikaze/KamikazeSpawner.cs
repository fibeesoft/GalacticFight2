using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeSpawner : MonoBehaviour
{
    [SerializeField] GameObject KamikazePrefab;
    [SerializeField] GameObject KamikazeSpawnEffectPrefab;
    [SerializeField] [Range(0.2f, 5f)] float spawningDelay = 0.5f;

    IEnumerator SpawnKamikaze(){
        float randomNumber = Random.Range(-3.3f, 3.3f);
        if(KamikazeSpawnEffectPrefab){
            GameObject h = Instantiate(KamikazeSpawnEffectPrefab, new Vector3(transform.position.x, randomNumber, 0f), Quaternion.identity);
        }
        yield return new WaitForSeconds(0.5f);
        if(KamikazePrefab){
            GameObject g = Instantiate(KamikazePrefab, new Vector3(transform.position.x, randomNumber, 0f), Quaternion.identity);
            g.transform.SetParent(gameObject.transform);
        }
        yield return new WaitForSeconds(spawningDelay);
        StartCoroutine(SpawnKamikaze());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Player")){
            StartCoroutine(SpawnKamikaze());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.transform.CompareTag("Player")){
            StopAllCoroutines();
        }        
    }
}
