using UnityEngine;

public class Utility
{
    public static Vector2 GetScreenResolution()
    {
        return new Vector2(Screen.width, Screen.height);
    }
    public static Vector2 GetCenteredScreenPosition()
    {
        return new Vector2(Screen.width / 2, Screen.height / 2);
    }
}
