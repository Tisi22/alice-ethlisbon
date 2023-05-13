using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryCanvas : MonoBehaviour
{
    static StoryCanvas current;

    [SerializeField] GameObject zeroImagePanel;
    [SerializeField] GameObject firstImagePanel;
    [SerializeField] GameObject secondImagePanel;
    [SerializeField] GameObject thirdImagePanel;
    [SerializeField] GameObject fourImagePanel;
    [SerializeField] GameObject FadeCanvas;
    [SerializeField] GameObject ConnectPanel;

    [SerializeField] Dialog dialog;


    int activePanel;

    void Awake()
	{
		//Si existe un UIManager y no es este...
		if (current != null && current != this)
		{
			//...destruimos y salimos
			Destroy(gameObject);
			return;
		}

		//Si no tenía valor el UIManager se lo ponemos
		//Objeto persistente (no le afecta el cambio de escenas)
		current = this;
		DontDestroyOnLoad(gameObject);
	}

    void Start()
    {
        activePanel = 0;
    }

    void Update()
    {
        if( Input.GetButtonDown("Submit") && dialog.isWriting == false)
        {
            if(activePanel == 0)
            {
                zeroImagePanel.SetActive(false);
                firstImagePanel.SetActive(true);
                StartCoroutine(dialog.SetDialog("She was a joyful girl who loved spending time in nature, and her favourite animal was the hedgehog. Like the tiny, sturdy creatures, she was small and strong too."));
                activePanel++;
            }
            else if(activePanel == 1)
            {
                firstImagePanel.SetActive(false);
                secondImagePanel.SetActive(true);
                StartCoroutine(dialog.SetDialog("From a young age, her parents and the society around her had instilled the belief that she must be flawless in everything she did. Excellence in school, impeccable appearance, and good behaviour were expected of her at all times."));
                activePanel++;
            }
            else if(activePanel == 2)
            {
                secondImagePanel.SetActive(false);
                thirdImagePanel.SetActive(true);
                StartCoroutine(dialog.SetDialog("One day, she started experiencing strange symptoms. She would forget things she had done, find herself in places she didn't remember going to, and even hear voices in her head."));
                activePanel++;

            }
            else if(activePanel == 3)
            {
                thirdImagePanel.SetActive(false);
                fourImagePanel.SetActive(true);
                StartCoroutine(dialog.SetDialog("She knew that something was wrong, but the voice in her head told her that they could be happy together and that she could become the perfect girl that everybody expected of her."));
                activePanel++;
            }
            else if(activePanel == 4)
            {
                StartCoroutine(ConnectCanvas());
                StartCoroutine(dialog.SetDialog("There was only one question left: How far was she willing to lose herself to become the person that society wanted her to be?"));
                activePanel++;
            }
            else if(activePanel == 5)
            {
                return;
    
            }
    

        }

        IEnumerator ConnectCanvas()
        {
            FadeCanvas.SetActive(true);

            yield return new WaitForSeconds(1.9f);

            ConnectPanel.SetActive(true);
            FadeCanvas.SetActive(false);

        }
    }
}
