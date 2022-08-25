using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionMessageBehavior : MonoBehaviour
{
	#region Singleton

	public static InteractionMessageBehavior instance;

	void Awake()
	{   
		instance = this;
	}
	#endregion

	private Text interactText;
    private Text failText;
    [SerializeField]private GameObject pickUpMessageGO;
    [SerializeField]private GameObject failMessageGO;

    private void Start()
    {
        interactText = pickUpMessageGO.GetComponent<Text>();
        failText = failMessageGO.GetComponent<Text>();
    }
    public void SetText(string message, MessageType messageType)
    {   
        if(messageType == MessageType.Interact)
        {
            if (!pickUpMessageGO.activeInHierarchy)
            {
                pickUpMessageGO.SetActive(true);
                interactText.text = message;
            }
        }else if(messageType == MessageType.Fail)
        {
            if (!failMessageGO.activeInHierarchy)
            {
                StartCoroutine(FadeText(failText, false, message));
            }
        }
        else
        {
            Debug.LogWarning("MessageType " + messageType + " doesn't exist");
        }
        
    }

    public void DeactiveText(MessageType messageType)
    {
        if (messageType == MessageType.Interact)
        {
            if (pickUpMessageGO.activeInHierarchy)
            {
                interactText.text = "";
                pickUpMessageGO.SetActive(false);
            }
        }
        else if (messageType == MessageType.Fail)
        {
            if (failMessageGO.activeInHierarchy)
            {
                StartCoroutine(FadeText(failText, true, ""));
            }
        }
        else
        {
            Debug.LogWarning("MessageType " + messageType + " doesn't exist");
        }
    }

    IEnumerator FadeText(Text text, bool fadeAway, string message)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, i);
                yield return null;
            }
            failText.text = "";
            failMessageGO.SetActive(false);
        }
        else
        {
            failText.text = message;
            failMessageGO.SetActive(true);
            for (float i = 1; i >= 0; i += (Time.deltaTime * 2))
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, i);
                yield return null;
            }
            
        }
        
    }

}

public enum MessageType
{
    Interact,
    Fail
}

