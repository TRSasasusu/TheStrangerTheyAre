using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class QuantumAnglerfish : QuantumObject
    {
        private float _noAnglerfishRadius = 100f; // Safe radius in meters

        public override void Awake()
        {
            base.Awake();
            _maxSnapshotLockRange = 1000; // amplifies snapshot range to fit the anglerfish, so snapshots can be made
        }

        public override bool ChangeQuantumState(bool skipInstantVisibilityCheck)
        {
            // Grab current bramble dimension sphere
            SphereShape sphere = GetCurrentBrambleSphere();
            if (sphere == null)
            {
                TheStrangerTheyAre.WriteLine("QuantumAnglerfish couldn't find a valid SphereShape to reposition within.", OWML.Common.MessageType.Warning);
                return false;
            }
            Transform sphereTransform = sphere.transform;

            // Get random point on sphere
            Vector3 localPoint = UnityEngine.Random.onUnitSphere * sphere.radius;
            Vector3 globalPoint = sphereTransform.TransformPoint(localPoint);

            // Get player position
            Vector3 playerPos = Locator.GetPlayerTransform().position;
            Vector3 offsetFromPlayer = globalPoint - playerPos;

            // If too close, move further away
            if (offsetFromPlayer.magnitude < _noAnglerfishRadius)
            {
                offsetFromPlayer = offsetFromPlayer.normalized * _noAnglerfishRadius;
                globalPoint = playerPos + offsetFromPlayer;
            }

            Quaternion randomYRot = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);

            // Set new position
            OWRigidbody rb = GetComponent<OWRigidbody>();
            if (rb != null)
            {
                rb.SetPosition(globalPoint);
                rb.SetRotation(randomYRot);
            }
            else
            {
                transform.position = globalPoint;
                transform.rotation = randomYRot;
            }

            if (!Physics.autoSyncTransforms) Physics.SyncTransforms();

            return true;
        }

        // TODO: Replace this with a reference to a sphere shape from the dimension 
        private SphereShape GetCurrentBrambleSphere()
        {
            return GetComponentInParent<SphereShape>();
        }
    }
}