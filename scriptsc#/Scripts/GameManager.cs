using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager current;

    int monedas;

    [SerializeField] GameObject LoosePannel;
    [SerializeField] MintingPanel mintingPanel;
    [SerializeField] Player player;

    [SerializeField] SoundManager soundManager;

    public AudioClip introClip, gameClip;


    void Awake()
    {
        if(current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }

        current = this;

        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();



    }

    void Start()
    {
        InicioPartida();
        SetMonedas();
        SetBombs();

        int network = Web3GL.Network();

        if(network != 10200)
        {
            soundManager.PlayMusic(introClip);
        }
        else
        {
            soundManager.PlayMusic(gameClip);
        }

    }

    public void changeMusic()
    {
        soundManager.PlayMusic(gameClip);
    }

    public static void ActualizarMonedas()
	{
		if (current == null)
		return;

	
		current.monedas += 1;
		
		UIManager.ActualizarMonedasUI(current.monedas);
	}

    void InicioPartida()
    {
        current.monedas = 0;
        UIManager.ActualizarMonedasUI(current.monedas);
    }

    public void Loose()
    {
        LoosePannel.SetActive(true);
        InicioPartida();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        
    }

    //Desde aqui la monedas

    [SerializeField] private Monedas monedasPrefab;
    [SerializeField] private int maxNumMonedas;

    [SerializeField] private Bomb bombPrefab;
    [SerializeField] private int maxNumBomb;

    [SerializeField] private Vector2 defaultInitialPlanePositionX = new Vector2(-10,10);
    [SerializeField] private Vector2 defaultInitialPlanePositionY = new Vector2(-3,-50);

    private void SetMonedas()
    {
        for(int i = 0; i < maxNumMonedas; i++)
        {
            Monedas moneda = Instantiate(monedasPrefab);
            moneda.gameObject.transform.position = new Vector3(Random.Range(defaultInitialPlanePositionX.x, defaultInitialPlanePositionX.y), Random.Range(defaultInitialPlanePositionY.x, defaultInitialPlanePositionY.y),0);
        }
    }

    private void SetBombs()
    {
        for(int i = 0; i < maxNumBomb; i++)
        {
            Bomb bomb = Instantiate(bombPrefab);
            bomb.gameObject.transform.position = new Vector3(Random.Range(defaultInitialPlanePositionX.x, defaultInitialPlanePositionX.y), Random.Range(defaultInitialPlanePositionY.x, defaultInitialPlanePositionY.y),0);
        }
    }

    public void MintALCTokens()
    {
        mintingPanel.MintTokens(current.monedas.ToString());
    }
}
