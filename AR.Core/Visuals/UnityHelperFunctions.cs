using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using AR.Core.Graph;

namespace AR.Core.Visuals
{
    public static class UnityHelperFunctions
    {
        public static GameObject CreateGameObject(AR.Core.Types.GraphProperties myProp, PrimitiveType unityPrim, UnityEngine.Color color)
        {
            var myID = myProp == Types.GraphProperties.Edge ? Globals.ID_NodesUsed : Globals.ID_EdgesUsed;


            GameObject tmpGo = GameObject.CreatePrimitive(unityPrim);
            tmpGo.name = myProp.ToString() + "_" + (myID + 1).ToString();
            tmpGo.AddComponent<MeshFilter>();
            tmpGo.AddComponent<MeshRenderer>();
            tmpGo.GetComponent<MeshRenderer>().material.color = color; // Visuals.Colors.Blue;
            return tmpGo;
        }


    }
}
