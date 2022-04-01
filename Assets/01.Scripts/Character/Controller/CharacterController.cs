using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    [SerializeField] PhysicsSettings settings;
    Rigidbody2D body2D;
    new Collider2D collider;

    void Awake()
    {
        body2D = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    public virtual void AddForce(Vector2 force, ForceMode2D mode = ForceMode2D.Force)
    {
        body2D.AddForce(force, mode);
    }

    public virtual void AddPosition(Vector3 positionAddition)
    {
        transform.position += positionAddition;
    }

    public virtual void MultiplyVelocity(float multiplier)
    {
        MultiplyVelocity(Vector2.one * multiplier);
    }

    public virtual void MultiplyVelocity(Vector2 multiplier)
    {
        SetVelocity(body2D.velocity * multiplier);
    }

    public virtual void SetVelocity(Vector2 newVelocity)
    {
        body2D.velocity = newVelocity;
    }

    #region Physics Check

    public RaycastHit2D RayCast(Collider2D origin, Vector2 direction, float distance, LayerMask mask)
    {
        float extentsAxis = Mathf.Abs(direction.x) > Mathf.Abs(direction.y) ? origin.bounds.extents.x : origin.bounds.extents.y;
        RaycastHit2D hit = Physics2D.Raycast(origin.bounds.center, direction, extentsAxis + distance, mask);
        return hit;
    }

    #endregion

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }


    public Rigidbody2D Body2D { get => body2D; }
    public PhysicsSettings Settings { get => settings; }
    public Collider2D Collider { get => collider; set => collider = value; }
    public Vector3 Velocity { get => body2D.velocity; }
}
