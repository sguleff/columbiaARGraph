﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AR.Core.Graph
{

    public class Node 
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

        public GameObject myARObject;


        public Node(GameObject myARObject)
        {
            ID = ++ Globals.ID_NodesUsed;
            Properties = new Dictionary<string, object>();
            EdgesOut = new Dictionary<uint, Edge>();
            EdgesIn = new Dictionary<uint, Edge>();
            this.myARObject = myARObject;
        }

        //Movement of nodes
        public Node MoveTo(Vector3 vec)
        {
            myARObject.transform.position = vec;
            return this;

        }
        public Node MoveDelta(Vector3 vec)
        {
            var curLoc = myARObject.transform.position;
            myARObject.transform.position = new Vector3(vec.x + curLoc.x, vec.y + curLoc.y, vec.z + curLoc.z);
            return this;

        }
        public Node Scale(Vector3 vec)
        {
            myARObject.transform.localScale = vec;
            return this;

        }
        public Node MoveTo(float x, float y, float z)
        {
            Vector3 vec = new Vector3(x, y, z);
            myARObject.transform.position = vec;
            return this;

        }
        public Node MoveDelta(float x, float y, float z)
        {
            Vector3 vec = new Vector3(x, y, z);
            var curLoc = myARObject.transform.position;
            myARObject.transform.position = new Vector3(vec.x + curLoc.x, vec.y + curLoc.y, vec.z + curLoc.z);
            return this;

        }
        public Node Scale(float x, float y, float z)
        {
            Vector3 vec = new Vector3(x, y, z);
            myARObject.transform.localScale = vec;
            return this;

        }
        public Node EvenlyScale(float evenly)
        {
            Vector3 vec = new Vector3(evenly, evenly, evenly);
            myARObject.transform.localScale = vec;
            return this;

        }
        public Node ChangeNodeColor(Visuals.Colors c)
        {
            myARObject.GetComponent<MeshRenderer>().material.color = c.myColor;
            return this;
        }
        public Node ChangeNodeColor(Color c)
        {
            myARObject.GetComponent<MeshRenderer>().material.color = c;
            return this;
        }

    }

}
