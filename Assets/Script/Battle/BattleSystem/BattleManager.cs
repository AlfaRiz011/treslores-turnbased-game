using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public BattlePhaseMenu bpm;
    public GameObject victory;
    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION,
        CHECKALIVE,
        LOSE,
        WIN
    }

    public AudioSource VictoryBattle;
    public AudioClip Win;
    public AudioSource VictoryMusic;

    public AudioSource LoosingBattle;
    public AudioClip Lost;


    public PerformAction battleState;
    public GameObject Warrior;
    public GameObject Archer;
    public GameObject Magician;

    private int currentEnemyIndex;


    public List<HandleTurn> PerformList = new List<HandleTurn>();
    public List<GameObject> HeroInBattle = new List<GameObject>();
    public List<GameObject> EnemyInBattle = new List<GameObject>();

    public Transform selectPanel;
    public enum HeroGUI
    {
        ACTIVATE,
        WAITING,
        INPUT1,
        INPUT2,
        DEF,
        DONE,

    }

    public HeroGUI HeroInput;

    public List<GameObject> HeroesToManage = new List<GameObject>();
    private HandleTurn HeroChoise;
    public GameObject enemyButton;

    public GameObject AttackPanel;
    public GameObject EnemySelectPanel;

    private List<GameObject> enemyBtns = new List<GameObject>();
    private bool hasWon;
    private bool haslost;

    void Start()
    {
        battleState = PerformAction.WAIT;
        EnemyInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        HeroInBattle.AddRange(GameObject.FindGameObjectsWithTag("Warrior"));
        HeroInBattle.AddRange(GameObject.FindGameObjectsWithTag("Magician"));
        HeroInBattle.AddRange(GameObject.FindGameObjectsWithTag("Archer"));
        HeroInput = HeroGUI.ACTIVATE;
        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(false);

        EnemyButtons();
    }

    void Update()
    {
        switch (battleState)
        {
            case PerformAction.WAIT:
                if (PerformList.Count > 0)
                {
                    battleState = PerformAction.TAKEACTION;
                }

                break;
            case PerformAction.TAKEACTION:
                GameObject performer = PerformList[0].AttacksGameObject;
                if (PerformList[0].Type == "Enemy")
                {
                    Enemy ESM = performer.GetComponent<Enemy>();
                    for (int i = 0; i < HeroInBattle.Count; i++)
                    {
                        if (PerformList[0].AttackerTarget == HeroInBattle[i])
                        {
                            ESM.HeroToAttack = PerformList[0].AttackerTarget;
                            ESM.currState = Enemy.TurnState.ACTION;
                            break;
                        }
                        else
                        {
                            PerformList[0].AttackerTarget = HeroInBattle[Random.Range(0, HeroInBattle.Count)];
                            ESM.HeroToAttack = PerformList[0].AttackerTarget;
                            ESM.currState = Enemy.TurnState.ACTION;
                        }
                    }

                }

                if (PerformList[0].Type == "Hero")
                {
                    WarriorState WsM = performer.GetComponent<WarriorState>();
                    ArcherState AsM = performer.GetComponent<ArcherState>();
                    MagicianState MsM = performer.GetComponent<MagicianState>();

                    if (PerformList[0].AttacksGameObject == Warrior)
                    {
                        WsM.EnemyToAttack = PerformList[0].AttackerTarget;
                        WsM.currState = WarriorState.TurnState.ACTION;
                    }
                    else if (PerformList[0].AttacksGameObject == Archer)
                    {
                        AsM.EnemyToAttack = PerformList[0].AttackerTarget;
                        AsM.currState = ArcherState.TurnState.ACTION;
                    }
                    else if (PerformList[0].AttacksGameObject == Magician)
                    {
                        MsM.EnemyToAttack = PerformList[0].AttackerTarget;
                        MsM.currState = MagicianState.TurnState.ACTION;
                    }
                }

                if (PerformList[0].Type == "HeroDef")
                {
                    WarriorState WsM = performer.GetComponent<WarriorState>();
                    ArcherState AsM = performer.GetComponent<ArcherState>();
                    MagicianState MsM = performer.GetComponent<MagicianState>();

                    if (PerformList[0].AttacksGameObject == Warrior)
                    {
                        WsM.EnemyToAttack = PerformList[0].AttackerTarget;
                        WsM.currState = WarriorState.TurnState.DEF;
                        
                    }
                    else if (PerformList[0].AttacksGameObject == Archer)
                    {
                        AsM.EnemyToAttack = PerformList[0].AttackerTarget;
                        AsM.currState = ArcherState.TurnState.DEF;
                    }
                    else if (PerformList[0].AttacksGameObject == Magician)
                    {
                        MsM.EnemyToAttack = PerformList[0].AttackerTarget;
                        MsM.currState = MagicianState.TurnState.DEF;
                    }
                }

                battleState = PerformAction.PERFORMACTION;
                break;
            case PerformAction.PERFORMACTION:

                break;
            case PerformAction.CHECKALIVE:
                if (HeroInBattle.Count < 1)
                {
                    battleState = PerformAction.LOSE;
                    
                }
                else if (EnemyInBattle.Count < 1)
                {
                    battleState = PerformAction.WIN;

                } else{
                    battleState = PerformAction.WAIT;
                }
                break;
            case PerformAction.LOSE:
                if (!haslost)
                {
                    bpm.StopMusic();
                    StartCoroutine(welost());
                    haslost = true;
                }
                break;
            case PerformAction.WIN:
                if (!hasWon)
                {
                    wewin();
                    hasWon = true;
                }
                break;

        }

        switch (HeroInput)
        {
            case (HeroGUI.ACTIVATE):
                if (HeroesToManage.Count > 0)
                {
                    HeroesToManage[0].transform.Find("SelectedHero").gameObject.SetActive(true);
                    HeroChoise = new HandleTurn();
                    AttackPanel.SetActive(true);
                    HeroInput = HeroGUI.WAITING;

                }
                break;
            case (HeroGUI.WAITING):
                //idle
                break;
            case (HeroGUI.DEF):
                 HeroInputDone2();
                break;
            case (HeroGUI.DONE):
                HeroInputDone();
                break;
        }
    }

    public void CollectActions(HandleTurn input)
    {
        PerformList.Add(input);

    }

    public void EnemyButtons()
    {
        //cleanup
        foreach (GameObject enemyBtn in enemyBtns)
        {
            Destroy(enemyBtn);
        }
        enemyBtns.Clear();
        foreach (GameObject enemy in EnemyInBattle)
        {
            GameObject newButton = Instantiate(enemyButton) as GameObject;
            SelectEnemyButton button = newButton.GetComponent<SelectEnemyButton>();

            Enemy cur_enemy = enemy.GetComponent<Enemy>();

            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = cur_enemy.EnemyBase.characterName;

            button.EnemyPrefab = enemy;

            newButton.transform.SetParent(selectPanel);

            enemyBtns.Add(newButton);
        }

    }

    public void Input1() //Attack
    {
        //HeroChoise.Attacker = HeroesToManage[0].characterName;
        HeroChoise.AttacksGameObject = HeroesToManage[0];
        HeroChoise.Type = "Hero";
        if (HeroChoise.AttacksGameObject == Warrior)
        {
            HeroChoise.choosenAttack = HeroesToManage[0].GetComponent<WarriorState>().attacks[0];
        }
        else if (HeroChoise.AttacksGameObject == Archer)
        {
            HeroChoise.choosenAttack = HeroesToManage[0].GetComponent<ArcherState>().attacks[0];
        }
        else if (HeroChoise.AttacksGameObject == Magician)
        {
            HeroChoise.choosenAttack = HeroesToManage[0].GetComponent<MagicianState>().attacks[0];
        }
        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(true);
    }

    public void Input2(GameObject choosenEnemy) //Pilih musuh
    {
        HeroChoise.AttackerTarget = choosenEnemy;
        HeroInput = HeroGUI.DONE;
    }

    public void Input3() //Def 
    {
        HeroChoise.AttacksGameObject = HeroesToManage[0];
        HeroChoise.Type = "HeroDef";
        AttackPanel.SetActive(false);
        HeroInput = HeroGUI.DEF;
    }

    public void Input4() //SPAttack
    {
        //HeroChoise.Attacker = HeroesToManage[0].characterName;
        HeroChoise.AttacksGameObject = HeroesToManage[0];
        HeroChoise.Type = "Hero";
        if (HeroChoise.AttacksGameObject == Warrior)
        {
            HeroChoise.choosenAttack = HeroesToManage[0].GetComponent<WarriorState>().attacks[1];
        }
        else if (HeroChoise.AttacksGameObject == Archer)
        {
            HeroChoise.choosenAttack = HeroesToManage[0].GetComponent<ArcherState>().attacks[1];
        }
        else if (HeroChoise.AttacksGameObject == Magician)
        {
            HeroChoise.choosenAttack = HeroesToManage[0].GetComponent<MagicianState>().attacks[1];
        }
        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(true);
    }


    void HeroInputDone()
    {
        PerformList.Add(HeroChoise);
        EnemySelectPanel.SetActive(false);
        HeroesToManage[0].transform.Find("SelectedHero").gameObject.SetActive(false);
        HeroesToManage.RemoveAt(0);
        HeroInput = HeroGUI.ACTIVATE;
    }
    void HeroInputDone2()
    {
        PerformList.Add(HeroChoise);
        HeroesToManage[0].transform.Find("SelectedHero").gameObject.SetActive(false);
        HeroesToManage.RemoveAt(0);
        HeroInput = HeroGUI.ACTIVATE;
    }

    private void wewin(){
        bpm.StopMusic();
        StartCoroutine(Victory());
    }
    private IEnumerator Victory()
    {
        VictoryBattle.PlayOneShot(Win);
        yield return new WaitForSeconds(Win.length);
        bpm.ChangeObject();
        VictoryMusic.Play();
        victory.SetActive(true);
    }

    public void stopvictory(){
        VictoryMusic.Stop();
    }

    private IEnumerator welost()
    {
        LoosingBattle.PlayOneShot(Lost);
        yield return new WaitForSeconds(Lost.length);
        SceneManager.LoadScene(4);
    }


}