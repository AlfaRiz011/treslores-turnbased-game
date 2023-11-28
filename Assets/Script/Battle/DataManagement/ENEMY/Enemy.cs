using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public HealthBar healthBar;
    public GameObject Warrior;
    public GameObject Archer;
    public GameObject Magician;
    private BattleManager BSM;
    [SerializeField] public BasePlayer EnemyBase;
    public string characterName;
    public float HP, MaxHP, MAG, ATK, DEF;
    public List<BaseAttack> myAttacks = new List<BaseAttack>();

    
    private float cur_cooldown = 0f;
    private float max_cooldown = 10f;
    public GameObject selector;

    private Vector3 startPosition;

    private bool actionStarted = false;

    private bool alive = true;

    public Sprite deadSprite;  
    private SpriteRenderer spriteRenderer;

    public GameObject HeroToAttack;
    private float animSpeed = 10f;
    public enum TurnState
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,

        ACTION,
        DEAD
    }

    public TurnState currState;

    public AudioSource Hurted;
    public AudioSource Slash;

    void Start()
    {

        Spawn();
        UpdateStats();
        healthBar.SetMaxHealth(EnemyBase.MaxHP);
        currState = TurnState.PROCESSING;
        selector.SetActive(false);
        BSM = GameObject.Find("BattleManagement").GetComponent<BattleManager>();
        startPosition = transform.position;
        
    }

    void Update()
    {
        Spawn();
        UpdateStats();
        healthBar.SetHealth(HP);
        switch (currState)
        {
            case (TurnState.PROCESSING):
                UpgradeProgressBar();
                break;
            case (TurnState.CHOOSEACTION):
                ChooseAction();
                currState = TurnState.WAITING;
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
                    //change tag
                    this.gameObject.tag = "DeadEnemy";

                    //not attackable
                    BSM.EnemyInBattle.Remove(this.gameObject);
                    //disableselector
                    selector.SetActive(false);


                    for (int i = 0; i < BSM.PerformList.Count; i++)
                    {

                        if (BSM.PerformList[i].AttacksGameObject == this.gameObject)
                        {
                            BSM.PerformList.Remove(BSM.PerformList[i]);
                        }
                    }
                    //animasi mati
                    animator.SetTrigger("Dead");
                  
                     

                    alive = false;

                    BSM.EnemyButtons();

                    BSM.battleState = BattleManager.PerformAction.CHECKALIVE;

                }
                break;

        }
    }

    void Spawn()
    {
        characterName = EnemyBase.characterName;
        HP = EnemyBase.HP;
        MaxHP = EnemyBase.MaxHP;
        ATK = EnemyBase.ATK;
        DEF = EnemyBase.DEF;
        MAG = EnemyBase.MAG;
    }

    void UpdateStats()
    {
        EnemyBase.characterName = characterName;
        EnemyBase.HP = HP;
        EnemyBase.MaxHP = MaxHP;
        EnemyBase.ATK = ATK;
        EnemyBase.DEF = DEF;
        EnemyBase.MAG = MAG;
    }

    void UpgradeProgressBar()
    {

        cur_cooldown = cur_cooldown + Time.deltaTime;

        if (cur_cooldown >= max_cooldown)
        {
            currState = TurnState.CHOOSEACTION;
        }
    }

    void ChooseAction()
    {

        if(BSM.HeroInBattle.Count > 0){
        HandleTurn myAttack = new HandleTurn();
        myAttack.Attacker = EnemyBase.characterName;
        myAttack.Type = "Enemy";
        myAttack.AttacksGameObject = this.gameObject;
        int num = Random.Range(0, EnemyBase.attacks.Count);
        myAttack.choosenAttack = EnemyBase.attacks[num];
        //Choose

        myAttack.AttackerTarget = BSM.HeroInBattle[Random.Range(0, BSM.HeroInBattle.Count)];

        BSM.CollectActions(myAttack);

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
        Vector3 heroPosition = new Vector3(HeroToAttack.transform.position.x + 1.5f, HeroToAttack.transform.position.y, HeroToAttack.transform.position.z);
        while (MoveTowardsEnemy(heroPosition))
        {
            yield return null;
        }

        //wait
        yield return new WaitForSeconds(0.5f);

        //do damage
        Slash.Play();
        animator.SetTrigger("Atk");
        doDamage();

        //animasi kembali ke posisi awal
        Vector3 firstPosition = startPosition;
        while (MoveTowardsStarts(firstPosition))
        {
            yield return null;
        }

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
        float calc_damage = ATK + BSM.PerformList[0].choosenAttack.attackDamage;
        if (BSM.PerformList[0].AttackerTarget == Warrior)
        {
            HeroToAttack.GetComponent<WarriorState>().Damaged(calc_damage);
        }
        else if (BSM.PerformList[0].AttackerTarget == Archer)
        {
            HeroToAttack.GetComponent<ArcherState>().Damaged(calc_damage);
        }
        else if (BSM.PerformList[0].AttackerTarget == Magician)
        {
            HeroToAttack.GetComponent<MagicianState>().Damaged(calc_damage);
        }
    }
    public void Damaged(float damage)
    {

        HP = HP - damage;
        EnemyBase.HP = HP;
        Hurted.Play();
        animator.SetTrigger("Hurt");
        if (HP <= 0)
        {
            EnemyBase.HP = 0;
            currState = TurnState.DEAD;
        }
        healthBar.SetHealth(HP);
    }
}
