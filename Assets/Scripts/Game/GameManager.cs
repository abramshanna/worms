using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    CameraController CameraController;
    public GameObject _activePlayer;
    GameObject[] _players;
    int _indexActivePlayer = 0;

    //set active player and camera
    private void Start()
    {
        CameraController = FindObjectOfType<CameraController>();
        _players = GameObject.FindGameObjectsWithTag("Player");
        _activePlayer = _players[0];
        CameraController.SetActivePlayerPosition();
    }

    //turns logic then call _changeActivePlayer

    //change active player
    public void ChangeActivePlayer()
    {
        //change active player in array
        _indexActivePlayer++;

        if (_indexActivePlayer > _players.Length) {
            _indexActivePlayer = 0;
        }
        _activePlayer = _players[_indexActivePlayer];

        //call function to set player position for camera
        CameraController.SetActivePlayerPosition();
    }

    //hitpoints? pickups? etc.

    ///playerprefs?
}
