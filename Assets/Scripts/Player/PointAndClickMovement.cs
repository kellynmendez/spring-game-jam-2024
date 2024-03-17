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
    public Vector3 _position;

    Camera _camera;
    GameObject _cameraObject;
    public GameObject _hitVisuals;
    //bool _acceptingNewDestination = true;

    private PlayerSM playerSM;

    [Header("Animation")]
    [SerializeField] Animator animator;
    private const string IDLE_ANIM = "Idle";
    private const string WALK_ANIM = "Walk";
    private const string HOLD_WALK_ANIM = "HoldAndWalk";

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _cameraObject = GameObject.FindGameObjectWithTag("PAC_Camera");
        _camera = _cameraObject.GetComponent<Camera>();
        playerSM = GetComponent<PlayerSM>();
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
                    animator.Play(IDLE_ANIM);
                }
            }
        }
    }

    public void SetDestination(Vector3 hitLocation)
    {
        if (_destination != hitLocation) 
        {
            if (playerSM.carryingWeapon)
            {
                animator.Play(HOLD_WALK_ANIM);
            }
            else
            {
                animator.Play(WALK_ANIM);
            }
        }

        _destination = hitLocation;
        _agent.SetDestination(hitLocation);
        _seekingDestination = true;
    }
}

