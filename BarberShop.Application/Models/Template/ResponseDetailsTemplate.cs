namespace BarberShop.Application.Models.Template
{
    public class ResponseDetailsTemplate<T> : ResponseTemplate<T>
    {
        public ResponseDetailsTemplate(T data)
        {
            Data = data;
        }
    }
}
