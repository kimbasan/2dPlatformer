using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Transform[] background;
    [SerializeField] private float[] moveCoeff;

    private int backgoundLength;

    void Start()
    {
        backgoundLength = background.Length;
    }

    void Update()
    {
       for (int i=0; i < backgoundLength; i++)
        {
            background[i].position = transform.position * moveCoeff[i];
        } 
    }
}
