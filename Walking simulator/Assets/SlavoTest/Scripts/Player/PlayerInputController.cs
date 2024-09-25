using UnityEngine;

namespace SlavoTest.Player
{
    public class PlayerInputController
    {
        private float _verticalInput;
        private float _horizontalInput;
        private float _previousYVelocity;
        
        public void SetInputValues()
        {
            _verticalInput = Input.GetAxisRaw("Vertical");
            _horizontalInput = Input.GetAxisRaw("Horizontal");
        }
    }
}