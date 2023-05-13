using UnityEngine.SceneManagement;
using UnityEngine;

public class LoosePanel : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeSelf && Input.GetButtonDown("Submit"))
        {
        
            SceneManager.LoadScene(0);
            
        }
    }
}
