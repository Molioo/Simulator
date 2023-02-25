using UnityEngine;

public class InvokeReactionHelper : MonoBehaviour
{
    [SerializeField]
    private Reaction _reaction;
        
    public void TryInvokeReaction()
    {
        _reaction.React();
    }
}
