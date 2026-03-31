namespace E_commerence.Models;

public class ErrorResponse
{
    public bool Success { get; set; } = false;
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public List<string> ValidationErrors { get; set; } = new();
}