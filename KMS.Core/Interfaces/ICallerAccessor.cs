namespace KMS.Core.Interfaces
{
    public interface ICallerAccessor
    {
        public Guid UserId { get; }
        public List<string> Permissions { get; }

        public bool HasPermission(string permisssion);
    }
}
