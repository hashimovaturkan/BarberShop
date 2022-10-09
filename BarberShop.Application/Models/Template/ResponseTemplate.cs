namespace BarberShop.Application.Models.Template
{
    public class ResponseTemplate<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }

        public ResponseTemplate() { }

        public ResponseTemplate(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
    }
}
