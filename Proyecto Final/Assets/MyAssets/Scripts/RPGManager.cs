using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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

    [SerializeField]
    TMP_Text HeroesWin;

    [SerializeField]
    TMP_Text EnemiesWin;

    [SerializeField]
    TMP_Text Commands;
    Queue<RPGActor> turnQueue;

    RPGActor actualTurn;

    static RPGManager instance;

    bool ended = false;


    private void Awake()
    {        
        if(instance == null)
        {
            instance = this;
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
        if(actualTurn == null && !ended)
        {
            ChangeTurn();

        }
    }

    public void SetCommands(bool s)
    {
        Commands.gameObject.SetActive(s);
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
        if (!ended) ChangeTurn();
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
        for(int i = 0; i< turnQueue.Count; i++)
        {
            RPGActor ac = turnQueue.Dequeue();
            if (ac != actor)
            {
                aux.Enqueue(ac);
            }
        }
        turnQueue = aux;

        enemies.Remove(actor);
        heroes.Remove(actor);

        //if no heroes or no enemies remain, END GAME
        if (enemies.Count == 0)
        {
            HeroesWin.gameObject.SetActive(true);
            actualTurn.SetSelect(false);
            ended = true;
        }
        if(heroes.Count == 0)
        {
            EnemiesWin.gameObject.SetActive(true);
            actualTurn.SetSelect(false);
            ended= true;
        }
               
    }

    public void RegisterHero(RPGActor hero)
    {
        if(!heroes.Contains(hero)) heroes.Add(hero);
    }

    public void RegisterEnemy(RPGActor enemy) {
        if (!enemies.Contains(enemy)) enemies.Add(enemy);
    }

    public void Reload()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying= false;
#endif
    }
}
