namespace API.Utils;

public class SendResponse
{
    public string? Message { get; set; }
    public object? Data { get; set; }
    public Exception? Exception { get; set; }
    public bool IsSuccess => Exception == null;

    public SendResponse() { }
    public SendResponse(string message) => Message = message;

    public SendResponse(object data) => Data = data;
    public SendResponse(Exception exception) => Exception = exception;

    public SendResponse(string message, object data)
    {
        Message = message;
        Data = data;
    }

    public SendResponse(string message, Exception exception)
    {
        Message = message;
        Exception = exception;
    }

    public SendResponse(string message, object data, Exception exception)
    {
        Message = message;
        Data = data;
        Exception = exception;
    }

    public static SendResponse Success(string message, object data) => new(message, data);

    public static SendResponse Success(string message) => new(message);


    public static SendResponse Success(object data) => new(data);


    public static SendResponse Failure(string message, Exception exception) => new(message, exception);


    public static SendResponse Failure(Exception exception) => new(exception);
}
