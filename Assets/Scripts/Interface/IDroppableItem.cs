using UnityEngine;

public interface IDroppableItem
{

    string Name { get; }

    GameObject prefab { get; }
    void OnDrop(GameObject target);
}
