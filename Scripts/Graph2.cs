using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextAsset __nodePositions;

    [SerializeField] GameObject _nodePrefab;

    [SerializeField] GameObject _edgePrefab;

    [SerializeField] int radius = 30;

    private List<NodeProperties> _nodeProps;
  
    private List<Edge> _edges;

    private List<Node> _nodes;


    // Update is called once per frame
    void Start()
    {
        CreateNodes(100, 100, 2);
        _edges = new List<Edge>();

        StartCoroutine(DrawNodesIE());
        
   
    }

    IEnumerator DrawNodesIE()
    {
        DrawNodes();
        yield return null;
        StartCoroutine(DrawTwoEdges());
    }

    IEnumerator DrawTwoEdges()
    {
        CreateEdges();
        yield return null;
        Create2ndEdges();
    }

   
    void CreateNodesFromDataFile()
    {
        _nodes = new List<Node>();
        _nodeProps = new List<NodeProperties>();
        var nodeLookup = new Dictionary<string, Node>();

        var sr = new System.IO.StringReader(__nodePositions.text);

        while (true)
        {
            var line = sr.ReadLine();
            if (line == null)
                break;
            if (line.Trim().Length > 0)
            {
                var tokens = line.Split(new string[] { "\t", " " }, StringSplitOptions.None);
                var no = tokens[0];
                int x = Convert.ToInt32(tokens[1]);
                int y = Convert.ToInt32(tokens[2]);
                int z = 10;

                _nodeProps.Add(new NodeProperties(no, x, y, z));
            }
        }
    }

    void CreateNodes(int x, int y, int step)
    {
        _nodes = new List<Node>();
        _nodeProps = new List<NodeProperties>();
        int counter = 0;
        for(int i = 0; i < x; i ++)
        {
            for(int j = 0; j < y; j += step)
            {
                _nodeProps.Add(new NodeProperties(("" + counter), i, j,radius));
                counter++;
            }
        }
    }

    void DrawNodes()
    {
        for (int i = 0; i < _nodeProps.Count; i++)
        {
            NodeProperties node = _nodeProps[i];
            Vector3 position = new Vector3(node.X, node.Y, node.Z) * radius;
            Vector3 sphericalCoordinates = GetSphericalCoordinates(position);

            _nodeProps[i].X = sphericalCoordinates.x;
            _nodeProps[i].Y = sphericalCoordinates.y;
            _nodeProps[i].Z = sphericalCoordinates.z;
            
            GameObject nodeObj = Instantiate(_nodePrefab, sphericalCoordinates, Quaternion.identity, transform);
            nodeObj.name = node.No;
            nodeObj.tag = "Node";

            Node n = nodeObj.GetComponent<Node>();
            n.pos = new Vector3(sphericalCoordinates.x, sphericalCoordinates.y, sphericalCoordinates.z);
            n.num = node.No;
                
            _nodes.Add(n);
        }
    }

    void Create2ndEdges()
    {
        foreach (Node node in _nodes)
        {
            if(node.tag == "EdgedNode")
            {
                GameObject nearestNodeGameObj = node.GetSecondNearest("EdgedNode");
                if (nearestNodeGameObj != null)
                {
                    Node nearestNode = nearestNodeGameObj.GetComponent<Node>();
                    node.tag = "EdgedNodeTwice";
                    // nearestNode.tag = "EdgedNodeTwice";
                    CreateEdge(node, nearestNode);
                }
            }           
        }
    }

    void CreateEdges()
    {
       
        foreach (Node node in _nodes)
        {
            if (node.tag == "Node")
            {
                GameObject nearestNodeGameObj = node.GetNearestObject("Node");
                if (nearestNodeGameObj != null)
                {
                    Node nearestNode = nearestNodeGameObj.GetComponent<Node>();
                    node.tag = "EdgedNode";
                    // nearestNode.tag = "EdgedNode";
                    CreateEdge(node, nearestNode);
                }
            }
        }
    }

    void CreateEdge(Node n1, Node n2)
    {
        GameObject edge2 = Instantiate(_edgePrefab, transform);
        var spring = edge2.GetComponent<Edge>();
        spring.Body1 = n1;
        spring.Body2 = n2;

        _edges.Add(spring);
        
        var render = edge2.GetComponent<EdgeRenderer>();
        render.Body1 = n1.transform;
        render.Body2 = n2.transform;
    }

    Vector3 GetSphericalCoordinates(Vector3 position)
    {
        float theta = Vector3.Angle(position, Vector3.forward);
        float phi = Vector3.Angle(position, new Vector3(1,1,0));
        
        float x = radius * Mathf.Cos(theta) * Mathf.Sin(phi);
        float y = radius * Mathf.Sin(theta) * Mathf.Sin(phi);
        float z = radius * Mathf.Cos(phi);

        return new Vector3(x, y, z);
    }

    public void DeleteNode(string no)
    {
        for(int i = 0; i < _nodes.Count; i++)
        {
            if (_nodes[i].num == no) _nodes.RemoveAt(i);
        }
    }

    

    
}
