using System.Diagnostics.CodeAnalysis;
using AntiDrone.Utils;
using Humanizer;
using Newtonsoft.Json;

namespace AntiDrone.Models;

public struct ResponseDTO<T>
{
    public bool _success { get; }
    public T _data { get; }
    public ErrorCode? _error { get; }
    
    public ResponseDTO(bool success, T data, ErrorCode? error)
    {
        this._success = success;
        this._data = data;
        this._error = error;
    }
    
    public ResponseDTO(bool success, string data, ErrorCode? error)
    {
        this._success = false;
        this._data = default(T);
        this._error = error.Value;
    }
}

public abstract class ResponseGlobal<T>
{
    public static object Success(T data)
    {
        var r = new ResponseDTO<T>(true, data, null);
        var result = new { Success = r._success, Data = r._data, Error = r._error };
        return result;
    }
    
    public static object Fail(ErrorCode code)
    {
        var r = new ResponseDTO<T>(false, null, code);
        var result = new { Success = r._success, Data = r._data, Error = code.message() };
        return result;
    }
}