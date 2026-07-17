using UnityEngine;

public interface IDroppable
{
    void OnTriggerEnter2D(Collider2D collision);
}
