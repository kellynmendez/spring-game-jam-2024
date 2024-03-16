using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Customer_Data : MonoBehaviour
{
    public int _ordersCompleted = 0;
    public int _ordersFailed = 0;
    public int _currentScore = 0;

    [SerializeField] GameObject _customerPreFab;
    [SerializeField] public Transform _customerSpawnPoint;
    [SerializeField] public Transform _customerDeathPoint;

    [SerializeField] GameObject _counter01;
    [SerializeField] GameObject _counter02;
    [SerializeField] GameObject _counter03;

    private Text _scoreValue;
    [SerializeField] Canvas _canvas;
    [SerializeField] UnityEngine.UI.Image _life01;
    [SerializeField] UnityEngine.UI.Image _life02;
    [SerializeField] UnityEngine.UI.Image _life03;
    [SerializeField] Sprite _brokenHeart;
    [SerializeField] GameObject _gameOverScreen;

    private GameObject _customer = null;

    //[SerializeField] GameObject _playerTEST;

    // Start is called before the first frame update
    void Start()
    {
        _currentScore = 0;
        SpawnNewCustomer();
        _scoreValue = _canvas.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            SpawnNewCustomer();
        }
    }

    public int CompleteOrder(GameObject customer, Weapon weapon)
    {
        customer.GetComponent<Customer>()._timerStarted = false;
        int scoreIncrease = customer.GetComponent<Customer>()._paymentAmount;
        _currentScore += scoreIncrease;
        _scoreValue.text = _currentScore.ToString();
        print("Current Score: " + _currentScore);

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
        if (_ordersFailed == 1)
        {
            _life03.sprite = _brokenHeart;
        }
        else if(_ordersFailed == 2)
        {
            _life02.sprite = _brokenHeart;
        }
        else if (_ordersFailed >= 3)
        {
            _life01.sprite = _brokenHeart;
            _gameOverScreen.SetActive(true);
            Text _finalScoreText = _gameOverScreen.GetComponentInChildren<Text>();
            _finalScoreText.text = _currentScore.ToString();
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
