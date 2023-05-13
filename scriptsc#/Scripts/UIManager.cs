using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    static UIManager current;

    public TextMeshProUGUI monedasText; 

    public TextMeshProUGUI timeUmbrellaText; 
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
		//DontDestroyOnLoad(gameObject);
	}

    public static void ActualizarMonedasUI(int monedasCount)
	{
		//Si no existe el UIManager, salimos
		if (current == null)
			return;

		//Actualizamos el texto de las monedas
		current.monedasText.text = monedasCount.ToString();
	}

    public static void ActualizarTimeUmbrellaUI(int secs)
	{
		//Si no existe el UIManager, salimos
		if (current == null)
			return;

		//Actualizamos el texto de las monedas
		current.timeUmbrellaText.text = secs.ToString();
	}
}
