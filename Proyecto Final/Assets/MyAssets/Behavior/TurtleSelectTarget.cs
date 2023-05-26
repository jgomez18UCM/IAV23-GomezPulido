using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem;

public class TurtleSelectTarget : Action
{
	RPGManager rpgMan;
	List<RPGActor> heroes;
	RPGActor myActor;

	[SerializeField]
	SharedGameObject thisGO;
	public override void OnStart()
	{
		rpgMan = RPGManager.GetInstance();
		heroes = rpgMan.GetHeroes();
        myActor = GetComponent<RPGActor>();
    }

	public override TaskStatus OnUpdate()
	{
		Debug.Log("Seleccionando...");
		//Detect Health, if <10%, heal
		float actualHealth = myActor.GetHealth();
		if (actualHealth < 10) {
			myActor.SelectAction(ActionType.Heal);
			myActor.SetTarget(myActor);
			return TaskStatus.Success;
		}

		//Will not heal, instead will attack
		myActor.SelectAction(ActionType.Attack);
        //Target selection
        return SelectTarget();
		
		
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