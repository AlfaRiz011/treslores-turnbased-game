using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherDungeon : MonoBehaviour
{
    [SerializeField] public BasePlayer ArcherBase;
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

    public void Damaged(float damage)
    {
        Hurted.Play();
        HP = HP - damage;
        ArcherBase.HP = HP;
        healthBar.SetHealth(ArcherBase.HP);
        healthBar_follower1.SetHealth(ArcherBase.HP);
        healthBar_follower2.SetHealth(ArcherBase.HP);
    }
}
