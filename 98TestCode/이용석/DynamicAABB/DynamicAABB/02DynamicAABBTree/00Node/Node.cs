using DynamicAABB._01Player;

namespace DynamicAABB._02DynamicAABBTree._00Node;

public class Node
{
    #region 멤버 변수

    private Player playerData;
    private Node parentNode;
    private Node[] childNodes = new Node[2]{null,null};

    private bool isLeaf = true;
    private int depth = 0; // 필요한가?
    private bool isDivided = false;

    #endregion

    #region 생성/소멸

    public Node()
    {
        
    }

    public Node(ref Player player)
    {
        this.playerData = player;
        parentNode = null;
        childNodes = null;
    }

    #endregion

    #region Func
    
    

    #endregion
    
}