using System.Collections;
using UnityEngine;

public class DoorTrigger : MonoBehaviour, IInteractableRaycast
{
    private bool isPlayerInTrigger;
    private DoorState _currentDoorStatus;

    private float _timeToCloseDoor;

    private float TimeToCloseDoor
    {
        get { return _timeToCloseDoor; }

        set
        {
            _timeToCloseDoor = value;

            if (_timeToCloseDoor <= 0)
            {
                Debug.Log("Odliczanie dobieg³o koñca. Drzwi zaczynaj¹ siê zamykaæ automatycznie. Box Collider blokuje mo¿liwoœæ przejœcia w trakcie zamykania");
                collisionBox.isTrigger = false;
                StartCoroutine(CloseDoor(ANIM_TIME));
            }
        }
    }

    const float ANIM_TIME = 2f;
    private Animator animator;
    const string DOOR_OPENING = "DoorOpening";
    const string DOOR_CLOSING = "DoorClosing";

    private BoxCollider collisionBox;

    void Start()
    {
        isPlayerInTrigger = false;
        animator = transform.parent.GetChild(0).gameObject.GetComponent<Animator>();
        collisionBox = GetComponent<BoxCollider>();

        _currentDoorStatus = DoorState.Closed;
        TimeToCloseDoor = 3f;
    }

    void Update()
    {
       if(_currentDoorStatus == DoorState.Opened)
        {
           if(!isPlayerInTrigger)
                TimeToCloseDoor -= Time.deltaTime;
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        if (_currentDoorStatus == DoorState.Opened)
        {
            Debug.Log("Obiekt jest w obszarze przesuwania drzwi");
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_currentDoorStatus == DoorState.Opened)
        {
            Debug.Log("Obiekt wyszed³ z obszaru przesuwania drzwi");
            isPlayerInTrigger = false;    
        }
    }
    
    public void Interact()
    {
        if (_currentDoorStatus == DoorState.Closed)
        { 
            StartCoroutine(OpenDoor(ANIM_TIME));
        }
        else if (_currentDoorStatus == DoorState.Opened)
        {
            StartCoroutine(CloseDoor(ANIM_TIME));
        }
    }

    IEnumerator OpenDoor(float waitTime)
    {
       // Debug.Log("Drzwi otwieraj¹ siê");
        _currentDoorStatus = DoorState.Moving;
        animator.Play(DOOR_OPENING);

        yield return new WaitForSeconds(waitTime);
        _currentDoorStatus = DoorState.Opened;
    }

    IEnumerator CloseDoor(float waitTime)
    {
       // Debug.Log("Drzwi zamykaj¹ siê");
        _currentDoorStatus = DoorState.Moving;
        animator.Play(DOOR_CLOSING);
        TimeToCloseDoor = 3f;

        yield return new WaitForSeconds(waitTime);
        _currentDoorStatus = DoorState.Closed;
        collisionBox.isTrigger = true;
    }

    private enum DoorState
    {
        Closed,
        Moving,
        Opened,
    }
}