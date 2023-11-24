using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour //Bmo (2020) 5 Minute Main Menu Tutorial
{
   public void startGame()
   {
     SceneManager.LoadScene("OutdoorsScene"); 
   }

   public void Quit ( )
   {
     Application.Quit () ; 
   }
}
