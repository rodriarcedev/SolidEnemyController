using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineCamera;
    public CinemachineComposer cinemachine;
    public float offsetY;
    private float bottomLimit = -0.5f;
    private float topLimit = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachine = cinemachineCamera.GetCinemachineComponent<CinemachineComposer>();
    }

    // Update is called once per frame
    void Update()
    {
        offsetY = Input.GetAxis("Mouse Y");

        cinemachine.m_TrackedObjectOffset.y += offsetY;
        cinemachine.m_TrackedObjectOffset.y = Mathf.Clamp(cinemachine.m_TrackedObjectOffset.y, bottomLimit, topLimit);

    }
}
