using Unity3DRudder;
using UnityEngine;
using UnityEngine.InputNew;

namespace UnityEditor.Experimental.EditorVR.Input
{
    sealed class D3dRudderInputToEvents : BaseInputToEvents
    {
        ns3DRudder.Axis axis = new ns3DRudder.Axis();
        ns3DRudder.ModeAxis mode = ns3DRudder.ModeAxis.ValueWithCurveNonSymmetricalPitch;
        ns3DRudder.CurveArray curves = new ns3DRudder.CurveArray();

        // Update is called once per frame
        void Update()
        {
            var isActive = false;
            if (s3DRudderManager.Instance.GetNumberOfConnectedDevice() > 0)
            {
                for (uint i = 0; i < s3DRudderManager._3DRUDDER_SDK_MAX_DEVICE; ++i)
                {
                    if (s3DRudderManager.Instance.GetStatus(i) == ns3DRudder.Status.InUse ||
                        s3DRudderManager.Instance.GetStatus(i) == ns3DRudder.Status.ExtendedMode)
                    {
                        isActive = true;
                        s3DRudderManager.Instance.GetAxis(i, mode, axis, curves);
                        // Forward
                        SendAxisEvents((int)i, (int)D3dRudder.Axis3dRudder.Pitch, axis.GetPhysicalPitch());
                        // Right
                        SendAxisEvents((int)i, (int)D3dRudder.Axis3dRudder.Roll, axis.GetPhysicalRoll());
                        // Rotation
                        SendAxisEvents((int)i, (int)D3dRudder.Axis3dRudder.Yaw, axis.GetPhysicalYaw());
                        // Up
                        SendAxisEvents((int)i, (int)D3dRudder.Axis3dRudder.UpDown, axis.GetUpDown());
                    }
                }
            }

            if (active != isActive)
                active = isActive;
        }

        void SendAxisEvents(int deviceIndex, int controlIndex, float value)
        {
            var inputEvent = InputSystem.CreateEvent<GenericControlEvent>();
            inputEvent.deviceType = typeof(D3dRudder);
            inputEvent.deviceIndex = deviceIndex;
            inputEvent.controlIndex = controlIndex;
            inputEvent.value = value;
            
            InputSystem.QueueEvent(inputEvent);
        }

        void OnDestroy()
        {
            s3DRudderManager.Instance.ShutDown();
        }
    }
}