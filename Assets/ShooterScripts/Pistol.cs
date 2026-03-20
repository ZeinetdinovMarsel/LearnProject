using UnityEngine;

public class Pistol : GunBase
{
    public void HandleShoot(PlayerInputManager.PressedState state)
    {
        if (state == PlayerInputManager.PressedState.Started)
        {
            Fire(state);
        }
    }
}