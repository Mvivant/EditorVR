using UnityEngine;
using UnityEngine.InputNew;

// GENERATED FILE - DO NOT EDIT MANUALLY
namespace UnityEngine.InputNew
{
	public class Locomotion3dRudder : ActionMapInput {
		public Locomotion3dRudder (ActionMap actionMap) : base (actionMap) { }
		
		public AxisInputControl @forward { get { return (AxisInputControl)this[0]; } }
		public AxisInputControl @right { get { return (AxisInputControl)this[1]; } }
		public AxisInputControl @up { get { return (AxisInputControl)this[2]; } }
		public AxisInputControl @rotation { get { return (AxisInputControl)this[3]; } }
	}
}
