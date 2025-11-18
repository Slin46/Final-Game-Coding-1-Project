using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public TMP_Text dialogueText;
    public string[] dialogueLines;
    private int index = 0;

    [Header("Next Scene")]
    public GameObject nextPanel;
    public string nextSceneName = "GameScene";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //dialogue lines in order
        dialogueText.text = dialogueLines[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ContinueDialogue()
    {
        //if the index list is less than the dialogue length than minus one on click
        if (index < dialogueLines.Length - 1)
        {
            index++;
            dialogueText.text = dialogueLines[index];
        }
        else
        {
            if (nextPanel != null)
            {
                nextPanel.SetActive(true);
            }

            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
                return;
            }
            gameObject.SetActive(false);
        }

    }
}
