using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class RPGAction :ScriptableObject
{
    public abstract void ExecuteAction(RPGActor target, List<Buff> buffs);
   
}

public enum ActionType
{
    Attack,
    Heal,
    Defend,
    Buff
}