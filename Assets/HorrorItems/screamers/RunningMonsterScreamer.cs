using System.Collections;
using UnityEngine;

public class RunningMonsterScreamer : Screamer
{
    [SerializeField] private float _speed = 5f;
    private Vector3 _initialPos;
    protected override void Start()
    {
        base.Start();
        _initialPos = transform.position;
    }
    public override void TriggerScream()
    {
        StartScream();
        StartCoroutine(RunMonsterCoroutine());
        
    }

    private IEnumerator RunMonsterCoroutine()
    {
        transform.position = _initialPos;
        _collider.isTrigger = true;
        Vector3 targetPos = _player.position;
        targetPos.y = transform.position.y;
        LookAtPlayer();
        float distance = Vector3.Distance(transform.position, targetPos);
        float coveredDistance = 0f;

        while (coveredDistance < distance)
        {
            coveredDistance += _speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPos, coveredDistance / distance);
            yield return null;
        }
        _collider.isTrigger = false;
        transform.position = targetPos;
    }
}