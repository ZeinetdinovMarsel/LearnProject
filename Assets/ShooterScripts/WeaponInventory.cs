using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class WeaponInventory
{
    private readonly List<IWeapon> _weapons = new();
    private int _currentIndex;

    public IWeapon Current => _weapons.Count > 0 ? _weapons[_currentIndex] : null;

    public UnityEvent<IWeapon> OnWeaponChanged { get; private set; } = new();

    public void AddWeapon(IWeapon weapon)
    {
        _weapons.Add(weapon);

        if (_weapons.Count == 1)
        {
            _currentIndex = 0;
            weapon.Equip();
            OnWeaponChanged?.Invoke(Current);
        }
    }

    public void Next()
    {
        if (_weapons.Count == 0) return;

        Current?.Unequip();

        _currentIndex = (_currentIndex + 1) % _weapons.Count;

        Current?.Equip();
        OnWeaponChanged?.Invoke(Current);
    }

    public void Previous()
    {
        if (_weapons.Count == 0) return;

        Current?.Unequip();

        _currentIndex = (_currentIndex - 1 + _weapons.Count) % _weapons.Count;

        Current?.Equip();
        OnWeaponChanged?.Invoke(Current);
    }
}