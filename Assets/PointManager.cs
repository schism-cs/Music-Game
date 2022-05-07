using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public TextMeshProUGUI pointsText;

    private int _points;

    private void Start()
    {
        _points = 0;
        pointsText.text = _points.ToString();
    }

    public void UpdatePoints(int value)
    {
        _points += value;
        pointsText.text = _points.ToString();
    }
}
