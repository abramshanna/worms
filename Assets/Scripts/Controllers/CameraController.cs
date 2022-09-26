using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] GameManager _gameManager;
    public static GameObject _pivotPoint;
    Transform _playerPosition;

    private void Awake()
    {
        if (_pivotPoint != null)
        {
            GameObject.Destroy(this);
        }
        else
        {
         _pivotPoint = this.transform.Find("PivotPoint").gameObject;
        }

    }

    //set position to active player (can be called to update)
    public void SetActivePlayerPosition()
    {
        _playerPosition = _gameManager._activePlayer.transform;
    }

    private void FixedUpdate()
    {
        //set pivotpoint to player position
        _pivotPoint.transform.position = _playerPosition.position;

        //rotate pivot point and clamp rotation up and down
        float h = _pivotPoint.transform.eulerAngles.x + _playerInput.lookInput.y;
        h = (h > 180) ? h - 360 : h;

        _pivotPoint.transform.rotation = Quaternion.Euler(Mathf.Clamp(h, -10f, 50f), _pivotPoint.transform.eulerAngles.y + _playerInput.lookInput.x, 0);
        
    }

}
