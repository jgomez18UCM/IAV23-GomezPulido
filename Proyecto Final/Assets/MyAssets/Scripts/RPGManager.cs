using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class RPGManager : MonoBehaviour
{
    [SerializeField]
    List<RPGActor> heroes;

    [SerializeField]
    List<RPGActor> enemies;

    [SerializeField]
    TMP_Text turnText;
    
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
        actualTurn.GiveTurn();
        actualTurn.SetSelect(true);
        turnText.SetText(actualTurn.Name + "'s Turn");
    }

    public static RPGManager GetInstance()
    {
        if(instance == null)
        {
            GameObject go = new GameObject();
            instance = go.AddComponent<RPGManager>();
        }

        return instance;
    }

    public void EndActualTurn()
    {
        turnQueue.Enqueue(actualTurn);
        actualTurn.SetSelect(false);
        actualTurn = turnQueue.Dequeue();
        actualTurn.GiveTurn();
        turnText.SetText(actualTurn.Name + "'s Turn");
        actualTurn.SetSelect(true);
    }

    public List<RPGActor> GetEnemies()
    {
        return enemies;
    }

    public List<RPGActor> GetHeroes()
    {
        return heroes;

    }
}
