namespace ASP.NET_CORE_API.Model
{
    public class ServiceResponse<T>
    {
        public int code { get; set; } 
        public string? message { get; set; }
        public T? Data { get; set; }
    }
    public class ServiceResponse1<T>
    {
        public int code { get; set; }
        public T? message { get; set; }
    }
    public class error
    {
        public string? stacktrace { get; set; }
        public string? message { get; set; } 
        public string? source { get; set; } 
    }
}
