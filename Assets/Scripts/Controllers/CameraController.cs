using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;
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

        //should be active player
        _playerPosition = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        //set pivotpoint to player position
        _pivotPoint.transform.position = _playerPosition.position;

        //clamp rotation

        float h = _pivotPoint.transform.eulerAngles.x + _playerInput.lookInput.y;
        h = (h > 180) ? h - 360 : h;

        _pivotPoint.transform.rotation = Quaternion.Euler(Mathf.Clamp(h, -10f, 50f), _pivotPoint.transform.eulerAngles.y + _playerInput.lookInput.x, 0);

        
    }
}
