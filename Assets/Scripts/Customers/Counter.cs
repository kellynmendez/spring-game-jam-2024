using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Counter : MonoBehaviour
{
    public bool _counterIsEmpty = true;
    PACPointer PACPointer;
    Customer_Data _customerData;

    public GameObject _currentCustomer;
    int ordersThru = 0;

    // Start is called before the first frame update
    void Start()
    {
        _counterIsEmpty = true;
        PACPointer = GameObject.FindObjectOfType(typeof(PACPointer)) as PACPointer;
        _customerData = GameObject.FindObjectOfType(typeof(Customer_Data)) as Customer_Data;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<Collider>().isTrigger = false;
        PACPointer.inputDisabled = false;
        if (_currentCustomer != null)
        {
            //_customerData.CompleteOrder(_currentCustomer);
        }
    }

    public void CheckOrder(Weapon _weapon)
    {
        var item01 = _currentCustomer.GetComponent<Customer>()._order;
        var item02 = _currentCustomer.GetComponent<Customer>()._order;
        var item03 = _currentCustomer.GetComponent<Customer>()._order;
        var itemUnknown = _currentCustomer.GetComponent<Customer>()._order;
        int items = 0;
        foreach (var item in _currentCustomer.GetComponent<Customer>()._orders)
        {
            items++;
            if (items == 1)
            {
                item01 = item;
            }
            if (items == 2)
            {
                item02 = item;
            }
            if (items == 3)
            {
                item03 = item;
            }
        }
        if (_currentCustomer.GetComponent<Customer>()._ordersThru == 0)
        {
            itemUnknown = item01;
        }
        else if (_currentCustomer.GetComponent<Customer>()._ordersThru == 1)
        {
            itemUnknown = item02;
        }
        else if (_currentCustomer.GetComponent<Customer>()._ordersThru == 2)
        {
            itemUnknown = item03;
        }

        if (_weapon is Helmet && (itemUnknown is Customer.Order.Helmet_R || itemUnknown is Customer.Order.Helmet_B))
        {
            //_weapon.GetComponentInChildren(Renderer).material;
            if (_weapon.weaponColor == Weapon.WeaponColor.Red && itemUnknown.ToString().Contains("_R"))
            {
                _customerData.CompleteOrder(_currentCustomer, _weapon);
                print("is red");
            }
            else if (_weapon.weaponColor == Weapon.WeaponColor.Blue && itemUnknown.ToString().Contains("_B"))
            {
                _customerData.CompleteOrder(_currentCustomer, _weapon);
                print("is blue");
            }
            else
            {
                _customerData.FailOrder(_currentCustomer);
            }
        }
        else if (_weapon is Sword && (itemUnknown is Customer.Order.Sword_R || itemUnknown is Customer.Order.Sword_B))
        {
            //_weapon.GetComponentInChildren(Renderer).material;
            if (_weapon.weaponColor == Weapon.WeaponColor.Red && itemUnknown.ToString().Contains("_R"))
            {
                _customerData.CompleteOrder(_currentCustomer, _weapon);
            }
            else if (_weapon.weaponColor == Weapon.WeaponColor.Blue && itemUnknown.ToString().Contains("_B"))
            {
                _customerData.CompleteOrder(_currentCustomer, _weapon);
            }
            else
            {
                _customerData.FailOrder(_currentCustomer);
            }
        }
        else if (_weapon is Arrow && (itemUnknown is Customer.Order.Arrow_R || itemUnknown is Customer.Order.Arrow_B))
        {
            //_weapon.GetComponentInChildren(Renderer).material;
            if (_weapon.weaponColor == Weapon.WeaponColor.Red && itemUnknown.ToString().Contains("_R"))
            {
                _customerData.CompleteOrder(_currentCustomer, _weapon);
            }
            else if (_weapon.weaponColor == Weapon.WeaponColor.Blue && itemUnknown.ToString().Contains("_B"))
            {
                _customerData.CompleteOrder(_currentCustomer, _weapon);
            }
            else
            {
                _customerData.FailOrder(_currentCustomer);
            }
        }
        else
        {
            _customerData.FailOrder(_currentCustomer);
        }
    }
}
