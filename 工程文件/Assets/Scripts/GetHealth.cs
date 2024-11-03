using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GetHealth : MonoBehaviour, Interface
{
    public RecoverHealthSO recoverHealth;
    private Collider2D player;
    private Character character;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerTag"))
            character = collision.GetComponent<Character>();
    }
    public void TriggerAction()
    {
        recoverHealth.RaiseEvent(character);
        if (this.gameObject.layer == 11)
            Destroy(this.gameObject);
    }

}
