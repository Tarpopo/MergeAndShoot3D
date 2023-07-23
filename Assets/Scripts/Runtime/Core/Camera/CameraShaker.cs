using Cinemachine;
using UnityEngine;

public class CameraShaker : Tool
{
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;
    [SerializeField] private float _time;
    private CinemachineBasicMultiChannelPerlin _virtualCameraNoise;
    private readonly Timer _timer = new Timer();

    public void Shake(float amplitude, float freq, float time)
    {
        _virtualCameraNoise = Toolbox.Get<CameraChanger>().CurrentCamera
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _virtualCameraNoise.m_AmplitudeGain = amplitude;
        _virtualCameraNoise.m_FrequencyGain = freq;
        _timer.StartTimer(time, StopShake);
    }

    public void Shake()
    {
        _virtualCameraNoise = Toolbox.Get<CameraChanger>().CurrentCamera
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _virtualCameraNoise.m_AmplitudeGain = _amplitude;
        _virtualCameraNoise.m_FrequencyGain = _frequency;
        _timer.StartTimer(_time, StopShake);
    }

    private void Update() => _timer.UpdateTimer();

    private void StopShake()
    {
        _virtualCameraNoise.m_AmplitudeGain = 0;
        _virtualCameraNoise.m_FrequencyGain = 0;
    }
}