using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditMenu : MonoBehaviour
{
    public AudioSource Press;
    public AudioClip pressSound;

    public void Retry()
    {
        StartCoroutine(PlaySoundAndLoadScene(2));
    }

    public void MainMenu()
    {
        StartCoroutine(PlaySoundAndLoadScene(0));
    }

    private IEnumerator PlaySoundAndLoadScene(int sceneIndex)
    {
        Press.PlayOneShot(pressSound);
        yield return new WaitForSeconds(pressSound.length);

        SceneManager.LoadScene(sceneIndex);
    }
}