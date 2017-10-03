using System.Collections.Generic;
using Assets.Utilities;
using System.Linq;

namespace UnityEngine.InputNew
{
    public class D3dRudder : InputDevice
    {
        public enum Axis3dRudder
        {
            Pitch,
            Roll,
            Yaw,
            UpDown,
        }

		private static readonly string[] kTags = { "Left", "Right", "Middle" };
		public static string[] Tags
		{
			get
			{
				return kTags;
			}
		}		

		public override int tagIndex
		{
			get
			{
				return 2;
			}
		}

		public D3dRudder() : this("D3dRudder", null)
        {}

        public D3dRudder(string deviceName, List<InputControlData> additionalControls)
        {
            this.deviceName = deviceName;
            var controlCount = EnumHelpers.GetValueCount<Axis3dRudder>();
            var controls = Enumerable.Repeat(new InputControlData(), controlCount).ToList();

            // Axis
            controls[(int)Axis3dRudder.Pitch] = new InputControlData { name = "Pitch", controlType = typeof(AxisInputControl) };
            controls[(int)Axis3dRudder.Roll] = new InputControlData { name = "Roll", controlType = typeof(AxisInputControl) };
            controls[(int)Axis3dRudder.Yaw] = new InputControlData { name = "Yaw", controlType = typeof(AxisInputControl) };
            controls[(int)Axis3dRudder.UpDown] = new InputControlData { name = "UpDown", controlType = typeof(AxisInputControl) };

            if (additionalControls != null)
                controls.AddRange(additionalControls);

            SetControls(controls);
            Debug.Log("Create device " + deviceName);
        }
    }
}