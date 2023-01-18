using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] bool isPlayerInTrigger;
    [SerializeField] bool isMoving;
    [SerializeField] bool isOpened;
    const float ANIM_TIME = 2f;
    [SerializeField] float timeToCloseDoor = 3f;

    Animator animator;
    const string DOOR_OPENING = "DoorOpening";
    const string DOOR_CLOSING = "DoorClosing";

    BoxCollider collisionBox;

    void Start()
    {
        isPlayerInTrigger = false;
        animator = transform.parent.GetChild(0).gameObject.GetComponent<Animator>();
        collisionBox = GetComponent<BoxCollider>();
        isOpened = false;
        isMoving = false;
    }

    void Update()
    {
       if(isOpened && !isPlayerInTrigger)
        {
           // Debug.Log("Zaczynam odliczanie do automatycznego zamkniêcia drzwi");
            timeToCloseDoor -= Time.deltaTime;
            if(isMoving)
            {
               // Debug.Log("Reset timera. Drzwi zamykaj¹ siê automatycznie lub gracz je zamkn¹³");
                timeToCloseDoor = 3f;
            }
            if(timeToCloseDoor <= 0)
            {
                Debug.Log("Odliczanie dobieg³o koñca. Drzwi zaczynaj¹ siê zamykaæ automatycznie. Box Collider blokuje mo¿liwoœæ przejœcia w trakcie zamykania");
                timeToCloseDoor = 3f;
                collisionBox.isTrigger = false;
                StartCoroutine(CloseDoor(ANIM_TIME));
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && !isOpened)
        {
            
            OnInteract();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("Player") && isOpened) || (other.CompareTag("Enemy") && isOpened))
        {
            Debug.Log("Obiekt jest w obszarze przesuwania drzwi");
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Player") && isOpened) || (other.CompareTag("Enemy") && isOpened))
        {
            Debug.Log("Obiekt wyszed³ z obszaru przesuwania drzwi");
            isPlayerInTrigger = false;    
        }
    }

    //Called by objects that want to interact with door
    public void OnInteract()
    {
        if (!isMoving && !isOpened)
        {
            Debug.Log("Drzwi otwieraj¹ siê");
            StartCoroutine(OpenDoor(ANIM_TIME));
        }
        else if (!isMoving && isOpened)
        {
            Debug.Log("Drzwi zamykaj¹ siê");
            StartCoroutine(CloseDoor(ANIM_TIME));
        }
    }

    IEnumerator OpenDoor(float waitTime)
    {
        isMoving = true;
        animator.Play(DOOR_OPENING);

        yield return new WaitForSeconds(waitTime);
        isMoving = false;
        isOpened = true;
    }

    IEnumerator CloseDoor(float waitTime)
    {
        isMoving = true;
        animator.Play(DOOR_CLOSING);

        yield return new WaitForSeconds(waitTime);
        isMoving = false;
        isOpened = false;
        collisionBox.isTrigger = true;
    }
}
