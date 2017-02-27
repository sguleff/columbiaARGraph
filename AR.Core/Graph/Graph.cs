using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace AR.Core.Graph
{

    public class Graph : MonoBehaviour, IUnityVisualProperties
    {
        public Dictionary<String, Node> AllNodes { get; set; }
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
            AllNodes = new Dictionary<String, Node>();
            AllEdges = new Dictionary<UInt32, Edge>();
            FlatGraph = new GraphSqlite();
        }


        //this handles all the node select, add, remove commands a user would need
     
        public Node AddNodes(String Label = "", UnityVisualProperties opVisProp = null)
        {
            var go = Visuals.UnityHelperFunctions.CreateGameObject(Types.GraphProperties.Node, 
                PrimitiveType.Sphere, Visuals.Colors.Blue);

            var node = new Node(go, Label);

            AllNodes.Add(node.UserID, node);
            FlatGraph.addNode(node);
            return node;
        }
        public void DeleteNode(String UserID)
        {
            var node = GetNode(UserID);

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
            AllNodes.Remove(UserID);
        }
        public Node GetNode(String UserID)
        {
            return AllNodes[UserID];
        }
        public List<Node> GetNodes(List<String> UserIDS)
        {
            var retList = new List<Node>();
            foreach (var ID in UserIDS)
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
                retList.Add(GetNode(ID.ToString()));

            }
            return retList;
        }


        //this handles all the edge select, add, remove commands a user would need

        public Edge AddEdges(Node StartNode, Node EndNode, String Label = "")
        {
            GameObject go = Visuals.UnityHelperFunctions.CreateGameObject(Types.GraphProperties.Edge,
                PrimitiveType.Cube, Visuals.Colors.Green);

            var edge = new Edge(go) { Label = Label };
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

        //Move Nodes Around
        public void RandomMoveAllNodes()
        {
            System.Random r = new System.Random((int)DateTime.Now.Ticks);

            foreach (KeyValuePair<String,Node> Nodes in AllNodes)
            {
                Nodes.Value.MoveTo(r.Next(-10,10), r.Next(-10,10), r.Next(0,10));
            }

        }



        //TODO implement graph traversal DFS, BFS, etc...





    }




}
