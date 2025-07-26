using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Wander : MonoBehaviour
{
    [Header("Wander Area")]
    public float wanderWidth = 5;
    public float wanderHeight = 5;
    public Vector2 startingPosition;

    public float pauseDuration = 1;
    public float speed = 2;
    public Vector2 target;

    [Header("Obstacle Avoidance")]
    public LayerMask obstacleLayer;
    public float checkRadius = 0.2f;
    public float castDistance = 0.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isPaused;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        startingPosition = transform.position; // Automatically set at runtime
        StartCoroutine(PauseAndPickNewDestination());
    }

    private void Update()
    {
        if (isPaused)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            StartCoroutine(PauseAndPickNewDestination());
        }

        Move();
    }

    private void Move()
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;

        // Obstacle check: if blocked, pause and re-pick destination
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, checkRadius, direction, castDistance, obstacleLayer);
        if (hit.collider != null)
        {
            Debug.DrawLine(transform.position, transform.position + (Vector3)(direction * castDistance), Color.red);
            StartCoroutine(PauseAndPickNewDestination());
            return;
        }

        if (direction.x < 0 && transform.localScale.x > 0 || direction.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

        rb.velocity = direction * speed;
    }

    IEnumerator PauseAndPickNewDestination()
    {
        isPaused = true;
        anim.Play("Idle");
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(pauseDuration);

        target = GetRandomTarget();
        anim.Play("Walk");
        isPaused = false;
    }

    private Vector2 GetRandomTarget()
    {
        float halfWidth = wanderWidth / 2;
        float halfHeight = wanderHeight / 2;

        for (int i = 0; i < 10; i++)
        {
            int edge = Random.Range(0, 4);
            Vector2 candidate = edge switch
            {
                0 => new Vector2(startingPosition.x - halfWidth, Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)),
                1 => new Vector2(startingPosition.x + halfWidth, Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)),
                2 => new Vector2(Random.Range(startingPosition.x - halfWidth, startingPosition.x + halfWidth), startingPosition.y - halfHeight),
                _ => new Vector2(Random.Range(startingPosition.x - halfWidth, startingPosition.x + halfWidth), startingPosition.y + halfHeight),
            };

            if (!Physics2D.OverlapCircle(candidate, checkRadius, obstacleLayer))
            {
                return candidate;
            }
        }

        Debug.LogWarning("NPC couldn't find a valid target after 10 tries.");
        return startingPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(PauseAndPickNewDestination());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startingPosition, new Vector3(wanderWidth, wanderHeight, 0));
    }
}
