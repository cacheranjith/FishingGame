using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    /// this script is taken from chatgpt Dont know hook transfromtion 

    ///Instance Creation
    public static PlayerController Instance;

    ///Hook movements configuration
    public float ascendingSpeed;
    public float descendingSpeed;
    public float maxDistance = 10f;
    public float speed = 5f;
    public float leftBound = -10f;
    public float rightBound = 10f;
    public bool isAscending = true;

    /// hook position value
    private Vector3 startPosition;

    ///getting boat as game object
    public GameObject boat;

    ///fishcount value  
    public int fishcount;

    //fish catching rod -> secondObject
    public GameObject secondObject;
    private LineRenderer lineRenderer;

    //game level
    public int depthlvl;

    //fish caught
    public bool iscaught = false;

    public Vector3 touchPosition;
    public Rigidbody2D rb;
    public float dragspeed = 10f;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        startPosition = transform.position;
        maxDistance = UIManager.Instance.maxdpth;
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lineRenderer.SetPosition(0, transform.position); // line renderer connection between fishing rod and hook
        lineRenderer.SetPosition(1, secondObject.transform.position); // line renderer connection between fishing rod and hook
        //Debug.Log("FishController.Instance.LmtRched" + FishController.Instance.LmtRched);
        //if (FishController.Instance.LmtRched == true)
        //{
        //    descendingSpeed = 10f;
        //}
        //else
        //{
        //    descendingSpeed = 1f;
        //}
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2f, 2f), transform.position.y, transform.position.z);
        if (GameManager.instance.isgamestart == true)
        {

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;
                Vector3 direction = (touchPosition - transform.position);
                // Zero out the Y component of the direction vector
                direction.y = 0f;
                rb.velocity = direction.normalized * dragspeed;
                if (touch.phase == TouchPhase.Ended)
                    rb.velocity = Vector2.zero;
            }

            if (isAscending)
            {
                transform.Translate(Vector3.down * ascendingSpeed * Time.deltaTime);
                if (transform.position.y <= startPosition.y - maxDistance)
                {
                    isAscending = false;
                }
            }
            else
            {
                transform.Translate(Vector3.up * descendingSpeed * Time.deltaTime);
                if (transform.position.y >= boat.transform.position.y)
                {
                    isAscending = true;
                    UIManager.Instance.CoincollectUI.SetActive(true);
                    GameManager.instance.StopGame();
                }
            }
        }
    }
}