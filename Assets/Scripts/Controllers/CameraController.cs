using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public GameObject pivotPoint;
    public static CameraController instance;
    new Camera camera;

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
        
        pivotPoint = this.transform.Find("PivotPoint").gameObject;
        camera = GetComponentInChildren<Camera>();

    }

    private void Update()
    {
        camera.fieldOfView = PlayerInput.instance.AdsInput ? 30f : 60f;

        //set pivotpoint to player position
        if (GameManager.instance.activePlayer != null)
        {
            pivotPoint.transform.position = GameManager.instance.activePlayer.transform.position;
        }

        //rotate pivot point and clamp rotation up and down
        float h = pivotPoint.transform.eulerAngles.x + PlayerInput.instance.LookInput.y;
        h = (h > 180) ? h - 360 : h;

        pivotPoint.transform.rotation = Quaternion.Euler(Mathf.Clamp(h, -10f, 50f), pivotPoint.transform.eulerAngles.y + PlayerInput.instance.LookInput.x, 0);
        
    }



}
