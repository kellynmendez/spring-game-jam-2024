using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropPoint3D : MonoBehaviour
{
    [SerializeField] public string slotName;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _clip;

    private bool dropped = false;
    private Draggable3DItem draggable;
    private Vector3 origStartPos;
    private Collider dropCollider;

    private void Awake()
    {
        dropCollider = GetComponent<Collider>();
    }

    public void SnapToSlot(Draggable3DItem draggable)
    {
        this.draggable = draggable;
        origStartPos = draggable.GetStartPosition();
        draggable.transform.position = transform.position;
        dropCollider.enabled = false;
        draggable.SetDropped(true);
        dropped = true;

        _audioSource.clip = _clip;
        _audioSource.Play();
    }

    public void SetSlotName(string slotName)
    {
        this.slotName = slotName;
    }

    public bool Filled()
    {
        return dropped;
    }

    public void ResetDropPoint()
    {
        draggable.transform.position = origStartPos;
        dropCollider.enabled = true;
        draggable.SetDropped(false);
        draggable = null;
        dropped = false;
    }
}
