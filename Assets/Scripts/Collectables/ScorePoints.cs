using UnityEngine;

public class ScorePoints : MonoBehaviour
{
    [SerializeField] private int points;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Score score = collision.attachedRigidbody.GetComponent<Score>();
        if (score != null)
        {
            score.AddScore(points);
            Destroy(gameObject);
        }
    }
}
