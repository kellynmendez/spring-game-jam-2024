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
    public enum Order { Helmet_Red, Helmet_Blue, Sword_Red, Sword_Blue, Arrow_Red, Arrow_Blue };
    public Order _order;
    [HideInInspector] public List<Order> _orders = new List<Order>();
    [HideInInspector] public int _paymentAmount = 0;
    public bool _timerStarted = false;
    public float _timerMax = 60;
    private float _timerTime = 60;
    [SerializeField] UnityEngine.UI.Slider _timerUI;
    [SerializeField] UnityEngine.UI.Image _timerFace;
    [SerializeField] Sprite _timerHappy;
    [SerializeField] Sprite _timerAnnoyed;
    [SerializeField] Sprite _timerAngry;
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
    [SerializeField] UnityEngine.UI.Image _orderSheetImage;
    [SerializeField] Sprite _orderCompletedSprite;
    [SerializeField] Sprite _orderFailedSprite;
    [SerializeField] Sprite _checkmark;
    [SerializeField] Sprite _helmetSpriteRed;
    [SerializeField] Sprite _swordSpriteRed;
    [SerializeField] Sprite _arrowSpriteRed;
    [SerializeField] Sprite _helmetSpriteBlue;
    [SerializeField] Sprite _swordSpriteBlue;
    [SerializeField] Sprite _arrowSpriteBlue;
    [SerializeField] public GameObject _1Order;
    [SerializeField] public GameObject _2Order;
    [SerializeField] public GameObject _3Order;
    [SerializeField] public GameObject _item01Check = null;
    [SerializeField] public GameObject _item02Check = null;
    [SerializeField] public GameObject _item03Check = null;
    public List<GameObject> _itemsChecks = new List<GameObject>();
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
        _itemsChecks.Add(_item01Check);
        _itemsChecks.Add(_item02Check);
        _itemsChecks.Add(_item03Check);
        _timerTime = _timerMax;
        _timerUI.maxValue = _timerMax;
        _timerUI.value = 0;
        _timerFace.sprite = _timerHappy;
    }

    public List<Order> GetOrder()
    {
        if (_customerData._ordersCompleted == 0)
        {
            int randomNum = Random.Range(1, 100);
            if (randomNum >= 50)
            {
                _order = Order.Helmet_Red;
            }
            else
            {
                _order = Order.Helmet_Blue;
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
                _order = Order.Sword_Red;
            }
            else
            {
                _order = Order.Sword_Blue;
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
                _order = Order.Arrow_Red;
            }
            else
            {
                _order = Order.Arrow_Blue;
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
                if ((_randOrderType <= ArrowOdds) && ((!_orders.Contains(Order.Arrow_Red)) || (!_orders.Contains(Order.Arrow_Blue))))
                {
                    int _ranNum = Random.Range(1, 100);
                    if (_ranNum >= 50)
                    {
                        _order = Order.Arrow_Red;
                    }
                    else
                    {
                        _order = Order.Arrow_Blue;
                    }
                }
                else if ((_randOrderType <= SwordOdds) && (_swordCount < 2))
                {
                    int _ranNum = Random.Range(1, 100);
                    if (_ranNum >= 50)
                    {
                        _order = Order.Sword_Red;
                    }
                    else
                    {
                        _order = Order.Sword_Blue;
                    }
                    _swordCount++;
                }
                else if (_randOrderType <= HelmetOdds)
                {
                    int _ranNum = Random.Range(1, 100);
                    if (_ranNum >= 50)
                    {
                        _order = Order.Helmet_Red;
                    }
                    else
                    {
                        _order = Order.Helmet_Blue;
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
        _orderSheetImage.gameObject.SetActive(true);
        //}
        //Set the UI
        if (_orders.Count == 1)
        {
            _1Order.SetActive(true);
            int numOfLoops = 0;
            foreach (var item in _orders)
            {
                GameObject _image = _1Order.transform.GetChild(numOfLoops).gameObject;
                _itemsChecks[numOfLoops] = _1Order.transform.GetChild(numOfLoops + 1).gameObject;
                numOfLoops++;
                if (item == Order.Helmet_Red)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _helmetSpriteRed;
                }
                else if (item == Order.Helmet_Blue)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _helmetSpriteBlue;
                }
                else if (item == Order.Sword_Red)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _swordSpriteRed;
                }
                else if (item == Order.Sword_Blue)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _swordSpriteBlue;
                }
                else if (item == Order.Arrow_Red)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _arrowSpriteRed;
                }
                else if (item == Order.Arrow_Blue)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _arrowSpriteBlue;
                }
            }
        }
        else if (_orders.Count == 2)
        {
            _2Order.SetActive(true);
            int numOfLoops = 0;
            foreach (var item in _orders)
            {
                GameObject _image = _2Order.transform.GetChild(numOfLoops).gameObject;
                _itemsChecks[numOfLoops] = _2Order.transform.GetChild(numOfLoops + 2).gameObject;
                numOfLoops++;
                if (item == Order.Helmet_Red)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _helmetSpriteRed;
                }
                else if (item == Order.Helmet_Blue)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _helmetSpriteBlue;
                }
                else if (item == Order.Sword_Red)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _swordSpriteRed;
                }
                else if (item == Order.Sword_Blue)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _swordSpriteBlue;
                }
                else if (item == Order.Arrow_Red)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _arrowSpriteRed;
                }
                else if (item == Order.Arrow_Blue)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _arrowSpriteBlue;
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
                _itemsChecks[numOfLoops] = _3Order.transform.GetChild(numOfLoops + 3).gameObject;
                numOfLoops++;
                if (item == Order.Helmet_Red)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _helmetSpriteRed;
                }
                else if (item == Order.Helmet_Blue)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _helmetSpriteBlue;
                }
                else if (item == Order.Sword_Red)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _swordSpriteRed;
                }
                else if (item == Order.Sword_Blue)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _swordSpriteBlue;
                }
                else if (item == Order.Arrow_Red)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _arrowSpriteRed;
                }
                else if (item == Order.Arrow_Blue)
                {
                    _image.GetComponent<SpriteRenderer>().sprite = _arrowSpriteBlue;
                }
            }
        }
        return _orders;
    }

    public int GetPayment(Order order)
    {
        int _randomNum = Random.Range(0, 100);
        if (order == Order.Helmet_Red || order == Order.Helmet_Blue)
        {
            _cost = _helmetCost;
        }
        else if (order == Order.Sword_Red || order == Order.Sword_Blue)
        {
            _cost = _swordCost;
        }
        else if (order == Order.Arrow_Red || order == Order.Arrow_Blue)
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
            if (_timerTime <= 0)
            {
                _customerData.FailOrder(gameObject);
                _timerStarted = false;
            }

            _timerTime -= Time.deltaTime;
            _timerUI.value = _timerTime;
            _timerUI.value = _timerMax - _timerTime;

            if (_timerUI.value >= .6 * _timerMax)
            {
                _timerFace.sprite = _timerAngry;
            }
            else if (_timerUI.value >= .3 * _timerMax)
            {
                _timerFace.sprite = _timerAnnoyed;
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
        _counter.GetComponent<Counter>()._ignoreItem01 = false;
        _counter.GetComponent<Counter>()._ignoreItem02 = false;
        _counter.GetComponent<Counter>()._ignoreItem03 = false;
        _counterPosistion = counter.position;
        _seekingDestination = true;
    }
    public void LeaveCounter(bool wasOrderCompleted)
    {
        _1Order.SetActive(false);
        _2Order.SetActive(false);
        _3Order.SetActive(false);
        _canvas.GetComponentInChildren<UnityEngine.UI.Image>().gameObject.SetActive(false);
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
