using UnityEngine;

public class PlayerWeaponInitializer
{
    private readonly WeaponInventory _inventory;

    public PlayerWeaponInitializer(WeaponInventory inventory, GunBase[] weapons)
    {
        _inventory = inventory;

        foreach (var weapon in weapons)
        {
            _inventory.AddWeapon(weapon);
        }
    }
}