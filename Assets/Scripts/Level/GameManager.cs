using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _player1;
    [SerializeField] GameObject _player2;
    public GameObject _activePlayer;

    //set active player
    private void Awake()
    {
        _activePlayer = _player1;
        
    }
    //manage turns

    //hitpoints? pickups? etc.

    ///playerprefs?
}
