using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace AR.Core.Communications
{
    /// <summary>
    /// Graph Factory from GraphMLFile
    /// </summary>
    public static class GraphMLGraphFactory
    {

        public static Graph.Graph GetGraph(FileInfo GraphFile)
        {
            Graph.Graph retGraph = new Graph.Graph();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("mixed.species_brain_1.graphml");

                // Get elements
                XmlNodeList Nodes = xmlDoc.GetElementsByTagName("node");
                XmlNodeList Edges = xmlDoc.GetElementsByTagName("edge");


                

                foreach (XmlNode Node in Nodes)
                {
                    var nextNode = retGraph.AddNodes(Node.Attributes["id"].Value, null);
                    
                    foreach (System.Xml.XmlAttribute a in Node.Attributes)
                    {
                        switch (a.Name)
                        {
                            case "id":
                                continue;
                            case "Weight":
                                nextNode.Weight = Convert.ToInt32(a.Value);
                                continue;
                            default:
                                nextNode.Properties.Add(a.Name, a.Value);
                                continue;
                        }
                    }

                }


                foreach (XmlNode Edge in Edges)
                {
                    Graph.Node sNode = null, tNode = null;

                    //oops edge with no node created.
                    if (Edge.Attributes["source"] == null)
                        sNode = retGraph.AddNodes(Edge.Attributes["id"].Value, null);
                    else
                        sNode = retGraph.GetNode(Edge.Attributes["source"].Value);

                    //oops edge with no node created.
                    if (Edge.Attributes["target"] == null)
                        tNode = retGraph.AddNodes(Edge.Attributes["id"].Value, null);
                    else
                        tNode = retGraph.GetNode(Edge.Attributes["target"].Value);

                    var nextEdge = retGraph.AddEdges(sNode, tNode);
             
                    foreach (System.Xml.XmlAttribute a in Edge.Attributes)
                    {
                        switch (a.Name)
                        {
                            case "id":
                                nextEdge.Label = a.Value;
                                continue;
                            case "Weight":
                                nextEdge.Weight = Convert.ToInt32(a.Value);
                                continue;
                            default:
                                nextEdge.Properties.Add(a.Name, a.Value);
                                continue; 
                        }
                    }

                }




            }
            catch (Exception exp)
            {
                return null;

            }
            return retGraph;
        }
    }
}
