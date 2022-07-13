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

    public void SetCenter(List<float> center)
    {
        _center = center;
    }

    #endregion
    public  static int NULL_NODE = 0xffffff;

    public AABB(int dimension)
    {
    }

    public AABB(in List<float> lowerBound, in List<float> upperBound)
    {
        _lowerBound = lowerBound;
        _upperBound = upperBound;

        if (_lowerBound.Count() != _upperBound.Count())
        {
            // err 처리 할 것
        }

        for (int i = 0; i < lowerBound.Count(); i++)
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

            for ( int d2 = 0; d2 < _lowerBound.Count(); d2++)
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
        for (int i = 0; i < _lowerBound.Count(); i++)
        {
            _lowerBound[i] = Math.Min(aabb1._lowerBound[i], aabb2._lowerBound[i]);
            _upperBound[i] = Math.Max(aabb1._upperBound[i], aabb2._upperBound[i]);
        }
        _sufaceArea = ComputeSurfaceArea();
        // _center = computeCenter();
    }

    public bool Contains(in AABB aabb)
    {
        for (int i = 0; i < _lowerBound.Count(); i++)
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
            for (int i = 0; i < _lowerBound.Count(); ++i)
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
            for (int i = 0; i < _lowerBound.Count(); ++i)
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
        List<float> pos = new List<float>(_lowerBound.Count());

        for (int i = 0; i < pos.Count(); i++)
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

    #region Getter/Setter

    public void SetNextIdx(in int nextIdx)
    {
        _nextIdx = nextIdx;
    }

    public int GetNextIdx()
    {
        return _nextIdx;
    }

    public void SetHeight(in int height)
    {
        _height = height;
    }

    public void SetHeightZero()
    {
        _height = 0;
    }

    public void SetParentIdx(in int parentIdx)
    {
        _parentIdx = parentIdx;
    }
    
    public void SetLeftIdx(in int leftIdx)
    {
        _leftIdx = leftIdx;
    }
    public void SetRightIdx(in int rightIdx)
    {
        _rightIdx = rightIdx;
    }

    public AABB GetAABB()
    {
        return _aabb;
    }

    public void SetParticleIdx(int particleIdx)
    {
        _particleIdx = particleIdx;
    }

    #endregion
    
    

    bool isLeaf()
    {
        return _leftIdx == AABB.NULL_NODE;
    }

}

public class Tree
{
    #region  변수
    private int _root;
    // tree
    private List<Node> _nodes;
    
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
        bool touchIsOverlap)
    {
        _dimension = dimension;
        _periodicity = periodicity;
        _skinThickness = skinThickness;
        _boxSize = boxSize;
        _touchIsOverlap = touchIsOverlap;
        if (dimension < 2)
        {
            // err처리
            // 2차원 이하로 나올 수 없음
        }

        if ((_periodicity.Count() != _dimension) || _boxSize.Count() != _dimension)
        {
            //err
        }
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
            //err 
            // 이미 map에 해당 id가 존재 하는 경우
            // 현재는 particle 이지만 player로 수정될 부분임
        }

        if (position.Count() != _dimension)
        {
            // 차원에 맞는 pos 값이 넘어왔는가에 대한 체크
            // try
            // {
            //     if (position.Count() != _dimension)
            //     {
            //         
            //     }
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine(e);
            // }
            // finally
            // {
            //     //
            // }
            // 에러처리 예시 사용할 경우 전부 수정 // 용석
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
        
        // 추가 구현 해야하는 부분임
        //InsertLeaf(node)

        // 파티클 맵에 id와 저장된 노드 저장
        _particleMap.Add(particle, node);

        // particle(player) id 삽입
        _nodes[node].SetParticleIdx(particle);

    }
    
    // 0713 12:52 insertParticle 오버로딩 함수 부터 구현 시작
    

    #endregion
}
