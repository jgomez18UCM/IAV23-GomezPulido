using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void Start()
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

       
    }

    private void Update()
    {
        if(actualTurn == null)
        {
            ChangeTurn();

        }
    }

    public void ChangeTurn()
    {
        actualTurn = turnQueue.Dequeue();
        actualTurn.OnStartTurn();
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
        actualTurn.OnEndTurn();
        ChangeTurn();
    }

    public List<RPGActor> GetEnemies()
    {
        return enemies;
    }

    public List<RPGActor> GetHeroes()
    {
        return heroes;

    }
    public void KillCharacter(RPGActor actor)
    {
        Queue<RPGActor> aux = new Queue<RPGActor>();
        foreach(RPGActor ac in turnQueue)
        {
            turnQueue.Dequeue();
            if (ac != actor)
            {
                aux.Enqueue(ac);
            }
        }
        turnQueue = aux;

        enemies.Remove(actor);
        heroes.Remove(actor);

        if(actor == actualTurn)
        {
            actualTurn.SetSelect(false);
            actualTurn.OnEndTurn();
            ChangeTurn();
        }

        //if no heroes or no enemies remain, END GAME
    }

    public void RegisterHero(RPGActor hero)
    {
        if(!heroes.Contains(hero)) heroes.Add(hero);
    }

    public void RegisterEnemy(RPGActor enemy) {
        if (!enemies.Contains(enemy)) enemies.Add(enemy);
    }
}
