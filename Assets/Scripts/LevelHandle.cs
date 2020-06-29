using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandle : MonoBehaviour
{
   public void LoadNextLevel()
   {
       
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex + 1 == SceneManager.sceneCountInBuildSettings)
        {
            currentSceneIndex = -1;
            
        }
        SceneManager.LoadScene(currentSceneIndex + 1);

    }


   public void ManinMenuScene()
   {
        SceneManager.LoadScene(0);
   }


    public void LoadFirstLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }


}
