using UnityEngine;

public class AiScript : MonoBehaviour
{
    public float MaxMovementSpeed;
    private Rigidbody2D rb;
    private Vector2 startingPosition;

    public Rigidbody2D Disk;

    public Transform PlayerBoundaryHolder;
    private Boundary playerBoundary;

    public Transform DiskBoundaryHolder;
    private Boundary diskBoundary;

    private Vector2 targetPosition;

    private bool isFirstTimeInOppnentsHalf = true;
    private float offsetXFromTarget;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = rb.position;

        playerBoundary = new Boundary(PlayerBoundaryHolder.GetChild(0).position.y,
                                      PlayerBoundaryHolder.GetChild(1).position.y,
                                      PlayerBoundaryHolder.GetChild(2).position.x,
                                      PlayerBoundaryHolder.GetChild(3).position.x);

        diskBoundary = new Boundary(DiskBoundaryHolder.GetChild(0).position.y,
                                      DiskBoundaryHolder.GetChild(1).position.y,
                                      DiskBoundaryHolder.GetChild(2).position.x,
                                      DiskBoundaryHolder.GetChild(3).position.x);
    }

    private void FixedUpdate()
    {
        if (!DiskScript.WasGoal)
        {
           float movementSpeed;

            if (Disk.position.x < diskBoundary.Left)
            {
                if (isFirstTimeInOppnentsHalf)
                {
                    isFirstTimeInOppnentsHalf = false;
                    offsetXFromTarget = Random.Range(-1f, 1f);
                }

                movementSpeed = MaxMovementSpeed * Random.Range(0.1f, 0.3f);
                targetPosition = new Vector2(Mathf.Clamp(Disk.position.x, playerBoundary.Left,
                                            playerBoundary.Right), startingPosition.x);
            }
            else
            {
                isFirstTimeInOppnentsHalf = true;

                movementSpeed = Random.Range(MaxMovementSpeed * 0.4f, MaxMovementSpeed);
                targetPosition = new Vector2(Mathf.Clamp(Disk.position.x, playerBoundary.Left,
                                            playerBoundary.Right),
                                            Mathf.Clamp(Disk.position.y, playerBoundary.Down,
                                            playerBoundary.Up));
            }
            rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition,
                movementSpeed * Time.fixedDeltaTime));
        }
    }
}