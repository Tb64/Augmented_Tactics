using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RFX4_TransformMotion : MonoBehaviour
{
    public bool explodePos;
    public Vector3 targetLocation;
    public float explosionDist;

    public float Distance = 30;
    public float Speed = 1;
    public float Dampeen = 0;
    public float MinSpeed = 1;
    public float TimeDelay = 0;
    public LayerMask CollidesWith = ~0;

    public GameObject[] EffectsOnCollision;
    public float CollisionOffset = 0;
    public float DestroyTimeDelay = 5;
    public bool CollisionEffectInWorldSpace = true;
    public GameObject[] DeactivatedObjectsOnCollision;
    [HideInInspector]
    public float HUE = -1;
    [HideInInspector]
    public List<GameObject> CollidedInstances;

    private Vector3 startPositionLocal;
    Transform t;
    private Vector3 oldPos;
    private bool isCollided;
    private bool isOutDistance;
    private Quaternion startQuaternion;
    private float currentSpeed;
    private float currentDelay;
    private const float RayCastTolerance = 0.3f;
    private bool isInitialized;
    private bool dropFirstFrameForFixUnityBugWithParticles;
    private bool hit = false;
    public event EventHandler<RFX4_CollisionInfo> CollisionEnter;

    void Start()
    {
        t = transform;
        startQuaternion = t.rotation;
        startPositionLocal = t.localPosition;
        oldPos = t.TransformPoint(startPositionLocal);

        Initialize();
        isInitialized = true;
    }

    void OnEnable()
    {

        if (isInitialized) Initialize();
    }

    void OnDisable()
    {

        if (isInitialized) Initialize();
    }

    private void Initialize()
    {
        GetSettings();
        isCollided = false;
        isOutDistance = false;
        currentSpeed = Speed;
        hit = false;
        currentDelay = 0;
        startQuaternion = t.rotation;
        t.localPosition = startPositionLocal;
        oldPos = t.TransformPoint(startPositionLocal);
        OnCollisionDeactivateBehaviour(true);
        dropFirstFrameForFixUnityBugWithParticles = true;
        //Debug.Log(LayerMask.LayerToName(CollidesWith) + " " + CollidesWith.value);
        if (explodePos)
            CollidesWith.value = 0;
    }

    private void GetSettings()
    {
        KFX_Settings settings = transform.parent.GetComponent<KFX_Settings>();
        if (settings != null)
        {
            explodePos = settings.explodePos;
            targetLocation = settings.targetLocation;
            explosionDist = settings.explosionDist;
        }
    }

    void Update()
    {
        if (!dropFirstFrameForFixUnityBugWithParticles)
        {
            UpdateWorldPosition();
        }
        else dropFirstFrameForFixUnityBugWithParticles = false;
    }

    void UpdateWorldPosition()
    {
        currentDelay += Time.deltaTime;
        if (currentDelay < TimeDelay)
            return;

        var frameMoveOffset = Vector3.zero;
        var frameMoveOffsetWorld = Vector3.zero;
        if(explodePos && !isCollided)
        {
            float distToTarget = Vector3.Distance(t.position, targetLocation);
            if (distToTarget <= explosionDist && !isCollided)
                Explode(targetLocation);
        }

        if (!isCollided && !isOutDistance)
        {
            currentSpeed = Mathf.Clamp(currentSpeed - Speed * Dampeen * Time.deltaTime, MinSpeed, Speed);
            var currentForwardVector = Vector3.forward * currentSpeed * Time.deltaTime;
            frameMoveOffset = t.localRotation * currentForwardVector;
            frameMoveOffsetWorld = startQuaternion * currentForwardVector;
        }

        var currentDistance = (t.localPosition + frameMoveOffset - startPositionLocal).magnitude;

        RaycastHit hit;
        if (!isCollided && Physics.Raycast(t.position, t.forward, out hit, 10, CollidesWith))
        {
            if (frameMoveOffset.magnitude + RayCastTolerance > hit.distance)
            {
                isCollided = true;
                t.position = hit.point;
                oldPos = t.position;
                OnCollisionBehaviour(hit);
                OnCollisionDeactivateBehaviour(false);
                return;
            }
        }

        if (!isOutDistance && currentDistance > Distance)
        {
            isOutDistance = true;
            t.localPosition = startPositionLocal + t.localRotation * Vector3.forward * Distance;
            oldPos = t.position;
            return;
        }

        t.position = oldPos + frameMoveOffsetWorld;
        oldPos = t.position;
    }



    void OnCollisionBehaviour(RaycastHit hit)
    {
        if (explodePos)
            return;
        var handler = CollisionEnter;
        if (handler != null)
            handler(this, new RFX4_CollisionInfo { Hit = hit });
        CollidedInstances.Clear();
        foreach (var effect in EffectsOnCollision)
        {
            var instance = Instantiate(effect, hit.point + hit.normal * CollisionOffset, new Quaternion()) as GameObject;
            CollidedInstances.Add(instance);
            if (HUE > -0.9f)
            {
                RFX4_ColorHelper.ChangeObjectColorByHUE(instance, HUE);
            }
            instance.transform.LookAt(hit.point + hit.normal + hit.normal * CollisionOffset);
            if (!CollisionEffectInWorldSpace) instance.transform.parent = transform;
            Destroy(instance, DestroyTimeDelay);
        }
    }

    void Explode(Vector3 location)
    {
        isCollided = true;
        hit = true;
        Debug.Log("Impact: " + name + " " + transform.position);
        CollidedInstances.Clear();
        var currentForwardVector = Vector3.forward * currentSpeed * Time.deltaTime;
        Vector3 velocity = currentForwardVector * -1f;
        velocity = t.transform.up;
        foreach (var effect in EffectsOnCollision)
        {
            var instance = Instantiate(effect, location + velocity.normalized * CollisionOffset, new Quaternion()) as GameObject;
            CollidedInstances.Add(instance);
            if (HUE > -0.9f)
            {
                RFX4_ColorHelper.ChangeObjectColorByHUE(instance, HUE);
            }
            instance.transform.LookAt(location + velocity.normalized + velocity.normalized * CollisionOffset);
            if (!CollisionEffectInWorldSpace) instance.transform.parent = transform;
            Destroy(instance, DestroyTimeDelay);
        }
        OnCollisionDeactivateBehaviour(false);
    }

    void OnCollisionDeactivateBehaviour(bool active)
    {
        foreach (var effect in DeactivatedObjectsOnCollision)
        {
            effect.SetActive(active);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
            return;

        t = transform;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(t.position, t.position + t.forward * Distance);

    }

    public enum RFX4_SimulationSpace
    {
        Local,
        World
    }

    public class RFX4_CollisionInfo : EventArgs
    {
        public RaycastHit Hit;
    }
}