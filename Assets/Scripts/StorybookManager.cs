using UnityEngine;
using System.Collections;
using TMPro;

public class StorybookManager : MonoBehaviour
{
    public TMP_Text storyText;
    public float typeSpeed = 0.05f; // Speed at which each character appears
    public float displayDuration = 5f;
    public string[] storyLines;

    private int currentLineIndex = 0;

    void Start()
    {
        if (storyLines.Length > 0)
        {
            StartCoroutine(DisplayStory());
        }
    }

    IEnumerator DisplayStory()
    {
        while (currentLineIndex < storyLines.Length)
        {
            yield return StartCoroutine(TypeText(storyLines[currentLineIndex]));
            yield return new WaitForSeconds(displayDuration);
            yield return StartCoroutine(ClearText());
            currentLineIndex++;
        }
    }

    IEnumerator TypeText(string line)
    {
        storyText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            storyText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    IEnumerator ClearText()
    {
        yield return new WaitForSeconds(typeSpeed);
        storyText.text = "";
    }
}