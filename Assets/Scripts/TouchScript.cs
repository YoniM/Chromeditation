using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour
{
    #region Singleton Setup
    public static TouchScript Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion Singleton Setup

    [Header("Tweaks")]
    [SerializeField] private float deadzone = Screen.width * 0.05f;
    [SerializeField] private float releasezone = 0.25f * (Screen.height + Screen.width);
    [SerializeField] public float sensitivity = 0.005f;

    private bool tap, isDraging, isSwipe;
    private Vector2 startTouch, holdDelta;
    private float releasezonesqr, deadzonesqr;

    public bool Tap { get { return tap; } }
    public bool IsDraging { get { return isDraging; } }
    public bool IsSwipe { get { return isSwipe; } }

    public Vector2 HoldDelta { get { return holdDelta; } }

    

    private void Start()
    {
        deadzonesqr = deadzone * deadzone;
        releasezonesqr = releasezone* releasezone;
    }


    private void Update()
    {
        tap = false;
        #if UNITY_EDITOR
            UpdateStandalone();
        #else
            UpdateMobile();
        #endif


        if (isDraging)
        {
            if (Input.touches.Length > 0)
                holdDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                holdDelta = (Vector2)Input.mousePosition - startTouch;
        }

        if (isDraging && holdDelta.SqrMagnitude() > deadzonesqr)
            isSwipe = true;
            
        if (!isDraging || (isDraging && holdDelta.SqrMagnitude() > releasezonesqr))
            Reset();
        
    }

    private void UpdateStandalone()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tap = isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }
    }

    private void UpdateMobile()
    {
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = isDraging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                Reset();
            }
        }
    }

    private void Reset()
    {
        tap = isDraging = false;
        startTouch = holdDelta = Vector2.zero;
    }

}
