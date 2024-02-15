using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner_JGD : MonoBehaviour
{
    [SerializeField] GameObject Position_1;
    [SerializeField] GameObject Position_2;
    [SerializeField] GameObject Obstacle;
    [SerializeField] float MaxTimmer;
    [SerializeField] float MinTimmer;
    float RanTime;
    float timmer;

    //풀링이랑 이런건 내일
    private void Start()
    {
        RanTime = Random.Range(0, MaxTimmer);
    }

    private void Update()
    {
        timmer += Time.deltaTime;
        if (timmer >= RanTime)
        {
            RanTime = Random.Range(MinTimmer, MaxTimmer);
            int Ran = Random.Range(0,2);
            switch (Ran)
            {
                case 0:
                    Instantiate(Obstacle, Position_1.transform);
                    timmer = 0;
                    break;
                case 1:
                    Instantiate(Obstacle, Position_2.transform);
                    timmer = 0;
                    break;
                default:
                    break;
            }
        }

    }

}
