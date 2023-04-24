using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotateSpeed = 0.0025f;
    
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveToTarget()
    {
        rb.velocity = transform.up * speed;
    }

    public void RotateTowardsTarget(Vector3 target)
    {
        Vector2 targetDirection = (target - transform.position).normalized;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }
}
