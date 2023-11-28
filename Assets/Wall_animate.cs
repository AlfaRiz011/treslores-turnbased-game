using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAnimate : MonoBehaviour
{
    public Animator animator;
   private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("FireBall")){
           StartCoroutine(DestroyWall());
        }
   }


   private IEnumerator DestroyWall()
   {

        animator.SetTrigger("Explode");
        yield return new WaitForSeconds(0.8f);
        Destroy(this.gameObject);
   }
}
