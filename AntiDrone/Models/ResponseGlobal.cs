using System.Diagnostics.CodeAnalysis;
using AntiDrone.Utils;
using Humanizer;

namespace AntiDrone.Models;

public struct ResponseDTO<T>
{
    private bool _success;
    private bool _fail;
    private T _data;
    private string _message;
    private ErrorCode? _error;

    public ResponseDTO(bool success, T data, ErrorCode? error)
    {
        this._success = success;
        this._data = data;
        this._error = error;
    }
    public ResponseDTO(bool fail, string message, ErrorCode? error)
    {
        this._fail = fail;
        this._message = message;
        this._error = error;
    }
}

public abstract class ResponseGlobal<T>
{
    public static ResponseDTO<T> Success(T data)
    {
        return new ResponseDTO<T>(true, data, null);
    }
    
    public static ResponseDTO<T> Fail(ErrorCode code)
    {
        return new ResponseDTO<T>(false,"no data.", code);
    }
}