using System;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    [SerializeField] private float boundary = 5f;
    [SerializeField] private float movementSpeed = 1f;

    public bool movingOnX = true;
    private bool isMoving = false;

    private float timeOffset;

    private void Start()
    {
        timeOffset = Time.time;
    }

    private void Update()
    {
        if(!isMoving)
        {
            return; 
        }

        Move();
    }

    private void Move()
    {
        float t = (Time.time - timeOffset) * movementSpeed;
        float value = Mathf.PingPong(t, boundary * 2) - boundary;

        if (movingOnX)
        {
            transform.position = new Vector3(value, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x,transform.position.y, value);
        }
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public bool GetMovingOnX()
    {
        return movingOnX;
    }

    public void SetMovingOnX(bool onX = true)
    {
        movingOnX = onX; 
    }
}
