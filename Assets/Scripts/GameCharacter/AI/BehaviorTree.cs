using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

namespace SoftwareModeling.GameCharacter.AI
{
    class BehaviorTree
    {

        private XmlDocument xmlDoc = new XmlDocument();
        private XmlNodeList nodeList;
        private XmlNodeList edgeList;
        private Dictionary<string, AINode> idTable = new Dictionary<string, AINode>();
        private Dictionary<string, AIComposite> comTable = new Dictionary<string, AIComposite>();
        //private Dictionary<string, AbstractAINode> idTable;
        //private Dictionary<string, string> idToName;
        private AbstractAINode rootAiNode;
        private List<string> idList = new List<string>();

        public BehaviorTree(string xml)
        {
            xmlDoc.LoadXml(xml);
            makeTree();
        }

        private void makeTree()
        {
            nodeList = xmlDoc.GetElementsByTagName("node");
            edgeList = xmlDoc.GetElementsByTagName("edge");

            /*AbstractAINode btNode;
            string name;
            string id;

            idTable = new Dictionary<string, AbstractAINode>();
            idToName = new Dictionary<string, string>();
            foreach( XmlElement node in nodeList)
            {
                id = node.Attributes.GetNamedItem("xmi:id").Value;
                name = node.Attributes.GetNamedItem("name").Value;
                idToName.Add(id, name);

                if (name != "root")
                {
                    btNode = nodeFactory(name);
                    idTable.Add(id, btNode);
                }
            }

            string src, dst;
            foreach( XmlElement edge in edgeList)
            {
                src = edge.Attributes.GetNamedItem("source").Value;
                dst = edge.Attributes.GetNamedItem("target").Value;

                if (idToName[src] == "root")
                {
                    rootAiNode = idTable[dst];
                }
                else
                {
                    (idTable[src] as Composite).addChild(idTable[dst]);
                }
            }
            */
            
            for (int i = 0; i < nodeList.Count; i++)
            {
                string nodeId;
                string nodeName;
                nodeId = nodeList.Item(i).Attributes.GetNamedItem("xmi:id").Value;
                nodeName = nodeList.Item(i).Attributes.GetNamedItem("name").Value;
                if (nodeName.Equals("root"))
                {
                    AIComposite tmpComposite = new AIComposite(nodeName);
                    
                    comTable.Add(nodeId, tmpComposite);
                    idTable.Add(nodeId, tmpComposite);
                }
                else if (nodeName.Equals("sequencer") || (nodeName.Equals("selector")))
                {
                    AIComposite tmpComposite = new AIComposite(nodeName);
                    comTable.Add(nodeId, tmpComposite);
                    idTable.Add(nodeId, tmpComposite);
                }
                else
                {
                    AINode tmpNode = new AINode(nodeName);
                    tmpNode.setNode(nodeName);
                    idTable.Add(nodeId, tmpNode);
                }
                idList.Add(nodeId);
            }

            for (int i = 0; i < edgeList.Count; i++)
            {
                string sourceId;
                string targetId;
                int chiledNum;
                AINode sourceNode;
                AINode targetNdoe;

                chiledNum = int.Parse(edgeList.Item(i).Attributes.GetNamedItem("name").Value.ElementAt(0).ToString()) - 1;
                sourceId = edgeList.Item(i).Attributes.GetNamedItem("source").Value;
                targetId = edgeList.Item(i).Attributes.GetNamedItem("target").Value;
                sourceNode = idTable[sourceId];
                targetNdoe = idTable[targetId];

                sourceNode.addChild(chiledNum, ref targetNdoe);
                targetNdoe.setParent(ref sourceNode);
            }

            Composite composite;
            string rootId = null;
            for (int i = 0; i < idList.Count; i++)
            {
                if (idTable[idList[i]].getName().Equals("sequencer"))
                {
                    composite = comTable[idList[i]].getNode() as Composite;
                    for (int j = 0; j < idTable[idList[i]].getChildList().Count; j++)
                    {
                        composite.addChild(comTable[idList[i]].getChild(j).getNode());
                    }
                }
                else if (idTable[idList[i]].getName().Equals("selector"))
                {
                    composite = comTable[idList[i]].getNode() as Composite;
                    for (int j = 0; j < idTable[idList[i]].getChildList().Count; j++)
                    {
                        composite.addChild(idTable[idList[i]].getChild(j).getNode());
                    }
                }
                else if(idTable[idList[i]].getName().Equals("root"))
                {
                    rootId = idList[i];
                }
            }
            rootAiNode = idTable[rootId].getChild(0).getNode();
        }

        public AbstractAINode getRoot()
        {
            return rootAiNode;
        }
    }

    class AINode
    {
        protected string name;
        protected AINode parentNode;
        protected Dictionary<int, AINode> childList = new Dictionary<int, AINode>();
        protected int childCount = 0;
        protected AbstractAINode nodeType;

        public AINode(string nodeName)
        {
            name = nodeName;

            setNode(name);
        }

        public AINode()
        {

        }

        public virtual void setNode(string nodeName)
        {
            int distance = 0;
            if(nodeName.Contains("#"))
            {
                try
                {
                    distance = Int32.Parse(nodeName.Split("#")[1]);
                }
                catch (FormatException e)
                {

                }
                nodeName = nodeName.Split("#")[0];
            }
           
            switch ( nodeName )
            {
                case "move":
                    nodeType = new MoveTo(distance);
                    break;
                case "find enemy":
                    nodeType = new FindNearestEnemy();
                    break;
                case "do attack":
                    nodeType = new UseSkillTo(0);
                    break;
                case "do defense":
                    nodeType = new UseSkillTo(1);
                    break;
                case "do skill":
                    nodeType = new UseSkillTo(2);
                    break;
                case "is low health":
                    nodeType = new isHealthLow();
                    break;
                case "find low health":
                    nodeType = new FindLowHealthAlly();
                    break;
                case "sequencer":
                    nodeType = new Sequencer();
                    break;
                case "selector":
                    nodeType = new Selector();
                    break;
                case "root":
                    nodeType = new Selector();
                    break;
                default:
                    Debug.LogError( "No such node : " + nodeName);
                    break;
            }
        }

        public virtual AbstractAINode getNode()
        {
            return nodeType;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string inputName)
        {
            name = inputName;
        }

        public void setParent(ref AINode inputNode)
        {
            parentNode = inputNode;
        }

        public AINode getParent()
        {
            return parentNode;
        }

        public void addChild(ref AINode inputNode)
        {
            if (childList.ContainsKey(childCount))
            {
                int i = 0;
                while (true)
                {
                    if (childList.ContainsKey(i))
                    {
                        i++;
                    }
                    else
                    {
                        childList.Add(i, inputNode);
                        childCount += 1;
                        return;
                    }
                }
            }
            else
            {
                childList.Add(childCount, inputNode);
            }


            childCount += 1;
        }

        public void addChild(int index, ref AINode inputNode)
        {
            childList.Add(index, inputNode);
            childCount += 1;
        }

        public Dictionary<int, AINode> getChildList()
        {
            return childList;
        }

        public AINode getChild(int index)
        {
            return childList[index];
        }
    }

    class AIComposite : AINode
    {
        public AIComposite(string nodeName) : base(nodeName)
        {
        }
    }
}
