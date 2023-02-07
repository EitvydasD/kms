using Ardalis.SmartEnum;
using KMS.Core.Interfaces;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Security.Claims;

namespace KMS.Core.Services;

public class CallerAccessor : ICallerAccessor
{   
    public CallerAccessor()
    {
        UserId = Guid.Empty;
        Permissions = new List<string>();
    }

    public CallerAccessor(IEnumerable<Claim> claims)
    {
        var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var property in properties)
        {
            if (property.PropertyType?.GetInterfaces().Contains(typeof(IEnumerable)) ?? false)
            {
                var claimsList = claims.Where(x => x.Type == property.Name).Select(x => x.Value).ToList();

                property.SetValue(this, claimsList);
                continue;
            }

            var claim = claims.FirstOrDefault(x => x.Type == property.Name);
            if (claim == null)
            {
                Throw(property.Name);
            }

            try
            {
                if (property.PropertyType?.BaseType?.Name == typeof(SmartEnum<>).Name)
                {
                    var mi = property.PropertyType?.BaseType?.BaseType?.GetMethod("FromName");
                    var instance = mi?.Invoke(property, new object[] { claim!.Value, true });
                    property.SetValue(this, instance);
                }
                else
                {
                    var converter = TypeDescriptor.GetConverter(property.PropertyType);

                    var value = converter.ConvertFromString(claim!.Value);
                    property.SetValue(this, value);
                }
            }
            catch (Exception)
            {
                Throw(property.Name);
            }
        }
    }

    public Guid UserId { get; init; }
    public List<string> Permissions { get; init; } = new List<string>();

    public bool HasPermission(string permission)
    {
        return Permissions.Any(x => x == permission);
    }

    private static void Throw(string field)
    {
        throw new KeyNotFoundException($"There is no {field} value in claims");
    }
}
