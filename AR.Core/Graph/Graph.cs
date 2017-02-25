﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace AR.Core.Graph
{

    public class Graph : MonoBehaviour, IUnityVisualProperties
    {
        public Dictionary<UInt32, Node> AllNodes { get; set; }
        public Dictionary<UInt32, Edge> AllEdges { get; set; }
        public GraphSqlite FlatGraph { get; set; }

        //From IUnityVisualProps

        public Color myColor { get; set; }
        public Mesh myMesh { get; set; }
        public Material myMaterial { get; set; }
        public Vector3 myLocation { get; set; }
        public Vector3 myRotation { get; set; }
        public Vector3 myScale { get; set; }
        public float mySize { get; set; }



        public Graph()
        {
            AllNodes = new Dictionary<UInt32, Node>();
            AllEdges = new Dictionary<UInt32, Edge>();
            FlatGraph = new GraphSqlite();
        }


        //this handles all the node select, add, remove commands a user would need

        public Node AddNodes(String Label = "", UnityVisualProperties opVisProp = null)
        {
            GameObject tmpGo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tmpGo.name = "NODE_" + (Globals.ID_NodesUsed + 1).ToString();
            tmpGo.AddComponent<MeshFilter>();
            tmpGo.AddComponent<MeshRenderer>();
            tmpGo.GetComponent<MeshRenderer>().material.color = Visuals.Colors.Blue;

            var node = new Node(tmpGo);


            //Destroy(tmpSphere);


            AllNodes.Add(node.ID, node);
            FlatGraph.addNode(node);
            return node;
        }
        public void DeleteNode(UInt32 ID)
        {
            var node = GetNode(ID);

            Destroy(node.myARObject);

            foreach (var ed in node.EdgesIn)
            {
                DeleteEdges(ed.Key);
            }
            foreach (var ed in node.EdgesOut)
            {
                DeleteEdges(ed.Key);
            }

            FlatGraph.deleteNodes(node);
            AllNodes.Remove(ID);
        }
        public Node GetNode(UInt32 ID)
        {
            return AllNodes[ID];
        }
        public List<Node> GetNodes(List<UInt32> IDS)
        {
            var retList = new List<Node>();
            foreach (var ID in IDS)
            {
                retList.Add(GetNode(ID));

            }
            return retList;
        }
        public List<Node> GetNodes(String SelectStatement)
        {

            var retList = new List<Node>();
            foreach (var ID in FlatGraph.getNodeList(SelectStatement))
            {
                retList.Add(GetNode(Convert.ToUInt32(ID)));

            }
            return retList;
        }


        //this handles all the edge select, add, remove commands a user would need

        public Edge AddEdges(Node StartNode, Node EndNode, String Label = "")
        {
            var edge = new Edge() { Label = Label };
            edge.StartNode = StartNode;
            edge.EndNode = EndNode;
            StartNode.EdgesOut.Add(edge.ID, edge);
            EndNode.EdgesIn.Add(edge.ID, edge);
            AllEdges.Add(edge.ID, edge);
            FlatGraph.addEdges(edge);

            return edge;
        }
        public void DeleteEdges(UInt32 ID)
        {
            var edge = getEdge(ID);
            edge.EndNode.EdgesIn.Remove(ID);
            edge.StartNode.EdgesOut.Remove(ID);
            FlatGraph.deleteEdges(edge);
            AllEdges.Remove(ID);
        }
        public Edge getEdge(UInt32 ID)
        {
            return AllEdges[ID];
        }
        public List<Edge> getEdges(List<UInt32> IDS)
        {
            var retList = new List<Edge>();
            foreach (var ID in IDS)
            {
                retList.Add(getEdge(ID));

            }
            return retList;
        }
        public List<Edge> getEdges(String SelectStatement)
        {

            var retList = new List<Edge>();
            foreach (var ID in FlatGraph.getEdgeList(SelectStatement))
            {
                retList.Add(getEdge(Convert.ToUInt32(ID)));

            }
            return retList;
        }

        //TODO implement graph traversal DFS, BFS, etc...



    }




}