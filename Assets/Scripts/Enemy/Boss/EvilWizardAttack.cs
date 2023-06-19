using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardAttack : MonoBehaviour
{

    [SerializeField] private List<GameObject> attackPoints;

    private int[] attackPatterns = new int[10] {2, 2, 2, 2, 2, 3, 3, 3, 4, 4};


    public void ExecuteAttack()
    {
        int pattern = attackPatterns[Random.Range(0, 10)];
        bool addOne = Random.value > 0.5f;
        for (int i = 0 + (addOne ? 1 : 0); i < attackPoints.Count + (addOne ? -1 : 0); i++)
        {
            if (i % pattern == 0)
            {
                attackPoints[i].SetActive(true);
            }
        }
    }
}
