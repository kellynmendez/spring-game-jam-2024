using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer_Order_Zone : MonoBehaviour
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
        if (other != null)
        {
            other.gameObject.GetComponent<CustomerOrders>().GetOrder();
        }
    }
}
