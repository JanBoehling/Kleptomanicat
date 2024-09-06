using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D)/*, typeof(PlayerMovement)*/)]
public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private float interactionRange;

    [SerializeField] private CircleCollider2D interactionZone;
    [SerializeField] private Interactable interactableInRange;
    [SerializeField] private GameObject interactionUI;

    private void OnValidate()
    {
        GetComponent<CircleCollider2D>().radius = interactionRange;
    }

    private void Awake()
    {
        interactionZone = GetComponent<CircleCollider2D>();
        interactionZone.isTrigger = true;

        interactionUI = GameObject.Find("Interaction UI"); // ToDo: replace "Find"
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) InteractWithInteractableInRange(); // TESTING
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out interactableInRange)) return;
        if (interactionUI) interactionUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        interactableInRange = null;

        if (interactionUI) interactionUI.SetActive(false);
    }

    public void InteractWithInteractableInRange()
    {
        if (!interactableInRange) return;

        interactableInRange.Interact();
        interactableInRange = null;
    }
}
