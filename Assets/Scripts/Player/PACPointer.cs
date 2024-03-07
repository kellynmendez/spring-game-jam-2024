using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PACPointer : MonoBehaviour
{
    Camera _camera;
    GameObject _cameraObject;

    public GameObject _hitVisuals;

    public PointAndClickMovement _pointAndClickMovement;
    public Station _station;

    private Transform objectHit = null;

    private Collider _targetCollider = null;
    public bool inputDisabled = false;


    //public bool AcceptingNewLocation = true;
    //public bool _playerHasDestination = false;

    private void Start()
    {
        _station = GameObject.FindObjectOfType(typeof(Station)) as Station;
        _pointAndClickMovement = GameObject.FindObjectOfType(typeof(PointAndClickMovement)) as PointAndClickMovement;
        _cameraObject = GameObject.FindGameObjectWithTag("PAC_Camera");
        _camera = _cameraObject.GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (inputDisabled == false)
            {
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            objectHit = hit.transform;
            if (objectHit.gameObject.GetComponent<Station>() != null)
            {
                inputDisabled = true;
                print(objectHit);

                _targetCollider = objectHit.gameObject.GetComponent<Collider>();
                _targetCollider.isTrigger = true;

                //GameObject impactObj = Instantiate(_hitVisuals, hit.point, Quaternion.LookRotation(hit.normal));
                //Destroy(impactObj, 2);
                //_pointAndClickMovement.SetDestination(impactObj.transform.position);

                Vector3 _destination = objectHit.GetChild(1).transform.position;
                _pointAndClickMovement.SetDestination(_destination);
            }
        }
    }
}
