using UnityEngine;

public class RaycastInteractionController : MonoBehaviour
{
    private Camera playerCam;
    private RaycastHit hit;

    void Start()
    {
        playerCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 4f))
            {
                if (hit.transform.TryGetComponent<IInteractableRaycast>(out IInteractableRaycast interactableObj))
                    interactableObj.Interact();
            }
        }
    }
}