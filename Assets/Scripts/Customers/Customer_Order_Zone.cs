using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer_Order_Zone : MonoBehaviour
{
    Counter _counter;
    // Start is called before the first frame update
    void Start()
    {
        _counter = gameObject.GetComponentInParent<Counter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            other.gameObject.GetComponent<Customer>().GetOrder();
            //other.gameObject.GetComponent<Customer>().GetPayment();
            other.gameObject.GetComponent<Customer>().StartTimer();

            _counter._counterIsEmpty = false;
        }
    }
}
