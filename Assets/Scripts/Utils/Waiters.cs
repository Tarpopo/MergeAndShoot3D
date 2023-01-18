using UnityEngine;

public static class Waiters
{
    public static readonly WaitForSeconds Second = new(1);
    public static readonly WaitForSeconds HalfSecond = new(0.5f);
    public static readonly WaitForFixedUpdate FixedUpdate = new WaitForFixedUpdate();
}