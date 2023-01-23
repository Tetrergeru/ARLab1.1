using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Victorine : MonoBehaviour
{
    public TMP_Text PointsText;
    public TMP_Text Question;
    public List<Question> Questions;

    private string _points = "Results:\n";
    private int _pointsCount = 0;
    private int _currentQuestion = 0;
    private bool _activatable = true;
    private static string GoodChar = "<color=\"green\">+</color>";
    private static string BadChar = "<color=\"red\">-</color>";

    public bool IsOver { get; private set; }
    public int PointsCount => _pointsCount;
    public int QuestionsCount => Questions.Count;

    void Start()
    {
        UpdateUI();
        StartCoroutine(WaitAfterAnswer());
    }

    public void Answer(bool answer)
    {
        if (!_activatable || IsOver) return;

        if (Questions[_currentQuestion].A == answer)
        {
            _points += GoodChar;
            _pointsCount += 1;
        }
        else
        {
            _points += BadChar;
        }

        _currentQuestion += 1;

        UpdateUI();

        if (_currentQuestion >= Questions.Count)
        {
            Question.text = "";
            IsOver = true;
            return;
        }

        StartCoroutine(WaitAfterAnswer());
    }

    private IEnumerator WaitAfterAnswer()
    {
        _activatable = false;
        yield return new WaitForSeconds(1.5f);
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