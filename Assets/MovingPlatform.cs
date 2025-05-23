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
            // �̵��� ���� 
            Vector3 moveVector = _waypoints[currentWaypointIndex].position - transform.position;

            Vector3 movement = moveVector.normalized * movementSpeed * Time.deltaTime;
            
            // ���� ��������Ʈ������ �Ÿ��� �� �������� �̵����� ������ ���� ��������Ʈ�� �̵�
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
                // ������Ʈ�� �̵��� ��ŭ ���� �ִ� ������Ʈ�� �̵�
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

    // ���� ��������Ʈ�� ������ �� ȣ��Ǵ� �Լ��Դϴ�.
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
