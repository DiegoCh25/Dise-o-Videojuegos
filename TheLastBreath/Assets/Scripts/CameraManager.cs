using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    Transform target;

    //[SerializeField]
    //float smoothTime;

    [SerializeField]
    MouseSensitivity mouseSensitivity;

    [SerializeField]
    CameraAngle cameraAngle;


    float _distanceToTarget;
    //Vector3 currentVelocity;

    Vector2 _input;

    CameraRotation _cameraRotation;

    private void Awake()
    {
        _distanceToTarget = Vector3.Distance(transform.position, target.position);
    }

    private void Update()
    {
        HandleInputs();
        HandleRotation();
    }

    private void LateUpdate()
    {
        transform.eulerAngles = new Vector3(_cameraRotation.pitch, _cameraRotation.yaw, 0.0F);
        transform.position = target.position - transform.forward * _distanceToTarget;
        //Vector3 targetPosition = target.position + _offset;
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }

    private void HandleInputs()
    {
        if (Input.GetMouseButton(0))
        {
            _input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            return;
        }

        _input = Vector2.zero;
    }

    private void HandleRotation()
    {
        _cameraRotation.yaw += _input.x * mouseSensitivity.horizontal * Helper.BoolToInt(mouseSensitivity.invertHorizontal) * Time.deltaTime;
        _cameraRotation.pitch += _input.y * mouseSensitivity.vertical * Helper.BoolToInt(mouseSensitivity.invertVertical) * Time.deltaTime;
        _cameraRotation.pitch = Mathf.Clamp(_cameraRotation.pitch, cameraAngle.min, cameraAngle.max);
    }


}

