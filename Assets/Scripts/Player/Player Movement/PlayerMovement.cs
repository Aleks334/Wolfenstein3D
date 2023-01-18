using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    //Movement variables
    float mvmtSpeed = 12f;
    float runningRate = 2.5f;

    Vector3 inputVector;
    Vector3 movementVector;
    bool isRunning = false;

    //Rotation variables
    float sensitivity = 0.35f; 
    float xMousePos;
    float currentLookingPos;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Movement
        MovePlayer();

        //Running
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;

        if (isRunning)
            movementVector *= runningRate;

        // Calling "Move" method.
        characterController.Move(movementVector * Time.deltaTime);

        //Rotation
        RotatePlayer();
    }

    void MovePlayer()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        inputVector.Normalize();
        inputVector = transform.TransformDirection(inputVector);
        movementVector = (inputVector * mvmtSpeed);
    }

    void RotatePlayer()
    {
        xMousePos = Input.GetAxisRaw("Mouse X");
        xMousePos *= sensitivity;
        currentLookingPos += xMousePos;
        transform.localRotation = Quaternion.AngleAxis(currentLookingPos, Vector3.up);
    }
}