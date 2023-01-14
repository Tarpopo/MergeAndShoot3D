using System.Collections;
using Cinemachine;
using DefaultNamespace;
using UnityEngine;

public class CameraChanger : ManagerBase, IAwake
{
    [SerializeField] private float _delay;
    [SerializeField] private CinemachineBlendDefinition.Style _defaultBlend;
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [SerializeField] private CinemachineVirtualCamera[] _virtualCameras;
    [SerializeField] private CinemachineVirtualCamera _characterCamera;
    private CinemachineVirtualCamera _currentCamera;

    public void OnAwake()
    {
        _cinemachineBrain = FindObjectOfType<CinemachineBrain>();
        _cinemachineBrain.m_DefaultBlend.m_Style = _defaultBlend;
        _currentCamera = _virtualCameras[0];
    }

    public void SetCameraFollow(Transform follow) => _currentCamera.Follow = follow;

    public void ChangeCamera(int cameraIndex)
    {
        _cinemachineBrain.m_DefaultBlend.m_Style = _defaultBlend;
        SetCurrentCamera(_virtualCameras[cameraIndex]);
    }

    public void SetCharacterCamera() => SetCurrentCamera(_characterCamera);

    private void SetCurrentCamera(CinemachineVirtualCamera camera)
    {
        _currentCamera.Priority = 0;
        _currentCamera = camera;
        _currentCamera.Priority = 10;
    }

    public void ChangeCamera(int cameraIndex,
        CinemachineBlendDefinition.Style blend = CinemachineBlendDefinition.Style.EaseIn)
    {
        _cinemachineBrain.m_DefaultBlend.m_Style = blend;
        _currentCamera.Priority = 0;
        _currentCamera.Priority = 10;
    }

    public void ChangeCameraWithDelay(int cameraIndex) => StartCoroutine(ChangeCameraEnumerator(cameraIndex));

    private IEnumerator ChangeCameraEnumerator(int cameraIndex)
    {
        yield return new WaitForSeconds(_delay);
        ChangeCamera(cameraIndex);
    }
}