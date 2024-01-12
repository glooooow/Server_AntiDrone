using System.Diagnostics.CodeAnalysis;
using AntiDrone.Utils;
using Humanizer;
using Newtonsoft.Json;

namespace AntiDrone.Models;

public struct ResponseDTO<T>
{
    public bool _success { get; }
    public bool _fail { get; }
    public T _data { get;  }
    public string _message { get; }
    public ErrorCode? _error { get;}

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
        return new ResponseDTO<T>(false, "no data.", code);
    }
}