using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EndTurn : Action
{
	[SerializeField]
	SharedGameObject go;

    RPGActor actor;
    public override void OnStart()
    {
        base.OnStart();
        actor = go.Value.GetComponent<RPGActor>();
    }

    public override TaskStatus OnUpdate()
	{
        RPGManager.GetInstance().EndActualTurn();
		return TaskStatus.Success;
	}
}

