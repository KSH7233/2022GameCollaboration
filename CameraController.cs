using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10.0f;
    public Transform cameraTarget;

    private Camera thisCamera;
    private Vector3 worldDefaultForward;



    void Start()
    {
        thisCamera = GetComponent<Camera>();
        worldDefaultForward = transform.forward;        
    }

    private void Update()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel") * speed;

        //최대줌인
        if (thisCamera.fieldOfView <= 20.0f && scroll < 0)
        {
            thisCamera.fieldOfView = 20.0f;
        }
        else if (thisCamera.fieldOfView >= 100.0f && scroll > 0) //최대줌아웃
        {
            thisCamera.fieldOfView = 100.0f;
        }
        else //줌인아웃
        {
            thisCamera.fieldOfView += scroll;
        }


    }



}
