using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3f;
    public float rotateSpeed = 0.0025f;
    private Rigidbody2D rb;

    // Calculations like this will be taken care of before we ever call the Move script to do stuff
    //public float distanceToStop = 3f;

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
    
    
    void Anchor()
    {
    Vector3 statBarPosition = canvasTransform.localPosition;
    statBarPosition.z = 0; // Make sure the stat bar stays in the same plane as the canvas
    statBar.transform.localPosition = statBarPosition;
    }

    // We will be sending the target into this script externally from other script when we call these public methods
    // private void GetTarget()
    // {
    //     if (GameObject.FindGameObjectWithTag("prey"))
    //     {
    //         target = GameObject.FindGameObjectWithTag("prey").transform;
    //     }
    // }

    // We don't want to control eating behavior from the moving script
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("prey"))
    //     {
    //         Destroy(other.gameObject);
    //         target = null;
    //     }
    // }
}
