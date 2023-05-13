using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Variables Umbrella Open")]
    [SerializeField] private bool umbrellaOpen = false; 

    [Header("Variables Movimiento")]
    [SerializeField] private float velocidadX;
    [SerializeField] private float velocidadXUmbrellaOpen;
    [SerializeField] private float velocidadY;
    [SerializeField] private float velocidadYUmbrellaOpen;

    [Header ("Referencia a Componentes")]
    Rigidbody2D rb;                                         
    Animator anim;

    private Vector2 input;
    bool loose; 
    bool penitenceTime;

    public bool enSuelo;
    private WebLogin webLogin;

    public LayerMask WallsLayer;

    void Awake()
    {
        webLogin = GameObject.FindWithTag("WebLogin").GetComponent<WebLogin>();
    }
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                   
        anim = GetComponent<Animator>(); 
        loose = false;
    }

    
    void FixedUpdate()
    {
        int network = Web3GL.Network();

        if(loose == false && network == 10200)
        {
            checkUmbrella();
            movimientoHorizontal();
        }
        
    }

   
    void movimientoHorizontal()
    {
        float velocidadx;
        float velocidady;
        input.x = Input.GetAxisRaw("Horizontal");
        var targetPosition = transform.position;
        targetPosition.x += input.x;

        
        if(umbrellaOpen)
        {

            if(IsAvailable(targetPosition))
            {
                velocidadx = input.x * velocidadXUmbrellaOpen;  
            }

            else
            {
                velocidadx = 0f;
            }
               
            if(!enSuelo)
            {
                velocidady = velocidadYUmbrellaOpen;
            }
            else
            {
                velocidady = 0f;
            }
            
        } 
        else
        {
            if(IsAvailable(targetPosition))
            {
                velocidadx = input.x * velocidadX;  
            }
            else
            {
                velocidadx = 0f;
            }

            if(!enSuelo)
            {
                velocidady = velocidadY;
            }
            else
            {
                velocidady = 0f;
            }

            
        }
        
        rb.velocity = new Vector2 (velocidadx, velocidady);              

        
    }

    void checkUmbrella()
    {
        if(Input.GetAxis("Fire1") != 0 && penitenceTime == false)
        {
            StartCoroutine(UmbrellaOpen());
            
        }

    }

    IEnumerator UmbrellaOpen()
    {

        umbrellaOpen = true;
        penitenceTime = true;
        anim.SetFloat("Umbrella", 1f);

    
        UIManager.ActualizarTimeUmbrellaUI(5);
        yield return new WaitForSeconds(1f);
        UIManager.ActualizarTimeUmbrellaUI(4);
        yield return new WaitForSeconds(1f);
        UIManager.ActualizarTimeUmbrellaUI(3);
        yield return new WaitForSeconds(1f);
        UIManager.ActualizarTimeUmbrellaUI(2);
        yield return new WaitForSeconds(1f);
        UIManager.ActualizarTimeUmbrellaUI(1);
        yield return new WaitForSeconds(1f);
        UIManager.ActualizarTimeUmbrellaUI(0);

        umbrellaOpen = false;
        anim.SetFloat("Umbrella", 0f);

        yield return new WaitForSeconds(2f);

        UIManager.ActualizarTimeUmbrellaUI(5);
        penitenceTime = false;

    }

    public IEnumerator LooseAnimation()
    {
        loose = true;

        rb.velocity = new Vector2 (0f, 0f);

        anim.SetFloat("Umbrella", 2f);

        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);

    }

    private bool IsAvailable(Vector3 target)
    {
        if(Physics2D.OverlapCircle(target, 0.05f, WallsLayer) != null)
        {
            return false;
        }
        return true;
    }

}
