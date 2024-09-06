using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(PlayerMovement))]
public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private float interactionRange = 2.5f;

    [Header("Debug")]
    [SerializeField] private SphereCollider interactionZone;
    [SerializeField] private Interactable interactableInRange;
    [SerializeField] private GameObject interactionUI;

    private void OnValidate()
    {
        GetComponent<SphereCollider>().radius = interactionRange;
    }

    private void Awake()
    {
        interactionZone = GetComponent<SphereCollider>();
        interactionZone.isTrigger = true;

        interactionUI = GameObject.Find("Interaction UI"); // ToDo: replace "Find"
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) InteractWithInteractableInRange(); // TESTING
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

    public void InteractWithInteractableInRange()
    {
        if (!interactableInRange) return;

        interactableInRange.Interact();
        interactableInRange = null;
    }
}
