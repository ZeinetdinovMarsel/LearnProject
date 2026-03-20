using System.Collections;
using UnityEngine;

public class JumpingMonsterScreamer : Screamer
{
    [SerializeField] private Vector3 _jumpOffset;
    [SerializeField] private float _jumpDuration = 0.3f;

    public override void TriggerScream()
    {
        StartScream();
        StartCoroutine(JumpMonsterCoroutine());
    }

    private IEnumerator JumpMonsterCoroutine()
    {
        Vector3 originalPos = transform.position;
        float timer = 0;

        while (timer < _jumpDuration)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(originalPos, originalPos + _jumpOffset, timer / _jumpDuration);
            yield return null;
        }

        transform.position = originalPos + _jumpOffset;
    }
}