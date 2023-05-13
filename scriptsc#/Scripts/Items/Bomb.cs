using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Player player;
    private GameManager gameManager;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
        
            StartCoroutine(player.LooseAnimation());
            gameManager.Loose();
		    gameObject.SetActive(false);

        }
    }
}
