using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger_Gate : MonoBehaviour
{
    public AudioSource ST;
    public AudioClip Sound_Trigger;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Warrior" || other.gameObject.tag == "Archer" || other.gameObject.tag == "Magician")
        {
            StartCoroutine(PlaySoundAndSetActive());
        }
    }

    private IEnumerator PlaySoundAndSetActive()
    {
        ST.PlayOneShot(Sound_Trigger);
        yield return new WaitForSeconds(Sound_Trigger.length);
        this.gameObject.SetActive(false);
    }
}
