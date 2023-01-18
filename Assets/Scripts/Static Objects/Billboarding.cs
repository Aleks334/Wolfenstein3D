using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    Transform target;
    Vector3 lastRotation;
    Transform ObjectToBillboard;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<CharacterController>().transform; // or [Serialize Field] Transform target;
        ObjectToBillboard = transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - ObjectToBillboard.position;
        if (lastRotation != direction)
        {
            ObjectToBillboard.LookAt(target, Vector3.up);
            ObjectToBillboard.rotation = Quaternion.Euler(0, ObjectToBillboard.eulerAngles.y, 0);
        } 

        lastRotation = direction;
    }
}
