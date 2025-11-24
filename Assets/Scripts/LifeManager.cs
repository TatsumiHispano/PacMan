using UnityEngine;
using UnityEngine.UI;
public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;

    public Image vida1;
    public Image vida2;
    public Image vida3;

    private int vidas = 3;

    private void Awake()
    {
        Instance = this;
    }

    public void QuitarVida()
    {
        vidas--;

        if (vidas == 2)
            vida3.enabled = false;

        if (vidas == 1)
            vida2.enabled = false;

        if (vidas == 0)
        {
            vida1.enabled = false;
            Debug.Log("Game Over");
           
        }
    }
}
