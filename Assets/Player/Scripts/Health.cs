using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health;

    public UnityEvent OnPlayerDeath { get; private set; } = new();
    public UnityEvent OnPlayerRespawn { get; private set; } = new();

    void Awake() => _health = _maxHealth;

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            _health = 0;
            OnPlayerDeath?.Invoke();
        }

    }

    public void Respawn(Vector3 position, Quaternion rotation)
    {
        _health = _maxHealth;

        transform.position = position;
        transform.rotation = rotation;

        gameObject.SetActive(true);

        OnPlayerRespawn?.Invoke();
    }
}
