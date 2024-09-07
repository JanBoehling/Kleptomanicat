using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInteractionController : MonoSingleton<PlayerInteractionController>
{
    [SerializeField] private float interactionRange = 2.5f;
    [SerializeField] private GameObject interactionUI;

    [SerializeField, Header("Debug")] private Interactable interactableInRange;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        interactableInRange = null;

        float smallestRange = float.MaxValue;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Interactable interactable = hitColliders[i].GetComponent<Interactable>();
            if (!interactable)
                continue;

            float range = (hitColliders[i].transform.position - transform.position).magnitude;
            if(range < smallestRange)
            {
                smallestRange = range;
                interactableInRange = interactable;
            }
        }

        interactionUI.SetActive(interactableInRange);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!other.TryGetComponent(out interactableInRange)) return;
    //    if (interactionUI) interactionUI.SetActive(true);
    //}
    //
    //private void OnTriggerExit(Collider other)
    //{
    //    interactableInRange = null;
    //
    //    if (interactionUI) interactionUI.SetActive(false);
    //}

    public void InteractWithInteractableInRange(CallbackContext ctx)
    {
        if (!interactableInRange) return;

        interactableInRange.Interact();
        interactableInRange = null;
    }
}
