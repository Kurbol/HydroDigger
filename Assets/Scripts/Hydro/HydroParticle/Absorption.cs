using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorption : MonoBehaviour
{
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
    private float absorptionRate = 0.5F;

    private void Start()
    {
        StartCoroutine(MoveAwayFromColliders());
    }

    private IEnumerator MoveAwayFromColliders()
    {
        float degrees = 0;
        Dictionary<float, int> hitCountPerAngle = new Dictionary<float, int>();

        while (true)
        {
            Vector2 direction = degrees.DegreeToVector();
            RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2)transform.position + direction, direction, 1);

            if (hits.Length <= 0)
            {
                degrees = 0;

                Debug.DrawLine(transform.position, (Vector2)transform.position + direction * absorptionRate);
                Rigidbody.AddForce(direction * absorptionRate);
            }

            yield return new WaitForFixedUpdate();

            degrees += 45;
        }
    }
}