using UnityEngine;
using System.Collections.Generic;
public class GhostPink : MonoBehaviour
{
    public float speed = 0.7f;

    public Node currentNode;

    private Node targetNode;
    private Node previousNode;

    void Start()
    {
        // Colocamos el fantasma exactamente en el nodo inicial
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

        // ¿Llegó al nodo?
        if (Vector2.Distance(transform.position, targetNode.transform.position) < 0.05f)
        {
            previousNode = currentNode;
            currentNode = targetNode;

            ChooseNextNode();
        }
    }

    void ChooseNextNode()
    {
        List<Node> neighbours = currentNode.GetNeighbours();

        if (neighbours.Count == 0)
            return;

        // Filtramos para NO volver al nodo anterior
        List<Node> validNodes = new List<Node>();

        foreach (Node node in neighbours)
        {
            if (node != previousNode)
                validNodes.Add(node);
        }

        // Si solo había un camino (callejón sin salida)
        if (validNodes.Count == 0)
        {
            validNodes.Add(previousNode);
        }

        targetNode = validNodes[Random.Range(0, validNodes.Count)];
    }

}
