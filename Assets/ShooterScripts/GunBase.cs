using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public struct HitData
{
    public Vector3 Point;
    public Vector3 Normal;
}

public abstract class GunBase : MonoBehaviour, IWeapon
{
    [SerializeField] protected int _damage = 10;
    [SerializeField] protected float _range = 100f;
    [SerializeField] protected float _fireRate = 0.1f;
    [SerializeField] protected int _ammo = 30;
    [SerializeField] protected int _maxAmmo = 30;
    [SerializeField] protected Transform _firePoint;

    protected bool _isReloading;
    protected bool _canShoot = true;

    public UnityEvent OnShot { get; private set; } = new();
    public UnityEvent<HitData> OnHit { get; private set; } = new();

    public virtual void Fire(PlayerInputManager.PressedState state)
    {
        if (_isReloading || _ammo <= 0 || !_canShoot) return;

        if (state == PlayerInputManager.PressedState.Started)
        {
            StartCoroutine(FireRoutine());
        }
        else if (state == PlayerInputManager.PressedState.Performed)
        {
            TryShoot();
        }
    }

    private IEnumerator FireRoutine()
    {
        TryShoot();
        yield return new WaitForSeconds(_fireRate);
        _canShoot = true;
    }

    protected void TryShoot()
    {
        if (!_canShoot) return;
        _canShoot = false;

        _ammo--;
        OnShot?.Invoke();
        ShootRaycast();
        StartCoroutine(ResetFire());
    }

    private IEnumerator ResetFire()
    {
        yield return new WaitForSeconds(_fireRate);
        _canShoot = true;
    }

    protected virtual void ShootRaycast()
    {
        if (Physics.Raycast(_firePoint.position, _firePoint.forward, out var hit, _range))
        {
            if (hit.collider.TryGetComponent(out Health health))
            {
                health.ApplyDamage(_damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForceAtPosition(
                    _firePoint.forward * 50f,
                    hit.point,
                    ForceMode.Impulse);
            }

            OnHit?.Invoke(new HitData
            {
                Point = hit.point,
                Normal = hit.normal
            });
        }
    }

    public virtual void Reload()
    {
        if (_isReloading) return;
        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        _isReloading = true;
        yield return new WaitForSeconds(1.5f);
        _ammo = _maxAmmo;
        _isReloading = false;
    }

    public virtual void Equip() => gameObject.SetActive(true);
    public virtual void Unequip() => gameObject.SetActive(false);
}