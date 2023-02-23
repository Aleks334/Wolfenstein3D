using System.Collections;
using UnityEngine;

public class DoorTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isPlayerInTrigger;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool isOpened;

    [SerializeField] private DoorState _currentDoorStatus;
    const float ANIM_TIME = 2f;

    [SerializeField]
    private float _timeToCloseDoor;
    public float TimeToCloseDoor
    {
        get
        {
            return _timeToCloseDoor;
        }

        private set
        {
            _timeToCloseDoor = value;
            if (TimeToCloseDoor <= 0)
            {
                Debug.Log("Odliczanie dobieg³o koñca. Drzwi zaczynaj¹ siê zamykaæ automatycznie. Box Collider blokuje mo¿liwoœæ przejœcia w trakcie zamykania");
                collisionBox.isTrigger = false;
                StartCoroutine(CloseDoor(ANIM_TIME));
            
            }
        }
    }

    private Animator animator;
    const string DOOR_OPENING = "DoorOpening";
    const string DOOR_CLOSING = "DoorClosing";

    private BoxCollider collisionBox;

    void Start()
    {
        isPlayerInTrigger = false;
        animator = transform.parent.GetChild(0).gameObject.GetComponent<Animator>();
        collisionBox = GetComponent<BoxCollider>();
      //  isOpened = false;
      //  isMoving = false;
        _currentDoorStatus = DoorState.Closed;
        TimeToCloseDoor = 3f;
    }

    void Update()
    {
       if(_currentDoorStatus == DoorState.Opened/*isOpened*/)
        {
           // Debug.Log("Zaczynam odliczanie do automatycznego zamkniêcia drzwi");
           if(!isPlayerInTrigger)
                TimeToCloseDoor -= Time.deltaTime;

           

        } else if (_currentDoorStatus == DoorState.Moving/*isMoving*/)
        {
            // Debug.Log("Reset timera. Drzwi zamykaj¹ siê automatycznie lub gracz je zamkn¹³");
            TimeToCloseDoor = 3f;
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && _currentDoorStatus == DoorState.Closed/*!isOpened*///)
   /*     {
          //  Interact();
        }
   // }*/

    void OnTriggerStay(Collider other)
    {

        if (_currentDoorStatus == DoorState.Opened/*isOpened*/)
        {
            Debug.Log("Obiekt jest w obszarze przesuwania drzwi");
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_currentDoorStatus == DoorState.Opened/*isOpened*/)
        {
            Debug.Log("Obiekt wyszed³ z obszaru przesuwania drzwi");
            isPlayerInTrigger = false;    
        }
    }

    public void Interact()
    {
        if (_currentDoorStatus == DoorState.Closed/*!isMoving && !isOpened*/)
        {
            Debug.Log("Drzwi otwieraj¹ siê");
            StartCoroutine(OpenDoor(ANIM_TIME));
        }
        else if (_currentDoorStatus == DoorState.Opened/*!isMoving && isOpened*/)
        {
            Debug.Log("Drzwi zamykaj¹ siê");
            StartCoroutine(CloseDoor(ANIM_TIME));
        }
    }

    IEnumerator OpenDoor(float waitTime)
    {
        _currentDoorStatus = DoorState.Moving;
       // isMoving = true;
        animator.Play(DOOR_OPENING);

        yield return new WaitForSeconds(waitTime);
       // isMoving = false;
       // isOpened = true;
        _currentDoorStatus = DoorState.Opened;
    }

    IEnumerator CloseDoor(float waitTime)
    {
        _currentDoorStatus = DoorState.Moving;
       // isMoving = true;
        animator.Play(DOOR_CLOSING);

        yield return new WaitForSeconds(waitTime);
       // isMoving = false;
       // isOpened = false;
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