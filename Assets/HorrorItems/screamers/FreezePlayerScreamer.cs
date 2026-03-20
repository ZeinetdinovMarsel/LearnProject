using System.Collections;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

public class FreezePlayerScreamer : Screamer
{
    
    private Coroutine _rotateCoroutine;
    public override void TriggerScream()
    {
        StartScream();
        LookAtMonster();
    }

    protected override void StartScream()
    {
        base.StartScream();
        _playerInput.enabled = false;
    }

    private void LookAtMonster()
    {

        _rotateCoroutine = StartCoroutine(RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        while (true)
        {
            LookAtPlayer();

            yield return new WaitForFixedUpdate();
        }
    }

    protected override void StopScream()
    {
        base.StopScream();
        StopCoroutine(_rotateCoroutine);
        _playerInput.enabled = true;
    }
}