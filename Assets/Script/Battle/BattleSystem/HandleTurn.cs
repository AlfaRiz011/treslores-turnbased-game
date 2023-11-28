using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HandleTurn
{
        public string Attacker; //Nama Yang nyerang
        public string Type;
        public GameObject AttacksGameObject; //Objek yang Nyerang
        public GameObject AttackerTarget; // Objek yang diserang

        public BaseAttack choosenAttack;
        
}
