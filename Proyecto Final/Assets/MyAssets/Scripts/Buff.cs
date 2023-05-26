using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BuffType
{
    Damage
}
public class Buff
{
    BuffType type;

    public BuffType Type { get { return type; } }

    int value;
    public int Value { get { return value; } }

    int duration;
    public int Duration { get { return duration; } }

    public Buff(BuffType type, int value, int duration)
    {
        this.type = type;
        this.value = value;
        this.duration = duration;
    }

    public void OnTurnEnd()
    {
        Debug.Log($"Buff value: {value} -1 turn.");
        duration--;
    }

    public void AddDuration(int duration)
    {
        this.duration += duration;
    }

    public static bool operator ==(Buff a, Buff b)
    {
        if (ReferenceEquals(a, b)) 
            return true;
        if (ReferenceEquals(b, null))
            return false;
        if (ReferenceEquals(a, null))
            return true;
        return  a.Equals(b);
    }
    
    public static bool operator !=(Buff a, Buff b)
    {
        return !(a == b);
    }

    public  bool Equals(Buff obj)
    {
        if (ReferenceEquals(obj, null))
            return false;
        if (ReferenceEquals(obj, this)) 
            return true;
        return obj.value == value && obj.type == type;
    }
    public override bool Equals(object obj)
    {
        return this.Equals(obj as Buff);
    }
}
