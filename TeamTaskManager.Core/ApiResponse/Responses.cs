public class Responses<T>
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public T Data { get; set; }

    public static Responses<T> Ok(T data)
    {
        return new Responses<T> { Success = true, Data = data };
    }

    public static Responses<T> Fail(string error)
    {
        return new Responses<T> { Success = false, ErrorMessage = error };
    }
}
