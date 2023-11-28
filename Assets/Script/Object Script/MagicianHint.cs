using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianHint : MonoBehaviour
{
    public GameObject Hint;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Warrior") || collision.gameObject.CompareTag("Magician") || collision.gameObject.CompareTag("Archer")) 
        {
           Hint.SetActive(true);
        }
    }
}
