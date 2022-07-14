using MyAABB._99Etc;

namespace MyAABB._01dynamicAABBTree._01BoundingBox;

public class Position
{
    private float _x;
    private float _y;

    public Position()
    {
        _x = 0.0f;
        _y = 0.0f;
    }

    public Position(in float x, in float y)
    {
        _x = x;
        _y = y;
    }

    public void SetX(in float x)
    {
        _x = x;
    }
    public float GetX()
    {
        return _x;
    }

    public void SetY(in float y)
    {
        _y = y;
    }
    public float GetY()
    {
        return _y;
    }

}

public class BoundingBox
{
    private Position _lowerBound;
    private Position _upperBound;
    private Position _center;

    private float _sufaceArea;

    #region Getter/Setter

    public Position GetLowerBound()
    {
        return _lowerBound;
    }


    public Position GetUppderBound()
    {
        return _upperBound;
    }


    public void SetSurfaceArea(float surFaceArea)
    {
        _sufaceArea = surFaceArea;
    }

    public Position GetCenter()
    {
        return _center;
    }

    public void SetCenter(Position center)
    {
        _center = center;
    }

    #endregion

    public BoundingBox()
    {

    }

    public BoundingBox(int dimension)
    {
        _lowerBound = new Position();
        _upperBound = new Position();

    }

    public BoundingBox(in Position lowerBound, in Position upperBound)
    {
        _lowerBound = lowerBound;
        _upperBound = upperBound;

        if (!IsValidBound())
        {
            TestFunc.PrintError(ErrCode.INVALID_BOUND, TestFunc.GetErrorData());
        }

        ComputeSurfaceAreaNCenter();
    }

    public void ComputeSurfaceAreaNCenter()
    {
        ComputeSurfaceArea();
        ComputeCenter();
    }

    public bool IsValidBound()
    {
        if (_lowerBound.GetX() > _upperBound.GetX() ||
            _lowerBound.GetY() > _upperBound.GetY())
        {
            return false;
        }

        return true;
    }

    public void ComputeSurfaceArea()
    {
        _sufaceArea =
            (_upperBound.GetX() - _lowerBound.GetX()) *
            (_upperBound.GetY() - _lowerBound.GetY());
    }

    public void Merge(in BoundingBox BoundingBox1, in BoundingBox BoundingBox2)
    {

        _lowerBound.SetX(Math.Min(BoundingBox1._lowerBound.GetX(), BoundingBox2._lowerBound.GetX()));
        _upperBound.SetX(Math.Min(BoundingBox1._upperBound.GetX(), BoundingBox2._upperBound.GetX()));
        ComputeSurfaceAreaNCenter();
    }



    public bool Contains(in BoundingBox BoundingBox)
    {
        return (BoundingBox._lowerBound.GetX() < _lowerBound.GetX() ||
                BoundingBox._lowerBound.GetX() < _lowerBound.GetX() ||
                BoundingBox._lowerBound.GetX() < _lowerBound.GetX() ||
                BoundingBox._lowerBound.GetX() < _lowerBound.GetX());
    }

    public bool Overlaps(in BoundingBox BoundingBox, bool touchIsOverlap)
    {
        // bool rv = true;
        // if (touchIsOverlap)
        // {
        //     for (int i = 0; i < _lowerBound.Count; ++i)
        //     {
        //         if (BoundingBox._upperBound[i] < _lowerBound[i] || BoundingBox._lowerBound[i] > _upperBound[i])
        //         {
        //             rv = false;
        //             break;
        //         }
        //     }
        // }
        // else
        // {
        //     for (int i = 0; i < _lowerBound.Count; ++i)
        //     {
        //         if (BoundingBox._upperBound[i] <= _lowerBound[i] || BoundingBox._lowerBound[i] >= _upperBound[i])
        //         {
        //             rv = false;
        //             break;
        //         }
        //     }
        // }
        //
        // return rv;

        return false;
    }

    private void ComputeCenter()
    {
        _center.SetX((_lowerBound.GetX() + _upperBound.GetX()/2.0f));
        _center.SetY((_lowerBound.GetY() + _upperBound.GetY()/2.0f));
    }

    #region Getter/Setter

    public float GetSurfaceArea()
    {
        return _sufaceArea;
    }
    
    #endregion
    
}