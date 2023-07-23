using UnityEngine;

public class ApplicationSetter : Tool
{
    [SerializeField] private int _targetFrameRate;

    public void SetFrameRate() => Application.targetFrameRate = _targetFrameRate;
}