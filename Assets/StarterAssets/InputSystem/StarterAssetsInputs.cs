using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool pause;
		public bool meleeAttack;
		public bool shoot;
		public bool dodge;
		public bool activeItem;
		public int buyItem;

		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		public void OnPause(InputValue value)
		{
			PauseInput(value.isPressed);
		}
		public void OnMeleeAttack(InputValue value)
		{
			MeleeAttackInput(value.isPressed);
		}
		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}
		public void OnDodge(InputValue value)
		{
			DodgeInput(value.isPressed);
		}
		public void OnActiveItem(InputValue value)
		{
			ActiveItemInput(value.isPressed);
		}
        public void OnBuyItem(InputValue value)
        {
            BuyItemInput(value.isPressed);
        }
        public void OnBuyItem2(InputValue value)
        {
            BuyItem2Input(value.isPressed);
        }
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		public void PauseInput(bool newPauseState)
		{
			pause = newPauseState;
		}
		public void MeleeAttackInput(bool newMeleeAttackState)
		{
			meleeAttack = newMeleeAttackState;
		}
		public void ShootInput(bool newShootState)
		{
			shoot = newShootState;
		}
		public void DodgeInput(bool newDodgeState)
		{
			dodge = newDodgeState;
		}
		public void ActiveItemInput(bool newActiveItemState) 
		{
			activeItem = newActiveItemState;
		}
        public void BuyItemInput(bool newBuyItemState)
        {
            buyItem++;
        }
        public void BuyItem2Input(bool newBuyItemState)
        {
            buyItem=0;
        }



#if !UNITY_IOS || !UNITY_ANDROID

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}