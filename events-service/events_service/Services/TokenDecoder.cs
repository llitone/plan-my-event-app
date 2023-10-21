using events_service.Model;
using System.Text;
using System.Text.Json;

namespace events_service.Services;

public class TokenDecoder
{
    public UserModel? Decode(string token)
    {
        token = token.Substring(7);
        string payload = token.Substring(token.IndexOf('.') + 1, token.LastIndexOf('.') - token.IndexOf('.') - 1);
        try
        {
            payload = Encoding.UTF8.GetString(Convert.FromBase64String(payload + "="));
        }
        catch
        {
            payload = Encoding.UTF8.GetString(Convert.FromBase64String(payload + "=="));
        }

        UserModel? result = JsonSerializer.Deserialize<UserModel>(payload);

        return result;
    }
}
