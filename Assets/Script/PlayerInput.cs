using System;
using System.Collections.Generic;
using UnityEngine;

namespace CholHo
{
    public class PlayerInput : MonoBehaviour
    {
        public enum commendKeyEnum
        {
            KeyLeftArrow,
            KeyRightArrow,
            KeyUpArrow,
            KeyA,
            KeyD,
            KeyW,
        }

        Dictionary<int, KeyCode> commendKey;

        public commendKeyEnum leftKey;
        public commendKeyEnum rightKey;
        public commendKeyEnum jumpKey;

        public float horizontal { get; private set; }
        public delegate void PlayerInputJump();
        public PlayerInputJump delegateJump;

        public event Action onJumpKeyDown;
        public event Action onJumpKey;
        public event Action onJumpKeyUp;

        private void Start()
        {
            commendKey = new Dictionary<int, KeyCode>
        {
            {0,KeyCode.LeftArrow },
            {1,KeyCode.RightArrow},
            {2,KeyCode.UpArrow},
            {3,KeyCode.A},
            {4,KeyCode.D },
            {5,KeyCode.W }
        };

            horizontal = 0;

        }

        void Update()
        {
            horizontal = GetHorizontalAxis();
            JumpAction();
        }

        private int GetHorizontalAxis()
        {
            if (Input.GetKey(commendKey[(int)leftKey]) && Input.GetKey(commendKey[(int)rightKey]))
            {
                return 0;
            }

            if (Input.GetKey(commendKey[(int)leftKey]))
            {
                return -1;
            }

            if (Input.GetKey(commendKey[(int)rightKey]))
            {
                return 1;
            }

            return 0;
        }
        private void JumpAction()
        {
            if (Input.GetKeyDown(commendKey[(int)jumpKey]))
            {
                if (onJumpKeyDown != null)
                {
                    onJumpKeyDown.Invoke();
                }
            }
            else if (Input.GetKey(commendKey[(int)jumpKey]))
            {
                if (onJumpKey != null)
                {
                    onJumpKey.Invoke();
                }
            }
            else if (Input.GetKeyUp(commendKey[(int)jumpKey]))
            {
                if (onJumpKeyUp != null)
                {
                    onJumpKeyUp.Invoke();
                }
            }
        }
    }
}
