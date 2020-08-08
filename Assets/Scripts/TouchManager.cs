using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    #region Singleton Setup
    public static TouchManager Instance { get; private set; }

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

    public bool Tap { get { Debug.Log("tap  = " + tap); return tap; } }
    public bool IsDraging { get { Debug.Log("draging = " +isDraging); return isDraging; } }
    public bool IsSwipe { get { Debug.Log("swipe = " + isSwipe); return isSwipe; } }

    public Vector2 StartTouch { get { Debug.Log("StartTouch = " + startTouch); return startTouch; } }

    public Vector2 HoldDelta { get { Debug.Log(holdDelta); return holdDelta; } }

    

    private void Start()
    {
        deadzonesqr = deadzone * deadzone;
        releasezonesqr = releasezone * releasezone;
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

        if (!isSwipe && isDraging && holdDelta.SqrMagnitude() > deadzonesqr)
            isSwipe = true;
            
        if (isDraging && holdDelta.SqrMagnitude() > releasezonesqr)
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
        tap = isDraging = isSwipe = false;
        startTouch = holdDelta = Vector2.zero;
    }

}
