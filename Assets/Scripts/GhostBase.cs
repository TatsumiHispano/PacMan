using UnityEngine;
using System.Collections.Generic;
public class GhostBase : MonoBehaviour
{

   public float speed = 0.7f;

    [Header("Nodes")]
    public Node currentNode;        // Nodo inicial
    public Node tunnelLeftNode;     // Nodo entrada izquierda
    public Node tunnelRightNode;    // Nodo entrada derecha

    private Node targetNode;
    private Node previousNode;

    private Transform pacman;

    void Start()
    {
        pacman = GameObject.FindGameObjectWithTag("Player").transform;

        transform.position = currentNode.transform.position;
        ChooseNextNode();
    }

    void Update()
    {
        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (targetNode == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetNode.transform.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, targetNode.transform.position) < 0.05f)
        {
            previousNode = currentNode;
            currentNode = targetNode;

            CheckTunnelTeleport();   // 👈 TELEPORT AQUÍ

            ChooseNextNode();
        }
    }

    void ChooseNextNode()
    {
        List<Node> neighbours = currentNode.GetNeighbours();

        if (neighbours.Count == 0) return;

        Node bestNode = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Node node in neighbours)
        {
            // ❌ No volver atrás
            if (node == previousNode)
                continue;

            float dist = Vector2.Distance(node.transform.position, pacman.position);

            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                bestNode = node;
            }
        }

        // Callejón sin salida
        if (bestNode == null)
            bestNode = previousNode;

        targetNode = bestNode;
    }

    void CheckTunnelTeleport()
    {
        // Si entra por la izquierda → sale por la derecha
        if (currentNode == tunnelLeftNode)
        {
            currentNode = tunnelRightNode;
            transform.position = tunnelRightNode.transform.position;
        }
        // Si entra por la derecha → sale por la izquierda
        else if (currentNode == tunnelRightNode)
        {
            currentNode = tunnelLeftNode;
            transform.position = tunnelLeftNode.transform.position;
        }
    }
    
}
