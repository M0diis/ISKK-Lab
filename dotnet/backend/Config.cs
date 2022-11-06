
namespace modkaz.Backend;

/// <summary>
/// <para>Configuration data.</para>
/// <para>Static members are thread safe, instance members are not.</para>
/// </summary>
public class Config 
{
    /// <summary>
    /// JWT secret. Ideally these should have a limited lifetime (2x max JWT lifetime) 
    /// and get changed automatically. Ideally we should also track current and expiring secrets.
    /// </summary>
    public static string JwtSecret => "jwt secret 12345678910111213141516";
};