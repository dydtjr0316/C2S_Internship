using System;
using System.Collections;
using System.Collections.Generic;

public struct Vector2
{
    public float x, y;

    public Vector2(float x = 0, float y = 0)
    {
        this.x = x;
        this.y = y;
    }

    public float Magnitude
    {
        get { return (float)Math.Sqrt(x * x + y * y); }
    }

    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x + b.x, a.y + b.y);
    }

    public static Vector2 operator -(Vector2 a, Vector2 b)
    {
        return a + (-b);
    }

    public static Vector2 operator -(Vector2 a)
    {
        return new Vector2(-a.x, -a.y);
    }

    public static Vector2 Min(Vector2 a, Vector2 b)
    {
        return new Vector2(Math.Min(a.x, b.x), Math.Min(a.y, b.y));
    }

    public static Vector2 Max(Vector2 a, Vector2 b)
    {
        return new Vector2(Math.Max(a.x, b.x), Math.Max(a.y, b.y));
    }

    public override string ToString()
    {
        return "( " + x + ", " + y + " )";
    }
}

public class BoundingBox
    {
        private Vector2 Min;
        private Vector2 Max;
        private float SurfaceArea;
        private float Volume;

        public float GetVolume()
        {
            return Volume;
        }
        public BoundingBox(Vector2 min, Vector2 max)
        {
            this.Min = min;
            this.Max = max;
            Vector2 diff = Max - Min;
            Volume = diff.x * diff.y;
            SurfaceArea = (diff.x * diff.y);
        }

        public void Set(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
            Vector2 diff = Max - Min;
            Volume = diff.x * diff.y;
            SurfaceArea =  (diff.x * diff.y);
        }

        public static BoundingBox operator +(BoundingBox a, BoundingBox b)
        {
            var mmin = Vector2.Min(a.Min, b.Min);
            var mmax = Vector2.Max(a.Max, b.Max);
            var rst = new BoundingBox(mmin, mmax);
            return rst;
        }

        public bool Intersect(BoundingBox another)
        {
            return !(
                this.Min.x > another.Max.x ||
                this.Max.x < another.Min.x ||
                this.Min.y > another.Max.y ||
                this.Max.y < another.Min.y);
        }

        public override string ToString()
        {
            return "Min: " + Min.ToString() + "\nMax: " + Max.ToString();
        }
    }

    // public class Node<T>
    // {
    //     public T Value;
    //     public Node<T> Parent;
    //     public Node<T> LeftChild;
    //     public Node<T> RightChild;
    //
    //     public Node(T v)
    //     {
    //         Value = v;
    //     }
    //
    //     public Node(Node<T> n)
    //     {
    //         Value = n.Value;
    //         Parent = n.Parent;
    //         LeftChild = n.LeftChild;
    //         RightChild = n.RightChild;
    //     }
    //
    //     public void SetHierarchies(Node<T> P, Node<T> L, Node<T> R)
    //     {
    //         Parent = P;
    //         LeftChild = L;
    //         RightChild = R;
    //     }
    //
    //     public override string ToString()
    //     {
    //         return Value.ToString();
    //     }
    // }

    public class Node //: Node<BoundingBox>
    {
        private BoundingBox Value;
        private Node Parent;
        private Node LeftChild;
        private Node RightChild;

        public Node(in BoundingBox v)
        {
            Value = v;
        }

        public void SetBoundingBox(in BoundingBox box)
        {
            Value = box;
        }
        public BoundingBox GetBoundingBox()
        {
            return Value;
        }

        public void SetParentNode(in Node parent)
        {
            Parent = parent;
        }

        public Node GetParentNode()
        {
            return Parent;
        }

        public bool ParentNodeNullCheck()
        {
            return Parent == null;
        }

        public void SetLeftChildNode(Node leftNode)
        {
            LeftChild = leftNode;
        }
        public Node GetLeftChildNode()
        {
            return LeftChild;
        }
        
        public void SetRightChildNode(Node rightNode)
        {
            RightChild = rightNode;
        }
        public Node GetRightChildNode()
        {
            return RightChild;
        }
        public Node(Node n)
        {
            Value = n.Value;
            Parent = n.Parent;
            LeftChild = n.LeftChild;
            RightChild = n.RightChild;
        }

        public void SetHierarchies(Node P, Node L, Node R)
        {
            Parent = P;
            LeftChild = L;
            RightChild = R;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
        public bool isLeaf()
        {
            return LeftChild == null;
        }
        

        public bool Intersect(Node another)
        {
            return this.Value.Intersect(another.Value);
        }
    }

public class Tree
{
    private Node _root;
    private HashSet<Node> _nodes;

    public Tree()
    {
        _nodes = new HashSet<Node>(32);
    }

    public void SetRoot(in Node root)
    {
        _root = root;
    }

    public BoundingBox Merge(BoundingBox box1, BoundingBox box2)
    {
        return box1 + box2;
    }

    public void Insert(Node node)
    {
        // 해당 노드가 이미 트리에 있는지 확인
        if (_nodes.Contains(node))
            return;

        _nodes.Add(node);

        if (_root == null)
        {
            // 첫 노드 무조건 루트로 박고 시작
            _root = node;
        }
        else
        {
            float score = -1;
            Node candidate = null;
            InsertTree(_root, node, score, 0, ref candidate);

            if (candidate != null)
            {
                var newNode = new Node(Merge(node.GetBoundingBox(), candidate.GetBoundingBox()));
                if (candidate.ParentNodeNullCheck()) //??????????
                {
                    _root = newNode;
                }
                else
                {
                    if (candidate == candidate.GetParentNode().GetLeftChildNode())
                        candidate.GetParentNode().SetLeftChildNode(newNode);
                    else
                        candidate.GetParentNode().SetRightChildNode(newNode);
                }
                
                newNode.SetParentNode(candidate.GetParentNode());
                newNode.SetLeftChildNode(candidate);
                newNode.SetRightChildNode(node);
                candidate.SetParentNode(newNode);
                node.SetParentNode(newNode);

                var cur = newNode;
                while (!cur.ParentNodeNullCheck())
                {
                    cur.GetParentNode().SetBoundingBox(Merge(cur.GetParentNode().GetBoundingBox(), cur.GetBoundingBox()));
                    cur = cur.GetParentNode();
                }
            }
        }
    }

    private void InsertTree(Node current, Node target, float bestcost, float inheritedcost, ref Node candidate)
    {
        var newVol = Merge(current.GetBoundingBox(), target.GetBoundingBox());
        var curCost = inheritedcost + newVol.GetVolume();
        inheritedcost += newVol.GetVolume() - current.GetBoundingBox().GetVolume();

        if (curCost <= bestcost || bestcost < 0)
        {
            bestcost = curCost;
            candidate = current;

            if (!current.isLeaf())
            {
                InsertTree((Node)current.GetLeftChildNode(), target, bestcost, inheritedcost, ref candidate);
                InsertTree((Node)current.GetRightChildNode(), target, bestcost, inheritedcost, ref candidate);
            }
        }
    }

    // public void Remove(Node node)
    // {
    //     if (!_nodes.Contains(node)) return;
    //     _nodes.Remove(node);
    //
    //     if (node == _root)
    //         _root = null;
    //     else
    //     {
    //         bool onLeft = node.Parent.LeftChild == node;
    //         Node brother;
    //         if (onLeft)
    //             brother = (Node)node.Parent.RightChild;
    //         else
    //             brother = (Node)node.Parent.LeftChild;
    //
    //         if (node.Parent == _root)
    //         {
    //             _root = brother;
    //             brother.Parent = null;
    //         }
    //         else
    //         {
    //             bool on_rootLeft = brother.Parent.Parent.LeftChild == brother.Parent;
    //             if (on_rootLeft)
    //                 brother.Parent.Parent.LeftChild = brother;
    //             else
    //                 brother.Parent.Parent.RightChild = brother;
    //             brother.Parent = brother.Parent.Parent;
    //         }
    //
    //         var cur = brother;
    //         while (cur.Parent != null)
    //         {
    //             cur.Parent.Value = cur.Parent.LeftChild.Value + cur.Parent.RightChild.Value;
    //             cur = (Node)cur.Parent;
    //         }
    //     }
    // }
    //
    // public Node[] Intersect(BoundingBox volume)
    // {
    //     var list = new List<Node>();
    //     if (_root != null)
    //     {
    //         var queue = new Queue<Node>();
    //         queue.Enqueue(_root);
    //         while (queue.Count > 0)
    //         {
    //             var node = queue.Dequeue();
    //             bool isLeaf = node.LeftChild == null;
    //             bool intersect = node.Value.Intersect(volume);
    //             if (intersect && isLeaf)
    //             {
    //                 list.Add(node);
    //             }
    //             else if (intersect)
    //             {
    //                 queue.Enqueue((Node)node.LeftChild);
    //                 queue.Enqueue((Node)node.RightChild);
    //             }
    //         }
    //     }
    //
    //     return list.ToArray();
    // }

    public void Print()
    {
        if (_root == null)
        {
            Console.WriteLine("Empty Tree/n");
            return;
        }

        Queue<Node> q = new Queue<Node>();
        q.Enqueue(_root);
        while (q.Count > 0)
        {
            var vol = q.Dequeue();
            if (vol == null) continue;
            string msg = "[ ";
            if (vol.GetParentNode() != null)
            {
                msg += vol.GetParentNode().GetHashCode() + " - ";
            }

            msg += vol.GetHashCode() + " ] IsLeaf: " + (vol.GetLeftChildNode() == null) + "\n" + vol.ToString();
            q.Enqueue((Node)vol.GetLeftChildNode());
            q.Enqueue((Node)vol.GetRightChildNode());
            Console.WriteLine(msg);
        }
    }
}