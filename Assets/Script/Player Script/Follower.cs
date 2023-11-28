using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject[] character;
    public GameObject[] uiFrame;

    public int whichCharacter = 0;

    public Vector2 lastPosition;

    void Start()
    {
        for (int i = 0; i < character.Length; i++)
        {
            if (i == whichCharacter)
            {
                character[i].transform.position = lastPosition;
                character[i].SetActive(true);
                uiFrame[i].SetActive(true);
            }
            else
            {
                character[i].SetActive(false);
                uiFrame[i].SetActive(false);
                character[i].transform.position = character[whichCharacter].transform.position;
            }
        }
    }

    void Update()
    {
        lastPosition = character[whichCharacter].transform.position;

        if (Input.GetKeyDown(KeyCode.E))
        {
            whichCharacter = (whichCharacter + 1) % character.Length;
            Swap();
        }
    }

    void Swap()
    {
        for (int i = 0; i < character.Length; i++)
        {
            if (i == whichCharacter)
            {
                character[i].transform.position = lastPosition;
                character[i].SetActive(true);
                uiFrame[i].SetActive(true);
            }
            else
            {
                character[i].SetActive(false);
                uiFrame[i].SetActive(false);
                character[i].transform.position = character[whichCharacter].transform.position;
            }
        }
    }
}
