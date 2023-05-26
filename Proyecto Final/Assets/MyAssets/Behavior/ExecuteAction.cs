using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ExecuteAction : Action
{
	RPGActor actor;
    public override void OnStart()
    {
        actor = GetComponent<RPGActor>();
    }
    public override TaskStatus OnUpdate()
	{
        Debug.Log("Atacando...");
        actor.DoSelectedAction();
		return TaskStatus.Success;
	}
}