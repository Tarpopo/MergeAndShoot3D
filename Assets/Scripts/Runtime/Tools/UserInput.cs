using UnityEngine;

public class UserInput : Tool
{
    public JoyStick JoyStick => _joyStick;
    [SerializeField] private JoyStick _joyStick;
}