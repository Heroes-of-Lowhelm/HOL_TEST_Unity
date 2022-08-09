using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class HelloWorldServerEdition : NetworkBehaviour
{

    private void Update()
    {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("I SAID HELLO WORLD");
            HelloAgainWorld();
        }
    }

    [Command]
    void HelloAgainWorld()
    {
        Debug.Log("CLIENT SAID HELLO WORLD AGAIN!");
    }

}
