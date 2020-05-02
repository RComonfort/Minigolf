using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void ResumeGame(){
       GameManager.instance.PauseGame(false);
   }
   public void RestartGame(){
      GameManager.instance.Restart();
   }
  public void QuitGame(){
        
       SceneManager.LoadScene("Menu");
   }
}
