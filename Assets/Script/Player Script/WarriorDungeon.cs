using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorDungeon : MonoBehaviour
{
    [SerializeField] public BasePlayer WarriorBase;
    public string characterName;
    public float HP, MaxHP, MAG, ATK, DEF;
    public Animator animator;
    public HealthBar healthBar;
    public HealthBar healthBar_follower1;
    public HealthBar healthBar_follower2;
    public AudioSource Hurted;

    void Start()
    {
        Spawn();
        UpdateStats();
        healthBar.SetMaxHealth(MaxHP);
        healthBar.SetHealth(HP);
        healthBar_follower1.SetMaxHealth(MaxHP);
        healthBar_follower1.SetHealth(HP);
        healthBar_follower2.SetMaxHealth(MaxHP);
        healthBar_follower2.SetHealth(HP);
    }

    void Update()
    {
        Spawn();
        UpdateStats();
        healthBar.SetHealth(HP);
    }

    void Spawn()
    {
        characterName = WarriorBase.characterName;
        HP = WarriorBase.HP;
        MaxHP = WarriorBase.MaxHP;
        ATK = WarriorBase.ATK;
        DEF = WarriorBase.DEF;
        MAG = WarriorBase.MAG;
    }

    void UpdateStats()
    {
        WarriorBase.characterName = characterName;
        WarriorBase.HP = HP;
        WarriorBase.MaxHP = MaxHP;
        WarriorBase.ATK = ATK;
        WarriorBase.DEF = DEF;
        WarriorBase.MAG = MAG;
    }

    public void Damaged(float damage)
    {
        Hurted.Play();
        HP = HP - damage;
        WarriorBase.HP = HP;
        healthBar.SetHealth(WarriorBase.HP);
        healthBar_follower1.SetHealth(WarriorBase.HP);
        healthBar_follower2.SetHealth(WarriorBase.HP);
    }

    
}
