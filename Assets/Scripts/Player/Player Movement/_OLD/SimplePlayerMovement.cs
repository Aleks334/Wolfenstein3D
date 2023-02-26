using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimplePlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    //Movement variables
    float mvmtSpeed = 12f;
    float runningRate = 1.7f;

    Vector3 inputVector;
    Vector3 movementVector;
    bool isRunning = false;
    bool isStrafing = false;

    float inputRotation;
    float sensitivity = 110f;

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
        MovePlayerVertical();

        //Running
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            isRunning = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            isRunning = false;

        //Strafing
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
            isStrafing = true;
        else if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt))
            isStrafing = false;

        if (isRunning)
        {
            movementVector *= runningRate;
          //  GameManager.Instance.playerNoiseLevel = PlayerNoiseLevel.running;
        }

        if(isStrafing)
        {
            MovePlayerHorizontal();
        } else
        {
            //Rotation
            RotatePlayer();
        }
/*
        if ((characterController.velocity.x != 0 || characterController.velocity.y != 0) && !isRunning)
            GameManager.Instance.playerNoiseLevel = PlayerNoiseLevel.walking;
        else if (characterController.velocity.x == 0 && characterController.velocity.y == 0)
            GameManager.Instance.playerNoiseLevel = PlayerNoiseLevel.standing;
*/
        //temporarily
        if (transform.position.y > 1.1f)
        {
            Debug.Log("Gracz jest wy¿ej ni¿ powinien");
            GetComponent<CharacterController>().enabled = false;
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            GetComponent<CharacterController>().enabled = true;
        }

        // Calling "Move" method.
        characterController.Move(movementVector * Time.deltaTime);   
    }

    void MovePlayerVertical()
    {
        inputVector = new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));
        inputVector.Normalize();
        inputVector = transform.TransformDirection(inputVector);
        movementVector = (inputVector * mvmtSpeed);
    }

    void MovePlayerHorizontal()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
        inputVector.Normalize();
        inputVector = transform.TransformDirection(inputVector);
        movementVector = (inputVector * mvmtSpeed);
    }

    void RotatePlayer()
    {
        inputRotation = Input.GetAxisRaw("Horizontal");
        inputRotation *= sensitivity;
        transform.Rotate(0f, inputRotation * Time.deltaTime, 0f);
    }
}
