using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject EffectDestroyPrefab;
    [SerializeField] GameObject EffectHitPrefab;
    [SerializeField] [Range(5f, 40f)] float timer = 10f;
    [SerializeField] AudioClip BgSound;
    [SerializeField] Text txt_counter;
    public bool IsGameOver{get; set;}

    AudioSource audioSource;
    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }
    void Start()
    {
        IsGameOver = false;
        audioSource = GetComponent<AudioSource>();
        if(BgSound != null){
            audioSource.clip = BgSound;
            audioSource.Play();
        }
        if(txt_counter != null){
            txt_counter.text = timer.ToString();
        }
    }
    public void GameOver(){
        IsGameOver = true;
        print("gameover");
        SceneManager.LoadScene(2);
    }

    public void WinTheGame(){
        print ("You won the game");
        SceneManager.LoadScene(0);
    }

    public void GenerateDestroyEffect(Vector3 pos){
        if(EffectDestroyPrefab != null){
            GameObject g = Instantiate(EffectDestroyPrefab, pos, Quaternion.identity);
            Destroy(g, 0.2f);
        }
    }

    public void GenerateHitEffect(Vector3 pos){
        if(EffectHitPrefab != null){
            GameObject g = Instantiate(EffectHitPrefab, pos, Quaternion.identity);
            Destroy(g, 0.2f);
        }
    }

    void Update(){
        CountTime();
    }

    void CountTime(){
        if(!IsGameOver){
            if(timer >= 0){
                timer -= Time.deltaTime;
                if(txt_counter != null){
                    txt_counter.text = timer.ToString();
                }
                //print(timer);
            }else{
                GameOver();
            }
        }
    }

}
