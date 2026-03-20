using UnityEngine;
using System.Collections;

public class Rifle : GunBase
{
    private Coroutine _autoFireRoutine;

    public void HandleShoot(PlayerInputManager.PressedState state)
    {
        switch (state)
        {
            case PlayerInputManager.PressedState.Started:
                if (_autoFireRoutine == null)
                    _autoFireRoutine = StartCoroutine(FireWhileHeld());
                break;

            case PlayerInputManager.PressedState.Canceled:
                if (_autoFireRoutine != null)
                {
                    StopCoroutine(_autoFireRoutine);
                    _autoFireRoutine = null;
                }
                break;
        }
    }

    private IEnumerator FireWhileHeld()
    {
        while (true)
        {
            Fire(PlayerInputManager.PressedState.Started);
            yield return null;
        }
    }
}