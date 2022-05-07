using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentation_blockfall : MonoBehaviour
{
    private Vector2 targetPos;
    public float speed;
    public float maxBounceAngle = 75f;

    void Start()
    {
        targetPos = new Vector2(0, -8);
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            targetPos = new Vector2(mousePos.x, -8);
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = 14f;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            Vector3 paddlePosition = this.transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float width = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);
            float bounceAngle = (offset / width) * this.maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -this.maxBounceAngle, this.maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rigidbody.velocity = rotation * Vector2.up * ball.rigidbody.velocity.magnitude;
        }
    }

    void OnCollisionExit2D()
    {
        speed = 14;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }
}
