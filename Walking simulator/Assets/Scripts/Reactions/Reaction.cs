using UnityEngine;

[System.Serializable]
public  class Reaction 
{
    [SerializeField]
    private string _reactionText;

    [SerializeField]
    private AudioClip _reactionAudioClip;

    public void React()
    {
        TextReaction();
        SoundReaction();
    }

    private void TextReaction()
    {
        GameUIManager.Instance.ShowReactionText(_reactionText);
    }

    private void SoundReaction()
    {
        //TO DO Audio stuff
    }
}
