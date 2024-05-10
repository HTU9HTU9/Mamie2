using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float charactersPerSecond = 10f; // Adjust speed here
    private string fullText;
    private float timer;
    private int charactersToShow;

    private void Start()
    {
        // Get the full text from TextMeshPro
        fullText = textMeshPro.text;
        textMeshPro.text = ""; // Clear text
        timer = 0f;
        charactersToShow = 0;
    }

    private void Update()
    {
        // Check if there are still characters to reveal
        if (charactersToShow < fullText.Length)
        {
            timer += Time.deltaTime;
            // Calculate the number of characters to reveal based on charactersPerSecond
            charactersToShow = Mathf.FloorToInt(timer * charactersPerSecond);
            charactersToShow = Mathf.Clamp(charactersToShow, 0, fullText.Length);
            // Update the TextMeshPro text with the revealed characters
            textMeshPro.text = fullText.Substring(0, charactersToShow);
        }
    }
}
