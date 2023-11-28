using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    public GameObject body;
    public bool checkTrigger;
    public bool W,A,M;
    public float speed;
    public Transform target;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Warrior").GetComponent<Transform>();  
    }


    void Update()
    {
        if (checkTrigger || W )
        {
            body.transform.position = Vector2.MoveTowards(body.transform.position, target.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Warrior"||other.gameObject.tag == "Archer"||other.gameObject.tag == "Magician"){
            checkTrigger = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {   
        if(other.gameObject.tag == "Warrior"||other.gameObject.tag == "Archer"||other.gameObject.tag == "Magician"){
            checkTrigger = false;
            
        } 
    }
}
