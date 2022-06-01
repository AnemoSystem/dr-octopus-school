using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimentationTest : MonoBehaviour
{
    private Vector2 targetPos;
    public float speed = 10;

    void Start()
    { 
        //targetPos = new Vector2(0, -4);
    }

    void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            targetPos = new Vector3(mousePos.x, mousePos.y);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

        //transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPos);
    }

    void OnCollisionEnter2D()
    {
        speed = 0.1f;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }

    void OnCollisionExit2D()
    {
        speed = 10;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }
}
