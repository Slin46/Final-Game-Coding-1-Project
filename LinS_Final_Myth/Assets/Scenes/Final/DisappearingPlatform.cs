using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public float disappearingTime = 2f;
    public bool playerOnPlatform = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOnPlatform)
        {
            disappearingTime -= Time.deltaTime;
            if (disappearingTime < 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;

        }
        else
        {
            playerOnPlatform = false;
        }
    }

    public void ResetPlatform()
    {
        //turn the platform back on
        gameObject.SetActive(true);
        //reset the time
        disappearingTime = 2;
        playerOnPlatform = false;

    }
}
