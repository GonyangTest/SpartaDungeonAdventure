using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float waitTime;

    [Header("Waypoints")]
    [SerializeField] private List<Transform> _waypoints;
    private int currentWaypointIndex;
    private Transform toWaypoint;
    private bool isWaiting;

    [Header("ObjectsOnPlatform")]
    private List<GameObject> _objectsOnPlatform;

    private void Start()
    {
        currentWaypointIndex = 0;
        toWaypoint = _waypoints[currentWaypointIndex];
        _objectsOnPlatform = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        if(_waypoints.Count <= 0)
            return;

        if(!isWaiting)
        {
            // 이동할 벡터 
            Vector3 moveVector = _waypoints[currentWaypointIndex].position - transform.position;

            Vector3 movement = moveVector.normalized * movementSpeed * Time.deltaTime;
            
            // 다음 웨이포인트까지의 거리가 이 프레임의 이동보다 작으면 다음 웨이포인트로 이동
            if(movement.magnitude > moveVector.magnitude)
            {
                transform.position = _waypoints[currentWaypointIndex].position;
            }
            else
            {
                transform.position += movement;
            }

            // transform.position = Vector3.MoveTowards(transform.position, toWaypoint.position, movementSpeed * Time.deltaTime);
            foreach(GameObject obj in _objectsOnPlatform)
            {
                // 오브젝트의 이동한 만큼 위에 있는 오브젝트도 이동
                obj.transform.position += movement;
            }
        }
        
        if(transform.position == toWaypoint.position)
        {
            StartCoroutine(WaitRoutine());
            UpdateWaypoint();
        }
    }

    private IEnumerator WaitRoutine()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }

    // 현재 웨이포인트에 도달한 후 호출되는 함수입니다.
    private void UpdateWaypoint()
    {
        Debug.Log("UpdateWaypoint");

        currentWaypointIndex++;
        if(currentWaypointIndex >= _waypoints.Count)
            currentWaypointIndex = 0;

        toWaypoint = _waypoints[currentWaypointIndex];
    }

    public void OnTriggerEnter(Collider other)
    {
        _objectsOnPlatform.Add(other.gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        _objectsOnPlatform.Remove(other.gameObject);
    }
}
