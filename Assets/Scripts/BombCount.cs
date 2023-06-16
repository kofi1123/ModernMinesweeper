using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombCount : MonoBehaviour
{
    public int bombsLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponentsInChildren<Text>()[0].text = this.bombsLeft.ToString();
    }
}
