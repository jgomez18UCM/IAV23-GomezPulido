using BehaviorDesigner.Runtime.Tactical.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGActor : MonoBehaviour
{

    protected bool onTurn;
    protected bool startTurn;

    [SerializeField]
    protected AttackAction attack;

    public AttackAction Attack
    {
        get { return attack; }
       
    }

    [SerializeField]
    protected HealAction heal;

    public HealAction Heal
    {
        get { return heal; }
       
    }

    [SerializeField]
    protected BuffAction buff;

    public BuffAction Buff
    {
        get { return buff; }
    }

    [SerializeField]
    protected DefendAction defend;

    public DefendAction Defend
    {
        get { return defend; }
      
    }

    protected RPGAction selectedAction;

    [SerializeField]
    protected int maxHealth = 100;

    [SerializeField]
    protected string actorName;   
    public string Name
    {
        get { return actorName; }
        set { name = value; }
    }

    protected RPGActor target = null;

    [SerializeField]
    protected int health = 100;

    [SerializeField]
    protected HPBar healthBar;

    public delegate void EndTurnFunc();
    public EndTurnFunc OnEndTurn; 

    public delegate void StartTurnFunc();
    public StartTurnFunc OnStartTurn;

    protected virtual void Awake()
    {
        onTurn = false;
        startTurn = false;
       // health = maxHealth;
        OnEndTurn += DoOnEndTurn;
        OnStartTurn += StartTurn;
    }

    protected virtual void Start()
    {
        if (healthBar != null)
        {
            healthBar.SetActor(this);
        }
    }

    void StartTurn()
    {
        onTurn = true;
        StartCoroutine(EndTurn());
        Debug.Log("Empezando Turno: " + gameObject.name);
    }


    public bool IsOnTurn()
    {
        return onTurn;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
            Die();
        }
        StartCoroutine(healthBar.SetDamage(health));
    }

    public void TakeHeal(int restored)
    {
        health += restored;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        StartCoroutine(healthBar.SetDamage(health));
    }

    void Die()
    {
        gameObject.SetActive(false);
        RPGManager.GetInstance().KillCharacter(this);
        
    }

    IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(3);
        onTurn = false;
        RPGManager.GetInstance().EndActualTurn();
    }

    public void SetSelect(bool select)
    {
        healthBar.SetSelected(select);
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetTarget(RPGActor other)
    {
        target = other;
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public virtual void DoOnEndTurn()
    {
        onTurn = false;
        startTurn = false;
        target = null;
        selectedAction = null;
    }

    public void SelectAction(ActionType act)
    {
        switch (act)
        {
            case ActionType.Heal:
                selectedAction = heal;
                break;
            case ActionType.Attack:
                selectedAction = attack;
                break;
            case ActionType.Defend:
                selectedAction = defend;
                break;
            case ActionType.Buff:
                selectedAction = buff;
                break;
            default:
                break;
        }
    }

    public void DoSelectedAction()
    {
        if (target != null && selectedAction != null) selectedAction.ExecuteAction(target);
        else if (target == null) Debug.LogError($"No selected target for: {actorName}'s action");
        else if (selectedAction == null) Debug.LogError($"No selected action for {actorName}'s turn");

    }

}


