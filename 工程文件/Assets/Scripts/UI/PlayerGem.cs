using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGem : MonoBehaviour
{
    private int Score = 0;
    [SerializeField] private Text scoreText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gem"))
        {
            Destroy(collision.gameObject);
            Score++;
            scoreText.text = " " + Score;
        }
    }
}
