using UnityEngine;
using System;
public class TriggerDataSender : MonoBehaviour
{
    public static event Action<Collider2D> OnPlayerStateChanged;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnPlayerStateChanged?.Invoke(other);
    }
}
