using UnityEngine;
using Zenject;

public class HitEffectController : MonoBehaviour
{
    [Inject] private WeaponInventory _inventory;

    [SerializeField] private ParticleSystem _hitEffect;

    private GunBase _currentWeapon;

    private void Start()
    {
        _inventory.OnWeaponChanged.AddListener(OnWeaponChanged);
        OnWeaponChanged(_inventory.Current);
    }

    private void OnDestroy()
    {
        _inventory.OnWeaponChanged.RemoveListener(OnWeaponChanged);
    }

    private void OnWeaponChanged(IWeapon weapon)
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.OnHit.RemoveListener(SpawnEffect);
        }

        _currentWeapon = weapon as GunBase;

        if (_currentWeapon != null)
        {
            _currentWeapon.OnHit.AddListener(SpawnEffect);
        }
    }

    private void SpawnEffect(HitData hit)
    {
        if (_hitEffect == null) return;

        var fx = Instantiate(_hitEffect, hit.Point, Quaternion.LookRotation(hit.Normal));
        fx.Play();

        Destroy(fx.gameObject, 2f);
    }
}