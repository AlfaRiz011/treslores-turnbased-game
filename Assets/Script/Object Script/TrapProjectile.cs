using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapProjectile : MonoBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall 2"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Warrior")
        {
            WarriorDungeon WD = collision.gameObject.GetComponent<WarriorDungeon>();
            WD.animator.SetTrigger("Hurt");
            Destroy(gameObject);
            WD.Damaged(damage);
           
        }

        if (collision.gameObject.tag == "Archer")
        {
            ArcherDungeon AD = collision.gameObject.GetComponent<ArcherDungeon>();
            AD.animator.SetTrigger("Hurt");
            Destroy(gameObject);
            AD.Damaged(damage);
        
        }

        if (collision.gameObject.tag == "Magician")
        {
            MagicianDungeon MD = collision.gameObject.GetComponent<MagicianDungeon>();
            MD.animator.SetTrigger("Hurt");
            Destroy(gameObject);
            MD.Damaged(damage);
          
        }
    }
}
