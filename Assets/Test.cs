using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private PlayerController playerController = null;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>(); // get the player controller script
    }
}
