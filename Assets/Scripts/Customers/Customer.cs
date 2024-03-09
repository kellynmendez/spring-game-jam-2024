using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] int HelmetOdds = 100;
    [SerializeField] int SwordOdds = 50;
    [SerializeField] int ArrowOdds = 20;
    public enum Order { Helmet, Sword, Arrow };
    private Order _order;
    public enum PaymentType { Metal, Wood, Arrow };
    private PaymentType _paymentType;
    private int _paymentAmount = 0;
    private bool _timerStarted = false;
    public float _timerTime = 10;
    private int _ordersDone = 0;
    private int _orderNumMax = 3;

    private void Start()
    {
        //GetOrder();
    }
    public Order GetOrder()
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


        if (_ordersDone == 0)
        {
            _order = Order.Helmet;
            print("Order is: " + _order);
        }
        else if (_ordersDone == 1)
        {
            _order = Order.Sword;
            print("Order is: " + _order);
        }
        else if (_ordersDone == 2)
        {
            _order = Order.Arrow;
            print("Order is: " + _order);
        }
        else if (_ordersDone >= 3)
        {
            int _randOrderType = Random.Range(0, 100);
            if (_randOrderType <= ArrowOdds)
            {
                _order = Order.Arrow;
                GetPayment(_order);
            }
            else if (_randOrderType <= SwordOdds)
            {
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
            print("Order is: " + _order + " based on random number: " + _randOrderType);
        }
        print(_ordersDone);
        return _order;
    }
    public PaymentType GetPayment(Order order)
    {
        int _randomNum = Random.Range(0, 100);
        if (order == Order.Helmet)
        {
            if (_randomNum <= ArrowOdds)
            {

            }
        }
        else if (order == Order.Sword)
        {

        }
        else if (order == Order.Arrow)
        {

        }


        return _paymentType + _paymentAmount;
    }
    public void StartTimer()
    {
        _timerStarted = true;
        print("timer started");
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
                _ordersDone = _ordersDone + 1;
                print(_ordersDone);
                print("timer ended");
                _timerStarted = false;
                //transform.Translate(0, 0, 15);
            }
        }
    }
}
