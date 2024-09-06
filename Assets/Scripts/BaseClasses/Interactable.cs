using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Collider), typeof(SpriteRenderer))]
public abstract class Interactable : AudioPlayer
{
    protected UnityEvent OnInteractEvent = new();

    public virtual void Interact()
    {
        Debug.Log($"{GetType()}: Interacting with {name}");

        PlayAudio();

        OnInteractEvent?.Invoke();
    }
}
