using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace BehaviorTree
{
    class behaviorTree
    {
        private XmlDocument xmlDoc = new XmlDocument();
        private XmlNodeList nodeList;
        private XmlNodeList edgeList;
        private AINode root;
        private Dictionary<string, AINode> idTable = new Dictionary<string, AINode>();

        public behaviorTree(string path)
        {
            try
            {
                xmlDoc.Load(path);
                makeTree();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public behaviorTree()
        {

        }

        public void setXmlPath(string path)
        {
            try
            {
                xmlDoc.Load(path);
                makeTree();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
           
        }

        private void makeTree()
        {
            nodeList = xmlDoc.GetElementsByTagName("node");
            edgeList = xmlDoc.GetElementsByTagName("edge");
            for (int i = 0; i < nodeList.Count; i++)
            {
                string nodeId;
                string nodeName;
                nodeId = nodeList.Item(i).Attributes.GetNamedItem("xmi:id").Value;
                nodeName = nodeList.Item(i).Attributes.GetNamedItem("name").Value;
                AINode tmpNode = new AINode(nodeName);
                idTable.Add(nodeId, tmpNode);
                if (nodeName.Equals("root"))
                {
                    root = tmpNode;
                }
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
        }

        public AINode returnRoot()
        {
            return root;
        }
    }

    class AINode
    {
        private string name;
        private AINode parentNode;
        private Dictionary<int, AINode> childList = new Dictionary<int, AINode>();
        private int childCount = 0;

        public AINode(string inputName)
        {
            name = inputName;
        }

        public AINode()
        {

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
            if(childList.ContainsKey(childCount))
            {
                int i = 0;
                while(true)
                {
                    if(childList.ContainsKey(i))
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
            return (AINode)childList[index];
        }
        

        public void printInfo()
        {
            Console.WriteLine("name : " + name);
            if(parentNode != null)
            {
                Console.WriteLine("parent : " + parentNode.getName());
            }
            else
            {
                Console.WriteLine("parent : no parent");
            }

            if(childList.Count != 0)
            {
                for (int i = 0; i < childList.Count; i++)
                {
                    Console.WriteLine("child " + i + " :" + childList[i].getName());
                }
            }
            else
            {
                Console.WriteLine("child : no child");
            }
            
        }

    }
}
