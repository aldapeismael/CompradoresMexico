using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

public static class Encriptado
{
    private const string StrLlaveCifrar = "6476569438782293";
    private const int IntLlaveCantidad = 256;

    public static string EncriptarDatos(string StrTextoCifrar)
    {
        try
        {
            string StrCotrasenaCifrar = "Te8$Pg?}*cP9]jHtPVV#9*a6";
            byte[] StrLlaveCifrarBytes = Encoding.UTF8.GetBytes(StrLlaveCifrar);
            byte[] StrTextoCifrarBytes = Encoding.UTF8.GetBytes(StrTextoCifrar);
            PasswordDeriveBytes password = new PasswordDeriveBytes(StrCotrasenaCifrar, null);
            byte[] keyBytes = password.GetBytes(IntLlaveCantidad / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, StrLlaveCifrarBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(StrTextoCifrarBytes, 0, StrTextoCifrarBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "Encriptado.cs", "Encriptado.cs", "Error al tratar de encriptar el dato. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.ALTA);
            RespuestaBD ObjRespuestaDB = new RespuestaBD(1, "Error al tratar de actualizar el registro. IdRegistroError: " + objRegistroError.IntIdRegistroError, "error");
        }
        return "";
    }
    
    //Decrypt
    public static string DesencriptarDatos(string cipherText)
    {
        try
        {
            string StrCotrasenaCifrar = "Te8$Pg?}*cP9]jHtPVV#9*a6";
            byte[] StrLlaveCifrarBytes = Encoding.UTF8.GetBytes(StrLlaveCifrar);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(StrCotrasenaCifrar, null);
            byte[] keyBytes = password.GetBytes(IntLlaveCantidad / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, StrLlaveCifrarBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] StrTextoCifrarBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(StrTextoCifrarBytes, 0, StrTextoCifrarBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(StrTextoCifrarBytes, 0, decryptedByteCount);
        }
        catch (Exception e)
        {
            RegistroError objRegistroError = new RegistroError(MethodBase.GetCurrentMethod().Name, "Encriptado.cs", "Encriptado.cs", "Error al tratar de desencriptar el dato. IdRegistroError: ", e.HResult, e.Message, (int)Severidad.ALTA);
            RespuestaBD ObjRespuestaDB = new RespuestaBD(1, "Error al tratar de actualizar el registro. IdRegistroError: " + objRegistroError.IntIdRegistroError, "error");
        }
        return "";
    }
}