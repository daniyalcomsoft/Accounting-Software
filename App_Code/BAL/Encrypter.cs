using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for Encrypter
/// </summary>
public class Encrypter
{
	public Encrypter()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //public static String EncryptIt(String s)
    //{
    //    Encoding byteEncoder = Encoding.UTF8;
    //    byte[] key = byteEncoder.GetBytes("mohammadaminshah");
    //    byte[] IV = byteEncoder.GetBytes("mohammadaminshah");
    //    String result;

    //    RijndaelManaged rijn = new RijndaelManaged();
    //    rijn.Mode = CipherMode.CBC;
    //    rijn.Padding = PaddingMode.Zeros;
    //    using (MemoryStream msEncrypt = new MemoryStream())
    //    {
    //        using (ICryptoTransform encryptor = rijn.CreateEncryptor(key, IV))
    //        {
    //            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
    //            {
    //                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
    //                {


    //                    swEncrypt.Write(s);
    //                }
    //            }
    //        }
    //        result = Convert.ToBase64String(msEncrypt.ToArray());
    //    }
    //    rijn.Clear();
    //    return result;
    //}
    //public static String DecryptIt(String s)
    //{
    //    Encoding byteEncoder = Encoding.UTF8;
    //    byte[] key = byteEncoder.GetBytes("mohammadaminshah");
    //    byte[] IV = byteEncoder.GetBytes("mohammadaminshah");
    //    String result;
    //    RijndaelManaged rijn = new RijndaelManaged();
    //    rijn.Mode = CipherMode.CBC;
    //    rijn.Padding = PaddingMode.Zeros;
    //    using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(s)))
    //    {
    //        using (ICryptoTransform decryptor = rijn.CreateDecryptor(key, IV))
    //        {
    //            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
    //            {
    //                using (StreamReader swDecrypt = new StreamReader(csDecrypt))
    //                {
    //                    result = swDecrypt.ReadToEnd();
    //                }
    //            }
    //        }
    //    }
    //    rijn.Clear();
    //    return result;
    //}

    public static string DecryptIt(string TextToBeDecrypted)
    {
        RijndaelManaged RijndaelCipher = new RijndaelManaged();

        string Password = "CSC";
        string DecryptedData;

        try
        {
            byte[] EncryptedData = Convert.FromBase64String(TextToBeDecrypted);

            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
            //Making of the key for decryption
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
            //Creates a symmetric Rijndael decryptor object.
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream(EncryptedData);
            //Defines the cryptographics stream for decryption.THe stream contains decrpted data
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

            byte[] PlainText = new byte[EncryptedData.Length];
            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
            memoryStream.Close();
            cryptoStream.Close();

            //Converting to string
            DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
        }
        catch
        {
            DecryptedData = TextToBeDecrypted;
        }
        return DecryptedData;
    }

    public static string EncryptIt(string TextToBeEncrypted)
    {
        RijndaelManaged RijndaelCipher = new RijndaelManaged();
        string Password = "CSC";
        byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(TextToBeEncrypted);
        byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
        //Creates a symmetric encryptor object. 
        ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
        MemoryStream memoryStream = new MemoryStream();
        //Defines a stream that links data streams to cryptographic transformations
        CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(PlainText, 0, PlainText.Length);
        //Writes the final state and clears the buffer
        cryptoStream.FlushFinalBlock();
        byte[] CipherBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        string EncryptedData = Convert.ToBase64String(CipherBytes);

        return EncryptedData;
    } 
}
