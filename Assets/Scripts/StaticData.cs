using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData
{
    static StaticData _instance;
    public string username;
    public int gameplayMode;
    public int AI;

    private StaticData(){
        username = "UNINITIALIZED";
        gameplayMode = 0;
        AI = 1;
    }
    public static StaticData GetInstance(){
        if(_instance == null){
            _instance = new StaticData();
        }
        return _instance;
    }



}
