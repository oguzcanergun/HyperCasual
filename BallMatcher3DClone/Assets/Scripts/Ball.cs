using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Backbones;

public class Ball : MonoBehaviour
{
    //Ball type, let's leave it for future iterations
    //Ball color, enum
    [SerializeField] private BallColour ballColour;
    //Speed constant, float
    [SerializeField] private float ballSpeedFactor;
    //Is it moving, bool
    [SerializeField] private bool isBallMoving;
    //Is it selected, bool
    [SerializeField] private bool isBallSelected;
    [SerializeField] private float ballScoreValue;

    //Ball status, enum
    private BallStatus ballStatus;
    //Direction or Destination, Vector3
    private Vector3 ballDestination;
    private BallMaster ballMaster;
    private EndGameAndScore endGameAndScore;
    private int sameColorBallCount;
    private int collidedBallCount;
    private Transform ballIndicator;
    private GameObject ballIndicatorImage;
    private GameObject ballSelectedImage;

    // Start is called before the first frame update
    void Start()
    {
        InitialValues();
    }

    private void FixedUpdate()
    {
        if ((isBallMoving) && (!isBallSelected))
        {
            MoveBall();
            ActivateCompass();
        }
    }

    public void SetBallColour(BallColour colour)
    {
        ballColour = colour;
    }

    public BallColour GetBallColour()
    {
        return ballColour;
    }

    public void SetBallSpeedFactor(float speedFactor)
    {
        ballSpeedFactor = speedFactor;
    }

    public void SetBallScoreValue(float ballValue)
    {
        ballScoreValue = ballValue;
    }

    public void SetBallDestination(Vector3 destination)
    {
        ballDestination = destination;
        isBallMoving = true;
        if (ballStatus == BallStatus.Resting)
        {
            ballStatus = BallStatus.Moving;
        }
    }

    public void SetBallSelected(bool status)
    {
        if ((status) && (!isBallMoving))
        {
            isBallSelected = status;
            ballStatus = BallStatus.Calling;
            sameColorBallCount = ballMaster.CallForBalls(ballColour, gameObject.transform.position);
            ActivateSelected();
        }
    }

    private void InitialValues()
    {
        isBallMoving = false;
        isBallSelected = false;
        ballIndicator = gameObject.transform.GetChild(0);
        ballIndicatorImage = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        ballIndicatorImage.SetActive(false);
        ballSelectedImage = gameObject.transform.GetChild(0).GetChild(1).gameObject;
        ballSelectedImage.SetActive(false);

        endGameAndScore = gameObject.transform.parent.gameObject.GetComponent<EndGameAndScore>();
        ballStatus = BallStatus.Resting;
        ballMaster = gameObject.transform.parent.gameObject.GetComponent<BallMaster>();
        collidedBallCount = 0;
    }

    private void MoveBall()
    {
        transform.position = Vector3.MoveTowards(transform.position, ballDestination, ballSpeedFactor * Time.deltaTime);
    }

    private void ActivateCompass()
    {
        ballIndicatorImage.SetActive(true);
        ballIndicator.transform.right = ballDestination - ballIndicator.transform.position;
    }

    private void ActivateSelected()
    {
        ballSelectedImage.SetActive(true);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Ball collidedBall = collision.gameObject.GetComponent<Ball>();
        if (collidedBall != null)
        {
            BallColour collidedBallColour = collidedBall.GetBallColour();
            if (ballColour.Equals(collidedBallColour))
            {
                if (ballStatus.Equals(BallStatus.Calling))
                {
                    collidedBall.gameObject.SetActive(false);
                    collidedBallCount++;
                    if (collidedBallCount == (sameColorBallCount - 1))
                    {
                        endGameAndScore.IncreaseScore(ballScoreValue);
                        gameObject.SetActive(false);
                    }
                    endGameAndScore.IncreaseScore(ballScoreValue);
                }
            }
            else
            {
                endGameAndScore.GameOver();
            }
        }
    }

}
