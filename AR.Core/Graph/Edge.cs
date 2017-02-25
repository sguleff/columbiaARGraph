using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AR.Core.Graph
{
    public class Edge : MonoBehaviour, IUnityVisualProperties
    {

        public UInt32 ID { get; set; }
        public float Weight { get; set; }
        public String Label { get; set; }
        public Dictionary<String, System.Object> Properties { get; set; }
        public Node StartNode { get; set; }
        public Node EndNode { get; set; }


        //From IUnityVisualProps
        public Color myColor { get; set; }
        public Mesh myMesh { get; set; }
        public Material myMaterial { get; set; }
        public Vector3 myLocation { get; set; }
        public Vector3 myRotation { get; set; }
        public Vector3 myScale { get; set; }
        public float mySize { get; set; }



        public Edge()
        {
            ID = ID = Globals.ID_EdgesUsed;
            Globals.ID_EdgesUsed++;
            Properties = new Dictionary<string, object>();
        }
    }
}
