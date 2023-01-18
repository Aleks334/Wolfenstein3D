using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class lookdirection : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        transform.rotation.Set(0, transform.rotation.y, 0,0);
    }
}
