
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour, IWeapon
{
    [SerializeField] private float _force = 10f;
    [SerializeField] private float _radius = 5f;
    [SerializeField] private int _damage = 50;
    [SerializeField] private float _delay = 2f;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private ParticleSystem _explosionFx;

    private bool _thrown;

    public void Fire(PlayerInputManager.PressedState state)
    {
        if (_thrown && state != PlayerInputManager.PressedState.Started) return;

        _thrown = true;

        _rb.isKinematic = false;
        _rb.AddForce(Camera.main.transform.forward * _force, ForceMode.Impulse);

        StartCoroutine(ExplodeRoutine());
    }

    private IEnumerator ExplodeRoutine()
    {
        yield return new WaitForSeconds(_delay);

        var colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (var col in colliders)
        {
            if (col.TryGetComponent(out Health health))
            {
                health.ApplyDamage(_damage);
            }

            if (col.attachedRigidbody != null)
            {
                col.attachedRigidbody.AddExplosionForce(_force * 10f, transform.position, _radius);
            }
        }

        if (_explosionFx != null)
        {
            var fx = Instantiate(_explosionFx, transform.position, Quaternion.identity);
            fx.Play();
            Destroy(fx.gameObject, 2f);
        }

        Destroy(gameObject);
    }

    public void Reload() { }

    public void Equip()
    {
        gameObject.SetActive(true);
    }

    public void Unequip()
    {
        gameObject.SetActive(false);
    }
}
