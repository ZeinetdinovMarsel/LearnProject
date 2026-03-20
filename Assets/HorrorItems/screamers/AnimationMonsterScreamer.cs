using UnityEngine;

public class AnimationMonsterScreamer : Screamer
{
    [SerializeField] private Animator _animator;

    protected override void Start()
    {
        base.Start();
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    public override void TriggerScream()
    {
        StartScream();
        if (_animator != null)
        {
            _animator.SetTrigger("Scream");
        }
    }
    public void EndScream()
    {
        if (_animator != null)
        {
            _animator.ResetTrigger("Scream");
        }
    }

   
}