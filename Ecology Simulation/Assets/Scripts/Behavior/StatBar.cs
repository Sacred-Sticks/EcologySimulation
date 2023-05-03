using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{

    [SerializeField] private Statistic statistic;


    
    private void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, statistic.Value, transform.localScale.z);

    } 


}
