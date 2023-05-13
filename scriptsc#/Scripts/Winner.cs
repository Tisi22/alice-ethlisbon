using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Winner : MonoBehaviour
{

    [SerializeField] private GameObject dialogueImage;
    [SerializeField] private GameObject fade;
    [SerializeField] private GameObject ConnectImage;
    [SerializeField] private GameObject PlayerInvertido;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject newPlayer;

    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private Dialog dialog;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
           dialogueImage.SetActive(true);
           StartCoroutine(final());

        }
    }

    IEnumerator final()
    {
        yield return StartCoroutine(dialog.SetDialog("Congratulations! You got all the acceptance that you wanted. Everybody is proud of you!  \n Now, we will be together for ever..."));
        yield return new WaitForSeconds(3f);
        PlayerInvertido.SetActive(false);
        Player.SetActive(false);
        newPlayer.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        fade.SetActive(true);
        yield return new WaitForSeconds(2f);
        ConnectImage.SetActive(true);
        fade.SetActive(false);
    }
}
