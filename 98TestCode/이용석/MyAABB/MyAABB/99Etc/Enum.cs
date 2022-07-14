namespace MyAABB._99Etc;

public enum NODE_TYPE
{
    IDLE,
    ROOT,
    NODE, /*ROOT, LEAF가 아닌 노드*/
    LEAF,
}

public enum ErrCode
{
    PARTICLE_ALREADY_EXIST = 0,
    DIMENSION_MISSMATCH,
    INVALID_BOUND,
    DIMENSION_LOWER_2,
    NOT_EXISTKEY_MAP,
        
}