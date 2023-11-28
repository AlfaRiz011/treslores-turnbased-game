using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEnemyButton : MonoBehaviour
{
    public GameObject EnemyPrefab;
 
    public void SelectEnemy(){
        GameObject.Find("BattleManagement").GetComponent<BattleManager>().Input2(EnemyPrefab);

    }

 
    public void ToggleSelector(){
        
             EnemyPrefab.transform.Find("SelectedHero").gameObject.SetActive(false);
           
    }
    public void ToggleSelector2(){
        
             EnemyPrefab.transform.Find("SelectedHero").gameObject.SetActive(true);
            
           
    }
}
