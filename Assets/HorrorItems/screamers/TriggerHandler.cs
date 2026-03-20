using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    [SerializeField] private Screamer[] _screamers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(var screamer in _screamers)
                screamer.TriggerScream();
        }
    }
}