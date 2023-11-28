using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonMenu : MonoBehaviour
{
    public GameObject Dungeon;
    public BattleManager stopmusic;
    public GameObject MenuPanel;
    public GameObject Victory;
    public AudioSource Pressed;
    public AudioClip pressSound;
    public AudioSource DungeonBGM;
    public void Dmenu(){
        DungeonBGM.Pause();
        StartCoroutine(ShowMenu());
    }

    public void Continue(){
        DungeonBGM.UnPause();
        StartCoroutine(Keep());
    }

    public void Continue_Victory(){
        stopmusic.stopvictory();
        DungeonBGM.UnPause();
        StartCoroutine(Keep());
    }

    public void Retry(){
        StartCoroutine(PlaySoundAndLoadScene(2));
    }

    public void MainMenu(){
        StartCoroutine(PlaySoundAndLoadScene(0));
    }

    private IEnumerator ShowMenu()
    {
        Pressed.PlayOneShot(pressSound);
        yield return new WaitForSeconds(pressSound.length);
        Dungeon.SetActive(false);
        MenuPanel.SetActive(true);
    }
    private IEnumerator Keep()
    {
        Pressed.PlayOneShot(pressSound);
        yield return new WaitForSeconds(pressSound.length);
        Dungeon.SetActive(true);
        MenuPanel.SetActive(false);
        Victory.SetActive(false);
    }
    private IEnumerator PlaySoundAndLoadScene(int sceneIndex)
    {
        Pressed.PlayOneShot(pressSound);
        yield return new WaitForSeconds(pressSound.length);
        SceneManager.LoadScene(sceneIndex);
    }
}
