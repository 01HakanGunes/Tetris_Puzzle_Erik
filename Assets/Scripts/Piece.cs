using UnityEngine;

public class Piece : MonoBehaviour
{
    private PiecePool piecePool;
    public float fallSpeed = 2f;
    public float cellSize = 1f;
    public Vector2Int correctPosition;
    public SpriteRenderer spriteRenderer;
    private bool hasLanded = false;

    public void Initialize(Sprite sprite, Vector2Int correctPos, PiecePool pool)
    {
        spriteRenderer.sprite = sprite;
        correctPosition = correctPos;
        piecePool = pool;
        gameObject.SetActive(false);
    }

    public void StartFalling()
    {
        hasLanded = false;
        fallSpeed = 2f;
    }

    void Update()
    {
        if (hasLanded) return;

        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Move(-1);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            Move(1);
    }

    void Move(int dir)
    {
        float newX = transform.position.x + (dir * cellSize);

        if (newX >= 0 && newX < piecePool.gridWidth * cellSize)
        {
            transform.position = new Vector3(newX, transform.position.y, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Floor"))
        {
            fallSpeed = 0;
            hasLanded = true;
            CheckPlacement();
        }
    }

    void CheckPlacement()
    {
        // Convert current position to grid coordinates
        int currentX = Mathf.RoundToInt(transform.position.x / cellSize);
        int currentY = Mathf.RoundToInt(-transform.position.y / cellSize);

        if (currentX == correctPosition.x && currentY == correctPosition.y)
        {
            Debug.Log("Correct placement");
            piecePool.SpawnNextPiece();
        }
        else
        {
            Debug.Log("Wrong position returning to pool");
            piecePool.ReturnToPool(this);
        }
    }
}
