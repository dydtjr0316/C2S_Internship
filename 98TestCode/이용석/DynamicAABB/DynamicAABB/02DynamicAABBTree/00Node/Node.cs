#define TEST_VER_0712
// Constainer Test Ver Switch
// #undef TEST_VER_0712

using DynamicAABB._01Player;

namespace DynamicAABB._02DynamicAABBTree._00Node;


public class Node
{
    #region 멤버 변수

    private Player _playerData;
    private Node _parentNode;
    private Node[] _childNodes = new Node[2]{null,null};

    //private bool isLeaf = true; //필요한가?
    private Int32 _depth = 0; // 필요한가?
    private bool _isDivided = false;
    
    #if TEST_VER_0712
    // idx 접근이므로 음수 제외
    private int _parentIdx;
    private int _nextIdx;
    private int _leftIdx;
    private int _rightIdx;
    
    
    private int _height;
    #endif

    #endregion

    #region 생성/소멸

    public Node()
    {
        
    }

    public Node(ref Player player)
    {
        this._parentNode = null;
        this._playerData = player;
        _parentNode = null;
        _childNodes[0] = null;
        _childNodes[1] = null;
    }

    #endregion

    #region Getter/Setter

    public Player GetPlayerData()
    {
        return _playerData;
    }

    public void SetPlayerData(in Player playerData)
    {
        _playerData = playerData;
    }

    public Node GetParentNode()
    {
        return _parentNode;
    }
    public void SetParentNode(in Node parentNode)
    {
        _parentNode = parentNode;
    }

    public Node[] GetChildNodes()
    {
        return _childNodes;
    }

    public void SetNextIdx(int nextIdx)
    {
        _nextIdx = nextIdx;
    }

    public int GetNextIdx()
    {
        return _nextIdx;
    }

    public void SetParentIdx(int parentIdx)
    {
        _parentIdx = parentIdx;
    }

    public void SetLeftIdx(int leftIdx)
    {
        _leftIdx = leftIdx;
    }

    public void SetRightIdx(int rightIdx)
    {
        _rightIdx = rightIdx;
    }

    public ErrorCode GetChildNode(ref Node tempNode/*param 네이밍 다시 하기*/, in int idx)
    {
        if (_childNodes.Length < idx)
        {
            return ErrorCode.CHILDNODE_OUT_OF_RANGE;
        }
        tempNode = _childNodes[idx];
        
        return ErrorCode.NONE;
    }

    public void SetChildNodes(Node childNode0, Node childNode1)
    {
        _childNodes[0] = childNode0;
        _childNodes[1] = childNode1;
    }

    public void SetChildNode(Node childeNode)
    {
        if (_childNodes[0] == null)
        {
            _childNodes[0] = childeNode;
        }
        else
        {
            _childNodes[1] = childeNode;
        }
    }

    #endregion

    #region Func
    
    public bool IsLeaf()
    {
        // dynamic aabb tree에서 leaf노드만이 player이고
        // 0번 자식노드만  null이여도 1은 없다고 판단 but 예외처리한다는 마인드로 할까 고민중
        return _childNodes[0] == null;
    }

    public void SetBranch(Node node1, Node node2)
    {
        // this 노드를 branch로 만든다.
        node1._parentNode = this;
        node2._parentNode = this;

        _childNodes[0] = node1;
        _childNodes[1] = node2;
    }

    public void SetLeaf(Player playerData)
    {
        _playerData = playerData;
        _childNodes[0] = null;
        _childNodes[1] = null;
    }

    #endregion
    
    
}

public enum ErrorCode
{
    NONE = 0,
    CHILDNODE_OUT_OF_RANGE,
    
    
    NODE_IDX_INITAILIZE = 1000000000
    
}