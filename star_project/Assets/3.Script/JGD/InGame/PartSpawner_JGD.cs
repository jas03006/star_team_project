using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PartSpawner_JGD : MonoBehaviour
{
    [SerializeField] GameObject Part1;
    [SerializeField] GameObject Part2;
    [SerializeField] GameObject Part3;

    [SerializeField] float MaxTimmer;
    float timmer;

    //풀링이랑 이런건 내일


    private void Update()
    {
        timmer += Time.deltaTime;
        if (timmer >= MaxTimmer)
        {
            int Ran = Random.Range(0, 3);
            switch (Ran)
            {
                case 0:
                    Instantiate(Part1,this.transform);
                    timmer = 0;
                    break;
                case 1:
                    Instantiate(Part2, this.transform);
                    timmer = 0;
                    break;
                case 2:
                    Instantiate(Part3, this.transform);
                    timmer = 0;
                    break;
                default:
                    break;
            }
        }

    }
}
