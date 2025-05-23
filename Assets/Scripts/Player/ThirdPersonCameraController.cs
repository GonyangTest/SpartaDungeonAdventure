using UnityEngine;
using System;
using UnityEngine.InputSystem;
using System.Collections;

public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0, 2, -5);
    private float _lerpSpeed = 20f;
    private float _height = 1.5f;
    private float _distance = 5f;
    Quaternion _currentRotation;
    Vector3 _currentPosition;


    private void Awake()
    {
        _currentRotation = transform.rotation;
        _currentPosition = transform.position;
    }

    private void LateUpdate()
    {
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, _target.eulerAngles.y, 0); // 카메라 회전 설정
        Vector3 offset = targetRotation * new Vector3(0, _height, -_distance); // 카메라 위치 설정

        Vector3 targetPosition = _target.position + offset; // 카메라 위치 계산
        _currentPosition = Vector3.Lerp(_currentPosition, targetPosition, _lerpSpeed * Time.deltaTime); // 부드럽게 카메라 위치 이동
        transform.position = _currentPosition;

        _currentRotation = Quaternion.Lerp(_currentRotation, targetRotation, _lerpSpeed * Time.deltaTime); // 부드럽게 카메라 회전 이동
        transform.rotation = _currentRotation;
    }

} 