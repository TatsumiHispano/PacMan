using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
     public TextMeshProUGUI scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame


    public void AddPoint()
    {
        score++;
        UpdateUI();
    }
    void UpdateUI()
    {
       scoreText.text = "Score: " + score;
        
    }
}
