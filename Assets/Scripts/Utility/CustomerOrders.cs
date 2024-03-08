using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CustomerOrders;

public class CustomerOrders : MonoBehaviour
{
    public class PossibleOrders
    {
        public string order;
        public float orderChance;

        public PossibleOrders(string neworder, float neworderChance)
        {
            order = neworder;
            orderChance = neworderChance;
        }

    }

    List<PossibleOrders> orders = new List<PossibleOrders>();

    void Start()
    {
        orders.Add(new PossibleOrders("Helmet", 100));
        orders.Add(new PossibleOrders("Sword", 60));
        orders.Add(new PossibleOrders("Arrow", 30));

        PossibleOrders customerOrder = GetOrder();
    }

    PossibleOrders GetOrder()
    {
        float randomNumber = Random.Range(1, 101);
        List<PossibleOrders> possibleOrders = new List<PossibleOrders>();

        foreach (PossibleOrders item in orders) 
        {
            if (randomNumber <= item.orderChance)
            {
                possibleOrders.Add(item);
            }
        }

        if (possibleOrders.Count > 0)
        {
            PossibleOrders order = possibleOrders[Random.Range(0, possibleOrders.Count)];
            print(order);
            return order;
        }
        Debug.Log("Nothing Was Ordered");
        return null;
    }


}
