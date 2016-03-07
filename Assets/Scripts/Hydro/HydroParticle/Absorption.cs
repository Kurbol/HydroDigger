using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Absorption : MonoBehaviour
{
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
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, degrees.ToVector());
            hitCountPerAngle[degrees] = hits.Length;

            if (degrees >= 360)
            {
                degrees = 0;

                if (hitCountPerAngle.Any())
                {
                    IEnumerable<KeyValuePair<float, int>> orderedHitCountPerAngle = hitCountPerAngle.OrderBy(a => a.Value);
                    IEnumerable<float> leastHitAngles = orderedHitCountPerAngle.Where(a => a.Value <= orderedHitCountPerAngle.First().Value).Select(a => a.Key);

                    // pick random leastHitAngle if leastHitAngles count is less than hitCountPerAngle count
                }

                yield return new WaitForFixedUpdate();
            }

            degrees += 30;
        }
    }
}