
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Animator animator;
   private void OnTriggerEnter2D(Collider2D collision){
         if(collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("box") || collision.gameObject.CompareTag("Wood")){
            Destroy(gameObject);
        }
    }
}
