using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] Stages;

    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject RestartButton;

    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }
    public void NextStage()
    {
        totalPoint += stagePoint;
        stagePoint = 0;

        // Change Stage
        Debug.Log(Stages.Length);
        Debug.Log(stageIndex);
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE " + (stageIndex + 1);
        } else
        {
            Time.timeScale = 0;
            Debug.Log("게임 클리어");
            Text btnText = RestartButton.GetComponentInChildren<Text>();
            btnText.text = "Game Clear!";
            RestartButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            HealthDown();

            // Plater Reposition 
            if (health > 1)
            {
                collision.attachedRigidbody.velocity = Vector2.zero;
                collision.transform.position = new Vector3(0, 0, 1);
            }   
        }
    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1,1,1, 0.2f);

        } else
        {
            UIhealth[health].color = new Color(1, 1, 1, 0.2f);
            // Player Die
            player.OnDie();
            // Result UI
            Debug.Log("죽었습니다.");
            // Retry Button UI
            RestartButton.SetActive(true);
        }
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, -1);
        player.VZero();
    }

    public void Restart()
    {
        Debug.Log("dasdasd");
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
