namespace MyAABB._99Etc;
using System;
using System.Diagnostics;
using System.Numerics;
public static class TestFunc
{
    #region ErrorThrow
    public static StackFrame GetErrorData()
    { 
        StackTrace _st = new StackTrace(new StackFrame());
        return _st.GetFrame(0);
    }

    public static void PrintError(ErrCode t, StackFrame sf)
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
            case ErrCode.INVALID_BOUND:
                Console.Write("[Error] : INVALID_BOUND\n"+basicString);
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
    
    #endregion
}