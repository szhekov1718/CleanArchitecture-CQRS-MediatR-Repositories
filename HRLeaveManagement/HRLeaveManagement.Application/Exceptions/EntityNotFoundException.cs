namespace HRLeaveManagement.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string name, object key) : base($"{name} ({key}) was not found!")
        {

        }
    }
}
