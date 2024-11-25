using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class o : MonoBehaviour
{
    Vector3 StartPosition;
   [SerializeField] Vector3 movementPaosition;
    float movementFactor;
    [SerializeField] float Period=2f;
    // Start is called before the first frame update
    void Start()
    {
        StartPosition=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Period<=Mathf.Epsilon){return;}

        float cycle= Time.time/Period;
        const float tau=2*Mathf.PI;
        float sinwave=Mathf.Sin(cycle*tau);

        movementFactor= (sinwave+1f)/2f;


        Vector3 offset=movementPaosition*movementFactor;
        transform.position=StartPosition+offset;
    }
}
