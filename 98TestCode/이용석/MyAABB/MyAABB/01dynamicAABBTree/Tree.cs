using MyAABB._01dynamicAABBTree._00Node;

namespace MyAABB._01dynamicAABBTree;

public class Tree
{
    private Node _rootNode;
    private int _capacity;
    private int _depth;

    public Tree(in Node rootNode, in int capacity, in int depth)
    {
        _rootNode = rootNode;
        _capacity = capacity;
        _depth = depth;
    }
}