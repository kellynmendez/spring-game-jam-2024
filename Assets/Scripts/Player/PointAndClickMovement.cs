using System.Collections;
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

    Camera _camera;
    GameObject _cameraObject;
    public GameObject _hitVisuals;
    public bool _lookingAtTarget = false;
    private Vector3 Target;
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
        if (_lookingAtTarget == true)
        {
            transform.LookAt(Target);
        }
    }

    public void SetDestination(Vector3 hitLocation)
    {
        _destination = hitLocation;
        _agent.SetDestination(hitLocation);
        print(_destination);
        _seekingDestination = true;
    }
}

