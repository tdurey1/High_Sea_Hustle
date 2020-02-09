using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public string playerName = "";
    public bool isFirstPlayer = false;
    public enum Avatar
    {
        malePirate,
        femalePirate,
        maleNavy,
        femaleNavy,
    }
}
