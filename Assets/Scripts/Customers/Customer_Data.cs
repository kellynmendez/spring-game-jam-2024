using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer_Data : MonoBehaviour
{
    public int _ordersCompleted = 0;
    public int _ordersFailed = 0;
    public int _currentScore = 0;

    [SerializeField] GameObject _customerPreFab;
    [SerializeField] Transform _customerSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        _currentScore = 0;
        SpawnNewCustomer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int CompleteOrder(GameObject customer)
    {
        bool _wasorderComleted = true;
        customer.GetComponent<Customer>().LeaveStore(_wasorderComleted);
        _ordersCompleted++;
        print(_ordersCompleted);
        return _ordersCompleted;
    }
    public void FailOrder(GameObject customer)
    {
        bool _wasorderComleted = false;
        customer.GetComponent<Customer>().LeaveStore(_wasorderComleted);

        _ordersFailed++;

        if (_ordersFailed <= 0)
        {
            //Lose the game
        }
    }
    public void SpawnNewCustomer()
    {
        GameObject newCustomer = Instantiate(_customerPreFab, _customerSpawnPoint);

    }
}
