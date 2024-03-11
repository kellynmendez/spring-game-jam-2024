using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer_Data : MonoBehaviour
{
    public int _ordersCompleted = 0;
    public int _ordersFailed = 0;
    public int _currentScore = 0;

    [SerializeField] GameObject _customerPreFab;
    [SerializeField] public Transform _customerSpawnPoint;

    [SerializeField] GameObject _counter01;
    [SerializeField] GameObject _counter02;
    [SerializeField] GameObject _counter03;

    private GameObject _customer = null;

    //[SerializeField] GameObject _playerTEST;

    // Start is called before the first frame update
    void Start()
    {
        _currentScore = 0;
        SpawnNewCustomer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            SpawnNewCustomer();
        }
    }

    public int CompleteOrder(GameObject customer)
    {
        bool _wasorderComleted = true;
        customer.GetComponent<Customer>().LeaveCounter(_wasorderComleted);
        _ordersCompleted++;
        print(_ordersCompleted);
        return _ordersCompleted;
    }
    public void FailOrder(GameObject customer)
    {
        bool _wasorderComleted = false;
        customer.GetComponent<Customer>().LeaveCounter(_wasorderComleted);

        _ordersFailed++;

        if (_ordersFailed <= 0)
        {
            //Lose the game
        }
    }
    public void SpawnNewCustomer()
    {
        //GameObject player = Instantiate(_playerTEST, _customerSpawnPoint);
        GameObject newCustomer = Instantiate(_customerPreFab, _customerSpawnPoint);
        _customer = newCustomer;
        
        if (_counter01.GetComponent<Counter>()._counterIsEmpty == true)
        {
            _customer.GetComponent<Customer>().ApproachCounter(_counter01.transform);
            _counter01.GetComponent<Counter>()._counterIsEmpty = false;
        }
        else if (_counter02.GetComponent<Counter>()._counterIsEmpty == true)
        {
            _customer.GetComponent<Customer>().ApproachCounter(_counter02.transform);
            _counter02.GetComponent<Counter>()._counterIsEmpty = false;
        }
        else if (_counter03.GetComponent<Counter>()._counterIsEmpty == true)
        {
            _customer.GetComponent<Customer>().ApproachCounter(_counter03.transform);
            _counter03.GetComponent<Counter>()._counterIsEmpty = false;
        }
    }
}
