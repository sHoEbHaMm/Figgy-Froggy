using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] WayPoints;
    private int currentWayPointIndex = 0;

    [SerializeField] private int MovSpeed = 2;

    // Update is called once per frame
   private void Update()
    {
        if(Vector2.Distance(WayPoints[currentWayPointIndex].transform.position, transform.position) < 0.1f)
        {
            currentWayPointIndex++;

            if(currentWayPointIndex >= WayPoints.Length)
            {
                currentWayPointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, WayPoints[currentWayPointIndex].transform.position, Time.deltaTime * MovSpeed);
    }
}
