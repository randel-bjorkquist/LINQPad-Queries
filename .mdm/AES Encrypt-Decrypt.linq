<Query Kind="Program">
  <Connection>
    <ID>f272bccf-6dae-49e2-af73-03b9209b7366</ID>
    <NamingService>2</NamingService>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\ChinookDemoDb.sqlite</AttachFileName>
    <DisplayName>localhost database (CluedIn - BlobStored)</DisplayName>
    <Server>localhost</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA54DOUebfQEikwaNxbHIyqQAAAAACAAAAAAAQZgAAAAEAACAAAABZR76LkdxlrztMM5G5rsGdQjeythb6RrjLDAT6zTAs7wAAAAAOgAAAAAIAACAAAADZcRMkFLDE0jWW/dnj8fT5DDnG9aZjd2xrGTqVeP7u+iAAAABWf2SsBplDqozBrPL2RVl2klHFLN0KIB+Q0bWUdxBNLkAAAADtpXrqYi4Bz184qTVfq/M49junGQIazLwgtcduuV4sDqv9r1HM49be2OrFMo0iKYITT73D0npebCwUmgWVsMSn</Password>
    <Database>DataStore.Db.BlobStorage</Database>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
      <MapSQLiteDateTimes>true</MapSQLiteDateTimes>
      <MapSQLiteBooleans>true</MapSQLiteBooleans>
      <TrustServerCertificate>True</TrustServerCertificate>
    </DriverData>
  </Connection>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	var secret = "0x504385ABD5BAADD3BF8062BB50EB3709B5D4C3F7D4AC2303F3C92BCE447CE3E4E5787974C1E02FCBEB1AA2E75695FB917FD48C775B9272AAA6A699555C6DF5E316E253775FB797054523B8E2AEDEDAF0";
	Blobs.Take(100)
//     .Select(r => new { length = r.DataLength
//                       ,md5    = r.DataMD5
//                       ,data   = Encoding.UTF8.GetString(r.Data) })
//     .Select(r => Decrypt(r.Data.ToString(), secret) == r.DataMD5)
//       .Select(r => Decrypt(Encoding.UTF8.GetString(ByteArrayFromHexaString(r.Data.ToString())), secret) == r.DataMD5)
//       .Select(r => Decrypt(Encoding.UTF8.GetString(r.Data), secret) == r.DataMD5)
       .Select(r => Decrypt(r.Data, secret))
       .Dump("Blobs.Take(100).Select(r => Decrypt(r.Data, secret))", 1)
//       .Dump("Blobs.Take(100)", 0)
			 ;       
       
//  var blah = "boo hoo hoo";
//  blah.Dump();
//  
//  var encrypted_value = Encrypt(blah, secret);
//  encrypted_value.Dump();
//  
//  Decrypt(encrypted_value, secret).Dump();
}

//AES

private static string Encrypt(string text, string key)
{
  string result = null;
  byte[] clearBytes = Encoding.Unicode.GetBytes(text);
  //byte[] clearBytes = Encoding.UTF32.GetBytes(text);
  //byte[] clearBytes = Encoding.UTF8.GetBytes(text);
  //byte[] clearBytes = Encoding.ASCII.GetBytes(text);
  
  using(Aes encryptor = Aes.Create())
  {
    var pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
    
    encryptor.Key     = pdb.GetBytes(32);
    encryptor.IV      = pdb.GetBytes(16);
//    encryptor.Padding = PaddingMode.Zeros;
    
    using(MemoryStream ms = new MemoryStream())
    {
      using(CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
      {
        cs.Write(clearBytes, 0, clearBytes.Length);
        cs.Close();
      }
      
      result = Convert.ToBase64String(ms.ToArray());
    }
  }
  
  return result;
}

private static string Decrypt(string text, string key)
{
  return Decrypt(Convert.FromBase64String(text), key);
}

private static string Decrypt(byte[] bytes, string key)
{
  string result = null;
  
  using(Aes encryptor = Aes.Create())
  {
    var pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
    
    encryptor.Key     = pdb.GetBytes(32);
    encryptor.IV      = pdb.GetBytes(16);
//    encryptor.Padding = PaddingMode.PKCS7;
    encryptor.Padding = PaddingMode.Zeros;
    
    using(MemoryStream ms = new MemoryStream())
    {
      using(CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
      {
        cs.Write(bytes, 0, bytes.Length);
        cs.Close();
      }
      
      result = Encoding.Unicode.GetString(ms.ToArray());
    }
  }
  
  return result;
}

#region COMMENTED OUT:
/*

public static byte[] ByteArrayFromHexaString(string hexa)
{
	int length = hexa.Length;
	List<byte> result = new List<byte>();
	
	// Fetch whether there are an odd or even number of chars
	bool isOdd = ((length & 1) == 1);
	
	if(isOdd)
	{
		result.Add(byte.Parse(hexa[0].ToString(), NumberStyles.HexNumber));
	}
	
	string s;
	
	for(int i = (isOdd) ? 1 : 0; i < length; i += 2)
	{
		s = hexa.Substring(i, 2);
		result.Add(byte.Parse(s, NumberStyles.HexNumber));
	}
	
	return result.ToArray();
}

public static string Encrypt(string text, string key)
{
	try
	{
		string textToEncrypt = "Water";
		string ToReturn = "";
		string publickey = "santhosh";
		string secretkey = "engineer";
		byte[] secretkeyByte = { };
		secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
		byte[] publickeybyte = { };
		publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
		MemoryStream ms = null;
		CryptoStream cs = null;
		byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
		using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
		{
			ms = new MemoryStream();
			cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
			cs.Write(inputbyteArray, 0, inputbyteArray.Length);
			cs.FlushFinalBlock();
			ToReturn = Convert.ToBase64String(ms.ToArray());
		}
		return ToReturn;
	}
	catch (Exception ex)
	{
		throw new Exception(ex.Message, ex.InnerException);
	}
}

public static string Decrypt(string data, string key)
{
	try
	{
//		string textToDecrypt = "VtbM/yjSA2Q=";
		string textToDecrypt = data;
		string ToReturn   = "";
		string publickey  = "santhosh";
		string privatekey = "engineer";
		
		byte[] privatekeyByte = { };
		privatekeyByte = System.Text.Encoding.UTF8.GetBytes(privatekey);
		
		byte[] publickeybyte = { };
		publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
		
		MemoryStream ms = null;
		CryptoStream cs = null;
		
		byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
		inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
		
		using(DESCryptoServiceProvider des = new DESCryptoServiceProvider())
		{
			ms = new MemoryStream();
			cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
			
			cs.Write(inputbyteArray, 0, inputbyteArray.Length);
			cs.FlushFinalBlock();
			
			Encoding encoding = Encoding.UTF8;
			ToReturn = encoding.GetString(ms.ToArray());
		}
		
		return ToReturn;
	}
	catch (Exception ae)
	{
		throw new Exception(ae.Message, ae.InnerException);
	}
}


//public static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";

public static string Encrypt(string text, string key)
{
	using (var md5 = new MD5CryptoServiceProvider())
	{
		using (var tdes = new TripleDESCryptoServiceProvider())
		{
			tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
			tdes.Mode = CipherMode.ECB;
			tdes.Padding = PaddingMode.PKCS7;

			using (var transform = tdes.CreateEncryptor())
			{
				byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
				byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
				return Convert.ToBase64String(bytes, 0, bytes.Length);
			}
		}
	}
}

public static string Decrypt(string cipher, string key)
{
	using (var md5 = new MD5CryptoServiceProvider())
	{
		using (var tdes = new TripleDESCryptoServiceProvider())
		{
			tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
			//			tdes.Key = md5.ComputeHash(UTF32Encoding.UTF32.GetBytes(key));
			tdes.Mode = CipherMode.ECB;
			tdes.Padding = PaddingMode.PKCS7;

			using (var transform = tdes.CreateDecryptor())
			{
				byte[] cipherBytes = Convert.FromBase64String(cipher);
				byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
				return UTF8Encoding.UTF8.GetString(bytes);
			}
		}
	}
}

public static string Decrypt(byte[] cipher, string key)
{
	using(var md5 = new MD5CryptoServiceProvider())
	{
		using(var tdes = new TripleDESCryptoServiceProvider())
		{
			tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
			tdes.Mode = CipherMode.ECB;
			tdes.Padding = PaddingMode.PKCS7;

			using(var transform = tdes.CreateDecryptor())
			{
				byte[] bytes = transform.TransformFinalBlock(cipher, 0, cipher.Length);
				return UTF8Encoding.UTF8.GetString(bytes);
			}
		}
	}
}

private string _salt = "*1234567890!@#$%^&*()14344*";

private string Crypto(string text)
{
	var hashmd5 = new MD5CryptoServiceProvider();
	byte[] toEncryptArray = Encoding.UTF8.GetBytes(_salt);

	byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(_salt));
	hashmd5.Clear();

	TripleDesProvider.Key = keyArray;
	TripleDesProvider.Mode = CipherMode.ECB;
	TripleDesProvider.Padding = PaddingMode.PKCS7;

	ICryptoTransform cTransform = TripleDesProvider.CreateEncryptor();

	byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

	return Convert.ToBase64String(resultArray, 0, resultArray.Length);
}

private string Decrypto(string text)
{
	try
	{
		var hashmd5 = new MD5CryptoServiceProvider();
		byte[] toEncryptArray = Convert.FromBase64String(text);

		byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(_salt));

		hashmd5.Clear();

		TripleDesProvider.Key = keyArray;
		TripleDesProvider.Mode = CipherMode.ECB;
		TripleDesProvider.Padding = PaddingMode.PKCS7;

		ICryptoTransform cTransform = TripleDesProvider.CreateDecryptor();
		byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

		TripleDesProvider.Clear();

		return Encoding.UTF8.GetString(resultArray);
		//return Encoding.UTF8.GetString(resultArray);
	}
	catch
	{
		return string.Empty;
	}
}
*/
#endregion