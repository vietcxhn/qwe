
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Assertions;

namespace LF2.Server
{
    
    public enum MovementState
    {
        Idle = 0,
        // PathFollowing = 1,
        Move = 1,
        Charging = 2,
        Knockback = 3,
        Air = 4,
        UnControl=5,
    }

    /// <summary>
    /// Component responsible for moving a character on the server side based on inputs.
    /// </summary>
    // [RequireComponent(typeof(NetworkCharacterState), typeof(ServerCharacter)), RequireComponent(typeof(Rigidbody))]
    public class ServerCharacterMovement : NetworkBehaviour
    {
        [SerializeField] AnimationCurve m_gravity;
        
        public float JumpHieght = 9f;
        public float JumpLength = 3f;

        



        [SerializeField] 
        BoxCollider m_BoxCollider;

        [SerializeField]
        Rigidbody m_Rigidbody;

        [SerializeField]
        NetworkCharacterState m_NetworkCharacterState;

        private MovementState m_MovementState;

        // private ServerCharacter m_CharLogic;

        // when we are in charging and knockback mode, we use these additional variables
        private float m_ForcedSpeed;
        private float m_SpecialModeDurationRemaining;

        // this one is specific to knockback mode
        private Vector3 m_KnockbackVector;
        private float m_startTime;
        private Vector3 moveDir;
      

        public int FacingDirection { get; private set; }
        public bool DebugPlayer { get; private set; }

        public float  SpeedWalk = 1f ;
        private float gaviti = 1f ;
        private int k_GroundLayerMask;
        private Vector3 size;

        private void Awake()
        {
            // m_CharLogic = GetComponent<ServerCharacter>();
        
            FacingDirection = 1;
            m_MovementState = MovementState.Idle;
        }

        public override void OnNetworkSpawn()
        {
            if (!IsServer)
            {
                // Disable server component on clients
                enabled = false;
                return;
            }
            m_NetworkCharacterState.InitNetworkRotationY((int)transform.rotation.eulerAngles.y);
            k_GroundLayerMask = LayerMask.GetMask(new[] { "Ground" });

        }

        

        

        /// <summary>
        /// Sets a movement target. We will path to this position, .
        /// </summary>
        /// <param name="position">Position in world space to path to. </param>

        public void SetVelocityXZ(Vector3 targetposition){
            m_MovementState = MovementState.Move;

            CheckIfShouldFlip(Mathf.RoundToInt(targetposition.x));

        }

        /// <summary>
        /// Sets Jump
        /// </summary>
        /// <param name="position">Position in world space to path to. </param>
        public void SetJump(Vector3 moveDir)
        {
            m_Rigidbody.AddForce(JumpHieght*Vector3.up + JumpLength*moveDir,ForceMode.Impulse);     
        }

        public void SetDoubleJump(Vector3 moveDir)
        {
            if (moveDir.x != 0){
                m_Rigidbody.AddForce(JumpHieght*Vector3.up + (JumpLength+1)*moveDir.x*Vector3.right,ForceMode.Impulse); 
            }
            else {
                m_Rigidbody.AddForce(JumpHieght*Vector3.up + (JumpLength+1)*FacingDirection*Vector3.right,ForceMode.Impulse); 
            }        }

        public void StartForwardCharge(float speed, float duration)
        {
            m_MovementState = MovementState.Charging;
            m_ForcedSpeed = speed;
            m_SpecialModeDurationRemaining = duration;
        }

        public void StartKnockback(Vector3 knocker, float speed, float duration)
        {
            m_MovementState = MovementState.Knockback;
            m_KnockbackVector = transform.position - knocker;
            m_ForcedSpeed = speed;
            m_SpecialModeDurationRemaining = duration;
        }

        public bool IsGounded(){
            bool hit_ground = Physics.Raycast(m_BoxCollider.bounds.center,Vector3.down ,m_BoxCollider.bounds.extents.y,k_GroundLayerMask);
            // Color rayColor;
            // if (!hit_ground){
            //     rayColor = Color.green;
            // }else {
            //     rayColor = Color.red;
            // }
            // Debug.DrawRay(m_BoxCollider.bounds.center , Vector3.down * (m_BoxCollider.bounds.extents.y),rayColor);

            return  hit_ground;
        }    



        /// <summary>
        /// Returns true if the current movement-mode is unabortable (e.g. a knockback effect)
        /// </summary>
        /// <returns></returns>
        public bool IsPerformingForcedMovement()
        {
            return m_MovementState == MovementState.Knockback || m_MovementState == MovementState.Charging;
        }

        /// <summary>
        /// Returns true if the character is actively moving, false otherwise.
        /// </summary>
        /// <returns></returns>
        public bool IsMoving()
        {
            return m_MovementState != MovementState.Idle;
        }

        /// <summary>
        /// Cancels any moves that are currently in progress.
        /// </summary>
        public void CancelMove()
        {
            m_MovementState = MovementState.Idle;
        }

        /// <summary>
        /// Instantly moves the character to a new position. NOTE: this cancels any active movement operation!
        /// This does not notify the client that the movement occurred due to teleportation, so that needs to
        /// happen in some other way, such as with the custom action visualization in DashAttackActionFX. (Without
        /// this, the clients will animate the character moving to the new destination spot, rather than instantly
        /// appearing in the new spot.)
        /// </summary>
        /// <param name="newPosition">new coordinates the character should be at</param>
        public void Teleport(Vector3 newPosition)
        {
            CancelMove();


            m_Rigidbody.position = transform.position;
            m_Rigidbody.rotation = transform.rotation;
        }

        private void FixedUpdate()
        {
            // Debug.Log(m_MovementState);
            m_NetworkCharacterState.MovementStatus.Value = GetMovementStatus();
        }
 

        public void SetFallingDown()
        {
            if ((m_Rigidbody.velocity.y) < 0f)
            {
                gaviti = m_gravity.Evaluate(Time.deltaTime);
                m_Rigidbody.velocity += gaviti * Physics.gravity.y * Vector3.up * Time.deltaTime;
            }
        }


        public void SetMovementState(MovementState movementState){
            m_MovementState = movementState;
        }
        public MovementState GetMovementState(){
            
            return m_MovementState;

        }


        // private float GetMaxMovementSpeed()
        // {
        //     switch (m_MovementState)
        //     {
        //         case MovementState.Charging:
        //         case MovementState.Knockback:
        //             return m_ForcedSpeed;
        //         case MovementState.Idle:
        //         case MovementState.Move:
        //         default:
        //             return GetBaseMovementSpeed();
        //     }
        // }

        // /// <summary>
        // /// Retrieves the speed for this character's class.
        // /// </summary>
        // private float GetBaseMovementSpeed()
        // {
        //     CharacterClass characterClass = GameDataSource.Instance.CharacterDataByType[m_CharLogic.NetState.CharacterType];
        //     Assert.IsNotNull(characterClass, $"No CharacterClass data for character type {m_CharLogic.NetState.CharacterType}");
        //     return characterClass.Speed;
        // }

        /// <summary>
        /// Determines the appropriate MovementStatus for the character. The
        /// MovementStatus is used by the client code when animating the character.
        /// </summary>
        private MovementStatus GetMovementStatus()
        {
            switch (m_MovementState)
            {
                case MovementState.Move:
                    return MovementStatus.Walking;
                case MovementState.Knockback:
                    return MovementStatus.Uncontrolled;
                case MovementState.Air:
                    return MovementStatus.Air;
                default:
                    return MovementStatus.Idle;
            }
        }

        
        public void CheckIfShouldFlip(int xInput){
            Debug.Log(FacingDirection);

            if (xInput != 0 && xInput != FacingDirection){
                Flip();
            }
        }
        public void Flip(){
            Debug.Log("flip server");
            FacingDirection *=-1;
            // transform.Rotate(0.0f,180.0f,0.0f);
            if (FacingDirection == 1){
                m_NetworkCharacterState.NetworkRotationY.Value = 0;
            }
            else{
                m_NetworkCharacterState.NetworkRotationY.Value = 180;
            }

        }
    }
}
