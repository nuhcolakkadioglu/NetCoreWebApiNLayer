

using System.Net;
using System.Text.Json.Serialization;

namespace App.Services;

public class ServiceResult<T>
{
    public T? Data { get; set; }
    public List<string>? ErrorMessage { get; set; }
    [JsonIgnore]
    public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
    [JsonIgnore]
    public bool IsFail => !IsSuccess;
    [JsonIgnore]
    public HttpStatusCode Status { get; set; }
    [JsonIgnore]
    public string? UrlAsCreated { get; set; }



    public static ServiceResult<T> Success(T data, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
       => new ServiceResult<T> { Data = data, Status = httpStatusCode };

    public static ServiceResult<T> SuccessAsCreated(T data, string urlAsCreated)
     => new ServiceResult<T> { Data = data, Status = HttpStatusCode.Created, UrlAsCreated = urlAsCreated };

    public static ServiceResult<T> Fail(List<string> errorrMessages, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
       => new ServiceResult<T> { ErrorMessage = errorrMessages, Status = httpStatusCode };
    public static ServiceResult<T> Fail(string errorrMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
      => new ServiceResult<T> { ErrorMessage = [errorrMessage], Status = httpStatusCode };


}

public class ServiceResult
{
    public List<string>? ErrorMessage { get; set; }
    [JsonIgnore]
    public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
    [JsonIgnore]
    public bool IsFail => !IsSuccess;
    [JsonIgnore]
    public HttpStatusCode Status { get; set; }



    public static ServiceResult Success(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
       => new ServiceResult { Status = httpStatusCode };
    public static ServiceResult Fail(List<string> errorrMessages, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
       => new ServiceResult { ErrorMessage = errorrMessages, Status = httpStatusCode };
    public static ServiceResult Fail(string errorrMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
      => new ServiceResult { ErrorMessage = [errorrMessage], Status = httpStatusCode };


}
