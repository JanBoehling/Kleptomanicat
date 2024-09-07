using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInteractionController : MonoSingleton<PlayerInteractionController>
{
    [SerializeField] private float interactionRange = 2.5f;
    [SerializeField] private GameObject interactionUI;

    [Header("Interaction Button")]
    [SerializeField] private Sprite e;
    [SerializeField] private Sprite E;
    [SerializeField, Range(0, 100)] private int memeInteractionButtonChance;
    private bool interactionUIOpen;

    private Interactable interactableInRange;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        interactableInRange = null;
        bool interactableFound = false;

        float smallestRange = float.MaxValue;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Interactable interactable = hitColliders[i].GetComponent<Interactable>();
            if (!interactable)
                continue;

            float range = (hitColliders[i].transform.position - transform.position).magnitude;
            if (range < smallestRange)
            {
                smallestRange = range;
                interactableInRange = interactable;
            }

            interactableFound = true;
        }

        if (interactableFound && !interactionUIOpen) OpenInteractionUI();
        else if (!interactableFound)
        {
            interactionUI.SetActive(false);
            interactionUIOpen = false;
        }
    }

    private void OpenInteractionUI()
    {
        interactionUIOpen = true;

        int rnd = Random.Range(0, 100) + 1;

        interactionUI.SetActive(true);
        interactionUI.transform.GetChild(0).GetComponent<Image>().sprite = rnd <= memeInteractionButtonChance ? E : e;
    }

    public void InteractWithInteractableInRange(CallbackContext ctx)
    {
        if (!interactableInRange) return;

        interactableInRange.Interact();
        interactableInRange = null;
    }
}
