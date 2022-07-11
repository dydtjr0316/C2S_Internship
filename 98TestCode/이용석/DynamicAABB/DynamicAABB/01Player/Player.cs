using System.Numerics;

namespace DynamicAABB._01Player;

public class Player
{
    #region 멤버 변수
    private Vector2 pos;
    private float viewRange;
    #endregion

    #region 생성/소멸

    public Player()
    {
    }

    public Player(ref Vector2 _pos, ref float _viewRange)
    {
        this.pos = _pos;
        this.viewRange = _viewRange;
    }

    public Player(ref float x, ref float y,  ref float _viewRange)
    {
        this.pos = new Vector2(x, y);
        this.viewRange = _viewRange;
    }

    #endregion

    #region Func

    public void Move(ref Vector2 movePos)
    {
        this.pos += movePos;
    }
    

    #endregion
}