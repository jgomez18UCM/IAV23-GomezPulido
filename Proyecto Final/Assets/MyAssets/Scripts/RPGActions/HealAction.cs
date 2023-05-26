using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RPGActions/Heal", order = 4)]
[System.Serializable]
public class HealAction : RPGAction
{
    [SerializeField]
    int value;

    public override void ExecuteAction(RPGActor target, List<Buff> buffs)
    {
       target.TakeHeal(value);
    }
}