using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RPGActions/Attack", order = 4)]
[System.Serializable]
public class AttackAction : RPGAction
{
    [SerializeField]
    int damage;

    public override void ExecuteAction(RPGActor target, List<Buff> buffs, RPGActor doer)
    {

        int finalDamage = damage;

        foreach (Buff buff in buffs)
        {
            if(buff.Type == BuffType.Damage)
            {
                finalDamage += buff.Value;
            }
        }

        if(finalDamage < 0) {
            finalDamage = 0;
        }

        target.TakeDamage(finalDamage, doer);
    }
}
