using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToogleMiniCam : MonoBehaviour
{
    public GameObject MiniCam;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Warrior") || collision.gameObject.CompareTag("Archer") || collision.gameObject.CompareTag("Magician")){
            MiniCam.SetActive(true);
        }
    }
}
