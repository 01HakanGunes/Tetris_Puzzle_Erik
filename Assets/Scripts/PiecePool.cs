using System.Collections.Generic;
using UnityEngine;

public class PiecePool : MonoBehaviour
{
    public GameObject piecePrefab;
    public Sprite[] pieceSprites;
    private List<Piece> pooledPieces = new List<Piece>();

    private List<Piece> randomQueue = new List<Piece>();
    public Transform spawnPoint;

    public int gridWidth = 4;
    public int gridHeight = 4;

    void Start()
    {
        InitializePieces();
    }

    private void InitializePieces()
    {
        for (int i = 0; i < pieceSprites.Length; i++)
        {
            GameObject obj = Instantiate(piecePrefab, transform);
            obj.SetActive(false);

            Piece piece = obj.GetComponent<Piece>();
            if (piece.spriteRenderer == null)
            {
                piece.spriteRenderer = obj.GetComponent<SpriteRenderer>();
            }

            piece.Initialize(pieceSprites[i], GetCorrectPosition(i), this);

            pooledPieces.Add(piece);
        }

        // Create new copy of the list
        randomQueue = new List<Piece>(pooledPieces);
        Randomize(randomQueue);

        SpawnNextPiece();
    }

    public void SpawnNextPiece()
    {
        if (randomQueue.Count > 0)
        {
            Piece nextPiece = randomQueue[0];
            randomQueue.RemoveAt(0);

            // Activate
            nextPiece.transform.position = spawnPoint.position;
            nextPiece.gameObject.SetActive(true);
            nextPiece.StartFalling();
        }
        else
        {
            Debug.Log("Puzzle completed!");
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("congratulations");
        enabled = false;
    }

    void Randomize(List<Piece> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Piece temp = list[i];
            int randIndex = Random.Range(i, list.Count);
            list[i] = list[randIndex];
            list[randIndex] = temp;
        }
    }

    public void ReturnToPool(Piece piece)
    {
        piece.gameObject.SetActive(false);
        randomQueue.Add(piece);

        SpawnNextPiece();
    }

    private Vector2Int GetCorrectPosition(int index)
    {
        return new Vector2Int(index % gridWidth, index / gridWidth);
    }
}
