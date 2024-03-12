using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Station;

public class Counter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<Collider>().isTrigger = false;
        //PACPointer.inputDisabled = false;
    }
}
