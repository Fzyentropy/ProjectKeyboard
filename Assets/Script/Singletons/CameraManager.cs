using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineBrain cameraBrain;
    [SerializeField] private CinemachineVirtualCamera menuCamera;
    [SerializeField] private List<CinemachineVirtualCamera> levelCameras = new List<CinemachineVirtualCamera>();
    [SerializeField] private Vector3 cameraOffset;

    // private CinemachineVirtualCamera activeCamera = null;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnLevelStarts += SwitchLevelCamera;
        GameManager.Instance.OnBackToMenu += SwitchLevelCamera;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLevelStarts -= SwitchLevelCamera;
        GameManager.Instance.OnBackToMenu -= SwitchLevelCamera;
    }

    /// <summary>
    /// initialize the camera system at the start of the game
    /// </summary>
    public void InitializeCameras(int levelIndex, Vector3 levelKeyPosition)
    {
        CinemachineVirtualCamera currentCam = levelCameras[levelIndex];

        SetUpLevelCamera(currentCam, levelKeyPosition, cameraOffset);
    }

    /// <summary>
    /// set up level camera after a level is selected
    /// require:
    /// 1. key position
    /// 2. camera position offset
    /// </summary>
    /// <param name="levelIndex"></param>
    private void SetUpLevelCamera(CinemachineVirtualCamera levelCamera, Vector3 levelKeyPosition, Vector3 cameraOffset)
    {
        // move the camera to the key position and move the camera by the offset
        levelCamera.transform.position = levelKeyPosition + cameraOffset;
    }

    // private bool IsActiveCamera(CinemachineVirtualCamera cam)
    // {
    //     return cam == activeCamera;
    // }

    private void SwitchLevelCamera()
    {
        if (cameraBrain.IsBlending) return;

        menuCamera.Priority = 10;
        // activeCamera = menuCamera;

        foreach (var c in levelCameras)
        {
            if (c.Priority != 0)
            {
                c.Priority = 0;
            }
        }
    }

    private void SwitchLevelCamera(int levelIndex)
    {
        if (cameraBrain.IsBlending) return;

        CinemachineVirtualCamera cam = levelCameras[levelIndex];
        cam.Priority = 10;
        // activeCamera = cam;

        foreach (var c in levelCameras)
        {
            if (c != cam & c.Priority != 0)
            {
                c.Priority = 0;
            }
        }
    }
}
