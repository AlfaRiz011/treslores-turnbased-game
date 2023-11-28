using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattlePhaseMenu : MonoBehaviour
{
    public GameObject Dungeon;
    public GameObject Battle;
    public MainCam SetBool;
    public AudioSource BGM;
    public AudioSource DungeonBGM;
    // Start is called before the first frame updat
    public void Retreat(){
        SetBool.SetBool = false;
        Dungeon.SetActive(true);
        Battle.SetActive(false);
        DungeonBGM.UnPause();
        BGM.Stop();
    }

    public void StopMusic(){
        BGM.Stop();
    }
    public void ChangeObject(){
        SetBool.SetBool = false;
        Battle.SetActive(false);
    }
}