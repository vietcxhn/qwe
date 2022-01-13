using Cinemachine;

using UnityEngine;
using UnityEngine.Assertions;

namespace LF2.Visual
{
    public class CameraController : MonoBehaviour
    {
        private CinemachineVirtualCamera m_MainCamera;

        void Start()
        {
            AttachCamera();
        }

        private void AttachCamera()
        {
            m_MainCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
            Assert.IsNotNull(m_MainCamera, "CameraController.AttachCamera: Couldn't find gameplay freelook camera");

            if (m_MainCamera)
            {
                // camera body / aim 
                m_MainCamera.Follow = transform;
                // m_MainCamera.LookAt = transform;
                // default rotation / zoom
                // m_MainCamera.m_Heading.m_Bias = 40f;
                // m_MainCamera.m_YAxis.Value = 0.5f;
            }
        }
    }
}
