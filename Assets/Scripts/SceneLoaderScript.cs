using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
 



public class SceneLoaderScript : MonoBehaviour
{
    public void loadlevel(string level)
    {
        if(level.Equals("Register")){
            SceneManager.LoadScene(1);
        }
        if(level.Equals("Login")){
            SceneManager.LoadScene(2);
        }
    }
}
