using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class RPGManager : MonoBehaviour
{
    [SerializeField]
    List<RPGActor> heroes;

    [SerializeField]
    List<RPGActor> enemies;

    
    Queue<RPGActor> turnQueue;

    RPGActor actualTurn;

    static RPGManager instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        turnQueue = new Queue<RPGActor>();
    }

    private void Start()
    {
        List<RPGActor> all = new List<RPGActor>();
        
        all.AddRange(heroes);
        all.AddRange(enemies);

        int actors = all.Count;
        for(int i = 0; i < actors; i++)
        {
            RPGActor actor = all[Random.Range(0, all.Count)];
            all.Remove(actor);
            turnQueue.Enqueue(actor);
        }

        actualTurn = turnQueue.Dequeue();
    }

    // Update is called once per frame
    void Update()
    {
        if(actualTurn == null)
        {
            actualTurn= turnQueue.Dequeue();
        }

        if (actualTurn.EndedTurn())
        {
            turnQueue.Enqueue(actualTurn);
            actualTurn = turnQueue.Dequeue();            
        }
        else if (!actualTurn.IsOnTurn())
        {
            actualTurn.GiveTurn();
        }
        

    }

    static RPGManager GetInstance()
    {
        if(instance == null)
        {
            GameObject go = new GameObject();
            instance = go.AddComponent<RPGManager>();
        }

        return instance;
    }
}
