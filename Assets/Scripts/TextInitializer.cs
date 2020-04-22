using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInitializer : MonoBehaviour
{
    public Text field;
    void Start()
    {
        field.text = StaticData.GetInstance().username;
    }

}
