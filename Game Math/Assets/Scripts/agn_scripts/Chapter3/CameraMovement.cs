using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    void Update()
    {
        transform.position = (new Vector3(0, player.transform.position.y + 3, player.transform.position.z - 10)); 
    }
}
