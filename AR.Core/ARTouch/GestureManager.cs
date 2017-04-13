/****************************** Module Header ******************************\
Module Name:  AR.CORE
Project:      3D Graph Rendering on Microsoft Hololens

//This is boilerplate code from Miccrosoft and not create by Sam Guleff
//Attribute this code to Microsoft Acadamy!!
//https://developer.microsoft.com/en-us/windows/mixed-reality/holograms_210
\***************************************************************************/
using UnityEngine;
using UnityEngine.VR.WSA.Input;


//Attribute this code to Microsoft Acadamy!!


namespace AR.Core.Graph.ARTouch
{
    /// <summary>
    /// GestureManager contains event handlers for subscribed gestures.
    /// </summary>
    public class GestureManager : MonoBehaviour
    {
        private GestureRecognizer gestureRecognizer;

        void Start()
        {
            gestureRecognizer = new GestureRecognizer();
            gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);

            gestureRecognizer.TappedEvent += (source, tapCount, ray) =>
            {
                GameObject focusedObject = InteractibleManager.Instance.FocusedGameObject;

                if (focusedObject != null)
                {
                    focusedObject.SendMessage("OnSelect");
                }
            };

            gestureRecognizer.StartCapturingGestures();
        }

        void OnDestroy()
        {
            gestureRecognizer.StopCapturingGestures();
        }
    }
}