using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
    public int buildIndex = 4;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Warrior" || other.gameObject.tag == "Archer" || other.gameObject.tag == "Magician")
        {
            SceneManager.LoadScene(buildIndex);
        }
    }
}
