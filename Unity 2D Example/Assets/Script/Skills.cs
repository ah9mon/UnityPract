using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{

    public GameObject Meteor;
    public GameObject MeteorHole;
    public GameObject FireGround1;

    
    // Start is called before the first frame update
    public void FallMeteor(float x, float y)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-11,12), Random.Range(8,13), 0); // Assuming z-position is 0
        GameObject meteor = Instantiate(Meteor, spawnPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
