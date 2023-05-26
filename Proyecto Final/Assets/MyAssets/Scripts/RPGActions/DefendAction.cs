using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(menuName ="RPGActions/Defend", order =4)]
public class DefendAction : RPGAction
{
    [SerializeField]
    int damageCovered;

    public override void ExecuteAction(RPGActor target, List<Buff> buffs, RPGActor doer)
    {
        // target.TakeDamage(damage);
    }
}