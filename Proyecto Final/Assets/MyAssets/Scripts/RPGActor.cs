using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGActor : MonoBehaviour
{

    bool onTurn;
    bool startTurn;
    bool turnEnded;

    [SerializeField]
    float maxHealth = 100.0f;

    [SerializeField]
    string actorName;

    public string Name
    {
        get { return actorName; }
        set { name = value; }
    }

    float health;

    [SerializeField]
    HPBar healthBar;

    private void Awake()
    {
        onTurn = false;
        startTurn = false;
        turnEnded = false;
        health = maxHealth;

    }

    private void Start()
    {
        if(healthBar!= null) healthBar.SetName(actorName);
    }
    // Update is called once per frame
    void Update()
    {
        if (startTurn)
        {
            startTurn = false;
            onTurn = true;
           
            StartCoroutine(EndTurn());
            Debug.Log("Empezando Turno: " + gameObject.name);
        }
    }

    public void GiveTurn()
    {
        if (!onTurn) startTurn = true;

    }

    public bool IsOnTurn()
    {
        return onTurn || startTurn;
    }

    public bool EndedTurn()
    {
        return turnEnded;
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
}