using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace NewtonVR
{


    public class controllercube2 : NVRInteractable
    {

        private const float MaxVelocityChange = 10f;
        private const float MaxAngularVelocityChange = 20f;
        private const float VelocityMagic = 6000f;
        private const float AngularVelocityMagic = 50f;

        public bool DisablePhysicalMaterialsOnAttach = false;

        [Tooltip("If you have a specific point you'd like the object held at, create a transform there and set it to this variable")]
        public Transform InteractionPoint;

        public UnityEvent OnUseButtonDown;
        public UnityEvent OnUseButtonUp;

        public UnityEvent OnHovering;

        public UnityEvent OnBeginInteraction;
        public UnityEvent OnEndInteraction;

        protected Transform PickupTransform;

        protected Vector3 ExternalVelocity;
        protected Vector3 ExternalAngularVelocity;

        protected Vector3?[] VelocityHistory;
        protected Vector3?[] AngularVelocityHistory;
        protected int CurrentVelocityHistoryStep = 0;

        protected float StartingDrag = -1;
        protected float StartingAngularDrag = -1;

        protected Dictionary<Collider, PhysicMaterial> MaterialCache = new Dictionary<Collider, PhysicMaterial>();



        private bool blocked;
        private Vector3 count;
        public float size1 = 0.25f;

 

        //child down parent up
        public GameObject child;
        public bool hasChild;

        public GameObject parent;
        public bool hasParent;

        //TODO : add parent


        protected override void Awake()
        {
            base.Awake();

            this.Rigidbody.maxAngularVelocity = 100f;
        }

        //public bool clicked = false;
        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            blocked = false;
            hasChild = false;
            hasParent = false;
        }

        protected virtual void FixedUpdate()
        {
            if (IsAttached == true)
            {
                bool dropped = CheckForDrop();

                if (dropped == false)
                {
                    UpdateVelocities();
                }
            }

            AddExternalVelocities();
        }

        // Update is called once per frame
        /*void Update () {
            if (true) {
                transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") ,0.0f, Input.GetAxis("Mouse Y"));
            }
            // Création d'un nouveau vecteur de déplacement

        }*/

       

        protected virtual void UpdateVelocities()
        {
            float velocityMagic = VelocityMagic / (Time.deltaTime / NVRPlayer.NewtonVRExpectedDeltaTime);
            float angularVelocityMagic = AngularVelocityMagic / (Time.deltaTime / NVRPlayer.NewtonVRExpectedDeltaTime);

            Quaternion rotationDelta;
            Vector3 positionDelta;

            float angle;
            Vector3 axis;

            if (InteractionPoint != null || PickupTransform == null) //PickupTransform should only be null
            {
                rotationDelta = AttachedHand.transform.rotation * Quaternion.Inverse(InteractionPoint.rotation);
                positionDelta = (AttachedHand.transform.position - InteractionPoint.position);
            }
            else
            {
                rotationDelta = PickupTransform.rotation * Quaternion.Inverse(this.transform.rotation);
                positionDelta = (PickupTransform.position - this.transform.position);
            }

            rotationDelta.ToAngleAxis(out angle, out axis);

            if (angle > 180)
                angle -= 360;

            if (angle != 0)
            {
                Vector3 angularTarget = angle * axis;
                if (float.IsNaN(angularTarget.x) == false)
                {
                    angularTarget = (angularTarget * angularVelocityMagic) * Time.deltaTime;
                    this.Rigidbody.angularVelocity = Vector3.MoveTowards(this.Rigidbody.angularVelocity, angularTarget, MaxAngularVelocityChange);
                }
            }

            Vector3 velocityTarget = (positionDelta * velocityMagic) * Time.deltaTime;
            if (float.IsNaN(velocityTarget.x) == false)
            {
                this.Rigidbody.velocity = Vector3.MoveTowards(this.Rigidbody.velocity, velocityTarget, MaxVelocityChange);
            }


            if (VelocityHistory != null)
            {
                CurrentVelocityHistoryStep++;
                if (CurrentVelocityHistoryStep >= VelocityHistory.Length)
                {
                    CurrentVelocityHistoryStep = 0;
                }

                VelocityHistory[CurrentVelocityHistoryStep] = this.Rigidbody.velocity;
                AngularVelocityHistory[CurrentVelocityHistoryStep] = this.Rigidbody.angularVelocity;
            }
        }

        protected virtual void AddExternalVelocities()
        {
            if (ExternalVelocity != Vector3.zero)
            {
                this.Rigidbody.velocity = Vector3.Lerp(this.Rigidbody.velocity, ExternalVelocity, 0.5f);
                ExternalVelocity = Vector3.zero;
            }

            if (ExternalAngularVelocity != Vector3.zero)
            {
                this.Rigidbody.angularVelocity = Vector3.Lerp(this.Rigidbody.angularVelocity, ExternalAngularVelocity, 0.5f);
                ExternalAngularVelocity = Vector3.zero;
            }
        }


        public override void AddExternalVelocity(Vector3 velocity)
        {
            if (ExternalVelocity == Vector3.zero)
            {
                ExternalVelocity = velocity;
            }
            else
            {
                ExternalVelocity = Vector3.Lerp(ExternalVelocity, velocity, 0.5f);
            }
        }

        public override void AddExternalAngularVelocity(Vector3 angularVelocity)
        {
            if (ExternalAngularVelocity == Vector3.zero)
            {
                ExternalAngularVelocity = angularVelocity;
            }
            else
            {
                ExternalAngularVelocity = Vector3.Lerp(ExternalAngularVelocity, angularVelocity, 0.5f);
            }
        }

        public override void BeginInteraction(NVRHand hand)
        {
            base.BeginInteraction(hand);

            StartingDrag = Rigidbody.drag;
            StartingAngularDrag = Rigidbody.angularDrag;
            Rigidbody.drag = 0;
            Rigidbody.angularDrag = 0.05f;

            if (DisablePhysicalMaterialsOnAttach == true)
            {
                DisablePhysicalMaterials();
            }

            PickupTransform = new GameObject(string.Format("[{0}] NVRPickupTransform", this.gameObject.name)).transform;
            PickupTransform.parent = hand.transform;
            PickupTransform.position = this.transform.position;
            PickupTransform.rotation = this.transform.rotation;

            ResetVelocityHistory();

            if (OnBeginInteraction != null)
            {
                OnBeginInteraction.Invoke();
            }
        }

        public override void EndInteraction()
        {
            base.EndInteraction();

            Rigidbody.drag = StartingDrag;
            Rigidbody.angularDrag = StartingAngularDrag;

            if (PickupTransform != null)
            {
                Destroy(PickupTransform.gameObject);
            }

            if (DisablePhysicalMaterialsOnAttach == true)
            {
                EnablePhysicalMaterials();
            }

            ApplyVelocityHistory();
            ResetVelocityHistory();

            if (OnEndInteraction != null)
            {
                OnEndInteraction.Invoke();
            }
        }

        public override void HoveringUpdate(NVRHand hand, float forTime)
        {
            base.HoveringUpdate(hand, forTime);

            if (OnHovering != null)
            {
                OnHovering.Invoke();
            }
        }

        public override void ResetInteractable()
        {
            EndInteraction();
            base.ResetInteractable();
        }

        public override void UseButtonDown()
        {
            base.UseButtonDown();

            if (OnUseButtonDown != null)
            {
                OnUseButtonDown.Invoke();
            }
        }

        public override void UseButtonUp()
        {
            base.UseButtonUp();

            if (OnUseButtonUp != null)
            {
                OnUseButtonUp.Invoke();
            }
        }

        protected virtual void ApplyVelocityHistory()
        {
            if (VelocityHistory != null)
            {
                Vector3? meanVelocity = GetMeanVector(VelocityHistory);
                if (meanVelocity != null)
                {
                    this.Rigidbody.velocity = meanVelocity.Value;
                }

                Vector3? meanAngularVelocity = GetMeanVector(AngularVelocityHistory);
                if (meanAngularVelocity != null)
                {
                    this.Rigidbody.angularVelocity = meanAngularVelocity.Value;
                }
            }
        }

        protected virtual void ResetVelocityHistory()
        {
            if (NVRPlayer.Instance.VelocityHistorySteps > 0)
            {
                CurrentVelocityHistoryStep = 0;

                VelocityHistory = new Vector3?[NVRPlayer.Instance.VelocityHistorySteps];
                AngularVelocityHistory = new Vector3?[NVRPlayer.Instance.VelocityHistorySteps];
            }
        }

        protected Vector3? GetMeanVector(Vector3?[] positions)
        {
            float x = 0f;
            float y = 0f;
            float z = 0f;

            int count = 0;
            for (int index = 0; index < positions.Length; index++)
            {
                if (positions[index] != null)
                {
                    x += positions[index].Value.x;
                    y += positions[index].Value.y;
                    z += positions[index].Value.z;

                    count++;
                }
            }

            if (count > 0)
            {
                return new Vector3(x / count, y / count, z / count);
            }

            return null;
        }

        protected void DisablePhysicalMaterials()
        {
            for (int colliderIndex = 0; colliderIndex < Colliders.Length; colliderIndex++)
            {
                if (Colliders[colliderIndex] == null)
                {
                    continue;
                }

                MaterialCache[Colliders[colliderIndex]] = Colliders[colliderIndex].sharedMaterial;
                Colliders[colliderIndex].sharedMaterial = null;
            }
        }

        protected void EnablePhysicalMaterials()
        {
            for (int colliderIndex = 0; colliderIndex < Colliders.Length; colliderIndex++)
            {
                if (Colliders[colliderIndex] == null)
                {
                    continue;
                }

                if (MaterialCache.ContainsKey(Colliders[colliderIndex]) == true)
                {
                    Colliders[colliderIndex].sharedMaterial = MaterialCache[Colliders[colliderIndex]];
                }
            }
        }

        public override void UpdateColliders()
        {
            base.UpdateColliders();

            if (DisablePhysicalMaterialsOnAttach == true)
            {
                for (int colliderIndex = 0; colliderIndex < Colliders.Length; colliderIndex++)
                {
                    if (MaterialCache.ContainsKey(Colliders[colliderIndex]) == false)
                    {
                        MaterialCache.Add(Colliders[colliderIndex], Colliders[colliderIndex].sharedMaterial);

                        if (IsAttached == true)
                        {
                            Colliders[colliderIndex].sharedMaterial = null;
                        }
                    }
                }
            }
        }

        void OnMouseDrag()
        {
            if (!hasChild)
                transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
            else
            {
                count += new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
                while (count.x >= size1)
                {
                    count.x -= size1;
                    transform.position += new Vector3(size1, 0.0f, 0.0f);
                }

                while (count.x <= -size1)
                {
                    count.x += size1;
                    transform.position -= new Vector3(size1, 0.0f, 0.0f);
                }
                if (count.y > 2)
                {
                    transform.position += new Vector3(0.0f, 2.0f, 0.0f);
                    blocked = false;
                }
                if (count.y < -child.GetComponent<Collider>().bounds.size.y - 2)
                {
                    transform.position -= new Vector3(0.0f, 3.0f, 0.0f);
                    blocked = false;
                }
                Vector3 pos = transform.position;
                Vector3 childPos = child.transform.position;
                Vector3 currSize = GetComponent<Collider>().bounds.size;
                Vector3 childSize = child.GetComponent<Collider>().bounds.size;
                if (pos.x > childPos.x + childSize.x / 2 + childSize.x / 2)
                {
                    hasChild = false;
                }

                if (pos.x < childPos.x - childSize.x / 2 - childSize.x / 2)
                {
                    hasChild = false;
                }
            }
        }

        void OnTriggerEnter(Collider col)
        {
            child = col.gameObject;
            if (IsAttached && child.name.Substring(0, 4) == "lego")
            {

                GameObject upperObj;
                GameObject lowerObj;
                Collider upperCol;
                Collider lowerCol;

                if (col.gameObject.transform.position.x > GetComponent<GameObject>().transform.position.x)
                {
                    upperObj = col.gameObject;
                    upperCol = col;
                    lowerObj = GetComponent<GameObject>();
                    lowerCol = GetComponent<Collider>();
                }
                else
                {
                    upperObj = GetComponent<GameObject>();
                    upperCol = GetComponent<Collider>();
                    lowerObj = col.gameObject;
                    lowerCol = col;
                }
                //TODO change for bricks of different size and z as x and from dowm

              
                hasChild = true;

                


                blocked = true;
                count = new Vector3(0.0f, 0.0f, 0.0f);

                print("Mouse moved left");
                print(col.gameObject.transform.position);
                Vector3 lowerPosition = lowerObj.transform.position;
                Vector3 lowerSize = lowerCol.bounds.size;
                Vector3 upperSize = upperCol.bounds.size;
                Vector3 upperPos = upperObj.transform.position;
                print(transform.position);


                transform.position = lowerPosition + new Vector3((upperSize.x - lowerSize.x) / 2, lowerSize.y, 0.0f);

                if (lowerPosition.x - lowerSize.x / 4 > upperPos.x)
                {
                    print("yes");
                    print(lowerSize.x / 4);
                    print(upperPos.x);

                    transform.position += new Vector3(-upperSize.x + 1, 0.0f, 0.0f);
                }
                else if (lowerPosition.x - lowerSize.x / 4 < transform.position.x)
                {
                    transform.position += new Vector3(lowerSize.x - 1, 0.0f, 0.0f);
                    print("no");

                }

            }
        }
    }
}
