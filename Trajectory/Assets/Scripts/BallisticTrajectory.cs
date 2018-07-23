using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BallisticTrajectory : MonoBehaviour {

    public LineRenderer trajectory;
    public Player player;
    public int segmentCount = 20;

    public float segmentScale = 1;
    private Collider _hitObject;
    public Collider HitObject { get { return _hitObject; } }

    bool isCharging;

    private void Awake()
    {
        trajectory = GetComponent<LineRenderer>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isCharging = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isCharging = false;
            trajectory.positionCount = 0;
            player.fireStrength = 500;
        }
    }

    private void FixedUpdate()
    {
        if (isCharging)
        {
            SimulatePath();
            player.fireStrength += 3f;
        }     
    }

    void SimulatePath()
    {
        Vector3[] segments = new Vector3[segmentCount];

        segments[0] = player.gameObject.transform.position;

        Vector3 segVelocity = player.transform.up * player.fireStrength * Time.deltaTime;

        _hitObject = null;

        for (int i = 1; i < segmentCount; i++)
        {
            float segTime = (segVelocity.sqrMagnitude != 0) ? segmentScale / segVelocity.magnitude : 0;
            
            segVelocity = segVelocity + Physics.gravity * segTime;

            RaycastHit hit;

            if (Physics.Raycast(segments[i - 1], segVelocity, out hit, segmentScale))
            {
                _hitObject = hit.collider;

                segments[i] = segments[i - 1] + segVelocity.normalized * hit.distance;
                segVelocity = segVelocity - Physics.gravity * (segmentScale - hit.distance) / segVelocity.magnitude;
                //segVelocity = Vector3.Reflect(segVelocity, hit.normal);
            }
            else
            {
                segments[i] = segments[i - 1] + segVelocity * segTime;
            }
            //Debug.Log(i + ": vel:" + segVelocity.magnitude + ". time: " + segTime);
        }

        trajectory.positionCount = segmentCount;
        for (int i = 0; i < segmentCount; i++)
        {
            trajectory.SetPosition(i, segments[i]);
        }

    }
}
