namespace AntiDrone.Utils;

public enum ErrorCode
{
    CanNotWrite,
    InvalidError,
    FeatureNotExist,
    NotFound,
    NoAuthority
}

public class Error
{
    public static void ErrorMessage()
    {
        ErrorCode CanNotWrite = ErrorCode.CanNotWrite;
        Console.WriteLine("데이터 작성에 실패했습니다.");

        ErrorCode InvalidError = ErrorCode.InvalidError;
        Console.WriteLine("알 수 없는 에러.");

        ErrorCode NotFound = ErrorCode.NotFound;
        Console.WriteLine("요청에 대한 응답을 찾을 수 없습니다.");
    }
}