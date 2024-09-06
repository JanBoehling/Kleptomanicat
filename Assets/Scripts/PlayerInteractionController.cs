using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider)/*, typeof(PlayerMovement)*/)]
public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private float interactionRange;

    private Collider interactionZone;
    private Interactable interactableInRange;
    private GameObject interactionUI;

    private void Awake()
    {
        interactionZone = GetComponent<Collider>();
        interactionZone.isTrigger = true;

        interactionUI = GameObject.Find("Interaction UI"); // ToDo: replace "Find"
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out interactableInRange)) return;

        interactionUI.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        interactableInRange = null;

        interactionUI.SetActive(false);
    }

    public void InteractWithInteractableInRange()
    {
        if (!interactableInRange) return;

        interactableInRange.Interact();
        interactableInRange = null;
    }
}
