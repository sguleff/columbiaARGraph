using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AR.Core.Graph
{
    public class Edge 
    {

        public UInt32 ID { get; set; }
        public float Weight { get; set; }
        public String Label { get; set; }
        public Dictionary<String, System.Object> Properties { get; set; }
        public Node StartNode { get; set; }
        public Node EndNode { get; set; }

        public GameObject myARObject { get; set; }

        public Edge(GameObject myARObject)
        {
            ID = Globals.ID_EdgesUsed;
            Globals.ID_EdgesUsed++;
            Properties = new Dictionary<string, object>();
            this.myARObject = myARObject;
        }
    }
}
