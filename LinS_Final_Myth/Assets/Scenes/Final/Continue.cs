using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Continue : MonoBehaviour
{
    [Header("Dialogue Setup")]
    public TMP_Text dialogueText;
    public string[] dialogueLines;
    private int index = 0;

    [Header("Next Scene")]
    public GameObject nextPanel;
    public string nextSceneName = "GameScene";

    private bool dialogueFinished = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //dialogue lines in order
        dialogueText.text = dialogueLines[index];
    }

    // Update is called once per frame
    void Update()
    {
        //left click to continue dialogue or continue to next scene or panel
        if (Input.GetMouseButtonDown(0))
        {
            if (!dialogueFinished)
            {
                ContinueDialogue();
            }
            else
            {
                ContinueToNext();
            }
        }
    }

    void ContinueDialogue()
    {
        //if the index list is less than the dialogue length than minus one on click
        if (index < dialogueLines.Length - 1)
        {
            index++;
            dialogueText.text = dialogueLines[index];
        }
        else
        {
            //or else finish dialogue is true
                dialogueFinished = true;
        }
    }

    void ContinueToNext()
    {
        //if panel
        if (nextPanel != null)
        {
            nextPanel.SetActive(true);
            gameObject.SetActive(false);
        }

        //if scene
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
