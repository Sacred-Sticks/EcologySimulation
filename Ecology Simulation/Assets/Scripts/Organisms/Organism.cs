using UnityEngine;
using Random = UnityEngine.Random;

public class Organism : MonoBehaviour
{
    [SerializeField] protected EnumeratedObject species;

    protected Statistic Sustenance;
    protected Statistic Hydration;

    public static Vector3 GetRandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        var direction = new Vector3(x, y, 0).normalized;
        return direction;
    }
}
