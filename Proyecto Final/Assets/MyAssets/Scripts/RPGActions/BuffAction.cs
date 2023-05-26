using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RPGActions/Buff", order = 4)]
[System.Serializable]
public class BuffAction : RPGAction
{
    [SerializeField]
    int value = 2;

    [SerializeField]
    BuffType type = BuffType.Damage;

    [SerializeField]
    int duration = 3;
    public override void ExecuteAction(RPGActor target, List<Buff> buffs, RPGActor doer)
    {
        Buff b = new Buff(type, value, duration);
        target.AddBuff(b);
        doer.HealthBarNotification = true;
    }

    public Buff GetBuff()
    {
        return new Buff(type, value, duration);
    }
}
