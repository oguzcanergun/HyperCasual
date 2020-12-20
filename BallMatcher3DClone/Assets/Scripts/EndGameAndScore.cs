using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameAndScore : MonoBehaviour
{
    [SerializeField] private MenuMethods menuObject;
    [SerializeField] private Text scoreText;

    private Transform ballsParent;
    private float userScore;
    private int deactiveChildCount;

    // Start is called before the first frame update
    void Start()
    {
        InitialValues();
    }

    public void GameOver()
    {
        menuObject.OnGameOver();
    }

    public void LevelComplete()
    {
        menuObject.OnLevelComplete();
    }

    public void IncreaseScore(float scoreCoefficient)
    {
        userScore = userScore + scoreCoefficient;
        scoreText.text = "Score: " + userScore;
        CheckForChild();
    }

    private void InitialValues()
    {
        ballsParent = gameObject.transform;
        userScore = 0.0f;
        scoreText.text = "Score: " + userScore;
    }

    private void CheckForChild()
    {
        deactiveChildCount = 0;
        foreach (Transform childBall in ballsParent)
        {
            if (!childBall.gameObject.activeInHierarchy)
            {
                deactiveChildCount = deactiveChildCount + 1;
            }
            else
            {
                break;
            }
            if (deactiveChildCount == ballsParent.childCount)
            {
                LevelComplete();
            }
        }
    }
}
