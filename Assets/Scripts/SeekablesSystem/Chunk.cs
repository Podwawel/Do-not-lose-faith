using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField]
    private List<SeekableData> _seekablesInMyRange;

    [SerializeField] private Coordinates _myCoordinates;

    private GlobalChunkData _myChunkData;
    public GlobalChunkData MyChunkData { get { return _myChunkData; } }
    public void Initialize()
    {
        _myChunkData.Chunk = this;
        _myChunkData.Coordinates = _myCoordinates;
    }

    public NearestSeekableData FindTheNearestSeekable(Vector3 position, SeekableType type)
    {
        float currentNearestDistance = 0;
        BaseSeekable nearestSeekable = null;

        for (int i = 0; i < _seekablesInMyRange.Count; i++)
        {
            if (_seekablesInMyRange[i].MyType == type)

            {
                float distance = Vector3.Distance(position, _seekablesInMyRange[i].Seekable.transform.position);
                if (currentNearestDistance == 0)
                {
                    currentNearestDistance = distance;
                    nearestSeekable = _seekablesInMyRange[i].Seekable;
                }
                else if (distance < currentNearestDistance)
                {
                    currentNearestDistance = distance;
                    nearestSeekable = _seekablesInMyRange[i].Seekable;
                }
            }

            NearestSeekableData data;
            data.Distance = currentNearestDistance;
            data.Seekable = nearestSeekable;

            return data;
        }

        NearestSeekableData nullData;
        nullData.Distance = 0;
        nullData.Seekable = null;
        return nullData;
    }
}
