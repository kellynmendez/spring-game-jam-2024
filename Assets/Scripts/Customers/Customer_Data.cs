using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer_Data : MonoBehaviour
{
    public int _ordersCompleted = 0;
    public int _ordersFailed = 0;
    public int _currentScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        _currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {

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
        if (_ordersFailed <= 0)
        {
            //Lose the game
        }
    }
}
