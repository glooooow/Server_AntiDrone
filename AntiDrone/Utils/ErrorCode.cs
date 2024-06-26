﻿namespace AntiDrone.Utils;

public enum ErrorCode
{
    NotWriteValue,
    UnknownError,
    FeatureNotExist,
    NoData,
    NotFound,
    NoAuthority,
    BadRequest,
    NeedToLogin,
    ExistedAccount,
    InvalidAccount,
    NotAllowedId,
    NotAllowedName
}

static class ErrorMessage {
    public static string message(this ErrorCode e) {
        switch (e) {
            case ErrorCode.NotWriteValue:
                return "내용을 입력해주세요.";
            case ErrorCode.UnknownError:
                return "알 수 없는 에러가 발생하였습니다.";
            case ErrorCode.FeatureNotExist:
                return "현재 지원하지 않는 기능입니다.";
            case ErrorCode.NoData:
                return "요청하신 사항에 대한 데이터가 없습니다.";
            case ErrorCode.NotFound:
                return "요청에 대한 응답을 찾을 수 없습니다.";
            case ErrorCode.NoAuthority:
                return "접근 권한이 없습니다.";
            case ErrorCode.BadRequest:
                return "올바르지 않은 요청입니다. 다시 확인해주시기 바랍니다.";
            case ErrorCode.NeedToLogin:
                return "로그인이 필요합니다.";
            case ErrorCode.ExistedAccount:
                return "이미 존재하는 아이디입니다.";
            case ErrorCode.InvalidAccount:
                return "로그인 정보가 일치하지 않습니다.";
            case ErrorCode.NotAllowedId:
                return "아이디는 영문(소문자) 또는 영문을 포함한 숫자로 입력해주세요.(최대 20자)";
            case ErrorCode.NotAllowedName:
                return "한글 이름을 올바르게 입력해주세요.(최대 7자)";            
        }
        return String.Empty;
    }
}