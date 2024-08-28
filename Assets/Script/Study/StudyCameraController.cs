using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StudyCameraController : MonoBehaviour
{
    //Camera
    public float cameraXRotateMove;
    public float cameraYRotateMove;
    public float cameraXRotate;
    public float cameraYRotate;
    public float cameraSpeed;

    private void Start()
    {
        cameraXRotate = transform.eulerAngles.x;
        cameraYRotate = transform.eulerAngles.y;

    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            CameraControll();
        }
    }


    #region CameraMove
    void CameraControll()
    {
        cameraXRotateMove = -Input.GetAxis("Mouse Y") * cameraSpeed * Time.deltaTime;
        cameraYRotateMove = Input.GetAxis("Mouse X") * cameraSpeed * Time.deltaTime;

        cameraYRotate = transform.eulerAngles.y + cameraYRotateMove;
        cameraXRotate = cameraXRotate + cameraXRotateMove;
        cameraXRotate = Mathf.Clamp(cameraXRotate, -90, 90);
        transform.eulerAngles = new Vector3(cameraXRotate, cameraYRotate, 0);
    }

    #endregion
}
