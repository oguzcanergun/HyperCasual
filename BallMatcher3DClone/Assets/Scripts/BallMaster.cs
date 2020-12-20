using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Backbones;

public class BallMaster : MonoBehaviour
{
    private IDictionary<Ball, BallColour> balls = new Dictionary<Ball, BallColour>();
    // Start is called before the first frame update
    void Start()
    {
        GetBalls();
    }

    public int CallForBalls(BallColour ballCallColour, Vector3 ballCallDestination)
    {
        int sameColorBallCount = 0;
        foreach (var toMoveCandidate in balls)
        {
            if (toMoveCandidate.Value.Equals(ballCallColour))
            {
                sameColorBallCount++;
                toMoveCandidate.Key.SetBallDestination(ballCallDestination);
            }
        }
        return sameColorBallCount;
    }

    private void GetBalls()
    {
        Ball ball;
        BallColour ballColour;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            ball = gameObject.transform.GetChild(i).GetComponent<Ball>();
            if (ball != null)
            {
                ballColour = ball.GetBallColour();
                balls.Add(ball, ballColour);
            }
        }
    }

}
