namespace MyAABB._02Object;

public static class PlayerMgr
{
    static PlayerMgr()
    {
        for (var i = 0; i < 1000; ++i)
        {
            _players.Add(i, new Player());
        }
    }
    private static Dictionary<int, Player> _players;

    public static Player Find(in int id)
    {
        Player tempObj;
        if (_players.TryGetValue(id, out tempObj)) ;
        return tempObj;
    }
    
    
}