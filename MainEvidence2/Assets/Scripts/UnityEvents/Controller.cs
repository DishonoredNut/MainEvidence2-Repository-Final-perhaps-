using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public int coins {get ; private set;}

    public void IncreaseCoins ()
    {
        coins++; 
    }
}
