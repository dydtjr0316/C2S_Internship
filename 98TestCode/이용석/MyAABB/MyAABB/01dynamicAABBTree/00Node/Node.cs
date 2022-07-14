using MyAABB._02Object;
using MyAABB._99Etc;

namespace MyAABB._01dynamicAABBTree._00Node;
using  MyAABB._01dynamicAABBTree._01BoundingBox;

public class Node
{
    #region 변수

    private BoundingBox _BoundingBox;
    private NODE_TYPE _nodeType;
    private Node _parentNode;
    private Node _leftChildNode;
    private Node _rightChildNode;
    private int _playerId;
    private bool _isAlloc;
    #endregion

    #region c/d

    public Node()
    {
        _BoundingBox = new BoundingBox();
        _nodeType = NODE_TYPE.IDLE;
        _parentNode = null;
        _leftChildNode = null;
        _rightChildNode = null;
        _playerId = -1;
        _isAlloc = false;
    }
    
    public Node(in NODE_TYPE type)
    {
        _BoundingBox = new BoundingBox();
        _nodeType = type;
        _parentNode = null;
        _leftChildNode = null;
        _rightChildNode = null;
        _playerId = -1;
        _isAlloc = true;
    }

    public void SetParent(in Node parentNode)
    {
        _parentNode = parentNode;
    }
    public void SetLeft(in Node leftChildNode)
    {
        _leftChildNode = leftChildNode;
    }
    public void SetRight(in Node rightChildNode)
    {
        _rightChildNode = rightChildNode;
    }
    public void SetNodeType(in NODE_TYPE type)
    {
        _nodeType = type;
    }
    public void SetPlayer(in int playerID)
    {
        _playerId = playerID;
    }
    public void SetParent(in bool isAlloc)
    {
        _isAlloc = isAlloc;
    }

    #endregion
}