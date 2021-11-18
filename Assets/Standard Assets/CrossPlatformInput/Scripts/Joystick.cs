using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		public enum AxisOption
		{
			// Options for which axes to use
			Both, // Use both
			OnlyHorizontal, // Only horizontal
			OnlyVertical // Only vertical
		}

		public int MovementRange = 100;
		public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
		public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input

		private Vector3 _joystickStartPos;
		private bool _isUseX; // Toggle for using the x axis
		private bool _isUseY; // Toggle for using the Y axis
		private CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
		private CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input

		private void OnEnable()
		{
			CreateVirtualAxes();
		}

		private void CreateVirtualAxes()
		{
			// set axes to use
			_isUseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
			_isUseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

			// create new axes based on axes to use
			if (_isUseX)
			{
				m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
			}
			if (_isUseY)
			{
				m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
			}
		}

        void Start()
        {
            _joystickStartPos = transform.position;
        }

        public void SetStartPosition(Vector3 position)
        {
	        _joystickStartPos = position;
        }


		public void OnDrag(PointerEventData data)
		{
			Vector3 newPos = Vector3.zero;

			if (_isUseX)
			{
				int delta = (int)(data.position.x - _joystickStartPos.x);
				delta = Mathf.Clamp(delta, - MovementRange, MovementRange);
				newPos.x = delta;
			}

			if (_isUseY)
			{
				int delta = (int)(data.position.y - _joystickStartPos.y);
				delta = Mathf.Clamp(delta, -MovementRange, MovementRange);
				newPos.y = delta;
			}
			transform.position = new Vector3(_joystickStartPos.x + newPos.x, _joystickStartPos.y + newPos.y, _joystickStartPos.z + newPos.z);
			UpdateVirtualAxes(transform.position);
		}


		public void OnPointerUp(PointerEventData data)
		{
			transform.position = _joystickStartPos;
			UpdateVirtualAxes(_joystickStartPos);
		}

		void UpdateVirtualAxes(Vector3 value)
		{
			var delta = _joystickStartPos - value;
			delta.y = -delta.y;
			delta /= MovementRange;
			if (_isUseX)
			{
				m_HorizontalVirtualAxis.Update(-delta.x);
			}

			if (_isUseY)
			{
				m_VerticalVirtualAxis.Update(delta.y);
			}
		}


		public void OnPointerDown(PointerEventData data)
		{
			
		}

		void OnDisable()
		{
			// remove the joysticks from the cross platform input
			if (_isUseX)
			{
				m_HorizontalVirtualAxis.Remove();
			}
			if (_isUseY)
			{
				m_VerticalVirtualAxis.Remove();
			}
		}
	}
}