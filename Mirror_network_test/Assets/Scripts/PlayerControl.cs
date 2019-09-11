using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerControl : NetworkBehaviour
{
    public float movementSpeed = 10f;

    // Update Controls
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.position += new Vector3(horizontal, 0f, vertical) * movementSpeed * Time.deltaTime;
    }
}
