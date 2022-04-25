using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksController : MonoBehaviour
{
    [SerializeField]
    private GlobalChunkData[] _chunks;


}

[System.Serializable]
public struct GlobalChunkData
{
    public Chunk Chunk;
    public int CoordX;
    public int CoordY;
}


[System.Serializable]
public enum SeekableType
{
    None = 0,
    TestSeekable = 1,
}
