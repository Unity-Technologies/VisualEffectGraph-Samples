

using UnityEngine.Events;

namespace UnityEngine.VFX.Utility
{
    [ExecuteAlways]
    [RequireComponent(typeof(VisualEffect))]
    class OutputEventHandlerMultipleRB : VFXOutputEventAbstractHandler
    {
        public override bool canExecuteInEditor => false;
        public enum Space
        {
            Local,
            World
        }

        static readonly int k_Position = Shader.PropertyToID("position");
        static readonly int k_Size = Shader.PropertyToID("size");
        static readonly int k_Velocity = Shader.PropertyToID("velocity");

        [Tooltip("The Rigid body to apply a force on.")]
        public Rigidbody[] rigidBodies;
        [Tooltip("The Space VFX Attributes values are expressed.")]
        public Space attributeSpace;
        public float UpwardsModifier = 1.0f;

        public enum RigidBodyEventType
        {
            Impulse,
            Explosion,
            VelocityChange
        }

        [Tooltip("Type of Instantaneous Force to apply on the RigidBody upon event:\n - Impulse using the Velocity attribute \n - Explosion at given Position attribute, using the Size for radius and the magnitude of Velocity Attribute for intensity\n - Velocity Change using Velocity Attribute")]
        public RigidBodyEventType eventType;

        public override void OnVFXOutputEvent(VFXEventAttribute eventAttribute)
        {
            if (rigidBodies == null|| rigidBodies.Length == 0)
                return;

            var position = eventAttribute.GetVector3(k_Position);
            var size = eventAttribute.GetFloat(k_Size);
            var velocity = eventAttribute.GetVector3(k_Velocity);
            if (attributeSpace == Space.Local)
            {
                position = transform.localToWorldMatrix.MultiplyPoint(position);
                velocity = transform.localToWorldMatrix.MultiplyVector(velocity);
                // We assume that the size is bound to the X component of the transform scale
                // and that the transform is uniform.
                size = transform.localToWorldMatrix.MultiplyVector(Vector3.right * size).magnitude;
            }

           foreach (var rigidBody in rigidBodies )
            {
                if (rigidBody == null)
                    continue;
                switch (eventType)
                {
                    case RigidBodyEventType.Impulse:
                        rigidBody.AddForce(velocity, ForceMode.Impulse);
                        break;
                    case RigidBodyEventType.Explosion:
                        rigidBody.AddExplosionForce(velocity.magnitude, position, size, UpwardsModifier);
                        break;
                    case RigidBodyEventType.VelocityChange:
                        rigidBody.AddForce(velocity, ForceMode.VelocityChange);
                        break;
                    default:
                        break;
                }
            }
          
        }
    }
}

