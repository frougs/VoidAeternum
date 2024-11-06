using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    [SerializeField] [Range(10, 100)] float minSpeed;
    [SerializeField] [Range(10,100)] float maxSpeed;
    private float rotationSpeed;
    private void Start(){
        rotationSpeed = Random.Range(minSpeed, maxSpeed);
        if(Random.value > 0.5f){
            rotationSpeed = -rotationSpeed;
        }
    }
    private void Update(){
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

}

