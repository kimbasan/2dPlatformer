using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerInfo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.GetComponent<PlayerHealth>() != null)
        {
            playerInfo.ShowVictoryScreen();
        }
        else
        {
            Debug.Log("Not player");
        }
    }
}
