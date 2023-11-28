using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcherState : MonoBehaviour
{
    public Animator animator;
    public HealthBar healthBar;
    private BattleManager BSM;
    public string characterName;
    public float HP, MaxHP, MAG, ATK, DEF;
    public List<BaseAttack> attacks = new List<BaseAttack>();

    public float cur_cooldown = 0f;
    private float max_cooldown = 5f;

    private bool alive = true;

    public Image ProgressBar;
    [SerializeField] public BasePlayer ArcherBase;

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEF,
        DEAD
    }

    public TurnState currState;

    public GameObject selector;

    //IEnumerate
    public GameObject EnemyToAttack;
    private Vector3 startPosition;
    private bool actionStarted = false;
    private float animSpeed = 10f;
    public AudioSource shooting;
    public AudioSource Hurted;
    void Start()
    {

        Spawn();
        UpdateStats();

        healthBar.SetMaxHealth(ArcherBase.MaxHP);
        healthBar.SetHealth(HP);
        startPosition = transform.position;
        cur_cooldown = Random.Range(0, 2.5f);
        selector.SetActive(false);

        BSM = GameObject.Find("BattleManagement").GetComponent<BattleManager>();
        currState = TurnState.PROCESSING;
    }

    void Update()
    {
        Spawn();
        UpdateStats();


        switch (currState)
        {
            case (TurnState.PROCESSING):
                UpgradeProgressBar();
                break;
            case (TurnState.ADDTOLIST):
                BSM.HeroesToManage.Add(this.gameObject);
                currState = TurnState.WAITING;

                break;
            case (TurnState.DEF):
                BSM.PerformList.RemoveAt(0);
                BSM.battleState = BattleManager.PerformAction.WAIT;
                cur_cooldown = 0f;
                currState = TurnState.PROCESSING;
                break;
            case (TurnState.WAITING):

                break;
            case (TurnState.ACTION):
                StartCoroutine(TimeForAction());
                break;
            case (TurnState.DEAD):
                if (!alive)
                {
                    return;
                }
                else
                {

                    //change Tag
                    this.gameObject.tag = "DeadHero";
                    //not manageable
                    BSM.HeroesToManage.Remove(this.gameObject);
                    BSM.HeroInBattle.Remove(this.gameObject);
                    //deactive selector
                    selector.SetActive(false);
                    //reset GUI
                    BSM.AttackPanel.SetActive(false);
                    BSM.EnemySelectPanel.SetActive(false);
                    //remove item from perfomlist
                    for (int i = 0; i < BSM.PerformList.Count; i++)
                    {
                        if (BSM.PerformList[i].AttacksGameObject == this.gameObject)
                        {
                            BSM.PerformList.Remove(BSM.PerformList[i]);
                        }
                    }
                    //Animation & selector change
                    animator.SetTrigger("Dead");
                    //reset heroInput
                    BSM.HeroInput = BattleManager.HeroGUI.ACTIVATE;

                    alive = false;
                    BSM.battleState = BattleManager.PerformAction.CHECKALIVE;
                }
                break;

        }
    }

    public void Damaged(float damage)
    {
        Hurted.Play();
        animator.SetTrigger("Hurt");
        HP = HP - damage;
        ArcherBase.HP = HP;

        if (HP <= 0)
        {
            currState = TurnState.DEAD;
            ArcherBase.HP = 0;
        }
        healthBar.SetHealth(HP);
    }


    void Spawn()
    {
        characterName = ArcherBase.characterName;
        HP = ArcherBase.HP;
        MaxHP = ArcherBase.MaxHP;
        ATK = ArcherBase.ATK;
        DEF = ArcherBase.DEF;
        MAG = ArcherBase.MAG;
    }

    void UpdateStats()
    {
        ArcherBase.characterName = characterName;
        ArcherBase.HP = HP;
        ArcherBase.MaxHP = MaxHP;
        ArcherBase.ATK = ATK;
        ArcherBase.DEF = DEF;
        ArcherBase.MAG = MAG;
    }

    void UpgradeProgressBar()
    {

        cur_cooldown = cur_cooldown + Time.deltaTime;
        float calc_cooldown = cur_cooldown / max_cooldown;
        ProgressBar.transform.localScale = new Vector3(Mathf.Clamp(calc_cooldown, 0, 1), ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z);
        if (cur_cooldown >= max_cooldown)
        {
            currState = TurnState.ADDTOLIST;
        }

    }
    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        //animasi sliding ke hero


        //wait
        yield return new WaitForSeconds(0.5f);
        shooting.Play();
        animator.SetTrigger("Atk");
        doDamage();
        //animasi kembali ke posisi awal
        Vector3 firstPosition = startPosition;


        //Hapus fungsi ini dari list BSM
        BSM.PerformList.RemoveAt(0);
        //reset BSM ke Wait
        BSM.battleState = BattleManager.PerformAction.WAIT;

        actionStarted = false;

        //reset state
        cur_cooldown = 0f;
        currState = TurnState.PROCESSING;
    }

    private bool MoveTowardsEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
    private bool MoveTowardsStarts(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
    void doDamage()
    {
        float calc_damage = ArcherBase.ATK + BSM.PerformList[0].choosenAttack.attackDamage;
        EnemyToAttack.GetComponent<Enemy>().Damaged(calc_damage);
    }
}
