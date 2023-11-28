using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatMenu : MonoBehaviour
{
    public AudioSource Press;
    public AudioClip pressSound;

    public void exit(){
        Application.Quit();
    }

    public void Dungeon_Select(){
        StartCoroutine(PlaySoundAndLoadScene(1));
    }

    public void retry(){
        StartCoroutine(PlaySoundAndLoadScene(2));
    }

    private IEnumerator PlaySoundAndLoadScene(int sceneIndex)
    {
        Press.PlayOneShot(pressSound);
        yield return new WaitForSeconds(pressSound.length);
        SceneManager.LoadScene(sceneIndex);
    }
}
