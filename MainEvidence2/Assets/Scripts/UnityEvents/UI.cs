using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro ;
using UnityEngine.InputSystem.Android.LowLevel;

public class UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText; 
    Controller controller ; 


    public void UpdateCoins()
    {
        controller = GameObject.FindGameObjectWithTag("Controller").GetComponent<Controller>();

    }

    public void UpdateScore()
    {
        coinsText.text = controller.coins.ToString();
    }
}
