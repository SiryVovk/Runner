using UnityEngine;

[System.Serializable]
public struct SpawnRowStruct
{
    public Transform Lane1;
    public Transform Lane2;
    public Transform Lane3;

    public Transform[] GetAll()
    {
        return new Transform[]
        {
            Lane1,
            Lane2,
            Lane3
        };
    }
}
