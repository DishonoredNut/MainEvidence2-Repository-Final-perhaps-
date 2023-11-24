using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    Controller controller;

    public void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Controller")?.GetComponent<Controller>();
    }

    public void UpdateScore()
    {
        if (controller != null)
        {
            scoreText.text = controller.score.ToString();
        }
    }
}
