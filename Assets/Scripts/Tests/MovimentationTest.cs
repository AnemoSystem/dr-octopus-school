using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimentationTest : MonoBehaviour
{
    private Vector2 targetPos;
    public float speed = 10;

    void Update() {
        Move(transform.position, targetPos);
    }

    void OnCollisionEnter2D()
    {
        speed = 0.1f;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }

    void OnCollisionExit2D()
    {
        speed = 10;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }

    public void Move(Vector2 origin, Vector2 target) {
        transform.position = Vector2.MoveTowards(origin, target, Time.deltaTime * speed);
    }

    public Vector3 getTargetPos() {
        return targetPos;
    }

    public void setTargetPos(float v1, float v2) {
        targetPos = new Vector2(v1, v2);
    }
}
