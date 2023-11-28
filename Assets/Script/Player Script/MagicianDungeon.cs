using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianDungeon : MonoBehaviour
{
    [SerializeField] public BasePlayer MagicianBase;
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
        characterName = MagicianBase.characterName;
        HP = MagicianBase.HP;
        MaxHP = MagicianBase.MaxHP;
        ATK = MagicianBase.ATK;
        DEF = MagicianBase.DEF;
        MAG = MagicianBase.MAG;
    }

    void UpdateStats()
    {
        MagicianBase.characterName = characterName;
        MagicianBase.HP = HP;
        MagicianBase.MaxHP = MaxHP;
        MagicianBase.ATK = ATK;
        MagicianBase.DEF = DEF;
        MagicianBase.MAG = MAG;
    }

    public void Damaged(float damage)
    {
        Hurted.Play();
        HP = HP - damage;
        MagicianBase.HP = HP;
        healthBar.SetHealth(MagicianBase.HP);
        healthBar_follower1.SetHealth(MagicianBase.HP);
        healthBar_follower2.SetHealth(MagicianBase.HP);
    }

    
}
