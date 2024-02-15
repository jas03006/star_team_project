using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll_JGD : MonoBehaviour
{
    [SerializeField] GameObject Player;
    Player_Controll_JGD Playercon;
    Transform thisTrans;
    
    private void Start()
    {
        
        thisTrans = GetComponent<Transform>();
    }

    private void Update()
    {
        if (this.transform.position.x == Player.transform.position.x)
        {
            StartCoroutine(CameraController());
        }
    }

    private IEnumerator CameraController()
    {
        this.transform.position = Vector2.right * Playercon.Speed * Time.deltaTime;

        yield return null;
    }
}
