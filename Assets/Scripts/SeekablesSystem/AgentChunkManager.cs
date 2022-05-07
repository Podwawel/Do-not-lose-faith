using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentChunkManager : MonoBehaviour
{
    private NavMeshAgent _agent;

    private Chunk _myCurrentChunk;

    public SeekableType SeekableToLook;

    private ChunksController _chunksController;

    public void Initialize(ChunksController chunksController)
    {
        _agent = GetComponent<NavMeshAgent>();
        _chunksController = chunksController;

        Invoke("TestFind", 3f);
    }

    private void TestFind()
    {
        int p = RandomGenerator.ReturnRandomInt(0, 3);

        if (p == 0) SeekableToLook = SeekableType.None;
        if (p == 1) SeekableToLook = SeekableType.TestSeekable1;
        if (p == 2) SeekableToLook = SeekableType.TestSeekable2;
        if (p == 3) SeekableToLook = SeekableType.TestSeekable3;

        SetDestinationToSeekable(FindSeekable(SeekableToLook));
    }

    public void CustomUpdate()
    {

    }

    public BaseSeekable FindSeekable(SeekableType type)
    {
        if (_myCurrentChunk == null) return null;
        if (type == SeekableType.None) return null;

        NearestSeekableData nearestSeekableData;
        nearestSeekableData = _myCurrentChunk.FindTheNearestSeekable(transform.position, type);

        if(nearestSeekableData.Seekable == null)
        {
            float currentNearestDistance = nearestSeekableData.Distance;

            NearestSeekableData newNearest = nearestSeekableData;

            int x = _myCurrentChunk.MyChunkData.Coordinates.X;
            int y = _myCurrentChunk.MyChunkData.Coordinates.Y;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    Coordinates coords;
                    coords.X = i;
                    coords.Y = j;
                    Chunk newChunk = _chunksController.GetChunkByCoordinates(coords);
                    if (newChunk == null) continue;

                    nearestSeekableData = newChunk.FindTheNearestSeekable(transform.position, type);

                    if(nearestSeekableData.Distance < currentNearestDistance)
                    {
                        newNearest = nearestSeekableData;
                    }
                }
            }

            return newNearest.Seekable;
        }
        return nearestSeekableData.Seekable;
    }

    public void SetDestinationToSeekable(BaseSeekable seekable)
    {
        if(seekable != null) _agent.SetDestination(seekable.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Chunk hitChunk = other.GetComponent<Chunk>();
        if(hitChunk)
        {
            _myCurrentChunk = hitChunk;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Chunk hitChunk = other.GetComponent<Chunk>();
        if (hitChunk)
        {
            _myCurrentChunk = hitChunk;
        }
    }

    public void Deinitialize()
    {

    }
}
