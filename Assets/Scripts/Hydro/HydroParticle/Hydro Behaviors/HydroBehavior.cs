using System.Collections;
using UnityEngine;

public abstract class HydroBehavior : MonoBehaviour
{
    [SerializeField]
    private Physics physics;
    protected Physics Physics
    {
        get
        {
            return physics;
        }

        set
        {
            physics = value;
            Rigidbody.mass = physics.Mass;
            Rigidbody.drag = physics.LinearDrag;
            Rigidbody.angularDrag = physics.AngularDrag;
            Rigidbody.gravityScale = physics.GravityScale;
        }
    }

    [SerializeField]
    private Rigidbody2D rigidBody;
    public Rigidbody2D Rigidbody
    {
        get
        {
            if (rigidBody == null)
            {
                rigidBody = gameObject.GetSafeComponent<Rigidbody2D>();
            }

            return rigidBody;
        }
    }

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = gameObject.GetSafeComponent<SpriteRenderer>();
            }

            return spriteRenderer;
        }
    }

    public abstract void InitializeState();

    public void StartBehavior()
    {
        StartCoroutine("RunPhysicsBehavior");
        StartCoroutine("RunGraphicsBehavior");
    }

    public void StopBehavior()
    {
        StopCoroutine("RunPhysicsBehavior");
        StopCoroutine("RunGraphicsBehavior");
    }

    private void CheckMaximumVelocity()
    {
        if (Rigidbody.velocity.magnitude > Physics.MaximumVelocity)
            Rigidbody.velocity = Rigidbody.velocity.normalized * Physics.MaximumVelocity;
    }

    private IEnumerator RunPhysicsBehavior()
    {
        while (true)
        {
            CheckMaximumVelocity();
            UpdatePhysicsBehavior();

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator RunGraphicsBehavior()
    {
        while (true)
        {
            UpdateGraphicsBehavior();

            yield return new WaitForEndOfFrame();
        }
    }

    protected abstract void UpdatePhysicsBehavior();

    protected abstract void UpdateGraphicsBehavior();
}