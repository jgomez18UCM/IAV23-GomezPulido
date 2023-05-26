using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : RPGActor
{
    BehaviorTree tree;

    protected override  void Awake()
    {
        base.Awake();
        OnStartTurn = StartTree;
    }
    protected override void Start()
    {
        base.Start();
        tree = GetComponent<BehaviorTree>();
        tree.enabled = false;       
    }
    public void StartTree()
    {
        onTurn = true;
        if(tree == null) tree = GetComponent<BehaviorTree>();  
        tree.enabled = true;
        tree.EnableBehavior();
        Debug.Log("Empezando Turno: " + gameObject.name);
    }
}
