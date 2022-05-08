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

    private float _testTimeElapsed = 0f;
    private float _testTimeBeetweenSeeking = 10f;

    public void Initialize(ChunksController chunksController)
    {
        _agent = GetComponent<NavMeshAgent>();
        _chunksController = chunksController;
        _chunksController.Initialize();
        Invoke("TestFind", 3f);
    }

    private void TestFind()
    {
        int p = RandomGenerator.ReturnRandomInt(1, 30);

        if (p < 9) SeekableToLook = SeekableType.TestSeekable1;
        if (p >= 9 && p < 18) SeekableToLook = SeekableType.TestSeekable2;
        if (p >= 18 && p < 27) SeekableToLook = SeekableType.TestSeekable3;
        if (p >= 27) SeekableToLook = SeekableType.UltimateSeekable;

        SetDestinationToSeekable(FindSeekable(SeekableToLook, 3));
    }

    public void CustomUpdate()
    {
        _testTimeElapsed += Time.deltaTime;
        if(_testTimeElapsed > _testTimeBeetweenSeeking)
        {
            TestFind();
            _testTimeBeetweenSeeking = RandomGenerator.ReturnRandomFloat(5f, 100f);
            _testTimeElapsed = 0f;
        }
    }

    public BaseSeekable FindSeekable(SeekableType type, int dimensionNumberOfChunksToSeekIn)
    {
        if (_myCurrentChunk == null) return null;
        if (type == SeekableType.None) return null;

        NearestSeekableData nearestSeekableData;
        nearestSeekableData = _myCurrentChunk.FindTheNearestSeekable(transform.position, type);

        if(nearestSeekableData.Seekable == null)
        {
            if (dimensionNumberOfChunksToSeekIn % 2 == 0)
            {
                Debug.LogError("CUSTOM ERROR: entered even number as dimension.");
                return null;
            }

            int radiusToSeekIn = (dimensionNumberOfChunksToSeekIn - 1) / 2;

            float currentNearestDistance = 100000f;

            NearestSeekableData newNearest = nearestSeekableData;

            int x = _myCurrentChunk.MyChunkData.Coordinates.X;
            int y = _myCurrentChunk.MyChunkData.Coordinates.Y;
            int xBorder = _chunksController.BorderCoordinates.X;
            int yBorder = _chunksController.BorderCoordinates.Y;

            for (int i = x - radiusToSeekIn; i <= x + radiusToSeekIn; i++)
            {
                for (int j = y - radiusToSeekIn; j <= y + radiusToSeekIn; j++)
                {

                  if (i < 0 || j < 0 || i > xBorder || j > yBorder) continue;
                  Coordinates coords;
                  coords.X = i;
                  coords.Y = j;
                    
                  Chunk newChunk = _chunksController.GetChunkByCoordinates(coords);

                  if (newChunk != null)
                  {
                      nearestSeekableData = newChunk.FindTheNearestSeekable(transform.position, type);

                       if (nearestSeekableData.Distance < currentNearestDistance && nearestSeekableData.Seekable != null)
                       {
                           newNearest = nearestSeekableData;
                           currentNearestDistance = newNearest.Distance;
                       }
                  }
                }
            }

            if (newNearest.Seekable != null) return newNearest.Seekable;
            else return FindSeekable(type, dimensionNumberOfChunksToSeekIn + 2);
        }
        return nearestSeekableData.Seekable;
    }

    public void SetDestinationToSeekable(BaseSeekable seekable)
    {
        if (seekable != null)
        {
            _agent.SetDestination(seekable.transform.position);
        }
        else Debug.Log("NULL SEEKABLE");
    }

    private void OnTriggerEnter(Collider other)
    {
        Chunk hitChunk = other.GetComponent<Chunk>();
        if(hitChunk)
        {
            _myCurrentChunk = hitChunk;
        }
    }

    public void Deinitialize()
    {

    }
}
