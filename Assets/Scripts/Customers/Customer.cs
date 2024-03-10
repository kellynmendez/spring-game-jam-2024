using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] int HelmetOdds = 100;
    [SerializeField] int SwordOdds = 50;
    [SerializeField] int ArrowOdds = 20;
    public enum Order { Helmet, Sword, Arrow };
    private Order _order;
    private List<Order> _orders = new List<Order>();
    public enum PaymentType { Metal, Wood, Arrow };
    private PaymentType _paymentType;
    private int _paymentAmount = 0;
    private bool _timerStarted = false;
    public float _timerTime = 2;
    private int _ordersDone = 0;
    private int _orderNumMax = 3;
    private int _cost = 0;

    public Customer_Data _customerData;

    private void Start()
    {
        _customerData = GameObject.FindObjectOfType(typeof(Customer_Data)) as Customer_Data;
        //GetOrder();
    }


    public List<Order> GetOrder()
    {
        //if (_ordersDone >= 10)
        //{
        //    int _randOrderNum = Random.Range(0, 3);
        //    _orderNumMax = 3;
        //}
        //else if (_ordersDone >= 4)
        //{
        //    int randOrderNum = Random.Range(1, 2);
        //    _orderNumMax = 2;
        //}
        //else 
        //{
        //    _orderNumMax = 1;
        //}


        if (_customerData._ordersCompleted == 0)
        {
            _order = Order.Helmet;
            print("Order is: " + _order);
        }
        else if (_customerData._ordersCompleted == 1)
        {
            _order = Order.Sword;
            print("Order is: " + _order);
        }
        else if (_customerData._ordersCompleted == 2)
        {
            _order = Order.Arrow;
            print("Order is: " + _order);
        }
        else if (_customerData._ordersCompleted >= 3)
        {
            if (_customerData._ordersCompleted >= 15)
            {
                _orderNumMax = 3;
            }
            else if (_customerData._ordersCompleted >= 9)
            {
                _orderNumMax = 2;
            }
            else
            {
                _orderNumMax = 1;
            }
            int _numOfOrders = Random.Range(1, _orderNumMax + 1);
            if (_customerData._ordersCompleted >= 27)
            {
                _numOfOrders = 1;
            }
            else if (_customerData._ordersCompleted >= 21)
            {
                _numOfOrders = Random.Range(1, _orderNumMax);
            }

            int _swordCount = 0;
            for (int i = _numOfOrders; i <= _orderNumMax; i++)
            {
                print("i = " + i);
                int _randOrderType = Random.Range(0, 100);
                if ((_randOrderType <= ArrowOdds) && (!_orders.Contains(Order.Arrow)))
                {
                    _order = Order.Arrow;
                    GetPayment(_order);
                }
                else if ((_randOrderType <= SwordOdds) && (_swordCount < 2))
                {
                    _swordCount++;
                    _order = Order.Sword;
                    GetPayment(_order);
                }
                else if (_randOrderType <= HelmetOdds)
                {
                    _order = Order.Helmet;
                    GetPayment(_order);
                }
                else
                {
                    Debug.Log("Error");
                }
                _orders.Add(_order);
                
            }
            string _ordersText = "";
            foreach (var item in _orders)
            {
                _ordersText += item.ToString() + ", ";
            }
            print("Order is: " + _ordersText);
        }
        //print(_customerData._ordersCompleted);
        return _orders;
    }
    public PaymentType GetPayment(Order order)
    {
        int _randomNum = Random.Range(0, 100);
        if (order == Order.Helmet)
        {
            _cost = 30;
        }
        else if (order == Order.Sword)
        {
            _cost = 80;
        }
        else if (order == Order.Arrow)
        {
            _cost = 150;
        }

        return _paymentType + _paymentAmount;
    }
    public void StartTimer()
    {
        _timerStarted = true;
        //print("timer started");
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            transform.Translate(4, 0, 0);
        }
        if (_timerStarted == true)
        {
            _timerTime -= Time.deltaTime;
            if (_timerTime <= 0)
            {
                _customerData.CompleteOrder(); //here for testing needs to be moved 
                // _customerData.FailOrder();  <--- this should be here
                //print("Timer Ended. Order Failed.");
                _timerStarted = false;
                //transform.Translate(0, 0, 15);
            }
        }
    }
}
