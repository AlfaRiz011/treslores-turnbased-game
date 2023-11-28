using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGet : MonoBehaviour
{
    public GameObject Hint;
    public GameObject ST;
    public int done = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Warrior") || collision.gameObject.CompareTag("Magician") || collision.gameObject.CompareTag("Archer")) && done == 4) 
        {
           Destroy(gameObject);
        } else if(collision.gameObject.CompareTag("Warrior") || collision.gameObject.CompareTag("Magician") || collision.gameObject.CompareTag("Archer")){
            Hint.SetActive(true);
        }
    }
    public void IncrementDone(){
        done += 1;
    }

    public void Update(){
        if(done == 4){
            ST.SetActive(true);
        }
    }
}
