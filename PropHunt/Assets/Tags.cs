using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour
{
    //public bool Mangnetizeable = false;
    // enum Color {blue,red,green};
    bool currentCollectable = false;
    public bool blue = false;
    public bool red = false;
    public bool green = false;
    //public bool Blue = false;
    //public bool Blue = false;

    public void setCurrentCollectable()
    {
        currentCollectable = true;
        gameObject.tag = "Collectable";
    }
}