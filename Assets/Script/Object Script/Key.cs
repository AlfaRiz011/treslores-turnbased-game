using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject[] keyhold;
    public KeyGet done;
    public AudioSource Collected;
      private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Warrior") || collision.gameObject.CompareTag("Archer") || collision.gameObject.CompareTag("Magician")){
            Collected.Play();
            Destroy(gameObject);
            keyhold[0].SetActive(true);
            done.IncrementDone();
        }
    }
}
