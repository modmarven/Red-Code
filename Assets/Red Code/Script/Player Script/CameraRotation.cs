using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float rotateSpeed = 100.0f;

    void Start()
    {
        
    }

   
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        cameraPosition.transform.rotation *= Quaternion.AngleAxis(mouseX * rotateSpeed * Time.deltaTime, Vector3.up);
        cameraPosition.transform.rotation *= Quaternion.AngleAxis(mouseY * rotateSpeed * Time.deltaTime, Vector3.right);

        var angles = cameraPosition.transform.localEulerAngles;
        angles.z = 0;

        var angle = cameraPosition.transform.localEulerAngles.x;

        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }

        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        transform.rotation = Quaternion.Euler(0, cameraPosition.transform.rotation.eulerAngles.y, 0);
        cameraPosition.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
       
    }
}
