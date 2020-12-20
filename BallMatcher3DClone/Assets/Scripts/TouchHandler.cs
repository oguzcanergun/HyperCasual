using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchHandler : MonoBehaviour
{
    private bool touchTimerBool;
    private float touchTimer;
    private float touchTimerLimit;
    private Ball targetBall;

    private void Start()
    {
        InitialValues();
    }

    private void InitialValues()
    {
        touchTimerBool = false;
        touchTimer = 0;
        touchTimerLimit = 0.2f;
        targetBall = null;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            LayerMask ballMask = LayerMask.GetMask("Ball");
            if (Time.timeScale != 0.0f)
            {
                if (Physics.Raycast(ray, out hit, ballMask))
                {
                    targetBall = hit.transform.gameObject.GetComponent<Ball>();
                    if (targetBall != null)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            touchTimer = 0.0f;
                            touchTimerBool = true;
                        }
                        else if (Input.GetMouseButtonUp(0))
                        {
                            touchTimer = 0.0f;
                        }
                    }
                }
            }
        }
#endif
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            LayerMask ballMask = LayerMask.GetMask("Ball");
            if (Time.timeScale != 0.0f)
            {
                if (Physics.Raycast(ray, out hit, ballMask))
                {
                    targetBall = hit.transform.gameObject.GetComponent<Ball>();
                    if (targetBall != null)
                    {
                        if (Input.GetTouch(0).phase == TouchPhase.Began)
                        {
                            touchTimer = 0.0f;
                            touchTimerBool = true;
                        }
                        else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                        {
                            touchTimer = 0.0f;
                        }
                    }
                }
            }
        }
#endif
    }

    private void FixedUpdate()
    {
        if (touchTimerBool)
        {
            touchTimer = touchTimer + Time.deltaTime;
            if ((touchTimer >= touchTimerLimit) && (touchTimerBool))
            {
                touchTimerBool = false;
                targetBall.SetBallSelected(true);
            }
        }
    }
}
