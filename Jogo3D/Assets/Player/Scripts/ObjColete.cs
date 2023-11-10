using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjColete : MonoBehaviour
{
    public int healthValue;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().increaseLife(healthValue);
            Destroy(gameObject);
        }
    }
}
