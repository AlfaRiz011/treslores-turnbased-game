using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionMenu : MonoBehaviour
{

    public AudioSource Press;
    public AudioClip pressSound;

    public void Catacombs()
    {
        StartCoroutine(PlaySoundAndLoadScene(2));
    }

     public void MainMenu()
    {
        StartCoroutine(PlaySoundAndLoadScene(0));
    }

    public void cutscene_play(){
        StartCoroutine(PlaySoundAndLoadScene(1));
    }


    private IEnumerator PlaySoundAndLoadScene(int sceneIndex)
    {
        Press.PlayOneShot(pressSound);
        yield return new WaitForSeconds(pressSound.length);
        SceneManager.LoadScene(sceneIndex);
    }
}
