using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.Networking;
using TMPro;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject dialogueImage;
    [SerializeField] private TextMeshProUGUI message;

    private List<Response> responses;

    [System.Serializable]
    public class Response
    {
        public string response;
    }

    private void Start()
    {
        StartCoroutine(FetchResponses("https://raw.githubusercontent.com/Tisi22/alice-ethlisbon/main/npc-ai.json"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            DisplayRandomResponse();
           dialogueImage.SetActive(true);
           StartCoroutine(SetFalseImage());
        }
    }

    public void DisplayRandomResponse()
    {
        if (responses == null || responses.Count == 0)
        {
            message.text = "I do not want to be alone...";
            return;
        }

        int randomIndex = Random.Range(0, responses.Count);

        message.text = responses[randomIndex].response;

    }

    private IEnumerator FetchResponses(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            responses = JsonConvert.DeserializeObject<List<Response>>(www.downloadHandler.text);
        }
    }

    IEnumerator SetFalseImage()
    {
        yield return new WaitForSeconds(3f);
        dialogueImage.SetActive(false);
    }
}
