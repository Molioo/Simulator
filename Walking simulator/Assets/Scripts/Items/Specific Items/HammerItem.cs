
using System.Collections;
using UnityEngine;

public class HammerItem : Item
{
    private bool _isUsingHammer = false;

    private string _animationName = "HammerUse";

    public override void UpdateItemBehaviour()
    {
        base.UpdateItemBehaviour();
        CheckForUseHammerInput();
    }

    private void CheckForUseHammerInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !_isUsingHammer)
        {
            UseHammer();
        }
    }

    private void UseHammer()
    {
        _isUsingHammer = true;
        StartCoroutine(UseHammerRoutine());
    }

    private IEnumerator UseHammerRoutine()
    {
        PlayerSystemsManager.Instance.PlayerAnimator.CrossFade(_animationName, 0.1f);
        yield return new WaitForSeconds(0.1f);
        //
        _isUsingHammer = false;
    }

    public override void OnEquipItem()
    {
        base.OnEquipItem();
        gameObject.SetActive(true);
    }

    public override void OnUnequipItem()
    {
        base.OnUnequipItem();
        gameObject.SetActive(false);
    }
}
