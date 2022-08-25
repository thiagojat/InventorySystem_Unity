using UnityEngine;

public class Interactable : MonoBehaviour
{   
    private bool interact = false;

    private void Update()
    {
        if (interact){
            Interact();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            interact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopInteraction();
        }
    }
    public virtual void Interact()
    {

    }
    public virtual void StopInteraction()
    {
        interact = false;
    }
}
