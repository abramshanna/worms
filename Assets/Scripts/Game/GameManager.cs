using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController activePlayer;
    List<PlayerController> players = new List<PlayerController>();
    int activePlayerIndex;
    [SerializeField] GameObject playerPrefab;
    float waitUntil;

    public GameState gameState;
    public enum GameState
    {
        Menu,
        Pregame,
        Preturn,
        Turn,
        Postturn,
        Postgame
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            GameObject.Destroy(this.gameObject);
            return;
        }
    }

    public void StartNewGame(int playerCount)
    {
        //if in any other state than menu, don't start new game
        if (gameState != GameState.Menu)
        {
            return;
        }

        //destroy players in list if any
        foreach (var player in players)
        {
            Destroy(player);
        }

        //make sure list is clear
        players.Clear();

        //put playerspawnpoints in list
        var playerSpawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint").ToList();

        if (playerSpawnPoints.Count < playerCount)
        {
            //more players than spawnpoints don't start game
            return;
        }
        
        for (int i = 0; i < playerCount; i++)
        {
            //get random spawnpoint from list
            int randomInt = UnityEngine.Random.Range(0, playerSpawnPoints.Count);
            var playerSpawnPoint = playerSpawnPoints.ElementAt(randomInt).gameObject.transform;
            
            //remove spawnpoint from list
            playerSpawnPoints.RemoveAt(randomInt);

            //use spawnpoint to spawn player
            var newPlayer = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);

            //add player to list players
            players.Add(newPlayer.GetComponent<PlayerController>());
        }

        activePlayer = players[0];

        gameState = GameState.Pregame;
    }

    public void ChangeActivePlayer()
    {
        //save player for check
        var previousPlayer = players[activePlayerIndex];

        while (true)
        {
            //next player
            activePlayerIndex++;

            //if index out of bounds back to 0
            if (activePlayerIndex >= players.Count) activePlayerIndex = 0;

            //if player not dead, set to active player
            if (players[activePlayerIndex].gameObject.activeSelf)
            {
                activePlayer = players[activePlayerIndex];

                //if one player left, player is same as previous player, end game
                if (activePlayer == previousPlayer)
                {
                    gameState = GameState.Postgame;
                    return;
                }

                gameState = GameState.Preturn;
                break;
            }  
            
        }


    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Menu:
                //start game with number of players
                StartNewGame(MainMenu.playerCount);
                break;
            case GameState.Pregame:
                gameState = GameState.Preturn;
                break;
            case GameState.Preturn:
                if (waitUntil <= Time.time)
                {
                    activePlayer.StartTurn();
                    gameState = GameState.Turn;
                    waitUntil = Time.time + 15f;
                }
                break;
            case GameState.Turn:
                if (activePlayer.finishedTurn)
                {
                    gameState = GameState.Postturn;
                    waitUntil = Time.time + 2f;
                }
                if (waitUntil <= Time.time) //turn out of time
                {
                    gameState = GameState.Postturn;
                    waitUntil = Time.time + 2f;
                }
                break;
            case GameState.Postturn:
                if (waitUntil <= Time.time)
                {
                    ChangeActivePlayer();
                    waitUntil = Time.time + 5f;
                }
                break;
            case GameState.Postgame:
                gameState = GameState.Menu;
                SceneManager.LoadScene(2);
                break;
        }
    }

    //playerprefs?
}
