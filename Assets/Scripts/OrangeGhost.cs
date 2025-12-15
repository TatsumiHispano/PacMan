using UnityEngine;
using System.Collections.Generic;

public class OrangeGhost : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 0.7f;          // Más lento que Pac-Man
    public Node currentNode;

    private Node targetNode;
    private Node previousNode;

    void Start()
    {
        // Colocar el fantasma exactamente en el nodo inicial
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

        // Cuando llega al nodo
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

        // Quitamos el nodo anterior para no ir hacia atrás
        if (previousNode != null)
        {
            neighbours.Remove(previousNode);
        }

        if (neighbours.Count == 0)
        {
            // Si solo había un camino, vuelve atrás
            targetNode = previousNode;
            return;
        }

        // Elegir nodo aleatorio
        targetNode = neighbours[Random.Range(0, neighbours.Count)];
    }
}
