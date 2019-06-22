using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineBrain mainCameraBrain;
    public CinemachineVirtualCamera vCamPlayer, vCamIso;
    Camera mainCam;
    public bool isoCamActive = false, playerCamActive = true;

    private void Start()
    {
        ShowPlayerCam();
    }

    private void Awake()
    {
        mainCam = mainCameraBrain.transform.GetComponent<Camera>();
    }

    public void ShowIsoCam()
    {
        vCamPlayer.Priority = 0;
        vCamIso.Priority = 1;
        //
        playerCamActive = false;
        isoCamActive = true;
        //
        mainCam.clearFlags = CameraClearFlags.SolidColor;
    }
    public void ShowPlayerCam()
    {
        vCamPlayer.Priority = 1;
        vCamIso.Priority = 0;
        //
        playerCamActive = true;
        isoCamActive = false;
        //
        mainCam.clearFlags = CameraClearFlags.Skybox;
    }
    public void ToggleCameras()
    {
        if(isoCamActive)
        {
            ShowPlayerCam();
        }

        if(playerCamActive)
        {
            ShowIsoCam();
        }
    }
}
