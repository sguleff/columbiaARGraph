using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AR.Core.Graph
{

    public class Node : MonoBehaviour, IUnityVisualProperties
    {
        private UInt32 _ID;
        public UInt32 ID
        {
            get
            {
                return _ID;
            }
            private set
            {
                _ID = value;
            }
        }
        public Int32 Weight { get; set; }
        public String Label { get; set; }
        public Dictionary<String, System.Object> Properties { get; set; }

        public Dictionary<UInt32, Edge> EdgesIn
        {
            get
            {
                return _EdgesIn;
            }
            private set
            {
                _EdgesIn = value;
            }
        }
        private Dictionary<UInt32, Edge> _EdgesIn;
        private Dictionary<UInt32, Edge> _EdgesOut;
        public Dictionary<UInt32, Edge> EdgesOut
        {
            get
            {
                return _EdgesOut;
            }
            private set
            {
                _EdgesOut = value;
            }
        }

        //From IUnityVisualProps
        public Color myColor { get; set; }
        public Mesh myMesh { get; set; }
        public Material myMaterial { get; set; }
        public Vector3 myLocation { get; set; }
        public Vector3 myRotation { get; set; }
        public Vector3 myScale { get; set; }
        public float mySize { get; set; }



        public Node()
        {
            ID = ++ Globals.ID_NodesUsed;
            Properties = new Dictionary<string, object>();
            EdgesOut = new Dictionary<uint, Edge>();
            EdgesIn = new Dictionary<uint, Edge>();
        }

    }

}
