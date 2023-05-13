using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI dialogText;

    public float charactersPerSecond = 50f;
    public float timeToWaitAfterText = 1f;

    public bool isWriting = false;

    [SerializeField] SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }

    public IEnumerator SetDialog(string message)
    {
        isWriting = true;
        
        dialogText.text = "";
        foreach (var character in message)
        {
            if(character!=' ')
            {
                soundManager.PlayRandomCharacterSound();
            }
            dialogText.text +=character;
            yield return new WaitForSeconds(1/charactersPerSecond);
        }

        yield return new WaitForSeconds(timeToWaitAfterText);
        
        isWriting = false;
    }
}
