namespace MyAABB._02Object;

public class Player
{
    private float _x;
    private float _y;

    public Player()
    {
        _x = 0.0f;
        _y = 0.0f;
    }

    public Player(in float x, in float y)
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