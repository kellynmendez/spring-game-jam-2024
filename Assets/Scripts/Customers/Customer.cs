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

    private void Start()
    {
        //GetOrder();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            transform.Translate(4, 0, 0);
        }
    }

    public Order GetOrder()
    {
        int _randomNum = Random.Range(0, 100);
        if (_randomNum <= ArrowOdds)
        {
            _order = Order.Arrow;
            GetPayment(_order);
        }
        else if (_randomNum <= SwordOdds)
        {
            _order = Order.Sword;
            GetPayment(_order);
        }
        else if (_randomNum <= HelmetOdds)
        {
            _order = Order.Helmet;
            GetPayment(_order);
        }
        else
        {
            Debug.Log("Error");
        }

        print("Order is: " + _order + " based on random number: " + _randomNum);
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
}
