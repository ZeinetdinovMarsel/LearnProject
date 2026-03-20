public interface IWeapon
{
    void Fire(PlayerInputManager.PressedState state);
    void Reload();
    void Equip();
    void Unequip();
}
