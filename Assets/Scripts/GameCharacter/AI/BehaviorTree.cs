using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace SoftwareModeling.GameCharacter.AI
{
    class behaviorTree
    {

        private XmlDocument xmlDoc = new XmlDocument();
        private XmlNodeList nodeList;
        private XmlNodeList edgeList;
        private Dictionary<string, AINode> idTable = new Dictionary<string, AINode>();
        private Dictionary<string, AIComposite> comTable = new Dictionary<string, AIComposite>();
        private Composite rootAiNode;
        private List<String> idList = new List<string>();

        public behaviorTree(string path)
        {
            try
            {
                xmlDoc.Load(path);
                makeTree();
            }
            catch (Exception e)
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
            catch (Exception e)
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

            for (int i = 0; i < idList.Count; i++)
            {
                if (idTable[idList[i]].getName().Equals("sequencer"))
                {
                    for (int j = 0; j < idTable[idList[i]].getChildList().Count; j++)
                    {
                        comTable[idList[i]].getNode().addChild(comTable[idList[i]].getChild(j).getNode());
                    }
                }
                else if (idTable[idList[i]].getName().Equals("selector"))
                {
                    for (int j = 0; j < idTable[idList[i]].getChildList().Count; j++)
                    {
                        rootAiNode = comTable[idList[i]].getNode();
                        rootAiNode.addChild(idTable[idList[i]].getChild(j).getNode());
                    }
                }
            }
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

            if (nodeName.Equals("move"))
            {
                nodeType = new MoveTo(0);
            }
            else if (nodeName.Equals("find enemy"))
            {
                nodeType = new FindNearestEnemy();
            }
            else if (nodeName.Contains("do attack"))
            {
                nodeType = new UseSkillTo(0);
            }
            else if (nodeName.Contains("do defense"))
            {
                nodeType = new UseSkillTo(1);
            }
            else if (nodeName.Contains("do skill"))
            {
                nodeType = new UseSkillTo(2);
            }
            else if(nodeName.Contains("is low health"))
            {
                nodeType = new AbstractAINode();
            }
            else if(nodeName.Contains("find low health"))
            {
                nodeType = new AbstractAINode();
            }
            else
            {
                nodeType = new AbstractAINode();
            }
        }

        public AINode()
        {
            nodeType = new AbstractAINode();
        }

        public virtual void setNode(string nodeName)
        {
            if (nodeName.Equals("move"))
            {
                nodeType = new MoveTo(0);
            }
            else if (nodeName.Equals("find enemy"))
            {
                nodeType = new FindNearestEnemy();
            }
            else if (nodeName.Contains("do attack"))
            {
                nodeType = new UseSkillTo(0);
            }
            else if (nodeName.Contains("do defense"))
            {
                nodeType = new UseSkillTo(1);
            }
            else if (nodeName.Contains("do skill"))
            {
                nodeType = new UseSkillTo(2);
            }
            else if (nodeName.Contains("is low health"))
            {
                nodeType = new AbstractAINode();
            }
            else if (nodeName.Contains("find low health"))
            {
                nodeType = new AbstractAINode();
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

        /*
                public void printInfo()
                {
                    Console.WriteLine("name : " + name);
                    if(nodeType != null)
                    {
                        Console.WriteLine("it is !" + nodeType.type);
                    }
                    else
                    {
                        Console.WriteLine("null!" + name);
                    }

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
        */
    }

    class AIComposite : AINode
    {
        private new Composite nodeType;

        public AIComposite(string nodeName)
        {
            name = nodeName;
            if (nodeName.Equals("sequencer"))
            {
                nodeType = new Sequencer();
            }
            else if (nodeName.Equals("selector"))
            {
                nodeType = new Selector();
            }
        }

        public override void setNode(string nodeName)
        {
            if (nodeName.Equals("sequencer"))
            {
                nodeType = new Sequencer();
            }
            else if (nodeName.Equals("selector"))
            {
                nodeType = new Selector();
            }

        }

        public new Composite getNode()
        {
            return nodeType;
        }


    }
}
