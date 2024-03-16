using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEditor.Progress;

public class Customer : MonoBehaviour
{
    [SerializeField] int HelmetOdds = 100;
    [SerializeField] int SwordOdds = 50;
    [SerializeField] int ArrowOdds = 20;
    public enum Order { Helmet_R, Helmet_B, Sword_R, Sword_B, Arrow_R, Arrow_B };
    public Order _order;
    [HideInInspector] public List<Order> _orders = new List<Order>();
    [HideInInspector] public int _paymentAmount = 0;
    public bool _timerStarted = false;
    public float _timerTime = 60;
    private int _orderNumMax = 3;
    private int _cost = 0;
    public int _helmetCost = 30;
    public int _swordCost = 80;
    public int _arrowCost = 150;

    [HideInInspector] public Customer_Data _customerData;

    [HideInInspector] public NavMeshAgent _agent;
    private bool _seekingDestination = false;
    private GameObject _counter;
    private Vector3 _counterPosistion;
    private bool _leavingCounter = false;
    
    private Canvas _canvas;
    private SpriteRenderer _spriteRender;
    [SerializeField] Sprite _orderCompletedSprite;
    [SerializeField] Sprite _orderFailedSprite;
    [SerializeField] Sprite _checkmark;
    [SerializeField] Sprite _helmetSpriteR;
    [SerializeField] Sprite _swordSpriteR;
    [SerializeField] Sprite _arrowSpriteR;
    [SerializeField] Sprite _helmetSpriteB;
    [SerializeField] Sprite _swordSpriteB;
    [SerializeField] Sprite _arrowSpriteB;
    [SerializeField] public GameObject _1Order;
    [SerializeField] public GameObject _2Order;
    [SerializeField] public GameObject _3Order;
    [HideInInspector] public Sprite _item01Sprite;
    [HideInInspector] public Sprite _item02Sprite;
    [HideInInspector] public Sprite _item03Sprite;
    public int _ordersThru = 0;

    private void Awake()
    {
        _customerData = GameObject.FindObjectOfType(typeof(Customer_Data)) as Customer_Data;
        _agent = GetComponent<NavMeshAgent>();
        _canvas = GetComponentInChildren<Canvas>();
        _spriteRender = _canvas.GetComponentInChildren<SpriteRenderer>();
        _spriteRender.enabled = false;
    }

    public List<Order> GetOrder()
    {
        if (_customerData._ordersCompleted == 0)
        {
            int randomNum = Random.Range(1, 100);
            if (randomNum >= 50)
            {
                _order = Order.Helmet_R;
            }
            else
            {
                _order = Order.Helmet_B;
            }
            _orders.Add(_order);
            _paymentAmount += GetPayment(_order);
            //print("Order is: " + _order);
        }
        else if (_customerData._ordersCompleted == 1)
        {
            int randomNum = Random.Range(1, 100);
            if (randomNum >= 50)
            {
                _order = Order.Sword_R;
            }
            else
            {
                _order = Order.Sword_B;
            }
            _orders.Add(_order);
            _paymentAmount += GetPayment(_order);
            //print("Order is: " + _order);
        }
        else if (_customerData._ordersCompleted == 2)
        {
            int randomNum = Random.Range(1, 100);
            if (randomNum >= 50)
            {
                _order = Order.Arrow_R;
            }
            else
            {
                _order = Order.Arrow_B;
            }
            _orders.Add(_order);
            _paymentAmount += GetPayment(_order);
            //print("Order is: " + _order);
        }

        if (_customerData._ordersCompleted >= 3)
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
                if ((_randOrderType <= ArrowOdds) && ((!_orders.Contains(Order.Arrow_R)) || (!_orders.Contains(Order.Arrow_B))))
                {
                    int _ranNum = Random.Range(1, 100);
                    if (_ranNum >= 50)
                    {
                        _order = Order.Arrow_R;
                    }
                    else
                    {
                        _order = Order.Arrow_B;
                    }
                }
                else if ((_randOrderType <= SwordOdds) && (_swordCount < 2))
                {
                    int _ranNum = Random.Range(1, 100);
                    if (_ranNum >= 50)
                    {
                        _order = Order.Sword_R;
                    }
                    else
                    {
                        _order = Order.Sword_B;
                    }
                    _swordCount++;
                }
                else if (_randOrderType <= HelmetOdds)
                {
                    int _ranNum = Random.Range(1, 100);
                    if (_ranNum >= 50)
                    {
                        _order = Order.Helmet_R;
                    }
                    else
                    {
                        _order = Order.Helmet_B;
                    }
                }
                _paymentAmount += GetPayment(_order);
                _orders.Add(_order);
            }
        }

        string _ordersText = "";
        foreach (var item in _orders)
        {
            _ordersText += item.ToString() + ", ";
        }
        print("Order is: " + _ordersText + " for " + _paymentAmount);

        //for (int numOfOrders = 0; numOfOrders < _orders.Count; numOfOrders++)
        //{

        //}
        //Set the UI
        if (_orders.Count == 1)
        {
            _1Order.SetActive(true);
            int numOfLoops = 0;
            foreach (var item in _orders)
            {
                GameObject _image = _1Order.transform.GetChild(numOfLoops).gameObject;
                numOfLoops++;
                if (item == Order.Helmet_R)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _helmetSpriteR;
                }
                else if (item == Order.Helmet_B)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _helmetSpriteB;
                }
                else if (item == Order.Sword_R)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _swordSpriteR;
                }
                else if (item == Order.Sword_B)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _swordSpriteB;
                }
                else if (item == Order.Arrow_R)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _arrowSpriteR;
                }
                else if (item == Order.Arrow_B)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _arrowSpriteB;
                }
            }
        }
        else if ( _orders.Count == 2) 
        {
            _2Order.SetActive(true);
            int numOfLoops = 0;
            foreach (var item in _orders)
            {
                GameObject _image = _2Order.transform.GetChild(numOfLoops).gameObject;
                numOfLoops++;
                if (item == Order.Helmet_R)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _helmetSpriteR;
                }
                else if (item == Order.Helmet_B)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _helmetSpriteB;
                }
                else if (item == Order.Sword_R)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _swordSpriteR;
                }
                else if (item == Order.Sword_B)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _swordSpriteB;
                }
                else if (item == Order.Arrow_R)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _arrowSpriteR;
                }
                else if (item == Order.Arrow_B)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _arrowSpriteB;
                }
            }
        }
        else if (_orders.Count == 3)
        {
            _3Order.SetActive(true);
            int numOfLoops = 0;
            foreach (var item in _orders)
            {
                GameObject _image = _3Order.transform.GetChild(numOfLoops).gameObject;
                numOfLoops++;
                if (item == Order.Helmet_R)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _helmetSpriteR;
                }
                else if (item == Order.Helmet_B)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _helmetSpriteB;
                }
                else if (item == Order.Sword_R)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _swordSpriteR;
                }
                else if (item == Order.Sword_B)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _swordSpriteB;
                }
                else if (item == Order.Arrow_R)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _arrowSpriteR;
                }
                else if (item == Order.Arrow_B)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _arrowSpriteB;
                }
            }
        }
        return _orders;
    }

    public int GetPayment(Order order)
    {
        int _randomNum = Random.Range(0, 100);
        if (order == Order.Helmet_R || order == Order.Helmet_B)
        {
            _cost = _helmetCost;
        }
        else if (order == Order.Sword_R || order == Order.Sword_B)
        {
            _cost = _swordCost;
        }
        else if (order == Order.Arrow_R || order == Order.Arrow_B)
        {
            _cost = _arrowCost;
        }

        return _cost;
    }
    public void StartTimer()
    {
        _timerStarted = true;
        //print("timer started");
    }
    private void Update()
    {
        //if (Input.GetButtonDown("Fire2"))
        //{
        //    transform.Translate(4, 0, 0);
        //}
        if (_timerStarted == true)
        {
            _timerTime -= Time.deltaTime;
            if (_timerTime <= 0)
            {
                _customerData.FailOrder(gameObject);
                //print("Timer Ended. Order Failed.");
                _timerStarted = false;
                //transform.Translate(0, 0, 15);
            }
        }

        if (_seekingDestination == false)
        { return; }

        if (!_agent.pathPending)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                transform.LookAt(_counterPosistion);
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    _seekingDestination = false;
                    if (_leavingCounter == true)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    public void ApproachCounter(Transform counter)
    {
        _agent.SetDestination(counter.GetChild(2).position);
        _counter = counter.gameObject;
        _counter.GetComponent<Counter>()._currentCustomer = gameObject;
        _counterPosistion = counter.position;
        _seekingDestination = true;
    }
    public void LeaveCounter(bool wasOrderCompleted)
    {
        if (wasOrderCompleted == true)
        {
            //happy pop up, walk away, destroy
            print("order done");
            _spriteRender.sprite = _orderCompletedSprite;
            _spriteRender.enabled = true;
        }
        else
        {
            //sadge pop up, walk away, setroy
            print("order failed");
            _spriteRender.sprite = _orderFailedSprite;
            _spriteRender.enabled = true;
        }
        StartCoroutine(WaitToLeave());
    }
    private IEnumerator WaitToLeave()
    {
        yield return new WaitForSeconds(1);
        _counter.GetComponent<Counter>()._counterIsEmpty = true;
        _agent.SetDestination(_customerData._customerDeathPoint.transform.position);
        _seekingDestination = true;
        _leavingCounter = true;
    }
}
