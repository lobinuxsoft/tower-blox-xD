using UnityEngine;

public class PendulumMovement : MonoBehaviour
{
    [SerializeField, Range(0, 360)] private float maxAngle = 45;
    [SerializeField] private float speedPendulum = 2;
    [SerializeField] private Rigidbody pivot;

    private void FixedUpdate()
    {
        float angle = maxAngle * Mathf.Sin(Time.time * speedPendulum);
        pivot.MoveRotation(Quaternion.Euler(0, 0, angle));
    }
}
