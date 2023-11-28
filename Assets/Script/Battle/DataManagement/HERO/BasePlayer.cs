using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BasePlayer", menuName ="BasePlayer")]

public class BasePlayer : ScriptableObject
{
     public string characterName;
     public float HP, MaxHP, MAG, ATK, DEF;

     public List<BaseAttack> attacks= new List<BaseAttack>();

}
