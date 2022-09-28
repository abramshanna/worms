using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    CameraController _cameraController;
    public GameObject _activePlayer;
    GameObject[] _players;
    int _indexActivePlayer = 0;

    //set active player and camera
    private void Start()
    {
        _cameraController = FindObjectOfType<CameraController>();
        _players = GameObject.FindGameObjectsWithTag("Player");
        _activePlayer = _players[0];
        _cameraController.SetActivePlayerPosition();
    }

    //change active player
    public void ChangeActivePlayer()
    {
        //change active player in array
        _indexActivePlayer++;

        if (_indexActivePlayer >= _players.Length) {
            _indexActivePlayer = 0;
        }

        _activePlayer = _players[_indexActivePlayer];

        //call function to set player position for camera
        _cameraController.SetActivePlayerPosition();
    }

    //playerprefs?
}
