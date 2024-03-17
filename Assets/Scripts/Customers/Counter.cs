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

    Counter _counter;

    public GameObject _currentCustomer;
    int ordersThru = 0;
    public bool _ignoreItem01 = false;
    public bool _ignoreItem02 = false;
    public bool _ignoreItem03 = false;

    // Start is called before the first frame update
    void Start()
    {
        _counter = gameObject.GetComponent<Counter>();
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
        //var itemUnknown = _currentCustomer.GetComponent<Customer>()._order;
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
            //}
            //if (_currentCustomer.GetComponent<Customer>()._ordersThru == 0)
            //{
            //    itemUnknown = item01;
            //}
            //else if (_currentCustomer.GetComponent<Customer>()._ordersThru == 1)
            //{
            //    itemUnknown = item02;
            //}
            //else if (_currentCustomer.GetComponent<Customer>()._ordersThru == 2)
            //{
            //    itemUnknown = item03;
            //}
        }
            if (_weapon is Helmet && (item01.ToString().Contains("Helmet") || item02.ToString().Contains("Helmet") || item03.ToString().Contains("Helmet")))
            {
                //_weapon.GetComponentInChildren(Renderer).material;
                if (_weapon.weaponColor == Weapon.WeaponColor.Red
                    && ((item01.ToString().Contains("_R") && _ignoreItem01 != true)
                    || (item02.ToString().Contains("_R") && _ignoreItem02 != true)
                    || (item03.ToString().Contains("_R") && _ignoreItem03 != true)))
                {
                    _customerData.CompleteOrder(_currentCustomer, _counter, _weapon);
                    print("is red");
                }
                else if (_weapon.weaponColor == Weapon.WeaponColor.Blue
                    && ((item01.ToString().Contains("_B") && _ignoreItem01 != true)
                    || (item02.ToString().Contains("_B") && _ignoreItem02 != true)
                    || (item03.ToString().Contains("_B") && _ignoreItem03 != true)))
                {
                    _customerData.CompleteOrder(_currentCustomer, _counter, _weapon);
                    print("is blue");
                }
                else
                {
                    _customerData.FailOrder(_currentCustomer);
                    print("HelmetCheckFailed");
                    print("ignore items: " + _ignoreItem01 + " " + _ignoreItem02 + " " + _ignoreItem03);
                    print("weapon color: " + _weapon.weaponColor);
                    print("items to strings red: " + item01.ToString().Contains("_R") + " " + item02.ToString().Contains("_R") + " " + item03.ToString().Contains("_R"));
                    print("items to strings blue: " + item01.ToString().Contains("_B") + " " + item02.ToString().Contains("_B") + " " + item03.ToString().Contains("_B"));
            }
            }
            else if (_weapon is Sword && (item01.ToString().Contains("Sword") || item02.ToString().Contains("Sword") || item03.ToString().Contains("Sword")))
            {
                //_weapon.GetComponentInChildren(Renderer).material;
                if (_weapon.weaponColor == Weapon.WeaponColor.Red
                    && ((item01.ToString().Contains("_R") && _ignoreItem01 != true)
                    || (item02.ToString().Contains("_R") && _ignoreItem02 != true)
                    || (item03.ToString().Contains("_R") && _ignoreItem03 != true)))
                {
                    _customerData.CompleteOrder(_currentCustomer, _counter, _weapon);
                }
                else if (_weapon.weaponColor == Weapon.WeaponColor.Blue
                     && ((item01.ToString().Contains("_B") && _ignoreItem01 != true)
                    || (item02.ToString().Contains("_B") && _ignoreItem02 != true)
                    || (item03.ToString().Contains("_B") && _ignoreItem03 != true)))
                {
                    _customerData.CompleteOrder(_currentCustomer, _counter, _weapon);
                }
                else
                {
                    _customerData.FailOrder(_currentCustomer);
                    print("SwordCheckFailed");
                    print("ignore items: " + _ignoreItem01 + " " + _ignoreItem02 + " " + _ignoreItem03);
                    print("weapon color: " + _weapon.weaponColor);
                    print("items to strings red: " + item01.ToString().Contains("_R") + " " + item02.ToString().Contains("_R") + " " + item03.ToString().Contains("_R"));
                    print("items to strings blue: " + item01.ToString().Contains("_B") + " " + item02.ToString().Contains("_B") + " " + item03.ToString().Contains("_B"));
                }
            }
            else if (_weapon is Arrow && (item01.ToString().Contains("Arrow") || item02.ToString().Contains("Arrow") || item03.ToString().Contains("Arrow")))
            {
                //_weapon.GetComponentInChildren(Renderer).material;
                if (_weapon.weaponColor == Weapon.WeaponColor.Red
                    && ((item01.ToString().Contains("_R") && _ignoreItem01 != true)
                    || (item02.ToString().Contains("_R") && _ignoreItem02 != true)
                    || (item03.ToString().Contains("_R") && _ignoreItem03 != true)))
                {
                    _customerData.CompleteOrder(_currentCustomer, _counter, _weapon);
                }
                else if (_weapon.weaponColor == Weapon.WeaponColor.Blue
                    && ((item01.ToString().Contains("_B") && _ignoreItem01 != true)
                    || (item02.ToString().Contains("_B") && _ignoreItem02 != true)
                    || (item03.ToString().Contains("_B") && _ignoreItem03 != true)))
                {
                    _customerData.CompleteOrder(_currentCustomer, _counter, _weapon);
                }
                else
                {
                    _customerData.FailOrder(_currentCustomer);
                    print("ArrowCheckFailed");
                    print("ignore items: " + _ignoreItem01 + " " + _ignoreItem02 + " " + _ignoreItem03);
                    print("weapon color: " + _weapon.weaponColor);
                    print("items to strings red: " + item01.ToString().Contains("_R") + " " + item02.ToString().Contains("_R") + " " + item03.ToString().Contains("_R"));
                    print("items to strings blue: " + item01.ToString().Contains("_B") + " " + item02.ToString().Contains("_B") + " " + item03.ToString().Contains("_B"));
            }
            }
            else
            {
                _customerData.FailOrder(_currentCustomer);
            }
    }
}
