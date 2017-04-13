﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AR.Core.Types;
using UnityEngine.Networking;
using System.Collections;
using System.ComponentModel;
using UnityEngine;



namespace AR.Core.Communications
{
    public class Neo4jConnector
    {
        private Logging.DBLogger myLogs;
        private String PostURL= AR.Core.Types.SystemSetup.Neo4j_Authless ?
            "http://" + SystemSetup.Neo4j_Server + ":7474/db/data/cypher" :
            "http://" + SystemSetup.Neo4j_Username + ":" + SystemSetup.Neo4j_Password + "@" + SystemSetup.Neo4j_Server + ":7474/db/data/cypher";

        private static Neo4jConnector instance;
        private Neo4jConnector()
        {
            myLogs = Logging.DBLogger.getInstance();

            myLogs.LogMessage(LoggingLevels.Error, "Init Neo4jConnection: " + PostURL,
Module: "Neo4jConnector.Neo4jConnector", Version: "ALPHA");
        }

        public static Neo4jConnector getInstance()
        {
            if (instance == null)
                instance = new Neo4jConnector();



            return instance;
        }

        public void GetGraphFromQuery(Graph.Graph retGraph, String Query)
        {
           
            try
            {
                var neo4jGraph = CypherQueryReturnGraph(Query);

                foreach (var dataarray in neo4jGraph.data)
                {
                    var x = dataarray[0]; //convert for 


                    String StartNodePath = x.start;
                    String EndNodePath = x.end;

                    var SNodeSplit = StartNodePath.Split('/');
                    var ENodeSplit = EndNodePath.Split('/');

                    Graph.Node StartNode = null;
                    Graph.Node EndNode = null;
                    if (retGraph.GetNode(SNodeSplit[SNodeSplit.Length - 1]) == null)
                    {
                        StartNode = retGraph.AddNodes(SNodeSplit[SNodeSplit.Length - 1], null);
                        StartNode.Neo4jPath = StartNodePath;
                    }
                    else
                        StartNode = retGraph.GetNode(SNodeSplit[SNodeSplit.Length - 1]);


                    if (retGraph.GetNode(ENodeSplit[ENodeSplit.Length - 1]) == null)
                    {
                        EndNode = retGraph.AddNodes(ENodeSplit[ENodeSplit.Length - 1], null);
                        EndNode.Neo4jPath = EndNodePath;
                    }
                    else
                        EndNode = retGraph.GetNode(ENodeSplit[ENodeSplit.Length - 1]);


                    var nextEdge = retGraph.AddEdges(StartNode, EndNode);

                    //TODO only ripping first element off array
                    if (x.relationships[0].Length > 0)
                        nextEdge.Neo4jPath = x.relationships[0];

                }

            }
            catch (Exception exp)
            {
                myLogs.LogMessage(LoggingLevels.Error, "Init Neo4jConnector Exception: " + exp.Message,
    Module: "Neo4jConnector.GetGraphFromQuery", Version: "ALPHA");
                return;

            }
            return;
        }

        public String CypherQuery(String Query)
        {

            String results = "";
            try
            {
                Dictionary<String, String> myLoad = new Dictionary<string, string>();
                myLoad.Add("query", Query);

                var www = Neo4jConnector.Post(PostURL, myLoad);

                while (!www.isDone)
                {
                    //intentionally blank
                }


                results = www.text;
            }
            catch (Exception exp)
            {
                UnityEngine.Debug.LogError(exp.Message);
                results= "";
            }


            return results;

        }

        public Neo4jGraph_SimpleJson CypherQueryReturnGraph(String Query)
        {
            return Neo4jConnector.SimpleJsonToGraph(CypherQuery(Query));
        }

        public static WWW Get(string url)
        {

            WWW www = new WWW(url);

            WaitForSeconds w;
            while (!www.isDone)
                w = new WaitForSeconds(0.1f);

            return www;
        }

        public static WWW Post(string url, Dictionary<string, string> post)
        {
            WWWForm form = new WWWForm();
            foreach (var pair in post)
                form.AddField(pair.Key, pair.Value);

            WWW www = new WWW(url, form);

            WaitForSeconds w;
            while (!www.isDone)
                w = new WaitForSeconds(0.1f);

            return www;
        }

        /*public static Neo4jGraph NewtonSoftJsonToGraph(String json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Neo4jGraph>(json);

        }*/

        public static Neo4jGraph_SimpleJson SimpleJsonToGraph(String json)
        {
            return AR.Core.IO.SimpleJson.DeserializeObject<Neo4jGraph_SimpleJson>(json);
        }



    }
}
//MATCH p=()-[r:ORDERS]->() RETURN p LIMIT 100