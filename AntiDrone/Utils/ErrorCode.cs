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
        //"데이터 작성에 실패했습니다."

        ErrorCode InvalidError = ErrorCode.InvalidError;
        //"알 수 없는 에러."

        ErrorCode FeatureNotExist = ErrorCode.FeatureNotExist;
        //"현재 지원하지 않는 기능입니다."
        
        ErrorCode NotFound = ErrorCode.NotFound;
        //요청에 대한 응답을 찾을 수 없습니다.
        
        ErrorCode NoAuthority = ErrorCode.NoAuthority;
        //"접근 권한이 없습니다."
    }
}