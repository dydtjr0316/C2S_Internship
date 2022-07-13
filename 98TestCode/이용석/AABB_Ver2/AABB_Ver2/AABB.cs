using System.ComponentModel.Design;
using System.Diagnostics;
using System.Numerics;
namespace AABB_Ver2;
using System;

/*
 * 전체적으로 vector resize를 해주는 이유가 뭔지 모르겠네,,,// 용석
 * 
 */

public class AABB
{
    private List<float> _lowerBound;
    private List<float> _upperBound;
    private List<float> _center;

    private float _sufaceArea;

    #region Getter/Setter

    public List<float> GetLowerBound()
    {
        return _lowerBound;
    }

    public float GetLowerBoundByIdx(int idx)
    {
        return _lowerBound[idx];
    }
    public void SetLowerBound(int idx, float value)
    {
        _lowerBound[idx] = value;
    }

    public List<float> GetUppderBound()
    {
        return _upperBound;
    }
    public float GetUppderBoundByIdx(int idx)
    {
        return _upperBound[idx];
    }
    public void SetUpperBound(int idx, float value)
    {
        _upperBound[idx] = value;
    }

    public void SetSurfaceArea(float surFaceArea)
    {
        _sufaceArea = surFaceArea;
    }

    public float GetCenterByIdx(int idx)
    {
        // idx 접근 할거면 예외 처리 필요할듯 모든 byidx 함수에
        return _center[idx];
    }

    public List<float> GetCenter()
    {
        return _center;
    }
    public void SetCenter(List<float> center)
    {
        _center = center;
    }

    #endregion
    public  static int NULL_NODE = 0xffffff;

    public AABB()
    {
        
    }
    
    public AABB(int dimension)
    {
        
    }

    public AABB(in List<float> lowerBound, in List<float> upperBound)
    {
        _lowerBound = lowerBound;
        _upperBound = upperBound;

        if (_lowerBound.Count != _upperBound.Count)
        {
            // err 처리 할 것
        }

        for (int i = 0; i < lowerBound.Count; i++)
        {
            if (lowerBound[i] > upperBound[i])
            {
                // err 처리
            }
        }

        _sufaceArea = ComputeSurfaceArea();
        // _center = computeCenter();
    }

    public float ComputeSurfaceArea()
    {
        float sum = 0.0f;
        
        for ( int d1 = 0; d1 < _lowerBound.Count; d1++)
        {
            // "Area" of current side.
            float product = 1.0f;

            for ( int d2 = 0; d2 < _lowerBound.Count; d2++)
            {
                if (d1 == d2)
                    continue;

                float dx = _upperBound[d2] - _lowerBound[d2];
                product *= dx;
            }

            // Update the sum.
            sum += product;
        }

        return 2.0f * sum;
    }

    public void Merge(in AABB aabb1, in AABB aabb2)
    {
        for (int i = 0; i < _lowerBound.Count; i++)
        {
            _lowerBound[i] = Math.Min(aabb1._lowerBound[i], aabb2._lowerBound[i]);
            _upperBound[i] = Math.Max(aabb1._upperBound[i], aabb2._upperBound[i]);
        }
        _sufaceArea = ComputeSurfaceArea();
        // _center = computeCenter();
    }

    public bool Contains(in AABB aabb)
    {
        for (int i = 0; i < _lowerBound.Count; i++)
        {
            if (aabb._lowerBound[i] < _lowerBound[i]) return false;
            if (aabb._upperBound[i] < _upperBound[i]) return false;
        }

        return true;
    }

    public bool Overlaps(in AABB aabb, bool touchIsOverlap)
    {
        bool rv = true;
        if (touchIsOverlap)
        {
            for (int i = 0; i < _lowerBound.Count; ++i)
            {
                if (aabb._upperBound[i] < _lowerBound[i] || aabb._lowerBound[i] > _upperBound[i])
                {
                    rv = false;
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < _lowerBound.Count; ++i)
            {
                if (aabb._upperBound[i] <= _lowerBound[i] || aabb._lowerBound[i] >= _upperBound[i])
                {
                    rv = false;
                    break;
                }
            }
        }

        return rv;
    }

    public List<float> ComputeCenter()
    {
        List<float> pos = new List<float>(_lowerBound.Count);

        for (int i = 0; i < pos.Count; i++)
        {
            pos[i] = 0.5f * (_lowerBound[i] + _upperBound[i]);
        }
        
        return pos;
    }

    #region Getter/Setter

    public float GetSurfaceArea()
    {
        return _sufaceArea;
    }

    #endregion
    
}

public class Node
{
    #region 변수

    private AABB _aabb;
    private int _parentIdx;

    private int _nextIdx;
    private int _leftIdx;
    private int _rightIdx;
    private int _height;
    private int _particleIdx;

    #endregion

    public Node()
    {
        
    }

    #region Getter/Setter

    public void SetNextIdx(in int nextIdx)
    {
        _nextIdx = nextIdx;
    }

    public int GetNextIdx()
    {
        return _nextIdx;
    }

    public int GetHeight()
    {
        return _height;
    }

    public void SetHeight(in int height)
    {
        _height = height;
    }

    public void SetHeightZero()
    {
        _height = 0;
    }

    public int GetParentIdx()
    {
        return _parentIdx;
    }
    public void SetParentIdx(in int parentIdx)
    {
        _parentIdx = parentIdx;
    }

    public int GetLeftIdx()
    {
        return _leftIdx;
        
    }
    public void SetLeftIdx(in int leftIdx)
    {
        _leftIdx = leftIdx;
    }

    public int GetRightIdx()
    {
        return _rightIdx;
    }
    public void SetRightIdx(in int rightIdx)
    {
        _rightIdx = rightIdx;
    }

    public AABB GetAABB()
    {
        return _aabb;
    }

    public void SetAABB(AABB aabb)
    {
        _aabb = aabb;
    }

    public int GetParticleIdx()
    {
        return _particleIdx;
    }
    public void SetParticleIdx(int particleIdx)
    {
        _particleIdx = particleIdx;
    }

    #endregion
    
    

    public bool IsLeaf()
    {
        return _leftIdx == AABB.NULL_NODE;
    }

}

public class Tree
{
    #region  변수
    private int _root;
    // tree
    private List<Node> _nodes = new List<Node>();
    
    // 트리의 현재 노드 갯수
    private int _nodeCount;
    
    // 현재 트리의 노드 용량
    private int _nodeCapacity;

    // system상의 차원 - 현재는 2차원만 사용하지 않을까 싶음
    private int _dimension;
    
    // 공부가 필요함 // 용석
    private int _freeList;
    
    // fat aabb 박스의 두께
    private float _skinThickness;
    
    // 공부가 필요함 // 용석
    private bool _isPeriodic;
    
    // 공부가 필요함 // 용석
    private List<bool> _periodicity;
    
    // 공부가 필요함 // 용석
    private List<float> _boxSize;
    
    // 공부가 필요함 // 용석
    private List<float> _negMinImage;
    
    // 공부가 필요함 // 용석
    private List<float> _posMinImage;
    
    // 객체 인덱스와 노드 인덱스 저장 정보같은데 확실히 모르겠음 // 용석
    private Dictionary<int, int> _particleMap;
    
    // 중복 계산을 방지하기 위함인 것 같은데 다시 봐야함 // 용석
    private bool _touchIsOverlap;
    #endregion

    #region Getter/Setter

    public int GetnParticles()
    {
        return _particleMap.Count;
    }

    #endregion

    #region Func

    public Tree(int dimension, float skinThickness, int nParticles, bool touchIsOverlap)
    {
        _dimension = dimension;
        _isPeriodic = false;
        _skinThickness = skinThickness;
        _touchIsOverlap = touchIsOverlap;
        
        if (_dimension < 2)
        {
            // err처리
            // 2차원 이하로 나올 수 없음
        }

        for (int i = 0; i < _dimension; ++i)
        {
            // 배열초기화 방법 찾아보고 수정 // 용석
            _periodicity.Add(false);
        }

        // 이부분 메서드화 해도 될듯? 자주사용되는데 // 용석
        _root = AABB.NULL_NODE;
        _nodeCount = 0;
        _nodeCapacity = nParticles;
        for (int i = 0; i < _nodeCapacity - 1; ++i)
        {
            _nodes[i].SetNextIdx(i + 1);
            _nodes[i].SetHeight(-1);
        }
        _nodes[_nodeCapacity-1].SetNextIdx(AABB.NULL_NODE);
        _nodes[_nodeCapacity-1].SetHeight(-1);

        _freeList = 0;
    }

    public Tree(int dimension, float skinThickness, ref List<bool> periodicity, ref List<float> boxSize, int nParticles,
        bool touchIsOverlap = true)
    {
        _dimension = dimension;
        _periodicity = periodicity;
        _skinThickness = skinThickness;
        _boxSize = boxSize;
        _touchIsOverlap = touchIsOverlap;
        if (dimension < 2)
        {
            PrintError(ErrCode.DIMENSION_LOWER_2, GetErrorData());

        }

        if ((_periodicity.Count != _dimension) || _boxSize.Count != _dimension)
        {
            PrintError(ErrCode.DIMENSION_MISSMATCH, GetErrorData());

        }
        _root = AABB.NULL_NODE;
        _nodeCount = 0;
        _nodeCapacity = nParticles;
        
        // 두가지 방법 중 어떤 방법이 더 효율적인지 판단
        _nodes.Resize(_nodeCapacity, new Node());
        _nodes.Reserve(_nodeCapacity+10);

        //_nodes.Capacity = _nodeCapacity;

        for (int i = 0; i < _nodeCapacity - 1; ++i)
        {
            _nodes[i].SetNextIdx(i + 1);
            _nodes[i].SetHeight(-1);
        }
        _nodes[_nodeCapacity-1].SetNextIdx(AABB.NULL_NODE);
        _nodes[_nodeCapacity-1].SetHeight(-1);

        _freeList = 0;

        _isPeriodic = false;
        for (int i = 0; i < _dimension; ++i)
        {
            _posMinImage[i] = 0.5f * _boxSize[i];
            _negMinImage[i] = -0.5f * _boxSize[i];

            if (_periodicity[i])
            {
                _isPeriodic = true;
            }
        }
    }

    public int AllocateNode()
    {
        if (_freeList == AABB.NULL_NODE)
        {
            _nodeCapacity *= 2;

            for (int i = _nodeCount; i < _nodeCapacity - 1; ++i)
            {
                _nodes[i].SetNextIdx(i+1);
                _nodes[i].SetHeight(-1);
            }
            _nodes[_nodeCapacity-1].SetNextIdx(AABB.NULL_NODE);
            _nodes[_nodeCapacity-1].SetHeight(-1);
            _freeList = 0;
        }

        // 실수했다 값바뀌는 경우엔 무조건 새로운 메모리 사용해야함
        // 이전에 이런코드 있었던것 같은데 찾아보기
        int node = _freeList;
        _freeList = _nodes[node].GetNextIdx();
        _nodes[node].SetParentIdx(AABB.NULL_NODE);
        _nodes[node].SetLeftIdx(AABB.NULL_NODE);
        _nodes[node].SetRightIdx(AABB.NULL_NODE);
        _nodes[node].SetHeight(0);
        //_nodes[node].GetAABB().SetDimension;
        // 주석 코드는 원래 있었으나 resize를 제거하며 삭제함
        // 혹시 resize의 필요성이 생긴다면 다시 작성
        _nodeCount++;

        return node;
    }

    public void FreeNode(int node)
    {
        _nodes[node].SetNextIdx(_freeList);
        _nodes[node].SetHeight(-1);
        _freeList = node;
        _nodeCount--;
    }

    public void InsertParticle(int particle, ref List<float> position, float radius)
    {
        if (_particleMap.ContainsKey(particle))
        {
            PrintError(ErrCode.PARTICLE_ALREADY_EXIST, GetErrorData());

        }

        if (position.Count != _dimension)
        {
            PrintError(ErrCode.DIMENSION_MISSMATCH, GetErrorData());

        }

        int node = AllocateNode();

        List<float> size = new List<float>();
        for (int i = 0; i < _dimension; ++i)
        {
            _nodes[node].GetAABB().SetLowerBound(i, position[i] - radius);
            _nodes[node].GetAABB().SetUpperBound(i, position[i] + radius);
            size.Add(_nodes[node].GetAABB().GetUppderBound()[i] - _nodes[node].GetAABB().GetLowerBound()[i]);
        }

        for (int i = 0; i < _dimension; ++i)
        {
            // 함수화 하기
            _nodes[node].GetAABB().SetLowerBound(i,_nodes[node].GetAABB().GetLowerBoundByIdx(i) - _skinThickness*size[i]);
            _nodes[node].GetAABB().SetUpperBound(i,_nodes[node].GetAABB().GetLowerBoundByIdx(i) + _skinThickness*size[i]);
        }

        _nodes[node].GetAABB().SetSurfaceArea(_nodes[node].GetAABB().ComputeSurfaceArea());
        _nodes[node].GetAABB().SetCenter(_nodes[node].GetAABB().ComputeCenter());
        
        _nodes[node].SetHeightZero();
        
        // 추가 구현 해야하는 부분임 // 구현 후 주석 해제
        //InsertLeaf(node)

        // 파티클 맵에 id와 저장된 노드 저장
        _particleMap.Add(particle, node);

        // particle(player) id 삽입
        _nodes[node].SetParticleIdx(particle);

    }
    
    // 0713 12:52 insertParticle 오버로딩 함수 부터 구현 시작
    public void InsertParticle(int particle, ref List<float> lowerBound, ref List<float> upperBound)
    {
        if (_particleMap.ContainsKey(particle))
        {
            //err 
            // 이미 map에 해당 id가 존재 하는 경우
            // 현재는 particle 이지만 player로 수정될 부분임
            PrintError(ErrCode.PARTICLE_ALREADY_EXIST, GetErrorData());
        }

        if (lowerBound.Count != _dimension || upperBound.Count != _dimension)
        {
            PrintError(ErrCode.DIMENSION_MISSMATCH, GetErrorData());
        }

        int node = AllocateNode();
        
        List<float> size = new List<float>();
        
        for (int i = 0; i < _dimension; ++i)
        {
            // 함수화 하기
            _nodes[node].GetAABB().SetLowerBound(i,_nodes[node].GetAABB().GetLowerBoundByIdx(i) - _skinThickness*size[i]);
            _nodes[node].GetAABB().SetUpperBound(i,_nodes[node].GetAABB().GetLowerBoundByIdx(i) + _skinThickness*size[i]);
        }

        _nodes[node].GetAABB().SetSurfaceArea(_nodes[node].GetAABB().ComputeSurfaceArea());
        _nodes[node].GetAABB().SetCenter(_nodes[node].GetAABB().ComputeCenter());
        
        _nodes[node].SetHeightZero();
        
        // 추가 구현 해야하는 부분임
        //InsertLeaf(node)

        // 파티클 맵에 id와 저장된 노드 저장
        _particleMap.Add(particle, node);

        // particle(player) id 삽입
        _nodes[node].SetParticleIdx(particle);
    }

    public void RemoveParticle(int particleIdx)
    {
        int node = 0;
        if(!_particleMap.TryGetValue(particleIdx,out node))
        {
            PrintError(ErrCode.NOT_EXISTKEY_MAP, GetErrorData());
        }

        _particleMap.Remove(particleIdx);
        
        // 메서드 구현 후 주석 해제
        //RemoveLeaf(node);
        //FreeNode(node);
    }

    public void RemoveAllParticle()
    {
        foreach (var obj in _particleMap)
        {
            int node = obj.Value;
            // 메서드 구현 후 주석 해제
            //RemoveLeaf(node);
            //FreeNode(node);
        }
        _particleMap.Clear();
    }

    public bool UpdateParticle(int particleIdx, ref List<float> lowerBound, List<float> upperBound, bool alwaysReinsert)
    {
        if (lowerBound.Count != _dimension || upperBound.Count != _dimension)
        {
            PrintError(ErrCode.DIMENSION_MISSMATCH, GetErrorData());
        }

        int node;
        if(!_particleMap.TryGetValue(particleIdx,out node))
        {
            PrintError(ErrCode.NOT_EXISTKEY_MAP, GetErrorData());
        }

        List<float> size = new List<float>();
        for (int i = 0; i < _dimension; ++i)
        {
            if (lowerBound[i] > upperBound[i])
            {
                PrintError(ErrCode.LOWERBOUND_GREATER, GetErrorData());
            }
            size.Add(upperBound[i] - lowerBound[i]);
        }

        AABB aabb = new AABB(lowerBound, upperBound);

        if (!alwaysReinsert && _nodes[node].GetAABB().Contains(aabb))
        {
            return false;
        }
        // 메서드 구현 후 주석 제거
        // removeLeaf(node);

        for (int i = 0; i < _dimension; ++i)
        {
            aabb.SetLowerBound(i,_nodes[node].GetAABB().GetLowerBoundByIdx(i) - _skinThickness*size[i]);
            aabb.SetUpperBound(i,_nodes[node].GetAABB().GetLowerBoundByIdx(i) + _skinThickness*size[i]);
        }

        _nodes[node].SetAABB(aabb);
        _nodes[node].GetAABB().SetSurfaceArea(_nodes[node].GetAABB().ComputeSurfaceArea());
        _nodes[node].GetAABB().SetCenter(_nodes[node].GetAABB().ComputeCenter());
        
        //InsertLeaf(node);

        return true;
    }

    public List<int> Query(int particle)
    {
        int node;
        if (!_particleMap.TryGetValue(particle, out node))
        {
            PrintError(ErrCode.NOT_EXISTKEY_MAP, GetErrorData());
        }

        return Query(particle, _nodes[node].GetAABB());
    }

    public List<int> Query(int particle, in AABB aabb)
    {
        // 현재 코드 매우 더러움 정리 필요
        // for if문 최적화 할 수 있는 만큼 해보기 // 용석
        
        List<int> stack = new List<int>();
        //List<int> stack = new List<int>(10); 이런식으로 초기화 사이즈 세팅 가능함
        stack.Add(_root);
        
        List<int> particles = new List<int>();

        while (stack.Count>0)
        {
            int node = stack[stack.Count - 1];
            stack.RemoveAt(stack.Count - 1);

            AABB nodeAABB = _nodes[node].GetAABB();
            if (node == AABB.NULL_NODE)
            {
                continue;
            }

            if (_isPeriodic)
            {
                List<float> separation = new List<float>(_dimension);
                List<float> shift = new List<float>(_dimension);

                for (int i = 0; i < _dimension; ++i)
                {
                    separation.Add(nodeAABB.GetCenterByIdx(i) - aabb.GetCenterByIdx(i));
                }

                //매개변수 ref 로 사용해야하는 부분 전부 수정 들어가야함 // 용석
                if (MinImage(ref separation, ref shift))
                {
                    for (int i = 0; i < _dimension; ++i)
                    {
                        nodeAABB.SetLowerBound(i, nodeAABB.GetLowerBoundByIdx(i) + shift[i]);
                        nodeAABB.SetUpperBound(i, nodeAABB.GetUppderBoundByIdx(i) + shift[i]);
                    }
                }
            }

            if (aabb.Overlaps(nodeAABB, _touchIsOverlap))
            {
                if (_nodes[node].IsLeaf())
                {
                    if (_nodes[node].GetParticleIdx() != particle)
                    {
                        particles.Add(_nodes[node].GetParticleIdx());
                    }
                }
                else
                {
                    stack.Add(_nodes[node].GetLeftIdx());
                    stack.Add(_nodes[node].GetRightIdx());
                }
            }
        }
        return particles;
    }

    public bool MinImage(ref List<float> separation, ref List<float> shift)
    {
        bool isShifted = false;
        for (int i = 0; i < _dimension; ++i)
        {
            if (separation[i] < _negMinImage[i])
            {
                // bool과 float을 곱하는 이짓거리는 왜하는 걸까...?
                // 공부해야함 // 용석
                separation[i] += Convert.ToInt32(_periodicity[i])  * _boxSize[i];
                shift[i] = Convert.ToInt32(_periodicity[i]) * _boxSize[i];
                isShifted = true;
            }
            else
            {
                if (separation[i] >= _posMinImage[i])
                {
                    separation[i] -= Convert.ToInt32(_periodicity[i])  * _boxSize[i];
                    shift[i] = -Convert.ToInt32(_periodicity[i]) * _boxSize[i];
                    isShifted = true;
                }
            }
        }
        return isShifted;
    }

    public List<int> Query(in AABB aabb)
    {
        if (_particleMap.Count == 0)
        {
            return new List<int>();
        }
        // 이렇게 null_node 값을 넣어줄거면 뭐하러 만들어 놓은 함수인가????
        return Query(AABB.NULL_NODE, aabb/*std::numeric_limits<unsigned int>::max(), aabb*/);
    }

    public AABB getAABBByParticleIdx(int particleIdx)
    {
        return _nodes[_particleMap[particleIdx]].GetAABB();
    }

    public void InsertLeaf(int leaf)
    {
        if (_root == AABB.NULL_NODE)
        {
            _root = leaf;
            _nodes[_root].SetParentIdx(AABB.NULL_NODE);
            return;
        }

        AABB leafAABB = _nodes[leaf].GetAABB();

        // root 부터 
        var idx = _root;

        while (!_nodes[idx].IsLeaf())
        {
            var left = _nodes[idx].GetLeftIdx();
            var right = _nodes[idx].GetRightIdx();

            var surfaceArea = _nodes[idx].GetAABB().GetSurfaceArea();

            AABB combinedAABB = new AABB();
            combinedAABB.Merge(_nodes[idx].GetAABB(), leafAABB);
            var combinedSurfaceArea = combinedAABB.GetSurfaceArea();
            
            var cost = 2.0f * combinedSurfaceArea;

            var inheritanceCost = 2.0f * (combinedSurfaceArea - surfaceArea);

            float costLeft;
            if (_nodes[leaf].IsLeaf())
            {
                AABB aabb = new AABB();
                aabb.Merge(leafAABB, _nodes[left].GetAABB());
                costLeft = aabb.GetSurfaceArea() + inheritanceCost;
            }
            else
            {
                AABB aabb = new AABB();
                aabb.Merge(leafAABB, _nodes[left].GetAABB());
                var oldArea = _nodes[left].GetAABB().GetSurfaceArea();
                var newArea = aabb.GetSurfaceArea();
                costLeft = newArea - oldArea + inheritanceCost;
            }
            
            float costRight;
            if (_nodes[leaf].IsLeaf())
            {
                AABB aabb = new AABB();
                aabb.Merge(leafAABB, _nodes[right].GetAABB());
                costRight = aabb.GetSurfaceArea() + inheritanceCost;
            }
            else
            {
                AABB aabb = new AABB();
                aabb.Merge(leafAABB, _nodes[right].GetAABB());
                var oldArea = _nodes[right].GetAABB().GetSurfaceArea();
                var newArea = aabb.GetSurfaceArea();
                costRight = newArea - oldArea + inheritanceCost;
            }

            if (cost < costLeft && cost < costRight)
            {
                break;
            }

            if (costLeft < costRight)
            {
                idx = left;
            }
            else
            {
                idx = right;
            }
        }

        var sibling = idx;
        var oldParent = _nodes[sibling].GetParticleIdx();
        var newParent = AllocateNode();

        _nodes[newParent].SetParentIdx(oldParent);
        _nodes[newParent].GetAABB().Merge(leafAABB, _nodes[sibling].GetAABB());
        _nodes[newParent].SetHeight(_nodes[sibling].GetHeight() + 1);

        if (oldParent != AABB.NULL_NODE)
        {
            if (_nodes[oldParent].GetLeftIdx() == sibling)
            {
                _nodes[oldParent].SetLeftIdx(newParent);
            }
            else
            {
                _nodes[oldParent].SetRightIdx(newParent);
            }
            
            _nodes[newParent].SetLeftIdx(sibling);
            _nodes[newParent].SetRightIdx(leaf);
            _nodes[sibling].SetParentIdx(newParent);
            _nodes[leaf].SetParentIdx(newParent);
        }
        else
        {
            _nodes[newParent].SetLeftIdx(sibling);
            _nodes[newParent].SetRightIdx(leaf);
            _nodes[sibling].SetParentIdx(newParent);
            _nodes[leaf].SetParentIdx(newParent);
            _root = newParent;
        }

        idx = _nodes[leaf].GetParentIdx();

        while (idx != AABB.NULL_NODE)
        {
            idx = Balance(idx);
            var left = _nodes[idx].GetLeftIdx();
            var right = _nodes[idx].GetRightIdx();

            _nodes[idx].SetHeight(1+Math.Max(_nodes[left].GetHeight(), _nodes[right].GetHeight()));
            _nodes[idx].GetAABB().Merge(_nodes[left].GetAABB(), _nodes[right].GetAABB());

            idx = _nodes[idx].GetParentIdx();
        }
    }

    public void RemoveLeaf(int leaf)
    {
        if (leaf == _root)
        {
            _root = AABB.NULL_NODE;
            return;
        }


        var parent = _nodes[leaf].GetParentIdx();
        var grandParent = _nodes[parent].GetParentIdx();
        int sibling;

        if (_nodes[parent].GetLeftIdx() == leaf)
        {
            sibling = _nodes[parent].GetRightIdx();
        }
        else
        {
            sibling = _nodes[parent].GetLeftIdx();
        }

        if (grandParent != AABB.NULL_NODE)
        {
            if (_nodes[grandParent].GetLeftIdx() == parent)
            {
                _nodes[grandParent].SetLeftIdx(sibling);
            }
            else
            {
                _nodes[grandParent].SetRightIdx(sibling);
            }

            _nodes[sibling].SetParentIdx(grandParent);
            FreeNode(parent);
            var idx = grandParent;

            while (idx != AABB.NULL_NODE)
            {
                idx = Balance(idx);

                var left = _nodes[idx].GetLeftIdx();
                var right = _nodes[idx].GetRightIdx();

                _nodes[idx].GetAABB().Merge(_nodes[left].GetAABB(), _nodes[right].GetAABB());
                _nodes[idx].SetHeight(1 + Math.Max(_nodes[left].GetHeight(), _nodes[right].GetHeight()));

                idx = _nodes[idx].GetParentIdx();

            }
        }
        else
        {
            _root = sibling;
            _nodes[sibling].SetParentIdx(AABB.NULL_NODE);
            FreeNode(parent);
        }
    }

    private int Balance(int node)
    {
        if (_nodes[node].IsLeaf() || _nodes[node].GetHeight() < 2)
        {
            return node;
        }

        var left = _nodes[node].GetLeftIdx();
        var right = _nodes[node].GetRightIdx();

        var currentBalance = _nodes[right].GetHeight() - _nodes[left].GetHeight();

        // rotate logic
        if (currentBalance > 1)
        {
            var rightLeft = _nodes[right].GetLeftIdx();
            var rightRight = _nodes[right].GetRightIdx();
            
            _nodes[right].SetLeftIdx(node);
            _nodes[right].SetParentIdx(_nodes[node].GetParentIdx());
            _nodes[node].SetParentIdx(right);

            if (_nodes[right].GetParentIdx() != AABB.NULL_NODE)
            {
                if (_nodes[_nodes[right].GetParentIdx()].GetLeftIdx() == node)
                {
                    _nodes[_nodes[right].GetParentIdx()].SetLeftIdx(right);
                }
                else
                {
                    _nodes[_nodes[right].GetParentIdx()].SetRightIdx(right);
                }
            }
            else
            {
                _root = right;
            }

            if (_nodes[rightLeft].GetHeight() > _nodes[rightRight].GetHeight())
            {
                _nodes[right].SetRightIdx(rightLeft);
                _nodes[node].SetRightIdx(rightRight);
                _nodes[rightRight].SetParentIdx(node);
                _nodes[node].GetAABB().Merge(_nodes[left].GetAABB(), _nodes[rightRight].GetAABB());
                _nodes[right].GetAABB().Merge(_nodes[node].GetAABB(), _nodes[rightLeft].GetAABB());
                
                _nodes[node].SetHeight(1+ Math.Max(_nodes[left].GetHeight(), _nodes[rightRight].GetHeight()));
                _nodes[right].SetHeight(1+ Math.Max(_nodes[node].GetHeight(), _nodes[rightLeft].GetHeight()));
            }
            else
            {
                _nodes[right].SetRightIdx(rightRight);
                _nodes[node].SetRightIdx(rightLeft);
                _nodes[rightLeft].SetParentIdx(node);
                _nodes[node].GetAABB().Merge(_nodes[left].GetAABB(), _nodes[rightLeft].GetAABB());
                _nodes[right].GetAABB().Merge(_nodes[node].GetAABB(), _nodes[rightRight].GetAABB());
                
                _nodes[node].SetHeight(1+ Math.Max(_nodes[left].GetHeight(), _nodes[rightLeft].GetHeight()));
                _nodes[right].SetHeight(1+ Math.Max(_nodes[node].GetHeight(), _nodes[rightRight].GetHeight()));
            }

            return right;
        }
        else if (currentBalance < -1)
        {
            var leftLeft = _nodes[left].GetLeftIdx();
            var leftRight = _nodes[left].GetRightIdx();
            
            _nodes[left].SetLeftIdx(node);
            _nodes[left].SetParentIdx(_nodes[node].GetParentIdx());
            _nodes[left].SetParentIdx(left);

            if (_nodes[left].GetParentIdx() != AABB.NULL_NODE)
            {
                if (_nodes[_nodes[left].GetParentIdx()].GetLeftIdx() == node)
                {
                    _nodes[_nodes[left].GetParentIdx()].SetRightIdx(left);
                }
                else
                {
                    _nodes[_nodes[left].GetParentIdx()].SetRightIdx(left);
                }
            }
            else
            {
                _root = left;
            }

            if (_nodes[leftLeft].GetHeight() > _nodes[leftRight].GetHeight())
            {
                _nodes[left].SetRightIdx(leftLeft);
                _nodes[node].SetLeftIdx(leftRight);
                _nodes[leftRight].SetParentIdx(node);
                _nodes[node].GetAABB().Merge(_nodes[right].GetAABB(), _nodes[leftRight].GetAABB());
                _nodes[left].GetAABB().Merge(_nodes[node].GetAABB(), _nodes[leftLeft].GetAABB());
                
                _nodes[node].SetHeight(1+ Math.Max(_nodes[right].GetHeight(), _nodes[leftRight].GetHeight()));
                _nodes[left].SetHeight(1+ Math.Max(_nodes[node].GetHeight(), _nodes[leftLeft].GetHeight()));
            }
            else
            {
                _nodes[left].SetRightIdx(leftRight);
                _nodes[node].SetLeftIdx(leftLeft);
                _nodes[leftLeft].SetParentIdx(node);
                _nodes[node].GetAABB().Merge(_nodes[right].GetAABB(), _nodes[leftLeft].GetAABB());
                _nodes[left].GetAABB().Merge(_nodes[node].GetAABB(), _nodes[leftRight].GetAABB());
                
                _nodes[node].SetHeight(1+ Math.Max(_nodes[right].GetHeight(), _nodes[leftLeft].GetHeight()));
                _nodes[left].SetHeight(1+ Math.Max(_nodes[node].GetHeight(), _nodes[leftRight].GetHeight()));
            }

            return left;
        }
        return node;
    }

    /*
     * ComputeHeight 코드 구현 미루기
     */

    #endregion

    #region ErrorThrow
    private StackFrame GetErrorData()
    { 
        StackTrace _st = new StackTrace(new StackFrame());
        return _st.GetFrame(0);
    }

    private void PrintError(ErrCode t, StackFrame sf)
    {
        string basicString = "";
        basicString += ("FileName : "+sf.GetFileName()+"\n");
        basicString += ("MethodName : "+sf.GetMethod().Name+"\n");
        basicString += ("LineNumber : "+sf.GetFileLineNumber()+"\n");
        basicString += ("ColumnNumber : "+sf.GetFileColumnNumber()+"\n");
        Console.Write("---------------------------------------------------");
        switch (t)
        {
            case ErrCode.PARTICLE_ALREADY_EXIST:
                Console.Write("[Error] : PARTICLE_ALREADY_EXIST\n"+basicString);
                break;
            case ErrCode.DIMENSION_MISSMATCH:
                Console.Write("[Error] : DIMENSION_MISSMATCH\n"+basicString);
                break;
            case ErrCode.LOWERBOUND_GREATER:
                Console.Write("[Error] : LOWERBOUND_GREATER\n"+basicString);
                break;
            case ErrCode.DIMENSION_LOWER_2:
                Console.Write("[Error] : DIMENSION_LOWER_2\n"+basicString);
                break;
            case ErrCode.NOT_EXISTKEY_MAP:
                Console.Write("[Error] : NOT_EXISTKEY_MAP\n"+basicString);
                break;
        }   
        Console.Write("---------------------------------------------------\n");
    }
    private enum ErrCode
    {
        PARTICLE_ALREADY_EXIST = 0,
        DIMENSION_MISSMATCH,
        LOWERBOUND_GREATER,
        DIMENSION_LOWER_2,
        NOT_EXISTKEY_MAP,
        
    }
    #endregion
}
