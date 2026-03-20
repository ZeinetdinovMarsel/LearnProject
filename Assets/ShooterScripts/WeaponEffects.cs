using PrimeTween;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class WeaponEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    [SerializeField] private GunBase _gun;

    private void Awake()
    {
        _gun = GetComponent<GunBase>();
    }
    private void OnEnable()
    { 
        _gun.OnShot.AddListener(PlayEffect);
    }

    private void OnDisable()
    {
        _gun.OnShot.RemoveListener(PlayEffect);
    }

    private void PlayEffect()
    {
        _particleSystem.Play();
    }
}