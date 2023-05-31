using System.Collections;
using UnityEngine;

public class DoorTest : MonoBehaviour
{
    /*
    [SerializeField] private Vector3 SlideDir = Vector3.right;
    private float SlideAmount = 1f;

    private Vector3 StartPos;
    private float _speed = 1.5f;
    [SerializeField] private bool isOpened = false;

    

    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
        SlideAmount = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (!isOpened)
                SlideDir = Vector3.right;
            else
                SlideDir = Vector3.left;

                StartCoroutine(MoveDoor(SlideDir));
                
          //  else
          //      StartCoroutine(CloseDoor());

        }
        
    }

    private IEnumerator MoveDoor(Vector3 direction)
    {
        direction = Vector3.Normalize(direction);
        Vector3 endPos = StartPos + SlideAmount * direction;
        Vector3 startPosition = transform.position;

        float time = 0f;
        while (time < 1f)
        {
            // t = Mathf.MoveTowards(0, 1, );
            transform.position = Vector3.Lerp(startPosition, endPos, time);

            yield return null;
            time += Time.deltaTime * _speed;
        }
        isOpened = !isOpened;
    }
   /* private IEnumerator CloseDoor()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / duration;

        while (Vector3.Distance(transform.position, endPos) > 0f)
        {
            // t = Mathf.MoveTowards(0, 1, );
            transform.position = Vector3.Lerp(startPos, endPos, percentageComplete);

            yield return null;
        }
        isOpened = true;
    }*/
}