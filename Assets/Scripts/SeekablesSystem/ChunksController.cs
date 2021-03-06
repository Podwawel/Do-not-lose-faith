using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksController : MonoBehaviour
{
    [SerializeField]
    private Chunk[] _chunks;

    private List<GlobalChunkData> _globalChunkDatas = new List<GlobalChunkData>();

    [SerializeField]
    private Coordinates _borderCoordinates;

    public Coordinates BorderCoordinates { get { return _borderCoordinates; } }



    public void Initialize()
    {
        for(int i = 0; i < _chunks.Length; i++)
        {
            _chunks[i].Initialize();
        }
        FillChunkData();
    }

    private void FillChunkData()
    {
        for(int i = 0; i < _chunks.Length; i++)
        {
            GlobalChunkData chunkData;
            chunkData = _chunks[i].MyChunkData;
            _globalChunkDatas.Add(chunkData);
        }
    }

    public Chunk GetChunkByCoordinates(Coordinates coordinates)
    {
        if (coordinates.X < 0 || coordinates.Y < 0)
        {
            Debug.LogError("CUSTOM ERROR: coords below 0." + " X: " + coordinates.X + " Y: " + coordinates.Y);
            return null;
        }
        if (coordinates.X > _borderCoordinates.X || coordinates.Y > _borderCoordinates.Y)
        {
            Debug.LogError("CUSTOM ERROR: given coordinates are beyond border." + " X: " + coordinates.X + " Y: " + coordinates.Y);
            return null;
        }
        for(int i = 0; i < _globalChunkDatas.Count; i++)
        {
            if (coordinates.X == _globalChunkDatas[i].Coordinates.X && coordinates.Y == _globalChunkDatas[i].Coordinates.Y)
                return _globalChunkDatas[i].Chunk;
        }

        Debug.LogError("CUSTOM ERROR: Invalid coordinates - didnt find chunk with given coordinates." + " X: " + coordinates.X + " Y: " + coordinates.Y);
        return null;
    }
}

[System.Serializable]
public struct GlobalChunkData
{
    public Chunk Chunk;
    public Coordinates Coordinates;
}

[System.Serializable]
public struct Coordinates
{
    public int X;
    public int Y;
}

[System.Serializable]
public struct SeekableData
{
    public BaseSeekable Seekable;
    public SeekableType MyType;
}

[System.Serializable]
public struct NearestSeekableData
{
    public float Distance;
    public BaseSeekable Seekable;
}

[System.Serializable]
public enum SeekableType
{
    None = 0,
    TestSeekable1 = 1,
    TestSeekable2 = 2,
    TestSeekable3 = 3,
    UltimateSeekable = 4,
}
