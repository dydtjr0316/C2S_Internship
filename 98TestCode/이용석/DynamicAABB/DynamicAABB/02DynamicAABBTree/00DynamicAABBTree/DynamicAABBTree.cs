#define TEST_VER_0712

// Constainer Test Ver Switch
// #undef TEST_VER_0712

using System.ComponentModel;
using DynamicAABB._01Player;
using DynamicAABB._02DynamicAABBTree._00Node;
namespace DynamicAABB._02DynamicAABBTree;

static class NodeIdxErr
{
    public const int invalidValue = 100000000;
}
public class DynamicAABBTree
{
    #region 멤버 변수

    private Node _rootNode;

    #if TEST_VER_0712
    private List<Node> _nodes;

    private int _rootNodeIdx;
    private int _allocatedNodeCount;
    private int _nextFreeNodeIdx;
    private int _nodeCapacity;
    private int _growthSize;
    
    #endif

    #endregion


    #region 생성/소멸자
    public DynamicAABBTree(int initialSize)
    {
        // 노드값 초기화 - 절대 접근할 일 없는 값
        _rootNodeIdx = NodeIdxErr.invalidValue;
        
        _allocatedNodeCount = 0;
        _nextFreeNodeIdx = 0;
        _nodeCapacity = initialSize;
        _growthSize = initialSize;

        for (int nodeIdx = 0; nodeIdx < initialSize; nodeIdx++)
        {
            _nodes[nodeIdx].SetNextIdx(nodeIdx + 1);
        }
        
        _nodes[initialSize].SetNextIdx(NodeIdxErr.invalidValue);
    }
    #endregion

    public int AllocateNode()
    {
        /*줜나 이해안감*/
        if (_nextFreeNodeIdx == NodeIdxErr.invalidValue)
        {
            _nodeCapacity += _growthSize;
            
            for (int nodeIdx = _allocatedNodeCount; nodeIdx < _nodeCapacity; nodeIdx++)
            {
                _nodes[nodeIdx].SetNextIdx(nodeIdx + 1);
            }
            _nodes[_nodeCapacity - 1].SetNextIdx(NodeIdxErr.invalidValue);
            _nextFreeNodeIdx = _allocatedNodeCount;
        }
        /*여기 코드*/
        
        _nodes[_nextFreeNodeIdx].SetParentIdx(NodeIdxErr.invalidValue);
        _nodes[_nextFreeNodeIdx].SetLeftIdx(NodeIdxErr.invalidValue);
        _nodes[_nextFreeNodeIdx].SetRightIdx(NodeIdxErr.invalidValue);
        _nextFreeNodeIdx = _nodes[_nextFreeNodeIdx].GetNextIdx();
        
        _allocatedNodeCount++;

        return _nextFreeNodeIdx;
    }

    public void DeallocateNode(int nodeIdx)
    {
        _nodes[nodeIdx].SetNextIdx(_nextFreeNodeIdx);
        _nextFreeNodeIdx = nodeIdx;
        _allocatedNodeCount--;
    }

    public void InsertObject(Player player)
    {
        int nodeIdx = AllocateNode();
        // AABBNode& node = _nodes[nodeIdx];
        //
        // _nodes[nodeIdx].SetPlayerData(player);
        //
        // _nodes[nodeIdx].aabb = object->getAABB();
        // _nodes[nodeIdx].object = object;
        //
        // insertLeaf(nodeIndex);
        // _objectNodeIndexMap[object] = nodeIndex;
    }
    
}