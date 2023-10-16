using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour //Bmo ()
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
