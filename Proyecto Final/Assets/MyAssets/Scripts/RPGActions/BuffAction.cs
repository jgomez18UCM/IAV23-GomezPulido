using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RPGActions/Buff", order = 4)]
[System.Serializable]
public class BuffAction : RPGAction
{
    [SerializeField]
    int buff;

    public override void ExecuteAction(RPGActor target)
    {
        // target.TakeDamage(damage);
    }
}
