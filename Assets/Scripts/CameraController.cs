using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineCam;

    private float minFov = 15f;
    private float maxFov = 90f;
    private float sensitivity = 10f;

    private void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal") ,Input.GetAxis("Vertical") ,0) * 0.1f;
        float ortSize = _cinemachineCam.m_Lens.OrthographicSize;
        ortSize -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        _cinemachineCam.m_Lens.OrthographicSize = Mathf.Clamp(ortSize, minFov, maxFov);
    }
}
