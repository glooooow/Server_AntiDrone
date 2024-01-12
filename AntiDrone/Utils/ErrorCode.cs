namespace AntiDrone.Utils;

public enum ErrorCode
{
    CanNotWrite,
    InvalidError,
    FeatureNotExist,
    NotExistBoard,
    NoAuthority
}

public class Error
{
    public static void ErrorMessage()
    {
        ErrorCode canNotWrite = ErrorCode.CanNotWrite;
        Console.WriteLine("데이터 작성에 실패했습니다.");

        var b = (ErrorCode)1;
        Console.WriteLine(b);  // output: Summer

        var c = (ErrorCode)4;
        Console.WriteLine(c);  // output: 4
    }
}