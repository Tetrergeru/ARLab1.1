using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Victorine : MonoBehaviour
{
    public TMP_Text PointsText;
    public TMP_Text Question;
    public TMP_Text ScoreText;
    public List<Question> Questions;

    private string _points = "";
    private int _pointsCount = 0;
    private int _currentQuestion = 0;
    private bool _activatable = true;
    private static string GoodChar = "<color=\"green\">+</color>";
    private static string BadChar = "<color=\"red\">-</color>";

    void Start()
    {
        ScoreText.text = "";
        UpdateUI();
    }

    public void Answer(bool answer)
    {
        if (!_activatable) return;

        if (Questions[_currentQuestion].A == answer)
        {
            _points += GoodChar;
            _pointsCount += 1;
        }
        else
        {
            _points += BadChar;
        }

        _activatable = false;
        _currentQuestion += 1;

        UpdateUI();

        if (_currentQuestion >= Questions.Count)
        {
            Debug.Log($"{_currentQuestion}: {_pointsCount}/{Questions.Count}");
            Question.text = "";
            ScoreText.text = $"Score:\n{_pointsCount}/{Questions.Count}";
            return;
        }

        StartCoroutine(WaitAfterAnswer());
    }

    private IEnumerator WaitAfterAnswer()
    {
        yield return new WaitForSeconds(0.5f);
        _activatable = true;
    }

    private void UpdateUI()
    {
        PointsText.text = _points;

        if (_currentQuestion < Questions.Count)
        {
            Question.text = Questions[_currentQuestion].Q;
        }
    }
}

[Serializable]
public struct Question
{
    public string Q;
    public bool A;
}