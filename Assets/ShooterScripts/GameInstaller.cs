
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GunBase[] _weapons;
    public override void InstallBindings()
    {
        Container.Bind<WeaponInventory>().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerWeaponInitializer>().AsSingle()
                .WithArguments(_weapons)
                .NonLazy();
    }
}
