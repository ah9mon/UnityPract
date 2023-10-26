using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject FireGround;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("���׿� ���� " +  collision.gameObject.tag);
        Debug.Log("���̾� " + " " + collision.gameObject.layer);
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.layer == 7)
        {
            EnemyMove enemyScript = collision.gameObject.GetComponent<EnemyMove>();
            if (enemyScript != null)
            {
                enemyScript.OnDamaged();
            }
        }
       
    }
}