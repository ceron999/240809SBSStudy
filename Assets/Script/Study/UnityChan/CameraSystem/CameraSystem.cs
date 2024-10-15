using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    TPS, QuaterView, FPS,
}

public class CameraSystem : MonoBehaviour
{
    // 각각의 VirtualCamera GameObject
    public Cinemachine.CinemachineVirtualCamera tpsCamera;
    public Cinemachine.CinemachineVirtualCamera quaterCamera;
    public Cinemachine.CinemachineVirtualCamera fpsCamera;

    private CameraType currentCameraType = CameraType.TPS;
    bool isZoom = false;

    public void ChangeCameraType(CameraType newType)
    {
        // 파라미터로 넘어온 카메라 타입 값을 currentCameraType에 저장한다.
        currentCameraType = newType;

        // 일단 모든 카메라를 꺼준다.
        tpsCamera.gameObject.SetActive(false);
        quaterCamera.gameObject.SetActive(false);
        fpsCamera.gameObject.SetActive(false);

        // 새로운 카메라 타입에 해당하는 카메라를 켜준다.
        switch (currentCameraType)
        {
            case CameraType.TPS:
                tpsCamera.gameObject.SetActive(true);
                break;
            case CameraType.QuaterView:
                quaterCamera.gameObject.SetActive(true);
                break;
            case CameraType.FPS:
                fpsCamera.gameObject.SetActive(true);
                break;
        }
    }

    private void Update()
    {
        // F1,F2,F3키를 누를 때, 각각의 대응되는 Virtual Camera 의 GameObject를 켜준다.
        // 해당하지 않는 Virtucal Camera의 gameObject는 꺼준다.
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ChangeCameraType(CameraType.TPS);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            ChangeCameraType(CameraType.QuaterView);
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            ChangeCameraType(CameraType.FPS);
        }

        if(Input.GetMouseButton(1))
        {
            isZoom = !isZoom;
        }

        float targetPov = isZoom ? 20f : 60f;
        tpsCamera.m_Lens.FieldOfView = Mathf.Lerp(tpsCamera.m_Lens.FieldOfView, targetPov, Time.deltaTime * 5);
    }
}
