using System.Collections;
using UnityEngine;

public class DoorTrigger : AudioPlayable, IInteractableRaycast
{
   private bool isPlayerInTrigger;
   private BoxCollider collisionBox;

   private DoorState _currentDoorStatus;

   [SerializeField] private float _openedDoorDuration = 3f;

   private float _timeToCloseDoor;
   private float _time;

    private float TimeToCloseDoor
    {
        get => _timeToCloseDoor;
        set
        {
            _timeToCloseDoor = value;

            if (_timeToCloseDoor <= 0)
            {
               // Debug.Log("Odliczanie dobieg³o koñca. Drzwi zaczynaj¹ siê zamykaæ automatycznie. Box Collider blokuje mo¿liwoœæ przejœcia w trakcie zamykania");
                collisionBox.isTrigger = false;
                StartCoroutine(CloseDoor(transform.parent.right));
            }
        }
    }
    private Transform _door;

    private float _mvmtAmount;

    private Vector3 _startPos;
    private Vector3 _endPos;

    [SerializeField] private float _mvmtSpeed = 1.5f;

    void Start()
    {
        isPlayerInTrigger = false;
        collisionBox = transform.GetComponent<BoxCollider>();

        _currentDoorStatus = DoorState.Closed;
        TimeToCloseDoor = _openedDoorDuration;

        _door = transform.parent.GetChild(0).transform;

        _startPos = _door.position;
        _mvmtAmount = 3.7f;
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
            //Debug.Log("Obiekt jest w obszarze przesuwania drzwi");
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_currentDoorStatus == DoorState.Opened)
        {
            //Debug.Log("Obiekt wyszed³ z obszaru przesuwania drzwi");
            isPlayerInTrigger = false;    
        }
    }
    
    public void Interact()
    {
        if (_currentDoorStatus == DoorState.Closed)
        {
            PlaySound();
            StartCoroutine(OpenDoor(-transform.parent.right));
        }
        else if (_currentDoorStatus == DoorState.Opened)
        {
            StartCoroutine(CloseDoor(transform.parent.right));
        }   
    }

    private IEnumerator OpenDoor(Vector3 direction)
    {
        _currentDoorStatus = DoorState.Moving;
        direction = Vector3.Normalize(direction);
        _startPos = _door.position;
        _endPos = _startPos + _mvmtAmount * direction; 

        _time = 0f;
        while (_time < 1f)
        {
            _door.position = Vector3.Lerp(_startPos, _endPos, _time);
            yield return null;

            _time += Time.deltaTime * _mvmtSpeed;
        }
        _currentDoorStatus = DoorState.Opened;
    }

    private IEnumerator CloseDoor(Vector3 direction)
    {
        _currentDoorStatus = DoorState.Moving;
        direction = Vector3.Normalize(direction);
        _startPos = _door.position;
        _endPos = _startPos + _mvmtAmount * direction;


        _time = 0f;
        while (_time < 1f)
        {
            _door.position = Vector3.Lerp(_startPos, _endPos, _time);
            yield return null;

            _time += Time.deltaTime * _mvmtSpeed;
        }

        PlaySound(1);
        _currentDoorStatus = DoorState.Closed;
        TimeToCloseDoor = _openedDoorDuration;
        collisionBox.isTrigger = true;
    }

    private enum DoorState
    {
        Closed,
        Moving,
        Opened,
    }
}