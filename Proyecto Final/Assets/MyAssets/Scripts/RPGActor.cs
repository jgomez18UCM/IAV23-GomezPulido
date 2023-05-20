using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGActor : MonoBehaviour
{

    bool onTurn;
    bool startTurn;
    bool turnEnded;
    private void Awake()
    {
        onTurn = false;
        startTurn = false;
        turnEnded = false;
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
        if(!onTurn) startTurn = true;
        
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
}
