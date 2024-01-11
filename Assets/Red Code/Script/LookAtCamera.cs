using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField]
    private Transform cameraFollowPos;

    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;

    void Start()
    {
      
    }

    private void Update()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);
    }


    void LateUpdate()
    {
        CameraLook();
    }

    private void CameraLook()
    {
        cameraFollowPos.localEulerAngles = new Vector3(yAxis.Value, cameraFollowPos.localEulerAngles.x, cameraFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);

    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
