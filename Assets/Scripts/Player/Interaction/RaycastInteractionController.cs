using UnityEngine;

public class RaycastInteractionController : MonoBehaviour
{
    private Camera playerCam;
    private RaycastHit hit;

    [SerializeField] private LayerMask _layerMask;

    void Start()
    {
        playerCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 5f, _layerMask, QueryTriggerInteraction.Collide))
            {
                Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * 5f, Color.green, 0.25f);
                if (hit.transform.TryGetComponent<IInteractableRaycast>(out IInteractableRaycast interactableObj))
                    interactableObj.Interact();
            }
            else
                Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * 5f, Color.green, 0.25f);
        }
    }
}