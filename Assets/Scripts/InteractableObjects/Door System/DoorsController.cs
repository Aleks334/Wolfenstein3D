using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    Camera playerCam;

    void Start()
    {
        playerCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TryOpenDoor();
    }

    void TryOpenDoor()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 4f))
        {
            Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * 4f, Color.blue, 0.25f);
            if (hit.transform.CompareTag("Door"))
            {
                hit.transform.gameObject.GetComponent<DoorTrigger>().OnInteract();
            } else if (hit.transform.CompareTag("ElevatorLever"))
            {
                GameManager.Instance.UpdateGameState(GameState.Victory);
            }
        }
    }
}
