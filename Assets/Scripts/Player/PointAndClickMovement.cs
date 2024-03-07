﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PointAndClickMovement : MonoBehaviour
{
    Vector3 _destination;
    public NavMeshAgent _agent;
    bool _seekingDestination = false;
    public PACPointer _pointer;
    public Vector3 _position;

    Camera _camera;
    GameObject _cameraObject;
    public GameObject _hitVisuals;
    //bool _acceptingNewDestination = true;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _cameraObject = GameObject.FindGameObjectWithTag("PAC_Camera");
        _camera = _cameraObject.GetComponent<Camera>();
    }

    private void Update()
    {
        if (_seekingDestination == false) 
        { return; }

        if (!_agent.pathPending)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                transform.LookAt(_position);
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    _seekingDestination = false;
                }
            }
        }
    }

    public void SetDestination(Vector3 hitLocation)
    {
        _destination = hitLocation;
        _agent.SetDestination(hitLocation);
        _seekingDestination = true;
    }
}

