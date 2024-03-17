using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Customer_Data : MonoBehaviour
{
    public int _ordersCompleted = 9;
    public int _ordersFailed = 0;
    public int _currentScore = 0;

    [SerializeField] GameObject _customerPreFab;
    [SerializeField] public Transform _customerSpawnPoint;
    [SerializeField] public Transform _customerDeathPoint;

    [SerializeField] GameObject _counter01;
    [SerializeField] GameObject _counter02;
    [SerializeField] GameObject _counter03;

    [SerializeField] Canvas _canvas;
    [SerializeField] TMP_Text _inGameScoreText;
    //TextMeshPro _inGameScoreText;
    [SerializeField] TMP_Text _gameOverScoreText;
    //TextMeshPro _gameOverScoreText;
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
        //_inGameScoreText = _inGameScoreTextGO.GetComponent<TextMeshPro>();
        //_gameOverScoreText = _gameOverScoreTextGO.GetComponent<TextMeshPro>();
        //print(_inGameScoreText);
        //print(_gameOverScoreText);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            SpawnNewCustomer();
        }
    }

    public void CompleteOrder(GameObject customer, Counter counter, Weapon weapon)
    {
        int itemToIgnore = 1;
        foreach (var item in customer.GetComponent<Customer>()._orders)
        {
            if (item.ToString().Contains(weapon.GetWeaponType()) && item.ToString().Contains(weapon.weaponColor.ToString()))
            {
                if (itemToIgnore == 1)
                {
                    counter._ignoreItem01 = true;
                    //customer.GetComponent<Customer>()._item01Check.SetActive(true);
                    customer.GetComponent<Customer>()._itemsChecks[0].SetActive(true);
                    print("ignoring item 1");
                    break;
                }
                else if (itemToIgnore == 2)
                {
                    counter._ignoreItem02 = true;
                    customer.GetComponent<Customer>()._itemsChecks[1].SetActive(true);
                    print("ignoring item 2");
                    break;
                }
                else if (itemToIgnore == 3)
                {
                    //should do nothing
                    counter._ignoreItem03 = true;
                    customer.GetComponent<Customer>()._itemsChecks[2].SetActive(true);
                    print("ignoring item 3");
                    break;
                }
            }
            else
            {
                itemToIgnore++;
            }
        }

        print("ignore items " + counter._ignoreItem01 + " " + counter._ignoreItem02 + " " + counter._ignoreItem03);

        customer.GetComponent<Customer>()._ordersThru++;
        if (customer.GetComponent<Customer>()._ordersThru < customer.GetComponent<Customer>()._orders.Count)
        {
            print("one down");
        }
        else
        {
            customer.GetComponent<Customer>()._timerStarted = false;
            int scoreIncrease = customer.GetComponent<Customer>()._paymentAmount;
            _currentScore += scoreIncrease;
            _inGameScoreText.text = _currentScore.ToString();
            print("Current Score: " + _currentScore);

            bool _wasorderComleted = true;
            customer.GetComponent<Customer>().LeaveCounter(_wasorderComleted);
            _ordersCompleted++;
            print(_ordersCompleted);
            print(customer.GetComponent<Customer>()._ordersThru++);
        }
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
        else if (_ordersFailed == 2)
        {
            _life02.sprite = _brokenHeart;
        }
        else if (_ordersFailed >= 3)
        {
            _life01.sprite = _brokenHeart;
            _gameOverScreen.SetActive(true);
            _gameOverScoreText.text = _currentScore.ToString();
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
