using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollider : MonoBehaviour
{
    public GameObject Dungeon;
    public GameObject Battle;
    public MainCam SetBool;

    public AudioSource BGM;
    public AudioSource DungeonBGM;

       private void OnTriggerEnter2D(Collider2D collision)
    {

        Dungeon.SetActive(false);
        Battle.SetActive(true);
        Destroy(gameObject);
        SetBool.SetBool = true;
        DungeonBGM.Pause();
        BGM.Play();
    }


}
