using Shared.Examinantion_System.Enums;

namespace Examinantion_System.ResponsViewModel
{
    public class ResponsViewModel<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
 
        public string Massage { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public StatusCode Status { get; set; }

        public static ResponsViewModel<T> Success(String massage, T data, StatusCode status = new StatusCode())
        {
            return new ResponsViewModel<T>
            {
                Massage = massage,
                Data = data,
                IsSuccess = true,
                Status = status

            };
        }

        public static ResponsViewModel<T> Failuare(String massage, List<String> errors , StatusCode status = new StatusCode() )
        {
            return new ResponsViewModel<T>
            {
                Massage = massage,
                Errors = errors ?? new List<string>(),
                IsSuccess = false,
                Status = status

            };

        }



    }
}
