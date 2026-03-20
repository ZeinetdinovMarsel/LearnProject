using System.Collections;
using UnityEngine;

public class TeleportingMonsterScreamer : Screamer
{
    [SerializeField] private float _minTeleportRadius = 1f;
    [SerializeField] private float _maxTeleportRadius = 10f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _blinkInterval = 0.2f;
    [SerializeField] private float _blinkDuration = 2f;

    private Vector3 _initialPos;
    private bool _isBlinking = false;
    private float _currentRadius;
    private Renderer _renderer;
    protected override void Start()
    {
        base.Start();
        _initialPos = transform.position;
        _currentRadius = _maxTeleportRadius;
        _renderer =  GetComponent<Renderer>();
    }

    public override void TriggerScream()
    {
        StartScream();
        StartCoroutine(TeleportAndBlinkCoroutine());
    }

    private IEnumerator TeleportAndBlinkCoroutine()
    {
        _isBlinking = true;
        float blinkEndTime = Time.time + _blinkDuration;
        while (Time.time < blinkEndTime)
        {
            transform.position = GetRandomPositionTowardsPlayer();
            ToggleRenderer();
            LookAtPlayer();
            yield return new WaitForSeconds(Random.Range(0,_blinkInterval));
        }

        _isBlinking = false;
        ToggleRenderer();
    }

    private void ToggleRenderer()
    {
        if (_renderer != null)
        {
            _renderer.enabled = !_renderer.enabled;
        }
    }

    private Vector3 GetRandomPositionTowardsPlayer()
    {
        Vector3 directionToPlayer = (_player.position - transform.position).normalized;

        Vector2 randomOffset = Random.insideUnitCircle * _currentRadius;

        Vector3 randomPos = new Vector3(randomOffset.x, 0, randomOffset.y) + directionToPlayer*0.5f;
        randomPos.y = 0;
        randomPos = transform.position + randomPos;
        _currentRadius = Mathf.Max(_currentRadius * 0.8f, _minTeleportRadius); 

        return randomPos;
    }
}