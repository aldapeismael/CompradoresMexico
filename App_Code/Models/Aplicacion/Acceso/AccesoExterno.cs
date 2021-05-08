using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Models.Aplicacion.Acceso
{
    public class AccesoExterno
    {
        [JsonProperty("username")]
        public string Usuario { get; set; }
        [JsonProperty("password")]
        public string Contrasena { get; set; }
        [JsonProperty("company")]
        public string Empresa { get; set; }
        internal Guid GuidUsuario { get; set; }
        internal string Token { get; set; }
        internal bool Validar()
        {
            SqlCommand sqlCommand = new SqlCommand
            {
                CommandText = "corporativo.spUsuarioAutenticar",
                CommandType = CommandType.StoredProcedure
            };
            sqlCommand.Parameters.AddWithValue("@p_ejecuta", 1);
            sqlCommand.Parameters.AddWithValue("@p_usuario", Usuario);
            sqlCommand.Parameters.AddWithValue("@p_contrasena", Contrasena);

            //int idEmpresa = 0;
            //switch (Empresa)
            //{
            //    case "tja":
            //        idEmpresa = 1;
            //        break;
            //}

            DataSet resultado = ConexionBD.EjecutarComandoExterno(sqlCommand, "Autenticar");
            if (resultado.Tables.Count <= 0 || resultado.Tables[0].Rows.Count <= 0)
            {
                return false;
            }

            if (!bool.Parse(resultado.Tables[0].Rows[0][0].ToString()))
            {
                return false;
            }

            GuidUsuario = new Guid(resultado.Tables[0].Rows[0][1].ToString());
            return true;
        }

        internal void GenerarToken()
        {
            Token = GenerateTokenJwt();
        }
        private string GenerateTokenJwt()
        {
            string secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            string audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
            string issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            string expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secretKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, Usuario) });

            // create token to the user
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials);

            string jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }
    }
}