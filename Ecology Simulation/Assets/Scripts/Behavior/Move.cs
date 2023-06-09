using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotateSpeed;

    private Rigidbody2D rb;
    private bool currentlyRotating;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveToTarget()
    {
        rb.velocity = transform.up * speed;
    }

    public void RotateTowardsTarget(Vector3 target)
    {
        if (currentlyRotating)
            return;
        StartCoroutine(Rotation(target));
    }

    public IEnumerator Rotation(Vector3 target)
    {
        float timer = 0;

        Vector2 targetDirection = (target - transform.position).normalized;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        var initialRotation = transform.localRotation;
        var targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        while (timer < rotateSpeed)
        {

            timer += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, rotateSpeed * timer);
            yield return new WaitForEndOfFrame();
        }
    }
}
