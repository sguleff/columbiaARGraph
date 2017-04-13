﻿/****************************** Module Header ******************************\
Module Name:  AR.CORE
Project:      3D Graph Rendering on Microsoft Hololens

//This is boilerplate code from Miccrosoft and not create by Sam Guleff
//Attribute this code to Microsoft Acadamy!!
//https://developer.microsoft.com/en-us/windows/mixed-reality/holograms_210

This source is subject to the MIT License.
See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
All other rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/
using UnityEngine;
namespace AR.Core.Graph.ARTouch
{
    public class CursorManager : Singleton<CursorManager>
    {
        [Tooltip("Drag the Cursor object to show when it hits a hologram.")]
        public GameObject CursorOnHolograms;

        [Tooltip("Drag the Cursor object to show when it does not hit a hologram.")]
        public GameObject CursorOffHolograms;

        void Awake()
        {
            if (CursorOnHolograms == null || CursorOffHolograms == null)
            {
                return;
            }

            // Hide the Cursors to begin with.
            CursorOnHolograms.SetActive(false);
            CursorOffHolograms.SetActive(false);
        }

        void Update()
        {

            if (GazeManager.Instance == null || CursorOnHolograms == null || CursorOffHolograms == null)
            {
                return;
            }

            if (GazeManager.Instance.Hit)
            {
                // 2.b: SetActive true the CursorOnHolograms to show cursor.
                CursorOnHolograms.SetActive(true);
                // 2.b: SetActive false the CursorOffHolograms hide cursor.
                CursorOffHolograms.SetActive(false);
            }
            else
            {
                // 2.b: SetActive true CursorOffHolograms to show cursor.
                CursorOffHolograms.SetActive(true);
                // 2.b: SetActive false CursorOnHolograms to hide cursor.
                CursorOnHolograms.SetActive(false);
            }

            // 2.b: Assign gameObject's transform position equals GazeManager's instance Position.
            gameObject.transform.position = GazeManager.Instance.Position;

            // 2.b: Assign gameObject's transform up vector equals GazeManager's instance Normal.
            gameObject.transform.up = GazeManager.Instance.Normal;
        }
    }
}