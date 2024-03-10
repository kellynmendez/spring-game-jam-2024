using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer_Data : MonoBehaviour
{
    public int _ordersCompleted;
    public int _ordersFailed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_ordersFailed >= 3)
        {
            //LOSE THE GAME
        }
    }

    public int CompleteOrder()
    {
        _ordersCompleted++;
        print(_ordersCompleted);
        return _ordersCompleted;
    }
    public void FailOrder()
    {
        _ordersFailed++;
    }
}
