using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : RPGActor
{
    enum State
    {
        SelectingAction,
        SelectingTarget,
        ExecutingAction, 
        Idle,
        Waiting
    }

    RPGManager man;

    int selectionIndex = 0;
    State turnState = State.Idle;

    List<RPGActor> forSelection = new List<RPGActor>();

    protected override void Awake()
    {
        base.Awake();
        OnStartTurn += StartTurn;
    }

    protected override void Start()
    {
        base.Start();
        man = RPGManager.GetInstance();
    }
    // Update is called once per frame
    void Update()
    {
        switch (turnState)
        {
            case State.SelectingAction:
                SelectAction();
                break;
            case State.SelectingTarget:
                SelectTarget();
                break;
            case State.ExecutingAction:
                DoSelectedAction();
                target.SelectAsTarget(false);
                turnState = State.Waiting;
                break;
            case State.Waiting:
                if (!HealthBarNotification) break;
                man.EndActualTurn();
                turnState = State.Idle;
                break;
            default: break;
        }
    }

    void StartTurn()
    {
        man.SetCommands(true);
        turnState = State.SelectingAction;
        forSelection.Clear();
        selectionIndex= 0;
    }

    void SelectAction()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectAction(ActionType.Attack);
            forSelection.AddRange(man.GetEnemies());
            turnState = State.SelectingTarget;
            man.SetCommands(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectAction(ActionType.Heal);
            forSelection.AddRange(man.GetHeroes());
            turnState = State.SelectingTarget;
            man.SetCommands(false);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectAction(ActionType.Buff);
            forSelection.AddRange(man.GetHeroes());
            turnState = State.SelectingTarget;
            man.SetCommands(false);
        }     
    }

    void SelectTarget()
    {
        if (forSelection.Count < 0) OnEndTurn();
        if(Input.GetKeyDown(KeyCode.Escape)) {
            turnState = State.SelectingAction;
            man.SetCommands(true);
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            forSelection[selectionIndex].SelectAsTarget(false);
            selectionIndex++;
            if(selectionIndex >= forSelection.Count)
            {
                selectionIndex= 0;
            }
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            forSelection[selectionIndex].SelectAsTarget(false);
            selectionIndex--;
            if (selectionIndex < 0)
            {
                selectionIndex = forSelection.Count-1;
            }
        }

        forSelection[selectionIndex].SelectAsTarget(true);

        if(forSelection.Count > 0)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                target = forSelection[selectionIndex];
                turnState = State.ExecutingAction;
            }
        } 
    }
}
