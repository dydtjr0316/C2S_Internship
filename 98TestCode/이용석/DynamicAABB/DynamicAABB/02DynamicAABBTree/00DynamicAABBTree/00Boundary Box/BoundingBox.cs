using System.Numerics;

namespace DynamicAABB._02DynamicAABBTree._00Boundary_Box;

public class BoundingBox
{
    #region 멤버변수
    // container로 바꾸는거 생각해보기
    private Vector2 []_vertex = new Vector2[4];
    
    //test version
    private float _minX;
    private float _minY;
    private float _maxX;
    private float _maxY;
    #endregion

    #region 생성/소멸자

    public BoundingBox(Vector2[] vertex)
    {
        // float pos / vector2 pos를 통해 넣을 경우 있는지 확인
        _vertex = vertex;
    }
    
    //test
    public BoundingBox(float minX, float minY, float maxX, float maxY)
    {
        _minX = minX;
        _minY = minY;
        _maxX = maxX;
        _maxY = maxY;
    }

    #endregion
    
    #region Getter/Setter

    public Vector2[] GetVertex()
    {
        // 아마 인덱스 접근 안하지 않을까해서 오버로딩 안해놨는데 생각해보기
        return _vertex;
    }

    //test
    public float GetWidth()
    {
        return _maxX - _minX;
    }
    
    public float GetHeight()
    {
        return _maxY - _minY;
    }


    #endregion

    #region func

    // 충돌 및 포함 체크 함수 구현

    #endregion
}