using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource Press;

    public AudioClip pressSound;

    public void PlayGame()
    {
        StartCoroutine(PlaySoundAndLoadScene());
    }

    private IEnumerator PlaySoundAndLoadScene()
    {
        Press.PlayOneShot(pressSound);
        yield return new WaitForSeconds(pressSound.length);
        SceneManager.LoadScene(5);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
