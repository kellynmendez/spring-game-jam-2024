using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static CustomerOrders;

public class CustomerOrders : MonoBehaviour
{
    [SerializeField] int HelmetOdds = 100;
    [SerializeField] int SwordOdds = 50;
    [SerializeField] int ArrowOdds = 20;
    public enum Order {Helmet, Sword, Arrow};
    private Order _order;


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
        }
        else if (_randomNum <= SwordOdds)
        {
            _order = Order.Sword;
        }
        else if (_randomNum <= HelmetOdds)
        {
            _order = Order.Helmet;
        }
        else
        {
            Debug.Log("Error");
        }

        print("Order is: " + _order + " based on random number: " + _randomNum);
        return _order;
    }
}

//public class PossibleOrders
//{
//    public string order;
//    public float orderChance;

//    public PossibleOrders(string neworder, float neworderChance)
//    {
//        order = neworder;
//        orderChance = neworderChance;
//    }

//}

//List<PossibleOrders> orders = new List<PossibleOrders>();

//void Start()
//{
//    orders.Add(new PossibleOrders("Helmet", 100));
//    orders.Add(new PossibleOrders("Sword", 60));
//    orders.Add(new PossibleOrders("Arrow", 30));

//    PossibleOrders customerOrder = GetOrder();
//}

//PossibleOrders GetOrder()
//{
//    float randomNumber = Random.Range(1, 101);
//    List<PossibleOrders> possibleOrders = new List<PossibleOrders>();

//    foreach (PossibleOrders item in orders) 
//    {
//        if (randomNumber <= item.orderChance)
//        {
//            possibleOrders.Add(item);
//        }
//    }

//    if (possibleOrders.Count > 0)
//    {
//        PossibleOrders order = possibleOrders[Random.Range(0, possibleOrders.Count)];
//        print(order);
//        return order;
//    }
//    Debug.Log("Nothing Was Ordered");
//    return null;
//}