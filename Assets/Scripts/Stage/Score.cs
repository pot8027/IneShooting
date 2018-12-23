using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float _score = 0;

    public void AddScore(float addScore)
    {
        _score += addScore;
        this.GetComponent<Text>().text = _score.ToString();
    }
}
