using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraContorl : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtual;
    private void Awake()
    {
        cinemachineVirtual = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        EventHelper.PlayerCreated += OnPlayerCreated;
    }

    private void OnDisable()
    {
        EventHelper.PlayerCreated -= OnPlayerCreated;
    }

    private void OnPlayerCreated(GameObject playObject)
    {
        cinemachineVirtual.Follow = playObject.transform;
    }
}
