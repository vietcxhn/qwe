using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.OnScreen
{
    public class JumpButton : MonoBehaviour, IPointerDownHandler,IDropHandler
    {
        public event Action<TypeSkills> classJumpComboEvent;
        public Action JumpAction;
        
        [InputControl(layout = "Button")]
        [SerializeField]
        private string m_ControlPath;
        private bool Up;
        private bool Def;
        private bool Down;

        private UpSlotButton slotUpButton;
        private DownSlotButton slotDownButton;

        private DefenseButton defenseButton;

        // protected override string controlPathInternal
        // {
        //     get => m_ControlPath;
        //     set => m_ControlPath = value;
        // }
        private void Awake() {
            slotUpButton = GameObject.FindGameObjectWithTag("UpSlotUI").GetComponent<UpSlotButton>();            
            slotDownButton = GameObject.FindGameObjectWithTag("DownSlotUI").GetComponent<DownSlotButton>();       
            defenseButton = GameObject.FindGameObjectWithTag("DefenseUI").GetComponent<DefenseButton>();            

            slotUpButton.upSlotEvent += SetSlotEvent;
            slotDownButton.downSlotEvent += SetSlotEvent;
            defenseButton.ResetComboEvent += SetSlotEvent;
        }

        private void SetSlotEvent(bool defEvent ,bool downEvent , bool upEvent)
        {
            Down = downEvent;
            Def = defEvent;
            Up = upEvent;
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            // SendValueToControl(1.0f);
            JumpAction?.Invoke();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (Up && Def) 
            {
                classJumpComboEvent?.Invoke(TypeSkills.DefUpJump);
                Def = false;
                Up = false;
            }

            else if (Down && Def) 
            {
                classJumpComboEvent?.Invoke(TypeSkills.DefDownJump);
                Def = false;
                Down = false;
            }

        }




    }

}