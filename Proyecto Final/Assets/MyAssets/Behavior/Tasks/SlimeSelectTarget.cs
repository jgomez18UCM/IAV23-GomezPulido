using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SlimeSelectTarget : Action
{
    RPGManager rpgMan;
    List<RPGActor> heroes;
    List<RPGActor> enemies;
    RPGActor myActor;
    RPGActor buffTarget;
    
    [SerializeField]
    SharedGameObject thisGO;
    public override void OnStart()
    {
        rpgMan = RPGManager.GetInstance();
        heroes = rpgMan.GetHeroes();
        enemies = rpgMan.GetEnemies();
        myActor = GetComponent<RPGActor>();
    }

    public override TaskStatus OnUpdate()
    {
        Debug.Log("Seleccionando...");
        //Detect Buff, if there's allies and there's an unbuffed ally, buff them
        if (enemies.Count > 1)
        {
            bool buff = SelectBuffTarget();
            if (buff)
            {
                return TaskStatus.Success;
            }
        }

        //Will not heal, instead will attack
        myActor.SelectAction(ActionType.Attack);
        //Target selection
        return SelectTarget();


    }

    bool SelectBuffTarget()
    {
        bool selected = false;
        int i = 0;
        while(!selected && i < enemies.Count)
        {
            RPGActor enemy = enemies[i];
            if (enemy != myActor)
            {
                if (!enemy.HasBuff(myActor.Buff.GetBuff()))
                {
                    selected = true;
                    myActor.SetTarget(enemy);
                    myActor.SelectAction(ActionType.Buff);
                    Debug.Log($"Buffing {enemy.Name}");
                }
            }
            i++;
        }
        return selected;
    }

    TaskStatus SelectTarget()
    {
        List<RPGActor> lessHealth = new List<RPGActor>();
        float actualLessHealth = 100;
        if (heroes.Count == 0)
        {
            Debug.LogError("NO HEROES TO TARGET ON TURTLE'S ATTACK");
            return TaskStatus.Failure;
        }
        for (int i = 0; i < heroes.Count; i++)
        {
            if (heroes[i].GetHealth() < actualLessHealth && heroes[i].IsAlive())
            {
                lessHealth.Clear();
                lessHealth.Add(heroes[i]);
                actualLessHealth = heroes[i].GetHealth();
            }
            else if (heroes[i].GetHealth() == actualLessHealth && heroes[i].IsAlive())
            {
                lessHealth.Add(heroes[i]);
            }
        }
        RPGActor target = lessHealth[Random.Range(0, lessHealth.Count)];
        myActor.SetTarget(target);
        return TaskStatus.Success;
    }
}