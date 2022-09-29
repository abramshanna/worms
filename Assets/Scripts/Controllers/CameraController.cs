using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public GameObject _pivotPoint;
    public static CameraController instance;
    Camera _camera;

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
        
        _pivotPoint = this.transform.Find("PivotPoint").gameObject;
        _camera = GetComponentInChildren<Camera>();

    }

    private void Update()
    {
        _camera.fieldOfView = PlayerInput.instance.adsInput ? 30f : 60f;

        //set pivotpoint to player position
        _pivotPoint.transform.position = GameManager.instance.activePlayer.transform.position;

        //rotate pivot point and clamp rotation up and down
        float h = _pivotPoint.transform.eulerAngles.x + PlayerInput.instance.lookInput.y;
        h = (h > 180) ? h - 360 : h;

        _pivotPoint.transform.rotation = Quaternion.Euler(Mathf.Clamp(h, -10f, 50f), _pivotPoint.transform.eulerAngles.y + PlayerInput.instance.lookInput.x, 0);
        
    }



}
