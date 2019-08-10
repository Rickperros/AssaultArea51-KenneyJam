using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public int HP = 100;

    private void Update()
    {
        if (HP < 0)
            gameObject.SetActive(false);
    }
}
