using UnityEngine;
using Zenject;

public class PlayerInputHandler : MonoBehaviour
{
    [Inject] private WeaponInventory _inventory;
    [Inject] private PlayerInputManager _playerInput;

    private void OnEnable()
    {
        _playerInput.OnShootEvent.AddListener(OnShoot);
        _playerInput.OnReloadEvent.AddListener(OnReload);
    }

    private void OnDisable()
    {
        _playerInput.OnShootEvent.RemoveListener(OnShoot);
        _playerInput.OnReloadEvent.RemoveListener(OnReload);
    }

    private void OnShoot(PlayerInputManager.PressedStateEventArgs args)
    {
        switch (_inventory.Current)
        {
            case Rifle rifle:
                rifle.HandleShoot(args.State);
                break;
            case Pistol pistol:
                pistol.HandleShoot(args.State);
                break;
            default:
                _inventory.Current?.Fire(args.State);
                break;
        }
    }

    private void OnReload(PlayerInputManager.PressedStateEventArgs args)
    {
        if (args.State == PlayerInputManager.PressedState.Started)
            _inventory.Current?.Reload();
    }

    private void Update()
    {
        float scroll = UnityEngine.InputSystem.Mouse.current.scroll.ReadValue().y;
        if (scroll > 0f) _inventory.Next();
        else if (scroll < 0f) _inventory.Previous();
    }
}