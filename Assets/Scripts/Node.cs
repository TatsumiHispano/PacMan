using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Id (opcional, para organización)")]
    public int id;

    [Header("Vecinos (arrastra aquí los nodos conectados)")]
    public List<Node> neighbours = new List<Node>();

    [Header("Opciones")]
    public bool isTunnel = false; // si este nodo es el túnel lateral
    public Node tunnelPair;       // par del túnel (opcional)

    // Método que pedía tu GhostBase
    public List<Node> GetNeighbours()
    {
        return neighbours;
    }

    // Gizmos para ver los nodos en la escena
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.08f);

        Gizmos.color = Color.cyan;
        if (neighbours != null)
        {
            foreach (var n in neighbours)
            {
                if (n != null)
                    Gizmos.DrawLine(transform.position, n.transform.position);
            }
        }
    }
}
