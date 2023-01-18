using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Sight : MonoBehaviour
{
    // Start is called before the first frame update
    public bool see = false;
    public bool seedeadfriend = false;
    public List<Collider> objectsintrigger;
    public List<Collider> seenobjects;
    Vector3 v = new Vector3(20, 0, 0);
    // Update is called once per frame
    /*private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            float angle = getangle(other.transform);
            if (angle < 22.6 && angle > -22.6)
            {
                Ray ray = new Ray(transform.position,other.transform.position);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.CompareTag("Player"))
                    {

                        see = true;
                        UnityEngine.Debug.Log("widze ciê");
                    }
                    else
                    {
                        see = false;
                        UnityEngine.Debug.Log("nie widze ciê");
                    }
                }
            }
            
        }
        if(other.gameObject.CompareTag("Enemy")&& !seedeadfriend)
        {
            if(other.GetComponent<EnemyManager>().dead = true)
            {
                float angle = getangle(other.transform);
                if (angle < 22.6 && angle > -22.6)
                {
                    Ray ray = new Ray(transform.position, other.transform.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.gameObject.CompareTag("Player"))
                        {

                            seedeadfriend = true;
                        }
                    }
                }
            }
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")||other.CompareTag("Enemy"))
        {
            objectsintrigger.Add(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            foreach(Collider c in objectsintrigger)
            {
                if(c.transform.position == other.transform.position)
                {
                    objectsintrigger.Remove(c);
                }
            }
        }
    }
    void Update()
    {
        see = false;
        seedeadfriend = false;
        seenobjects.Clear();
        foreach(Collider collider in objectsintrigger)
        {
            float angle = getangle(collider.transform);
            if (angle<22.6 && angle >-22.6  )
            {
                Vector3 dir = (transform.position - collider.transform.position).normalized;
                Ray ray = new Ray(transform.position,dir*-1);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.CompareTag("Player"))
                    {

                        see = true;
                        seenobjects.Add(collider);
                    }
                    if(hit.transform.gameObject.CompareTag("Enemy"))
                    {
                        if(hit.transform.gameObject.GetComponent<EnemyManager>().dead )
                        {
                            seedeadfriend = true;
                            seenobjects.Add(collider);
                        }
                    }
                }
            }
        }
    }
    float getangle(Transform t)
    {
        float angle;
        Vector3 targetpos = new Vector3(t.position.x, transform.position.y, t.position.z);
        Vector3 targetdir = targetpos - transform.position;
        angle = Vector3.SignedAngle(targetdir, transform.forward, Vector3.up);
        return angle;
    }
}
