using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
 



public class SceneLoaderScript : MonoBehaviour
{
    public void loadlevel(string level)
    {
        if(level.Equals("GameMenu")){
            SceneManager.LoadScene(2);
        }        
        if(level.Equals("AuthorizedMenu")){
            SceneManager.LoadScene(1);
        }
        if(level.Equals("StartMenu")){
            SceneManager.LoadScene(0);
        }
        if(level.Equals("Game")){
            SceneManager.LoadScene(3);
        }
        if(level.Equals("SuccessfulLogin")){
            SceneManager.LoadScene(0);
        }
    }

    public static void loadlevel(int i){
        SceneManager.LoadScene(i);
    }
}
