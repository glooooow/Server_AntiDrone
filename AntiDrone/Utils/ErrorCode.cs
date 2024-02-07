namespace AntiDrone.Utils;

public enum ErrorCode
{
    CanNotWrite,
    InvalidError,
    FeatureNotExist,
    NotFound,
    NoAuthority,
    NeedToLogin,
    ExistedAccount,
    NoAccount
}

static class ErrorMessage {
    public static string message(this ErrorCode e) {
        switch (e) {
            case ErrorCode.CanNotWrite:
                return "요청에 대한 응답을 찾을 수 없습니다.";
            case ErrorCode.InvalidError:
                return "알 수 없는 에러가 발생하였습니다.";
            case ErrorCode.FeatureNotExist:
                return "현재 지원하지 않는 기능입니다.";
            case ErrorCode.NotFound:
                return "요청에 대한 응답을 찾을 수 없습니다.";
            case ErrorCode.NoAuthority:
                return "접근 권한이 없습니다.";
            case ErrorCode.NeedToLogin:
                return "로그인이 필요합니다.";
            case ErrorCode.ExistedAccount:
                return "이미 존재하는 아이디입니다.";
            case ErrorCode.NoAccount:
                return "로그인 정보로 가입된 계정이 없습니다.";
        }
        return String.Empty;
    }
}