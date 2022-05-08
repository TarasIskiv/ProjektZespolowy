using System.IdentityModel.Tokens.Jwt;

namespace FindJobWebApi.JWTLogic
{
    public static class JWTParser
    {
        public static int parseToken(this string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            //var claims = tokenHandler.ReadJwtToken(token).Claims.ToList();
            //return Int32.Parse(claims[2].Value);
            // temp fix in here 
            var tokenDiv = token.Substring(token.IndexOf(' ') + 1);

            var claims = tokenHandler.ReadJwtToken(tokenDiv).Claims.ToList();
            return Int32.Parse(claims[1].Value);
        }

        public static bool tryParseToken(this string token, out int result)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            result = -1;
            var tokenDiv = token.Substring(token.IndexOf(' ') + 1);

            if (!tokenHandler.CanReadToken(tokenDiv))
                return false;

            var claims = tokenHandler.ReadJwtToken(tokenDiv).Claims.ToList();
            result = Int32.Parse(claims[1].Value);
            return true;
        }
    }
}
