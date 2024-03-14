using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public bool _counterIsEmpty = true;
    PACPointer PACPointer;
    Customer_Data _customerData;

    public GameObject _currentCustomer;
    // Start is called before the first frame update
    void Start()
    {
        _counterIsEmpty = true;
        PACPointer = GameObject.FindObjectOfType(typeof(PACPointer)) as PACPointer;
        _customerData = GameObject.FindObjectOfType(typeof(Customer_Data)) as Customer_Data;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<Collider>().isTrigger = false;
        PACPointer.inputDisabled = false;
        if (_currentCustomer != null)
        {
            _customerData.CompleteOrder(_currentCustomer);
        }
    }
}
