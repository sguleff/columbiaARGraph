using AR.Core.Graph.ARTouch;
using UnityEngine;
using AR.Core.Logging;

//Attribute this code to Microsoft Acadamy!!
/****************************** Module Header ******************************\
Module Name:  AR.CORE
Project:      3D Graph Rendering on Microsoft Hololens

//This is boilerplate code from Miccrosoft and not create by Sam Guleff
//Attribute this code to Microsoft Acadamy!!
//https://developer.microsoft.com/en-us/windows/mixed-reality/holograms_210
\***************************************************************************/
/// <summary>
/// InteractibleManager keeps tracks of which GameObject
/// is currently in focus.
/// </summary>
public class InteractibleManager : Singleton<InteractibleManager>
{
    private DBLogger myLogs;
    public GameObject FocusedGameObject { get; private set; }

    private GameObject oldFocusedGameObject = null;


    private void Awake()
    {
        myLogs = AR.Core.Logging.DBLogger.getInstance();
    }


    void Start()
    {
        myLogs.LogMessage(AR.Core.Types.LoggingLevels.Verbose, "Starting GazeManager", "InteractibleManager.Start", "Alpha");
        FocusedGameObject = null;
    }

    void Update()
    {
        oldFocusedGameObject = FocusedGameObject;

        if (GazeManager.Instance.Hit)
        {
            myLogs.LogMessage(AR.Core.Types.LoggingLevels.Verbose, "GazeManager.Instance.Hit", "InteractibleManager.update","Alpha");

            RaycastHit hitInfo = GazeManager.Instance.HitInfo;
            if (hitInfo.collider != null)
            {
                // 2.c: Assign the hitInfo's collider gameObject to the FocusedGameObject.
                FocusedGameObject = hitInfo.collider.gameObject;
            }
            else
            {
                FocusedGameObject = null;
            }
        }
        else
        {
            FocusedGameObject = null;
        }

        if (FocusedGameObject != oldFocusedGameObject)
        {
            ResetFocusedInteractible();
            myLogs.LogMessage(AR.Core.Types.LoggingLevels.Verbose, "GazeManager.Instance.Hit (NEW OBJECT)", "InteractibleManager.update", "Alpha");
            if (FocusedGameObject != null)
            {
                if (FocusedGameObject.GetComponent<Interactible>() != null)
                {
                    // 2.c: Send a GazeEntered message to the FocusedGameObject.
                    FocusedGameObject.SendMessage("GazeEntered");
                }
            }
        }
    }

    private void ResetFocusedInteractible()
    {
        if (oldFocusedGameObject != null)
        {
            if (oldFocusedGameObject.GetComponent<Interactible>() != null)
            {
                // 2.c: Send a GazeExited message to the oldFocusedGameObject.
                oldFocusedGameObject.SendMessage("GazeExited");
            }
        }
    }
}