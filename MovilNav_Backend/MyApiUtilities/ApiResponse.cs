using System;

namespace MovilNav_Backend.Model
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; } = null!;
        public T? Data { get; set; }
    }
}
