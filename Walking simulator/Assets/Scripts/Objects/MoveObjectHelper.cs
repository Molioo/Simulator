using UnityEngine;

[RequireComponent(typeof(MovableObject))]
public class MoveObjectHelper : MonoBehaviour
{
    [SerializeField]
    private MovableObject _movableObject;

    public void StartMovingObject()
    {
        PlayerSystemsManager.Instance.ObjectMovingController.StartMovingObject(_movableObject);
    }
   
}
