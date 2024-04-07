using UnityEngine;

public class UserInput
{
    public JoyStick JoyStick { get; private set; }

    public UserInput(JoyStick joyStick)
    {
        JoyStick = joyStick;
        Debug.Log("HI");
    }
}