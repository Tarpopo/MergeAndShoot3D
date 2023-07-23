using System.Collections;
using Cinemachine;
using DefaultNamespace;
using UnityEngine;

public class CameraChanger : Tool, IAwake
{
    public CinemachineVirtualCamera CurrentCamera { get; private set; }
    [SerializeField] private float _delay;
    [SerializeField] private CinemachineBlendDefinition.Style _defaultBlend;
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [SerializeField] private CinemachineVirtualCamera[] _virtualCameras;
    [SerializeField] private CinemachineVirtualCamera _characterCamera;

    public void OnAwake()
    {
        _cinemachineBrain = FindObjectOfType<CinemachineBrain>();
        _cinemachineBrain.m_DefaultBlend.m_Style = _defaultBlend;
        CurrentCamera = _virtualCameras[0];
    }

    public void SetCameraFollow(Transform follow) => CurrentCamera.Follow = follow;

    public void ChangeCamera(int cameraIndex)
    {
        _cinemachineBrain.m_DefaultBlend.m_Style = _defaultBlend;
        SetCurrentCamera(_virtualCameras[cameraIndex]);
    }

    public void SetCharacterCamera() => SetCurrentCamera(_characterCamera);

    private void SetCurrentCamera(CinemachineVirtualCamera camera)
    {
        CurrentCamera.Priority = 0;
        CurrentCamera = camera;
        CurrentCamera.Priority = 10;
    }

    public void ChangeCamera(int cameraIndex,
        CinemachineBlendDefinition.Style blend = CinemachineBlendDefinition.Style.EaseIn)
    {
        _cinemachineBrain.m_DefaultBlend.m_Style = blend;
        CurrentCamera.Priority = 0;
        CurrentCamera.Priority = 10;
    }

    public void ChangeCameraWithDelay(int cameraIndex) => StartCoroutine(ChangeCameraEnumerator(cameraIndex));

    private IEnumerator ChangeCameraEnumerator(int cameraIndex)
    {
        yield return new WaitForSeconds(_delay);
        ChangeCamera(cameraIndex);
    }
}