using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public float countdown = 13f;

  void Start() {
    StartCoroutine(Countdown());
  }

  IEnumerator Countdown() {
    while (countdown > 0f) {
      yield return new WaitForSeconds(1f);
      countdown -= 1f;
    }

    if (countdown <= 0f) {
      SceneManager.LoadScene(1);
    }
  }
}
