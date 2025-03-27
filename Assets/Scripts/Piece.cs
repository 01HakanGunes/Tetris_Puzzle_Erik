using UnityEngine;

public class Piece : MonoBehaviour
{
    public float fallSpeed = 2f;
    public float cellSize = 1f;
    private Vector2Int gridPosition;

    void Start()
    {
        Vector3 pos = transform.position;
        gridPosition = new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
    }

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Move(-1);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            Move(1);
    }

    void Move(int dir)
    {
        int newX = gridPosition.x + dir;

        if (newX >= 0 && newX < 4)
        {
            gridPosition.x = newX;
            Vector3 newPos = new Vector3(gridPosition.x * cellSize, transform.position.y, 0);
            transform.position = newPos;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Floor"))
    {
        fallSpeed = 0;
    }
}
}
