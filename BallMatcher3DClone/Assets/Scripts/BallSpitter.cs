using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpitter : MonoBehaviour
{
    [SerializeField] private Ball[] toMinusX;
    [SerializeField] private Ball[] toPlusX;
    [SerializeField] private Ball[] toMinusY;
    [SerializeField] private Ball[] toPlusY;
    private Vector3 minusXDestination;
    private Vector3 plusXDestination;
    private Vector3 minusYDestination;
    private Vector3 plusYDestination;

    // Start is called before the first frame update
    void Start()
    {
        InitialValues();
        SetBallDestinations();
    }

    private void InitialValues()
    {
        minusXDestination = new Vector3(-200.0f, 0.0f, 0.0f);
        plusXDestination = new Vector3(200.0f, 0.0f, 0.0f);
        minusYDestination = new Vector3(0.0f, -200.0f, 0.0f);
        plusYDestination = new Vector3(0.0f, 200.0f, 0.0f);
    }

    private void SetBallDestinations()
    {
        GameObject ballObject;
        for (int i = 0; i < toMinusX.Length; i++)
        {
            ballObject = toMinusX[i].gameObject;
            minusXDestination = new Vector3(-200.0f, ballObject.transform.position.y, ballObject.transform.position.z);
            toMinusX[i].SetBallDestination(minusXDestination);
        }
        for (int i = 0; i < toPlusX.Length; i++)
        {
            ballObject = toPlusX[i].gameObject;
            plusXDestination = new Vector3(200.0f, ballObject.transform.position.y, ballObject.transform.position.z);
            toPlusX[i].SetBallDestination(plusXDestination);
        }
        for (int i = 0; i < toMinusY.Length; i++)
        {
            ballObject = toMinusY[i].gameObject;
            minusYDestination = new Vector3(ballObject.transform.position.x, -200.0f, ballObject.transform.position.z);
            toMinusY[i].SetBallDestination(minusYDestination);
        }
        for (int i = 0; i < toPlusY.Length; i++)
        {
            ballObject = toPlusY[i].gameObject;
            plusYDestination = new Vector3(ballObject.transform.position.x, 200.0f, ballObject.transform.position.z);
            toPlusY[i].SetBallDestination(plusYDestination);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}
