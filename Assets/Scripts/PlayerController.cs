using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int maxHealth = 5;
    public Text scoreText;
    public Text healthText;
    public Text winLoseText;
    public Image winLoseBG;
    private Rigidbody rb;
    private int score;
    private int health;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        health = maxHealth;
        SetScoreText();
        SetHealthText();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            score++;
            SetScoreText();
        }
        else if (other.gameObject.CompareTag("Trap"))
        {
            health--;
            health = Mathf.Clamp(health, 0, maxHealth); // Ensure health doesn't go below 0
            SetHealthText();
        }
        else if (other.gameObject.CompareTag("Goal"))
        {
            WinCondition();
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            LoseCondition();
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

    void WinCondition()
    {
        winLoseText.text = "You Win!";
        winLoseBG.color = Color.green;
        winLoseText.gameObject.SetActive(true);
        winLoseBG.gameObject.SetActive(true);
        StartCoroutine(LoadScene(3)); // Start the coroutine when player wins
    }

    void LoseCondition()
    {
        winLoseText.text = "Game Over!";
        winLoseBG.color = Color.red;
        winLoseText.gameObject.SetActive(true);
        winLoseBG.gameObject.SetActive(true);
        StartCoroutine(LoadScene(3)); // Start the coroutine when player loses
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
