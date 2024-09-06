using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(SphereCollider), typeof(PlayerMovement))]
public class PlayerInteractionController : MonoSingleton<PlayerInteractionController>
{
    [SerializeField] private float interactionRange = 2.5f;
    [SerializeField] private GameObject interactionUI;

    [SerializeField, Header("Debug")] private Interactable interactableInRange;

    private SphereCollider interactionZone;

    private void OnValidate()
    {
        GetComponent<SphereCollider>().radius = interactionRange;
    }

    protected override void Awake()
    {
        base.Awake();

        interactionZone = GetComponent<SphereCollider>();
        interactionZone.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out interactableInRange)) return;
        if (interactionUI) interactionUI.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        interactableInRange = null;

        if (interactionUI) interactionUI.SetActive(false);
    }

    public void InteractWithInteractableInRange(CallbackContext ctx)
    {
        if (!interactableInRange) return;

        interactableInRange.Interact();
        interactableInRange = null;
    }
}
