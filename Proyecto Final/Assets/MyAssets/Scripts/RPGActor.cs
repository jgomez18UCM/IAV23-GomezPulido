using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGActor : MonoBehaviour
{

    bool onTurn;
    bool startTurn;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (startTurn)
        {
            
            startTurn = false;
            Debug.Log("Empezando Turno");
            return;
        }
        if(onTurn)
        {
            EndTurn();
        }
    }

    public void GiveTurn()
    {
        startTurn = true;
    }

    public bool IsOnTurn()
    {
        return onTurn || startTurn;
    }
    void EndTurn()
    {
        onTurn= false;
    }
}
