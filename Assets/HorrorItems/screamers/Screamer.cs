using PrimeTween;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

public abstract class Screamer : MonoBehaviour, ITriggerable
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _screamSound;
    [SerializeField] protected bool _enableVisualEffects;
    [SerializeField] protected bool _enableLookAt;
    [SerializeField] private float _screenEffectsDuration = 0.5f;
    [SerializeField] protected CinemachineCamera _screamerCamera;
    [SerializeField, Inject(Id = "PlayerFPCamera")] protected CinemachineCamera _playerCamera;
    [SerializeField] private Volume _postProcessingVolume;
    [SerializeField, Inject(Id = "PlayerTransform")] protected Transform _player;

    [SerializeField, Inject] protected PlayerInput _playerInput;

    [SerializeField] protected Collider _collider;
    [SerializeField] private Transform _monsterLookAt;
    protected virtual void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<Collider>();
    }

    public abstract void TriggerScream();

    protected void StartPlayerLookAt()
    {
        _screamerCamera.LookAt = _monsterLookAt;
    }

    protected void StopPlayerLookAt()
    {
        _screamerCamera.LookAt = null;
    }

    protected void LookAtPlayer()
    {
        Vector3 direction = _player.position - transform.position;
        direction.y = 0;
        if (direction.magnitude > 0)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        }
    }
    protected void ApplyRedScreenEffect()
    {

        if (_postProcessingVolume != null)
        {
            _postProcessingVolume.enabled = true;

        }
    }
    protected virtual void StartScream()
    {
        gameObject.SetActive(true);
        _audioSource.PlayOneShot(_screamSound);
        if (_enableVisualEffects)
        {
            _screamerCamera.gameObject.SetActive(true);
            _playerCamera.gameObject.SetActive(false);
            ApplyRedScreenEffect();
        }

        if (_enableLookAt)
        {
            StartPlayerLookAt();
        }
        Tween.Delay(_screenEffectsDuration).OnComplete(() => StopScream());
    }

    protected virtual void StopScream()
    {
        _screamerCamera.gameObject.SetActive(false);
        _playerCamera.gameObject.SetActive(true);
        _audioSource.Stop();
        StopPlayerLookAt();
        _postProcessingVolume.enabled = false;
        gameObject.SetActive(false);
    }
}