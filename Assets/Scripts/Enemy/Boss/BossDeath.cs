using System;
using System.Collections;
using UnityEngine;

public class BossDeath : EnemyDeath
{
    [SerializeField] private EvilWizardBoss bossControls;
    public event EventHandler BossDied;

    public void EnableGravity()
    {
        rb.isKinematic = false;
    }

    public new void OnDeath()
    {
        bossControls.enabled = false;
        StartCoroutine(TriggerEndGameEvent());
    }

    public IEnumerator TriggerEndGameEvent()
    {
        yield return new WaitForSeconds(1.5f);
        if (BossDied != null)
        {
            BossDied(this, EventArgs.Empty);
        }
    }
}
