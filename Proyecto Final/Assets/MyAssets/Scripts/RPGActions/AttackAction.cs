using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RPGActions/Attack", order = 4)]
[System.Serializable]
public class AttackAction : RPGAction
{
    [SerializeField]
    int damage;

    public override void ExecuteAction(RPGActor target)
    {
        target.TakeDamage(damage);
    }
}
