using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{

    public TextMeshProUGUI pointsGameText;
    public TextMeshProUGUI pointsText;
    
    private void Awake()
    {
        pointsText.text = pointsGameText.text;
    }

}
