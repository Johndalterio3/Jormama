using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChase : MonoBehaviour
{
    public Transform player;
    [SerializeField]private float smoothSped = 50f;
    [SerializeField] private Vector3 velocity = Vector3.zero;

    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desPos = Vector3.SmoothDamp(transform.position, player.position, ref velocity, smoothSped * Time.deltaTime);
        transform.position = desPos;

    }
}
