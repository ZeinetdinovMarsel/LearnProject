using PrimeTween;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

public class RecoilController : MonoBehaviour
{
    [Inject(Id = "PlayerFPCamera")] private CinemachineCamera _playerCamera;
    [Inject] private WeaponInventory _inventory;

    [SerializeField] private float _recoilX = 2f;
    [SerializeField] private float _recoilY = 1f;

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
            _currentWeapon.OnShot.RemoveListener(ApplyRecoil);
        }

        _currentWeapon = weapon as GunBase;

        if (_currentWeapon != null)
        {
            _currentWeapon.OnShot.AddListener(ApplyRecoil);
        }
    }

    private void ApplyRecoil()
    {
        var panTilt = _playerCamera.GetComponent<CinemachinePanTilt>();
        if (panTilt == null) return;

        float targetX = panTilt.TiltAxis.Value - _recoilX;
        float targetY = panTilt.PanAxis.Value + Random.Range(-_recoilY, _recoilY);

        Tween.Custom(panTilt.TiltAxis.Value, targetX, 0.05f,
            v => panTilt.TiltAxis.Value = v);

        Tween.Custom(panTilt.PanAxis.Value, targetY, 0.05f,
            v => panTilt.PanAxis.Value = v);
    }
}