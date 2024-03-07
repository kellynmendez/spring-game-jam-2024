using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointAndClickMovement : MonoBehaviour
{
    Vector3 _destination;
    NavMeshAgent _agent;
    bool _seekingDestination = false;
    public PACPointer _pointer;

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
        //if (Input.GetButtonDown("Fire1"))
        ////if(AcceptingNewLocation == true)
        //{
        //    Shoot();
        //}

        if (_seekingDestination == false) 
        { return; }

        //_agent.SetDestination(_destination);

    }

    //void Shoot()
    //{
    //    RaycastHit hit;
    //    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        Transform objectHit = hit.transform;
    //        print(objectHit);

    //        GameObject impactObj = Instantiate(_hitVisuals, hit.point, Quaternion.LookRotation(hit.normal));
    //        Destroy(impactObj, 2);

    //        //AcceptingNewLocation = false;
    //        SetDestination(impactObj.transform.position);
    //    }
    //}

    public void SetDestination(Vector3 hitLocation)
    {
        _destination = hitLocation;
        _agent.SetDestination(hitLocation);
        print(_destination);
        _seekingDestination = true;
    }
}

