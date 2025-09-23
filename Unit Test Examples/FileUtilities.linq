#load "xunit"

void Main()
{
  #region NON-TEST CODE

  #region AesEncryptionStrategy

  //---------------------------------------------------------------------------------------------
  //NOTE: Encrypt/Decrypt Text Example ----------------------------------------------------------
  //---------------------------------------------------------------------------------------------
  //var aes = new AesEncryptionStrategy("my_p@ssw0rd");
  //
  //string plain_text = "Secret Message";
  //plain_text.Dump("plain_text");
  //
  //byte[] plain_bytes = Encoding.UTF8.GetBytes(plain_text);
  //plain_bytes.Dump("plain_bytes", 0);
  //
  //byte[] cipher_bytes = aes.Encrypt(plain_bytes);
  //cipher_bytes.Dump("cipher_bytes", 0);
  //
  //byte[] roundtrip_bytes = aes.Decrypt(cipher_bytes);
  //roundtrip_bytes.Dump("roundtrip_bytes", 0);
  //
  //string roundtrip_text = Encoding.UTF8.GetString(roundtrip_bytes);
  //roundtrip_text.Dump("roundtrip_text");

  //---------------------------------------------------------------------------------------------
  //NOTE: Encrypt/Decrypt Binary (e.g. image) Example -------------------------------------------
  //---------------------------------------------------------------------------------------------
  //var aes = new AesEncryptionStrategy("my_p@ssw0rd");
  //
  //var path = @"C:\temp\dog_with_vr_goggles.jpg";
  //byte[] image_data = File.ReadAllBytes(path);
  //
  //byte[] encrypted = aes.Encrypt(image_data);
  //encrypted.Dump("encrypted", 0);
  //
  //byte[] decrypted = aes.Decrypt(encrypted);
  //decrypted.Dump("decrypted", 0);
  //
  //File.WriteAllBytes(@"C:\temp\dog_with_vr_goggles(restored).jpg", decrypted);

  #endregion

  #region XorEncryptionStrategy
  //
  //var xor = new XorEncryptionStrategy(0x42);
  //
  //// Encrypt file to stream
  //using var fs = File.OpenWrite("xor.dat");
  //using var xorWriter = xor.Encrypt(fs);
  //using var sw = new StreamWriter(xorWriter);
  //sw.WriteLine("Hello XOR World!");
  //
  //// Decrypt file back
  //using var fs2 = File.OpenRead("xor.dat");
  //using var xorReader = xor.Decrypt(fs2);
  //using var sr = new StreamReader(xorReader);
  //string text = sr.ReadToEnd();
  //Console.WriteLine(text); // "Hello XOR World!"
  //
  #endregion

  #region Impersonation

  //---------------------------------------------------------------------------------------------
  //NOTE: Impersonation Text Example ------------------------------------------------------------
  //ImpersonationLogonTypeEnum.LOGON_INTERACTIVE      //FYI: use for local machine accounts ...
  //ImpersonationLogonTypeEnum.LOGON_NEW_CREDENTIALS  //FYI: use for UNC/server access ..
  //---------------------------------------------------------------------------------------------
  //using IImpersonator imp = new WindowsImpersonator(".", "test_user", "P@ssw0rd", ImpersonationLogonTypeEnum.LOGON_INTERACTIVE);
  ////using IImpersonator imp = new WindowsImpersonator(".", "test_user", "P@ssw0rd", ImpersonationLogonTypeEnum.LOGON_NEW_CREDENTIALS);
  //
  //imp.Run(() => {  
  //  File.WriteAllText(@"C:\temp\impersonation.txt", "(impersonation) Hello World!");
  //});
  //
  //var image_bytes = imp.Run(() => {
  //  return File.ReadAllBytes(@"C:\temp\dog_with_vr_goggles.jpg");
  //});
  //
  //imp.Run(() => {
  //  File.WriteAllBytes(@"C:\temp\dog_with_vr_goggles(impersonation - restore).jpg", image_bytes);
  //});

  #endregion

  #region mcsFileUtilities

  //---------------------------------------------------------------------------------------------
  //NOTE: Non-Encrypted Text Example ------------------------------------------------------------
  //---------------------------------------------------------------------------------------------
  //var path = @"C:\temp\mcsFileUtilitiesTest.txt";
  //
  //mcsFileUtilities.WriteTextFile(path, "(non-encrypted) Secret Message");
  //string roundtrip_text = mcsFileUtilities.ReadTextFile(path);
  //roundtrip_text.Dump("roundtrip_text ");

  //---------------------------------------------------------------------------------------------
  //NOTE: Non-Encrypted Binary Example ----------------------------------------------------------
  //---------------------------------------------------------------------------------------------
  //var path = @"C:\temp\dog_with_vr_goggles.jpg";
  //
  ////read raw image bytes
  //byte[] image_data = mcsFileUtilities.ReadBinaryFile(path);
  //
  ////write binary file
  //var bak_file = @"C:\temp\dog_with_vr_goggles.bak";
  //mcsFileUtilities.WriteBinaryFile(bak_file, image_data);
  //
  ////read the binary file
  //byte[] restored = mcsFileUtilities.ReadBinaryFile(bak_file);
  //
  //File.WriteAllBytes(@"C:\temp\dog_with_vr_goggles(restored).jpg", restored);

  //---------------------------------------------------------------------------------------------
  //NOTE: Encrypt/Decrypt Text Example ----------------------------------------------------------
  //---------------------------------------------------------------------------------------------
  //var aes  = new AesEncryptionStrategy("my_p@ssw0rd");
  //var no_encryption = NoEncryptionStrategy.Instance;
  //
  //var opts = new mcsFileOptions { //Encryption = no_encryption
  //                             Encryption = aes
  //                            ,Encoding   = Encoding.UTF8
  //                            ,Logger     = ConsoleLogger };
  //
  //var path = @"C:\temp\mcsFileUtilitiesTest.txt";
  //
  //mcsFileUtilities.WriteTextFile(path, "(encrypted) Secret Message", opts);
  //string roundtrip_text = mcsFileUtilities.ReadTextFile(path, opts);
  //roundtrip_text.Dump("roundtrip_text ");

  //---------------------------------------------------------------------------------------------
  //NOTE: Encrypt/Decrypt Binary (e.g. image) Example -------------------------------------------
  //---------------------------------------------------------------------------------------------
  //var path = @"C:\temp\dog_with_vr_goggles.jpg";
  //
  //var aes  = new AesEncryptionStrategy("my_p@ssw0rd");
  //var opts = new mcsFileOptions { Encryption = aes };
  //
  ////read raw image bytes
  //byte[] image_data = File.ReadAllBytes(path);
  //
  ////write encrypted binary file
  //var enc_file = @"C:\temp\dog_with_vr_goggles.enc";
  //mcsFileUtilities.WriteBinaryFile(enc_file, image_data, opts);
  //
  ////read the encrypted binary file
  //byte[] restored = mcsFileUtilities.ReadBinaryFile(enc_file, opts);
  //
  //File.WriteAllBytes(@"C:\temp\dog_with_vr_goggles(encrypt-restored).jpg", restored);

  //---------------------------------------------------------------------------------------------
  //NOTE: Encrypt/Decrypt Text Example with Dynamic-Swapping EncryptionStrategies ---------------
  //---------------------------------------------------------------------------------------------
  //var aes  = EncryptionFactory.Create(EncryptionTypeEnum.AES);        //AES (real)
  //var tdes = EncryptionFactory.Create(EncryptionTypeEnum.TRIPLEDES);  //TripleDES (real, weaker but fine for testing)
  //var xor  = EncryptionFactory.Create(EncryptionTypeEnum.XOR);        // XOR (toy, not secure, great for testing swapability)

  #region Manual x1 Swapping ...
  //
  ////Pick a strategy dynamically (from config, UI, or test code)
  //IEncryptionStrategy strategy = aes;   //OPTIONS: aes, tdes, xor
  //
  //var opts = new mcsFileOptions { Encryption = strategy
  //                            ,Encoding   = Encoding.UTF8 };
  //
  //var path = @"C:\temp\manual_encryption.txt";
  //
  ////Write Encrypted File ...
  //mcsFileUtilities.WriteTextFile(path, "Dynamic Encryption - Secret Hello", opts);
  //
  ////Read & Decrypt File (using the same strategy)
  //string decrypted = mcsFileUtilities.ReadTextFile(path, opts);
  //
  ////Output to Console, file content ...
  //Console.WriteLine($"Strategy: {strategy.Name}, Decripted: {decrypted}");
  //
  #endregion

  #region Automated Encryption Swapping ...

  //var strategies = new IEncryptionStrategy[] { aes, tdes, xor };
  //var strategy_types = Enum.GetValues<EncryptionTypeEnum>().ToArray();
  //
  //var test_bytes = new byte[] {1, 2, 3, 4, 5};
  //var path       = @"C:\temp\dynamic_encryption.txt";
  //
  //foreach(var strategy_type in strategy_types)
  //{
  //  var strategy = EncryptionFactory.Create(strategy_type);
  //  var opts = new mcsFileOptions { Encryption = strategy
  //                              ,Logger     = ConsoleLogger };
  //  
  //  //Write Encrypted File ...
  //  mcsFileUtilities.WriteBinaryFile(path, test_bytes, opts);
  //  
  //  //Read & Decrypt File (using the same strategy) ...
  //  var restored = mcsFileUtilities.ReadBinaryFile(path, opts);
  //  
  //  //Output to Console, file content ...
  //  Console.WriteLine($"{strategy.Name} restored bytes: {string.Join(",", restored)}");
  //}

  #endregion

  //---------------------------------------------------------------------------------------------
  //NOTE: Compression & Logging Example ---------------------------------------------------------
  //---------------------------------------------------------------------------------------------
  //var path = @"C:\temp\compression_example.txt";
  //var opts = new mcsFileOptions { Encryption   = NoEncryptionStrategy.Instance
  //                               ,Compression  = CompressionFactory.Create(CompressionTypeEnum.NONE)
  //                               ,Logger       = ConsoleLogger };
  //
  //string long_text = new string('A', 1000000);  //NOTE: highly compressible
  //mcsFileUtilities.WriteTextFile(path, long_text, opts);
  //
  //string roundtrip_text = mcsFileUtilities.ReadTextFile(path, opts);
  //roundtrip_text.Dump("roundtrip_text");

  //---------------------------------------------------------------------------------------------
  //NOTE: Compression, Encryption, & Logging Example --------------------------------------------
  //---------------------------------------------------------------------------------------------
  //var path = @"C:\temp\compression(gzip)_encryption(aes)_example.bin";
  //var opts = new mcsFileOptions { Encryption   = EncryptionFactory.Create(EncryptionTypeEnum.AES)
  //                               ,Compression  = CompressionFactory.Create(CompressionTypeEnum.GZIP)
  //                               ,Logger       = ConsoleLogger };
  //
  //byte[] original = Enumerable.Range(0, 256)
  //                            .Select(i => (byte)i)
  //                            .ToArray();
  //                            
  //mcsFileUtilities.WriteBinaryFile(path, original, opts);
  //
  //byte[] restored = mcsFileUtilities.ReadBinaryFile(path, opts);
  //
  //Console.WriteLine();
  //Console.WriteLine("GZIP+AES binary roundtip OK: " + original.SequenceEqual(restored).ToString().ToUpper());
  //
  #endregion

  #region Misc. Code: DataProcessingModeEnum
  //
  //var at_rest = DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress;
  //
  //((int)at_rest).Dump("((int)at_rest)");
  //at_rest.Dump("at_rest = DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress");
  //
  //var in_transit = DataProcessingModeEnum.Compress;
  //
  //in_transit.Dump("in_transit = DataProcessingModeEnum.Compress");
  //
  //DataProcessingModeEnum mode = (DataProcessingModeEnum)8; // invalid
  //
  //if(!mcsFileUtilities.TryValidateProtectionMode(mode, out var valid_mode))
  //{
  //  Console.WriteLine($"Invalid protection mode '{mode}' detected. Falling back to '{valid_mode}'.");
  //}
  //
  #endregion

  #endregion

  //RunTests();  // Call RunTests() or press Alt+Shift+T to initiate testing.
  //RunTests(filter: test_case => test_case.TestMethod.TestClass.Class.Name == "UserQuery+mcsFileUtilitiesTests");
  
  //RunTests(filter: test_case => test_case.TestMethod.TestClass.Class.Name == "UserQuery+mcsFileUtilitiesTests1");
  //RunTests(filter: test_case => test_case.TestMethod.TestClass.Class.Name == "UserQuery+mcsFileUtilitiesTests2");
  
  //RunTests(filter: test_case => test_case.TestMethod.TestClass.Class.Name == "UserQuerymcsFileUtilitiesPerformanceTests"
  //                           && test_case.TestMethod.Method.Name == "FileRoundTrip_PerformanceTest");
  
  //RunTest("FileRoundTrip_PerformanceTest");
  //RunTest("FileRoundTrip_PerformanceTest");
  //RunTest("FileRoundTrip_WithBuffer_PerformanceTest");

  RunTest("FileRoundTrip_WithoutBuffer_PerformanceTest");
  RunTest("FileRoundTrip_ZeroLength");
  //RunTest("FileToStream_LeaveOpenBehavior");
  //RunTest("FileToStream_MemoryCleanup");
  
  //Util.OnDump += obj => Console.WriteLine($"DUMP: {obj}");
}

/// <summary>
/// Centralized file utility class for text and binary operations. Supports optional encryption, 
/// compression, impersonation, and logging via <see cref="mcsFileOptions"/>.
/// </summary>
public static class mcsFileUtilities
{
  #region TEXT FILES

  /// <summary>
  /// Reads the contents of a text file. Applies options such as encoding, impersonation, encryption, and compression.
  /// </summary>
  /// <param name="path">The full path to the file to read.</param>
  /// <param name="options">Optional <see cref="mcsFileOptions"/> to configure encoding, impersonation, encryption, compression, and logging.</param>
  /// <returns>
  /// The file contents as a string.  
  /// Never returns <c>null</c>; returns <see cref="string.Empty"/> if the file is empty or if a decoding step yields no data.
  /// </returns>
  /// <exception cref="IOException">Thrown if the file cannot be read.</exception>
  /// <exception cref="UnauthorizedAccessException">Thrown if access is denied.</exception>
  /// <exception cref="CryptographicException">Thrown if decryption fails when encryption is enabled.</exception>
  public static string ReadTextFile(string path, mcsFileOptions options = null)
  {
    options ??= new mcsFileOptions();
    
    return ExecuteFileOperation(nameof(ReadTextFile), path, options, () => {      
      var bytes = ReadBinaryFile(path, options);
      return options.Encoding.GetString(bytes);
    });
  }
  
  /// <summary>
  /// Attempts to read the contents of a text file. Returns <c>true</c> if successful, otherwise 
  /// <c>false</c>. Does not throw exceptions.
  /// </summary>
  /// <param name="path">The full path to the file to read.</param>
  /// <param name="value">
  /// Outputs the file contents if successful.  
  /// Never <c>null</c>; will be <see cref="string.Empty"/> if unsuccessful or if the file is empty.
  /// </param>
  /// <param name="options">Optional <see cref="mcsFileOptions"/> to configure encoding, impersonation, encryption, compression, and logging.</param>
  /// <returns><c>true</c> if the file was read successfully; otherwise <c>false</c>.</returns>
  public static bool TryReadTextFile(string path, out string value, mcsFileOptions options = null)
  {
    try
    {
      value = ReadTextFile(path, options);
      return true;
    }
    catch
    {
      value = default;
      return false;
    }
  }

  /// <summary>
  /// Attempts to read the contents of a text file, returning a structured result object. Captures success/failure state, exception details, and metadata.
  /// </summary>
  /// <param name="path">The full path to the file to read.</param>
  /// <param name="options">Optional <see cref="mcsFileOptions"/> to configure encoding, impersonation, encryption, compression, and logging.</param>
  /// <returns>
  /// A <see cref="FileOperationResult{T}"/> containing the file contents if successful, or failure details including the exception if unsuccessful.
  /// </returns>
  public static FileOperationResult<string> TryReadTextFile(string path, mcsFileOptions options = null)
  {
    try
    {	        
      var result = ReadTextFile(path, options);
      return FileOperationResult<string>.AsSuccess(result, nameof(TryReadTextFile), path);
    }
    catch(Exception ex)
    {
      return FileOperationResult<string>.AsFailure(ex, nameof(TryReadTextFile), path);
    }
  }

  /// <summary>
  /// Writes text content to a file. Applies options such as encoding, impersonation, encryption, and compression.
  /// </summary>
  /// <param name="path">The full path to the file to write.</param>
  /// <param name="content">The text content to write. If null, an empty string is written.</param>
  /// <param name="options">Optional <see cref="mcsFileOptions"/> to configure encoding, impersonation, encryption, compression, overwrite, and logging.</param>
  /// <exception cref="IOException">Thrown if the file exists and overwrite is disabled.</exception>
  /// <exception cref="UnauthorizedAccessException">Thrown if access is denied.</exception>
  /// <exception cref="CryptographicException">Thrown if encryption fails when encryption is enabled.</exception>
  public static void WriteTextFile(string path, string content, mcsFileOptions options = null)
  {
    options ??= new mcsFileOptions();
    
    ExecuteFileOperation(nameof(WriteTextFile), path, options, () => {
      var bytes = options.Encoding.GetBytes(content ?? string.Empty);
      WriteBinaryFile(path, bytes, options);
    });    
  }

  #endregion
  
  #region BINARY FILES

  /// <summary>
  /// Reads the contents of a binary file as a byte array. Applies options such as impersonation, encryption, and compression.
  /// </summary>
  /// <param name="path">The full path to the file to read.</param>
  /// <param name="options">Optional <see cref="mcsFileOptions"/> to configure impersonation, encryption, compression, and logging.</param>
  /// <returns>
  /// The file contents as a byte array.  
  /// Never returns <c>null</c>; returns an empty array (<see cref="Array.Empty{Byte}"/>) if the file is empty 
  /// or if a decoding step yields no data.
  /// </returns>
  /// <exception cref="IOException">Thrown if the file cannot be read.</exception>
  /// <exception cref="UnauthorizedAccessException">Thrown if access is denied.</exception>
  /// <exception cref="CryptographicException">Thrown if decryption fails when encryption is enabled.</exception>
  public static byte[] ReadBinaryFile(string path, mcsFileOptions options = null)
  {
    if(path == null)
      throw new ArgumentNullException(nameof(path));
    
    var now = DateTime.UtcNow;
    
    options ??= new mcsFileOptions();
    
    return ExecuteFileStreamOperation(nameof(ReadBinaryFile), path, options, () => {
      EnsureReadable(path, options);

      using var ms = new MemoryStream();
      using(var file = new FileStream( path
                                      ,FileMode.Open
                                      ,FileAccess.Read
                                      ,options.FileReadShare
                                      ,options.FileBufferSize
                                      ,options.FileReadOptions ))
      {
        if(file.Length == 0)
        {
          options?.Logger?.Invoke(new FileLogEntry { Timestamp = now
                                                    ,Level = LogLevelEnum.INFORMATION
                                                    ,Operation = nameof(ReadBinaryFile)
                                                    ,Message = $"Empty source file detected, returning empty array (AtRest={options.AtRest})"
                                                    ,AtRest = options.AtRest
                                                    ,Success = true });
                                                    
          return (0, Array.Empty<byte>());
        }
        
        using var pipeline = BuildReadPipeline(file, options, options.AtRest, leave_open: true);
        
        pipeline.CopyTo(ms, options.FileBufferSize);
        ms.Flush();
        
        options?.Logger?.Invoke(new FileLogEntry { Timestamp = now
                                                  ,Level     = LogLevelEnum.INFORMATION
                                                  ,Operation = nameof(ReadBinaryFile)
                                                  ,Message   = $"Read {ms.Length} bytes from {path} (AtRest={options.AtRest})"
                                                  ,AtRest    = options.AtRest
                                                  ,Success   = true });
        return (file.Length, ms.ToArray());        
      }
    }).Item2; //Converts the tuple of (int, byte[]) to simply byte[]
    
    #region COMMENTED OUT: original code, access data via File.ReadAllBytes
    //
    //options ??= new mcsFileOptions();
    //
    //return ExecuteFileOperation(nameof(ReadBinaryFile), path, options, () => {
    //  var raw_bytes    = File.ReadAllBytes(path);
    //  var decrypted    = options.Encryption?.Decrypt(raw_bytes) ?? raw_bytes;
    //  var decompressed = options.Compression?.Decompress(decrypted) ?? decrypted;
    //  
    //  return decompressed ?? Array.Empty<byte>();
    //});
    //
    #endregion
  }

  /// <summary>
  /// Attempts to read the contents of a binary file. Returns <c>true</c> if successful, otherwise 
  /// <c>false</c>. Does not throw exceptions.
  /// </summary>
  /// <param name="path">The full path to the file to read.</param>
  /// <param name="value">
  /// Outputs the file contents as a byte array if successful.  
  /// Never <c>null</c>; will be an empty array (<see cref="Array.Empty{Byte}"/>) if unsuccessful or if the file is empty.
  /// </param>
  /// <param name="options">Optional <see cref="mcsFileOptions"/> to configure impersonation, encryption, compression, and logging.</param>
  /// <returns><c>true</c> if the file was read successfully; otherwise <c>false</c>.</returns>
  public static bool TryReadBinaryFile(string path, out byte[] value, mcsFileOptions options = null)
  {
    try
    {	        
      value = ReadBinaryFile(path, options);
      return true;
    }
    catch
    {
      value = default;
      return false;
    }
  }

  /// <summary>
  /// Attempts to read the contents of a binary file, returning a structured result object. Captures success/failure state, exception 
  /// details, and metadata.
  /// </summary>
  /// <param name="path">The full path to the file to read.</param>
  /// <param name="options">Optional <see cref="mcsFileOptions"/> to configure impersonation, encryption, compression, and logging.</param>
  /// <returns>
  /// A <see cref="FileOperationResult{T}"/> containing the file contents as a byte array if successful, or failure details including the
  /// exception if unsuccessful.
  /// </returns>
  public static FileOperationResult<byte[]> TryReadBinaryFile(string path, mcsFileOptions options = null)
  {
    try
    {
      var result = ReadBinaryFile(path, options);
      return FileOperationResult<byte[]>.AsSuccess(result, nameof(ReadBinaryFile), path);
    }
    catch (Exception ex)
    {
      return FileOperationResult<byte[]>.AsFailure(ex, nameof(ReadBinaryFile), path);
    }
  }

  /// <summary>
  /// Writes binary data to a file. Applies options such as impersonation, encryption, and compression.
  /// </summary>
  /// <param name="destination">The full path to the file to write.</param>
  /// <param name="data">The binary data to write. Cannot be null.</param>
  /// <param name="options">Optional <see cref="mcsFileOptions"/> to configure impersonation, encryption, compression, overwrite, and logging.</param>
  /// <exception cref="IOException">Thrown if the file exists and overwrite is disabled.</exception>
  /// <exception cref="UnauthorizedAccessException">Thrown if access is denied.</exception>
  /// <exception cref="CryptographicException">Thrown if encryption fails when encryption is enabled.</exception>
  public static void WriteBinaryFile(string destination, byte[] data, mcsFileOptions options = null)
  {
    if(destination == null)
      throw new ArgumentNullException(nameof(destination));
      
    if(data == null)
      throw new ArgumentNullException(nameof(data));
    
    options ??= new mcsFileOptions();

    ExecuteFileStreamOperation(nameof(WriteBinaryFile), destination, options, () => {
      EnsureWritable(destination, options);
      
      ValidateProtectionMode(options.AtRest);
      ValidateStrategies(options, options.AtRest);
      
      var now = DateTime.UtcNow;
      long? final_position;
      
      using(var file = new FileStream( destination
                                      ,options.Overwrite ? FileMode.Create : FileMode.CreateNew
                                      ,FileAccess.Write
                                      ,options.FileWriteShare
                                      ,options.FileBufferSize
                                      ,options.FileWriteOptions ))
      {
        if(data.Length == 0)
        {
          options?.Logger?.Invoke(new FileLogEntry { Timestamp = now
                                                    ,Level     = LogLevelEnum.INFORMATION
                                                    ,Operation = nameof(WriteBinaryFile)
                                                    ,Message   = $"Empty input detected, writing zero-length file (AtRest={options.AtRest})"
                                                    ,AtRest    = options.AtRest
                                                    ,Success   = true });
          
          file.SetLength(0);
          final_position = 0;
        }
        else
        {
          using var pipeline = BuildWritePipeline(file, options, options.AtRest, leave_open: false);
          
          pipeline.Write(data, 0, data.Length);
          pipeline.Flush();
          
          file.SetLength(file.Position);
          final_position = file.Position;

          options?.Logger?.Invoke(new FileLogEntry { Timestamp = now
                                                    ,Level     = LogLevelEnum.INFORMATION
                                                    ,Operation = nameof(WriteBinaryFile)
                                                    ,Message   = $"Wrote {data.Length} bytes, final file size={final_position} (AtRest={options.AtRest})"
                                                    ,AtRest = options.AtRest
                                                    ,Success = true });
        }
      }
                                      
      return (data.Length, final_position);
    });
    
    #region COMMENTED OUT: original code
    //
    //ExecuteFileOperation(nameof(WriteBinaryFile), path, options, () => {
    //  EnsureWritable(path, options);
    //  
    //  var compressed  = options.Compression?.Compress(data) ?? data ?? Array.Empty<byte>();
    //  var final_bytes = options.Encryption?.Encrypt(compressed) ?? compressed;
    //  
    //  File.WriteAllBytes(path, final_bytes);
    //});
    //
    #endregion
  }

  #endregion
  
  #region TEXT/BINARY in-memory <-> Stream
  
  public static string ReadTextFromStream(Stream input, mcsFileOptions options, bool leave_open = true)
  {
    if(input == null)
      throw new ArgumentNullException(nameof(input));    

    options ??= new mcsFileOptions();

    return ExecuteStreamOperation(nameof(ReadTextFromStream), options, () =>
    {
      //Reuse the binary method/logic
      var bytes = ReadBinaryFromStream(input, options, leave_open);
      
      //Decode using configure encoding
      return options.Encoding.GetString(bytes);
    });
  }
  
  public static bool TryReadTextFromStream(Stream input, mcsFileOptions options, out string result, bool leave_open = true)
  {
    if(input == null) 
      throw new ArgumentNullException(nameof(input));
      
    options ??= new mcsFileOptions();

    try
    {
      result = ReadTextFromStream(input, options, leave_open);
      return true;
    }
    catch
    {
      result = default;
      return false;
    }
  }
  
  public static StreamOperationResult<string> TryReadTextFromStream(Stream input, mcsFileOptions options, bool leave_open = true)
  {
    if(input == null) 
      throw new ArgumentNullException(nameof(input));
      
    options ??= new mcsFileOptions();

    //Reuse the binary method/logic
    var binary_result = TryReadBinaryFromStream(input, options, leave_open);
    
    if(binary_result.IsSuccess)
    {
      //Convert the byte array into a string
      var text = options.Encoding.GetString(binary_result.Value);
      return StreamOperationResult<string>.AsSuccess(text, nameof(TryReadTextFromStream), binary_result.BytesTransferred, binary_result.FinalPosition);
    }
    
    //Bubble up failure result with same metadata
    return StreamOperationResult<string>.AsFailure(binary_result.Exception, nameof(TryReadTextFromStream), binary_result.BytesTransferred, binary_result.FinalPosition);
  }
  
  public static void WriteTextToStream(Stream output, string text, mcsFileOptions options, bool leave_open = true)
  {
    if(output == null)
      throw new ArgumentNullException(nameof(output));
    
    if(text == null)
      throw new ArgumentNullException(nameof(text));

    ExecuteStreamOperation(nameof(WriteTextToStream), options, () => {
      var data = options.Encoding.GetBytes(text);
      WriteBinaryToStream(output, data, options, leave_open);
    });
  }
  
  public static byte[] ReadBinaryFromStream(Stream input, mcsFileOptions options, bool leave_open = true)
  {
    if(input == null)
      throw new ArgumentNullException(nameof(input));
    
    options ??= new mcsFileOptions();

    return ExecuteStreamOperation(nameof(ReadBinaryFromStream), options, () => {
      ValidateProtectionMode(options.InTransit);
      ValidateStrategies(options, options.InTransit);
      
      using var pipeline = BuildReadPipeline(input, options, options.InTransit);
      using var ms = new MemoryStream();
      
      pipeline.CopyTo(ms, options.FileBufferSize);
      
      return ms.ToArray();
    });
  }
  
  public static bool TryReadBinaryFromStream(Stream input, mcsFileOptions options, out byte[] result, bool leave_open = true)
  {
    if(input == null) 
      throw new ArgumentNullException(nameof(input));
    
    options ??= new mcsFileOptions();
    
    try
    {	        
      result = ReadBinaryFromStream(input, options, leave_open);
      return true;
    }
    catch
    {
      result = default;
      return false;
    }
  }
  
  public static StreamOperationResult<byte[]> TryReadBinaryFromStream(Stream input, mcsFileOptions options, bool leave_open = true)
  {
    if(input == null) 
      throw new ArgumentNullException(nameof(input));
    
    options ??= new mcsFileOptions();
    
    try
    {	        
      var starting_position = input.CanSeek ? input.Position : (long?)null;
      
      var data = ReadBinaryFromStream(input, options, leave_open);
      
      var ending_position = input.CanSeek ? input.Position : (long?)null;
      
      var bytes_written = ending_position.HasValue && starting_position.HasValue
                        ? ending_position.Value - starting_position.Value
                        : data.Length;
      
      return StreamOperationResult<byte[]>.AsSuccess(data, nameof(TryReadBinaryFromStream), bytes_written, ending_position);
    }
    catch(Exception ex)
    {
      var current_position = input.CanSeek ? input.Position : (long?)null;
      
      return StreamOperationResult<byte[]>.AsFailure(ex, nameof(TryReadBinaryFromStream), 0, current_position);
    }
  }
  
  public static void WriteBinaryToStream(Stream output, byte[] data, mcsFileOptions options, bool leave_open = true)
  {
    if(output == null) throw new ArgumentNullException(nameof(output));
    if(data   == null) throw new ArgumentNullException(nameof(data));
    
    options ??= new mcsFileOptions();

    ExecuteStreamOperation(nameof(WriteBinaryToStream), options, () => {
      ValidateProtectionMode(options.InTransit);
      ValidateStrategies(options, options.InTransit);
    
      using var pipeline = BuildWritePipeline(output, options, options.InTransit);
      
      pipeline.Write(data, 0, data.Length);
      pipeline.Flush();
    });
  }

  #endregion

  #region BRIDGE Method(s)

  /// <summary>Reads a file from a stream → write it to disk</summary>
  public static void FileFromStream(Stream input, string destination, mcsFileOptions options, bool leave_open = true)
  {
    if(input == null)
      throw new ArgumentNullException(nameof(input));
      
    if(destination == null)
      throw new ArgumentNullException(nameof(destination));
    
    options ??= new mcsFileOptions();
    var now = DateTime.UtcNow;
    
    ExecuteFileStreamOperation(nameof(FileFromStream), destination, options, () => {
      EnsureWritable(destination, options);
      
      ValidateProtectionMode(options.InTransit);
      ValidateProtectionMode(options.AtRest);
      
      ValidateStrategies(options, options.InTransit);
      ValidateStrategies(options, options.AtRest);
      
      #region COMMENTED OUT: attempted fix
      
      long? final_position;
      
      using(var file = new FileStream( destination
                                      ,options.Overwrite ? FileMode.Create : FileMode.CreateNew
                                      ,FileAccess.Write
                                      ,options.FileWriteShare
                                      ,options.FileBufferSize
                                      ,options.FileWriteOptions ))
      {
        if(input.CanSeek && input.Length == 0)
        {
          options?.Logger?.Invoke(new FileLogEntry { Timestamp = now
                                                    ,Level     = LogLevelEnum.INFORMATION
                                                    ,Operation = nameof(FileFromStream)
                                                    ,Message   = $"Empty input stream detected, writing zero-length file (AtRest={options.AtRest}, InTransit={options.InTransit})"
                                                    ,AtRest    = options.AtRest
                                                    ,InTransit = options.InTransit
                                                    ,Success   = true });
                                                   
          file.SetLength(0);
          final_position = 0;
          
          return (0, final_position);
        }
        
        using var transit = BuildReadPipeline(input, options, options.InTransit, leave_open);
        using(var atRest = BuildWritePipeline(file, options, options.AtRest, leave_open: true))
        {
          //Console.WriteLine($"[DEBUG - FileFromStream] Before CopyTo: transit.CanRead={transit.CanRead}, atRest.CanWrite={atRest.CanWrite}");
          transit.CopyTo(atRest, options.FileBufferSize);
          //Console.WriteLine($"[DEBUG - FileFromStream] After CopyTo: fileLength={file.Length}, filePosition={(file.CanSeek ? file.Position.ToString() : "N/A")}");
          
          atRest.Flush();
          
          file.Flush(true); // Force flush to disk
          final_position = file.Position;
        }
        
        options?.Logger?.Invoke(new FileLogEntry { Timestamp  = now
                                                  ,Level      = LogLevelEnum.INFORMATION
                                                  ,Operation  = nameof(FileFromStream)
                                                  ,Message    = $"Wrote {final_position} bytes to {destination} (AtRest={options.AtRest}, InTransit={options.InTransit})"
                                                  ,AtRest     = options.AtRest
                                                  ,InTransit  = options.InTransit
                                                  ,Success    = true });
      }
      
      return (input.CanSeek ? input.Length : 0, final_position);
      
      #endregion
      
      #region COMMENTED OUT: original code
      //
      //using var transit = BuildReadPipeline(input, options, options.InTransit, leave_open);
      //
      //using var file = new FileStream( destination
      //                                ,options.Overwrite ? FileMode.Create : FileMode.CreateNew
      //                                ,FileAccess.Write
      //                                ,options.FileWriteShare
      //                                ,options.FileBufferSize
      //                                ,options.FileWriteOptions );
      //
      //using var atRest = BuildWritePipeline(file, options, options.AtRest, leave_open);
      //
      //long starting_position = file.CanSeek ? file.Position : 0;
      //
      ////Console.WriteLine($"[DEBUG - FileFromStream] Before CopyTo: transit.CanRead={transit.CanRead}, atRest.CanWrite={atRest.CanWrite}");
      //transit.CopyTo(atRest, options.FileBufferSize);
      ////Console.WriteLine($"[DEBUG - FileFromStream] After CopyTo: fileLength={file.Length}, filePosition={(file.CanSeek ? file.Position.ToString() : "N/A")}");
      //
      //atRest.Flush();
      //file.Flush();
      //
      ////NOTE: was a suggestion to help with possible extra-bytes being written.
      ////file.SetLength(file.Position);
      //
      //long? final_position = file.CanSeek ? file.Position : (long?)null;
      //
      //long bytes_transferred = final_position.HasValue 
      //                       ? final_position.Value - starting_position 
      //                       : file.Length;
      //                       
      //return (bytes_transferred, final_position);
      //
      #endregion
    });
  }
  
  /// <summary>Read from a physical file → write it to stream</summary>
  public static void FileToStream(string source, Stream output, mcsFileOptions options, bool leave_open = true)
  {
    options ??= new mcsFileOptions();

    ExecuteFileStreamOperation(nameof(FileToStream), source, options, () => {
      EnsureReadable(source, options);
      
      ValidateProtectionMode(options.AtRest);
      ValidateProtectionMode(options.InTransit);
      
      ValidateStrategies(options, options.AtRest);
      ValidateStrategies(options, options.InTransit);
      
      #region COMMENTED OUT: attempted fix
      //
      //long? final_position;
      //
      //using(var file = new FileStream( source
      //                                ,FileMode.Open
      //                                ,FileAccess.Read
      //                                ,options.FileReadShare
      //                                ,options.FileBufferSize
      //                                ,options.FileReadOptions ))
      //{
      //  using var atRest = BuildReadPipeline(file, options, options.AtRest, leave_open);
      //  using(var transit = BuildWritePipeline(output, options, options.InTransit, leave_open))
      //  {
      //    Console.WriteLine($"[DEBUG - FileToStream] Before CopyTo: atRest.CanRead={atRest.CanRead}, transit.CanWrite={transit.CanWrite}");
      //    atRest.CopyTo(transit, options.FileBufferSize);
      //    Console.WriteLine($"[DEBUG - FileToStream] After CopyTo: sourceLength={file.Length}, outputPosition={(output.CanSeek ? output.Position.ToString() : "N/A")}");
      //
      //    transit.Flush();
      //    final_position = output.CanSeek ? output.Position : (long?)null;
      //  }
      //}
      //
      //var file_info = new FileInfo(source);
      //return (file_info.Length, final_position);
      //
      #endregion
      
      using var file = new FileStream( source
                                      ,FileMode.Open
                                      ,FileAccess.Read
                                      ,options.FileReadShare
                                      ,options.FileBufferSize
                                      ,options.FileReadOptions );
      
      using var atRest  = BuildReadPipeline(file, options, options.AtRest, leave_open);
      using var transit = BuildWritePipeline(output, options, options.InTransit, leave_open);
      
      long start_position = output.CanSeek ? output.Position : 0;
      
      //Console.WriteLine($"[DEBUG - FileToStream] Before CopyTo: atRest.CanRead={atRest.CanRead}, transit.CanWrite={transit.CanWrite}");
      atRest.CopyTo(transit, options.FileBufferSize);
      //Console.WriteLine($"[DEBUG - FileToStream] After CopyTo: sourceLength={file.Length}, outputPosition={(output.CanSeek ? output.Position.ToString() : "N/A")}");
      
      transit.Flush();
      output.Flush();
      
      long? final_position = output.CanSeek ? output.Position : (long?)null;
      
      long bytes_transferred = final_position.HasValue 
                             ? final_position.Value - start_position 
                             : file.Length;
      
      return (bytes_transferred, final_position);
    });
  }
  
  #endregion
  
  #region INTERNAL HELPERS
  
  private static bool IsDevelopment()
  {
    var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
    return string.Equals(env, "Development", StringComparison.OrdinalIgnoreCase);
  }
  
  private static void EnsureWritable(string path, mcsFileOptions options)
  {
    //Argument 'null' checks ...
    if(string.IsNullOrWhiteSpace(path))
    {
      options?.Logger?.Invoke(new LogEntry { Message   = $"Argument '{nameof(path)}' was either null or whitespaces."
                                            ,Level     = LogLevelEnum.DEBUG
                                            ,Timestamp = DateTime.UtcNow });
      
      throw new ArgumentNullException(nameof(path));
    }
    
    if(options == null)
    {
      options?.Logger?.Invoke(new LogEntry { Message   = $"Argument '{nameof(options)}' was null."
                                            ,Level     = LogLevelEnum.DEBUG
                                            ,Timestamp = DateTime.UtcNow });
      
      throw new ArgumentNullException(nameof(options));
    }
    
    //Overwrite check ...
    if(!options.Overwrite && File.Exists(path))
      throw new IOException($"File exists and Overwrite=false: {path}");
    
    //Ensure directory exists ...
    var dir = Path.GetDirectoryName(Path.GetFullPath(path));
    
    if(!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
      Directory.CreateDirectory(dir);
  }
  
  private static void EnsureReadable(string path, mcsFileOptions options)
  {
    if(string.IsNullOrWhiteSpace(path))
    {
      options?.Logger?.Invoke(new LogEntry { Message   = $"Argument '{nameof(path)}' was either null or whitespaces."
                                            ,Level     = LogLevelEnum.DEBUG
                                            ,Timestamp = DateTime.UtcNow });
      
      throw new ArgumentNullException(nameof(path));
    }
    
    if(options == null)
    {
      options?.Logger?.Invoke(new LogEntry { Message   = $"Argument '{nameof(options)}' was null."
                                            ,Level     = LogLevelEnum.DEBUG
                                            ,Timestamp = DateTime.UtcNow });
      
      throw new ArgumentNullException(nameof(options));
    }
    
    if(!File.Exists(path))
    {
      var ex = new FileNotFoundException();

      options?.Logger?.Invoke(new LogEntry { Message   = ex.Message
                                            ,Level     = LogLevelEnum.ERROR
                                            ,Timestamp = DateTime.UtcNow });
      throw ex;
    }
    
    //try opening it to confirm access rights ...
    try
    {
      using var stream = new FileStream( path
                                        ,FileMode.Open
                                        ,FileAccess.Read
                                        ,options.FileReadShare
                                        ,bufferSize: 1
                                        ,options.FileReadOptions );
    }
    catch(Exception ex)
    {
      options?.Logger?.Invoke(new LogEntry { Message   = $"File '{path}' exists but could not be opened for read access. {ex.Message}"
                                            ,Level     = LogLevelEnum.ERROR
                                            ,Timestamp = DateTime.UtcNow });
      throw;
    }
  }
  
  private static void ValidateProtectionMode(DataProcessingModeEnum mode)
  {
    //NOTE: Checks to see if 'mode' contains any bits outside of 'Encrypt' or 'Compress'; if it
    //      does, reject it. Helps to guard against something like this: 'mode = (DataProcessingModeEnum)99'
    const DataProcessingModeEnum all_vaild_flags = DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress;
    
    //NOTE: 'None (0)' is always valid
    if((mode & ~all_vaild_flags) != 0)
      throw new ArgumentOutOfRangeException( nameof(mode)
                                            ,mode
                                            ,"Unsupported protection mode flags were specified; " + 
                                             "valid values are: None, Encrypt, Compress, or Encrypt | Compress." );
  }
  
  public static bool TryValidateProtectionMode(DataProcessingModeEnum mode, out DataProcessingModeEnum valid_mode)
  {
    //NOTE: Checks to see if 'mode' contains any bits outside of 'Encrypt' or 'Compress'; if it
    //      does, reject it. Helps to guard against something like this: 'mode = (DataProcessingModeEnum)99'
    const DataProcessingModeEnum all_vaild_flags = DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress;
    
    //NOTE: 'None (0)' is always valid
    if((mode & ~all_vaild_flags) == 0)
    {
      valid_mode = mode;
      return true;
    }
    
    valid_mode = DataProcessingModeEnum.None;
    return false;
  }
  
  private static void ValidateStrategies(mcsFileOptions options, DataProcessingModeEnum mode)
  {
    if(options.Compression == null)
    {
      var message = "mcsFileOptions.Compression must not be null. Use NoCompressionStrategy.Instance if no compression is desired.";
      throw new ArgumentNullException(nameof(options.Compression), message);
    }
    
    if(options.Encryption == null)
    {
      var message = "mcsFileOptions.Encryption must not be null. Use NoEncryptionStrategy.Instance if no encryption is desired.";
      throw new ArgumentNullException(nameof(options.Encryption), message);
    }
    
    //Compression: can always be No-Op (no compression strategy, that's allowed)
    //Encryption: stricter, No-Op is only allowed if Encrypt flag is not set.
    if(mode.HasFlag(DataProcessingModeEnum.Encrypt) && options.Encryption.IsNoOp && !IsDevelopment())
    {
      var message = "Encryption was requested (mode includes Encrypt), but a no-op encryption strategy is configured. " +
                    "This is only permitted in Development environments.";
      
      throw new InvalidOperationException(message);
    }
  }
  
  private static void ExecuteFileOperation(string operation_name, string path, mcsFileOptions options, Action operation)
    => ExecuteFileOperation(operation_name, path, options, () => { operation(); return true; });
  
  private static T ExecuteFileOperation<T>(string operation_name, string path, mcsFileOptions options, Func<T> operation)
  {
    if(string.IsNullOrWhiteSpace(operation_name))
      throw new ArgumentNullException(nameof(operation_name));
    
    if(string.IsNullOrWhiteSpace(path))
      throw new ArgumentNullException(nameof(path));
    
    options ??= new mcsFileOptions();
    
    var start_time = DateTime.UtcNow;
    
    try
    {	        
      var result = RunWithImpersonation(options, operation);
      var now    = DateTime.UtcNow;
      
      options?.Logger?.Invoke(new FileLogEntry { Operation     = operation_name
                                                ,Path          = path
                                                
                                                ,AtRest        = options.AtRest
                                                ,InTransit     = options.InTransit
                                                
                                                ,Compression   = options.Compression?.Name ?? nameof(CompressionTypeEnum.NONE)
                                                ,Encryption    = options.Encryption?.Name  ?? nameof(EncryptionTypeEnum.NONE)
                                                ,Impersonator  = options.Impersonator?.ToString()
                                                
                                                ,Success       = true
                                                ,Level         = LogLevelEnum.INFORMATION
                                                ,Message       = $"{operation_name} succeeded for {Path.GetFileName(path)}"
                                                ,Timestamp     = now
                                                ,Duration      = now - start_time });
                                               
      return result;
    }
    catch(Exception ex)
    {
      var now = DateTime.UtcNow;
      
      options?.Logger?.Invoke( new FileLogEntry { Operation    = operation_name
                                                 ,Path         = path
                                                 
                                                 ,AtRest       = options.AtRest
                                                 ,InTransit    = options.InTransit
                                                 
                                                 ,Compression  = options.Compression?.Name ?? nameof(CompressionTypeEnum.NONE)
                                                 ,Encryption   = options.Encryption?.Name  ?? nameof(EncryptionTypeEnum.NONE)
                                                 ,Impersonator = options.Impersonator?.ToString()
                                                 
                                                 ,Success      = false
                                                 ,Level        = LogLevelEnum.EXCEPTION
                                                 ,Exception    = ex
                                                 ,Timestamp    = now
                                                 ,Duration     = now - start_time
                                                 
                                                 ,Message      = $"{operation_name} failed for {Path.GetFileName(path)}: {ex.Message}" });
      
      throw;
    }
  }
  
  private static void ExecuteStreamOperation(string operation_name, mcsFileOptions options, Action operation)
    => ExecuteStreamOperation(operation_name, options, () => { operation(); return true; });
  
  private static T ExecuteStreamOperation<T>(string operation_name, mcsFileOptions options, Func<T> operation)
  {
    if(string.IsNullOrWhiteSpace(operation_name))
      throw new ArgumentNullException(nameof(operation_name));
    
    options ??= new mcsFileOptions();
    
    var start_time = DateTime.UtcNow;
    
    try
    {
      var value = RunWithImpersonation(options, operation);
      var now   = DateTime.UtcNow;

      options?.Logger?.Invoke(new LogEntry { Message   = $"Operation '{operation_name}' succeeded."
                                            ,Level     = LogLevelEnum.INFORMATION
                                            ,Timestamp = now
                                            ,Duration  = now - start_time } );
      return value;
    }
    catch(Exception ex)
    {
      var now = DateTime.UtcNow;

      options?.Logger?.Invoke(new LogEntry { Message   = $"Operation '{operation_name}' failed. Message: {ex.Message}"
                                            ,Level     = LogLevelEnum.ERROR
                                            ,Timestamp = now
                                            ,Duration  = now - start_time } );
      throw;
    }
  }
  
  ///<summary>
  /// Executes a file/stream hybrid operation with impersonation, protection validation, and SOC2-compliant logging.
  /// </summary>
  private static void ExecuteFileStreamOperation(string operation_name, string path, mcsFileOptions options, Action operation)
    => ExecuteFileStreamOperation<object>(operation_name, path, options, () => { operation(); return null; });
  
  /// <summary>
  /// Executes a file/stream hybrid operation with impersonation, protection validation, and SOC2-compliant 
  /// logging. The operation body must return metrics for logging.
  /// </summary>
  private static T ExecuteFileStreamOperation<T>(string operation_name, string path, mcsFileOptions options, Func<T> operation )
  {
    if(string.IsNullOrWhiteSpace(operation_name))
      throw new ArgumentNullException(nameof(operation_name));
      
    if(string.IsNullOrWhiteSpace(path))
      throw new ArgumentNullException(nameof(path));
    
    options ??= new mcsFileOptions();
      
    var start_time = DateTime.UtcNow;
    
    try
    {
      //Run operation under impersonation if configured ...
      var result = RunWithImpersonation(options, operation);

      // Support logging extra metrics if T is a (long, long?) tuple
      long bytes = 0;
      long? position = null;

      //Extract logging metrics
      if(result is ValueTuple<long, long?> metrics)
      {
        bytes    = metrics.Item1;
        position = metrics.Item2;
      }
      
      //Logging success, if configured ...
      var now = DateTime.UtcNow;

      options?.Logger?.Invoke(new FileLogEntry { Operation  = operation_name
                                                ,Path       = path
                                                
                                                ,AtRest     = options.AtRest
                                                ,InTransit  = options.InTransit
                                                
                                                ,Compression  = options.Compression?.Name ?? nameof(CompressionTypeEnum.NONE)
                                                ,Encryption   = options.Compression?.Name ?? nameof(EncryptionTypeEnum.NONE)
                                                ,Impersonator = options?.Impersonator?.ToString()
                                                
                                                ,Success    = true
                                                ,Level      = LogLevelEnum.INFORMATION
                                                ,Duration   = now - start_time 
                                                ,Timestamp  = now
                                                
                                                ,Message    = $"Operation '{operation_name}' succeeded for {Path.GetFileName(path)}"
                                                            + (bytes > 0 ? $" with {bytes} bytes transferred" : "")
                                                            + (position.HasValue ? $", final position {position}." : ".") });
      return result;
    }
    catch(Exception ex)
    {
      var now = DateTime.UtcNow;

      options?.Logger?.Invoke(new FileLogEntry { Operation  = operation_name
                                                ,Path       = path
                                                
                                                ,AtRest     = options.AtRest
                                                ,InTransit  = options.InTransit
                                                
                                                ,Compression  = options.Compression?.Name ?? nameof(CompressionTypeEnum.NONE)
                                                ,Encryption   = options.Compression?.Name ?? nameof(EncryptionTypeEnum.NONE)
                                                ,Impersonator = options?.Impersonator?.ToString()
                                                
                                                ,Success    = false
                                                ,Level      = LogLevelEnum.EXCEPTION
                                                ,Duration   = now - start_time 
                                                ,Timestamp  = now
                                                
                                                ,Exception  = ex
                                                ,Message    = $"{operation_name} failed for {Path.GetFileName(path)}: Message: {ex.Message}" });
      throw;
    }
  }
  
  private static T RunWithImpersonation<T>(mcsFileOptions options, Func<T> func)
  {
    if(options.Impersonator != null)
    {
      return options.Impersonator.Run(func);
    }
    else
    {
      return func();
    }
  }

  /// <summary>
  /// Builds a read pipeline sourcing from an initial stream (e.g., file or network). You READ PLAINTEXT from the returned stream.
  /// Data from initialSource is decrypted, then decompressed, then you read plaintext.
  /// </summary>
  /// <remarks>
  /// The returned stream yields PLAINTEXT data. As data flows through:
  /// <list type="number">
  ///   <item><description>The ciphertext is first decrypted (if <see cref="mcsFileOptions.Encryption"/> is configured).</description></item>
  ///   <item><description>Then the decrypted bytes are decompressed (if <see cref="mcsFileOptions.Compression"/> is configured).</description></item>
  ///   <item><description>The result is plaintext, which can be read from the returned stream.</description></item>
  /// </list>
  /// <para>
  /// This means the pipeline applies transforms in the order: <c>Decrypt → Decompress → Plaintext</c>.
  /// </para>
  /// <para>
  /// Note: While the wrapping order in code resembles <see cref="BuildWritePipeline"/>, the data flow
  /// direction is reversed. This ensures that decryption and decompression properly invert encryption
  /// and compression performed on write.
  /// </para>
  /// </remarks>
  /// <param name="source">The source stream containing ciphertext and/or compressed data.</param>
  /// <param name="options">The file options containing encryption and compression strategies.</param>
  /// <returns>
  /// A wrapped <see cref="Stream"/> from which PLAINTEXT can be read. Disposing the returned
  /// stream will close any transform streams (unless <c>leaveOpen</c> was specified).
  /// </returns>
  private static Stream BuildReadPipeline(Stream source, mcsFileOptions options, DataProcessingModeEnum mode, bool leave_open = true)
  {
    //Pipeline Order: Source → Decrypt → Decompress → Plaintext
    if(source == null) throw new ArgumentNullException(nameof(source));
    
    var now = DateTime.UtcNow;
    
    if(!TryValidateProtectionMode(mode, out var valid_mode))
    {
      options?.Logger
             ?.Invoke(new LogEntry { Message   = $"Invalid protection mode '{mode}' detected. Falling back to 'None'."
                                    ,Level     = LogLevelEnum.WARNING
                                    ,Timestamp = now });
    }
    
    mode = valid_mode;
    options ??= new mcsFileOptions();
    
    ValidateStrategies(options, mode);
    
    //Stream current = source;
    Stream current = new LeaveOpenStream(source);
    
    if(source.CanSeek && source.Length == 0)
    {
      options.Logger?.Invoke(new FileLogEntry { Timestamp = now
                                               ,Level     = LogLevelEnum.INFORMATION
                                               ,Operation = nameof(BuildReadPipeline)
                                               ,Message = $"Empty source stream detected, returning unprocessed stream (Mode={mode})"
                                               ,AtRest = mode
                                               ,Success = true });
      
      return leave_open ? new LeaveOpenStream(source) : source;      
    }
    
    //NOTE: First unwrap encryption (ciphertext → compressed plaintext); decrypt first
    //      (ciphertext → plaintext-ish stream)
    if(mode.HasFlag(DataProcessingModeEnum.Encrypt))
      current = options.Encryption.Decrypt(current, leave_open: false);
    
    //NOTE: Then unwrap compression (compressed plaintext → plaintext); decompress 
    //      (plaintext compressed → plaintext)
    if(mode.HasFlag(DataProcessingModeEnum.Compress))
      current = options.Compression.Decompress(current, leave_open: false);
    
//    //REQUIRED: catches edge cases where either encryption or compression are configured 
//    //          with no-op strategies, it honors leave_open
//    if(leave_open && current == source)
//      return new LeaveOpenStream(source);
//    
    //REQUIRED: for all other, no-op strategies ...
    return current;
    
    // REQUIRED: Honor leave_open for the source stream
    //return leave_open && current != source ? new LeaveOpenStream(current) : current;
  }

  /// <summary>
  /// Builds a write pipeline targeting finalDest. You WRITE PLAINTEXT into the returned stream. 
  /// Data is compressed, then encrypted, then forwarded to finalDest.
  /// </summary>
  /// <remarks>
  /// The returned stream accepts PLAINTEXT data. As data flows through:
  /// <list type="number">
  ///   <item><description>It is first compressed (if <see cref="mcsFileOptions.Compression"/> is configured).</description></item>
  ///   <item><description>Then the compressed bytes are encrypted (if <see cref="mcsFileOptions.Encryption"/> is configured).</description></item>
  ///   <item><description>Finally, the ciphertext is written into the provided <paramref name="destination"/> stream.</description></item>
  /// </list>
  /// <para>
  /// This means the pipeline applies transforms in the order: <c>Compress → Encrypt → Destination</c>.
  /// </para>
  /// <para>
  /// Note: Although <see cref="BuildWritePipeline"/> and <see cref="BuildReadPipeline"/> look similar in code,
  /// they are inverses because of data flow direction. On write, plaintext flows into compression first;
  /// on read, ciphertext flows into decryption first.
  /// </para>
  /// </remarks>
  /// <param name="destination">The final sink stream (e.g., file, network, or response stream).</param>
  /// <param name="options">The file options containing encryption and compression strategies.</param>
  /// <returns>
  /// A wrapped <see cref="Stream"/> into which PLAINTEXT should be written. Disposing the returned
  /// stream will flush and finalize transforms (e.g., GZip footer, crypto block).
  /// </returns>
  private static Stream BuildWritePipeline(Stream destination, mcsFileOptions options, DataProcessingModeEnum mode, bool leave_open = true)
  {
    //Pipeline Order: Plaintext → Compress → Encrypt → Destination    
    if(destination == null) throw new ArgumentNullException(nameof(destination));
    
    var now = DateTime.UtcNow;
    
    if(!TryValidateProtectionMode(mode, out var valid_mode))
    {
      options?.Logger
             ?.Invoke(new LogEntry { Message   = $"Invalid protection mode '{mode}' detected. Falling back to 'None'."
                                    ,Level     = LogLevelEnum.WARNING
                                    ,Timestamp = DateTime.UtcNow });    
    }
    
    mode = valid_mode;
    options ??= new mcsFileOptions();

    ValidateStrategies(options, mode);
    
//    Stream current = destination;
    Stream current = new LeaveOpenStream(destination);
    
//    if(destination.CanSeek && destination.Length == 0)
//    {
//      options.Logger?.Invoke(new FileLogEntry { Timestamp = now
//                                               ,Level     = LogLevelEnum.INFORMATION
//                                               ,Operation = nameof(BuildReadPipeline)
//                                               ,Message   = $"Empty destination stream detected, returning unprocessed stream (Mode={mode})"
//                                               ,AtRest    = mode
//                                               ,Success   = true });
//      
//      return leave_open ? new LeaveOpenStream(destination) : destination;
//    }
    
    //NOTE: First wrap with compression (compress plaintext → feed into encryption); Compression 
    //      wraps encryption (so it runs first on plain text)
    if(mode.HasFlag(DataProcessingModeEnum.Compress) && !options.Compression.IsNoOp)
      current = options.Compression.Compress(current, leave_open: false);
    
    //NOTE: Then wrap with encryption (ciphertext is what hits the destination); Encryption
    //      is close to the final destination (because we want to compress THEN encrypt)
    if(mode.HasFlag(DataProcessingModeEnum.Encrypt) && !options.Encryption.IsNoOp)
      current = options.Encryption.Encrypt(current, leave_open: false);
    
//    //REQUIRED: catches edge cases where either encryption or compression are configured 
//    //          with no-op strategies, it honors leave_open
//    if(leave_open && current == destination)
//      return new LeaveOpenStream(destination);
//    
    //REQUIRED: for all other, no-op strategies ...
    return current;

    // REQUIRED: Honor leave_open for the destination stream
    //return leave_open && current != destination ? new LeaveOpenStream(current) : current;
  }

  #endregion
}

//--------------------------------------------------------------------------------------------
// MAIN GOAL/FOCUS ---------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------
/// <summary>
/// Specifies protection modes that can be applied to files either when stored on disk 
/// (<see cref="mcsFileOptions.AtRest"/>) or while being streamed/transferred 
/// (<see cref="mcsFileOptions.InTransit"/>). Supports bitwise combination of values.
/// </summary>
/// <remarks>
/// Typical usage examples:
/// <code>
/// // Store encrypted + compressed on disk; transfer compressed only
/// var opts = new mcsFileOptions
/// {
///     AtRest   = DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress,
///     InTransit = DataProcessingModeEnum.Compress,
///     Encryption = new AesTextEncryptionStrategy(key, iv),
///     Compression = new GzipCompressionStrategy()
/// };
///
/// // Store plain files, but encrypt during transport
/// var opts = new mcsFileOptions
/// {
///     AtRest = DataProcessingModeEnum.None,
///     InTransit = DataProcessingModeEnum.Encrypt,
///     Encryption = new AesTextEncryptionStrategy(key, iv)
/// };
/// </code>
/// </remarks>
/// <examples>
///   //Encryption: NO && Compression: NO
///   var ProtectedModeEnum AtRest = DataProcessingModeEnum.None;
/// 
///   //Encryption: YES && Compression: NO
///   var ProtectedModeEnum AtRest = DataProcessingModeEnum.Encrypt;
/// 
///   //Encryption: YES && Compression: YES
///   var ProtectedModeEnum AtRest = DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress;
/// </examples>
[Flags]
public enum DataProcessingModeEnum
{
  /// <summary>
  /// No protection. The file or stream is stored and transferred as plain text or raw 
  /// binary, without encryption or compression.
  /// </summary>
  None = 0,
  
  /// <summary>
  /// Apply encryption to the file or stream using the configured 
  /// <see cref="mcsFileOptions.Encryption"/> strategy (e.g., AES, TripleDES, or custom).
  /// </summary>   
  Encrypt = 1,
  
  /// <summary>
  /// Apply compression to the file or stream using the configured
  /// <see cref="mcsFileOptions.Compression"/> strategy
  /// (e.g., GZip, Brotli, Zip).
  /// </summary>  
  Compress = 2

  /// <summary>
  /// Reserved for future expansion. Placeholder to support custom or advanced protection modes.
  /// Possible Future Extensions:
  ///   Hashing/Integrity → Hash (SHA-256 for integrity checks).
  ///             Signing → Sign (digital signatures, non-repudiation).
  ///            Encoding → Base64, UrlEncode for safe transmission.
  ///         Obfuscation → simple XOR or redaction masks (dev/test use).
  ///       Deduplication → marking content for chunk-level dedupe.
  ///   Masking/Scrubbing → anonymization of PII before write.
  /// </summary>    
  //Custom = 4
}

/// <summary>
/// A thin wrapper around another stream that ignores Dispose(), ensuring the underlying stream is not
/// closed. Useful for no-op strategies (e.g., NoEncryption, NoCompression) so caller streams are 
/// preserved.
/// 
/// “Pipelines guarantee: always return a disposable stream. In no-op cases with leave_open=true, a 
/// LeaveOpenStream wrapper protects the caller’s stream. This keeps the caller’s lifecycle simple 
/// and safe — dispose what you’re given, and the right thing happens.”
/// </summary>
public sealed class LeaveOpenStream : Stream
{
  private readonly Stream _inner;
  public LeaveOpenStream(Stream inner)
  {
    _inner = inner ?? throw new ArgumentNullException(nameof(inner));
  }

  protected override void Dispose(bool disposing)
  {
    // Intentially skip disposing the inner stream to keep it open for caller usage.
    //base.Dispose(disposing);
  }

  public override bool CanRead  => _inner.CanRead;
  public override bool CanSeek  => _inner.CanSeek;
  public override bool CanWrite => _inner.CanWrite;
  public override long Length   => _inner.Length;
  
  public override long Position
  { 
    get => _inner.Position;
    set => _inner.Position = value;
  }

  public override void Flush() 
    => _inner.Flush();
  
  public override int Read(byte[] buffer, int offset, int count)
    => _inner.Read(buffer, offset, count);
  
  public override long Seek(long offset, SeekOrigin origin)
    => _inner.Seek(offset, origin);
  
  public override void SetLength(long value)
    => _inner.SetLength(value);
  
  public override void Write(byte[] buffer, int offset, int count)
    => _inner.Write(buffer, offset, count);

  // For .NET 5+ we should override async methods too
  public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
      => _inner.ReadAsync(buffer, offset, count, cancellationToken);

  public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
      => _inner.WriteAsync(buffer, offset, count, cancellationToken);

  public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
      => _inner.ReadAsync(buffer, cancellationToken);

  public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
      => _inner.WriteAsync(buffer, cancellationToken);

  public override Task FlushAsync(CancellationToken cancellationToken)
    => _inner.FlushAsync(cancellationToken);

  // For .NET 6+ we should override async methods too
  public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
      => _inner.CopyToAsync(destination, bufferSize, cancellationToken);    
}

/// <summary>
/// Encapsulates optional behaviors for file opterations. Keeps mcsFileUtilities extensible
/// without overloading its API.
/// </summary>
public sealed class mcsFileOptions
{
  /// <summary>
  /// Whether to overwrite an existing file. Defaults to true.
  /// </summary>
  public bool Overwrite { get; set; } = true;

  /// <summary>
  /// Defines how the file should be stored on disk ("at rest"). The chosen mode is also 
  /// recorded in logs for traceability. Use <see cref="DataProcessingModeEnum.Encrypt"/> to 
  /// encrypt, <see cref="DataProcessingModeEnum.Compress"/> to compress, or a combination 
  /// using bitwise OR (e.g. Encrypt | Compress).
  /// </summary>
  public DataProcessingModeEnum AtRest { get; set; } = DataProcessingModeEnum.None;

  /// <summary>
  /// Defines how the file should be handled while being streamed, transferred, or written to
  /// another medium ("in transit"). The chosen mode is also recorded and logs for traceability.
  /// Supports the same flag combinations as <see cref="AtRest"/>. Typically used for protecting
  /// data during network, pipeline, or inter-process operations.
  /// </summary>
  public DataProcessingModeEnum InTransit { get; set; } = DataProcessingModeEnum.None;
  
  /// <summary>
  /// Encoding for text operations. Defaults to UTF8 (no BOM).
  /// </summary>
  public Encoding Encoding { get; set; } = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
  
  /// <summary>
  /// Optional compression strategy. If null, no compression is applied.
  /// Example: 
  /// </summary>
  public ICompressionStrategy Compression { get; set; } = NoCompression.Instance;
  
  /// <summary>
  /// Optional encryption strategy. If null, no encryption is applied.
  /// Example: new AesTextEncryptionStrategy(keys)
  /// </summary>
  public IEncryptionStrategy Encryption { get; set; } = NoEncryptionStrategy.Instance;

  /// <summary>
  /// Optional impersonator for secure file access.
  /// Example: new WindowsImpersonator(domain, user, pass)
  /// </summary>
  public IImpersonator Impersonator { get; set; }

  /// <summary>
  /// Optional logger callback (action). Keeps mcsFileOptions free
  /// of a logging framework dependency (Serilog, NLog, etc.).
  /// </summary>
  public Action<LogEntry> Logger { get; set; }

  /// <summary>Buffer size used for FileStream. Default: 81,920 (BCL CopyTo default).</summary>
  public int FileBufferSize { get; set; } = 81920;

  /// <summary>FileShare used when opening files for READ. Default: Read.</summary>
  public FileShare FileReadShare { get; set; } = FileShare.Read;

  /// <summary>FileShare used when opening files for WRITE. Default: None.</summary>
  public FileShare FileWriteShare { get; set; } = FileShare.None;

  /// <summary>System IO flags for READ streams (e.g., SequentialScan, Asynchronous).</summary>
  public FileOptions FileReadOptions { get; set; } = FileOptions.SequentialScan;

  /// <summary>System IO flags for WRITE streams (e.g., WriteThrough, Asynchronous).</summary>
  public FileOptions FileWriteOptions { get; set; } = FileOptions.SequentialScan;

}

public abstract class OperationResult<T>
{
  public bool IsSuccess         { get; }
  public T Value                { get; }
  public Exception Exception    { get; }  
  public string Operation       { get; }
  
  protected OperationResult(bool success, T value, Exception exception, string operation )
  {
    IsSuccess = success;
    Value     = value;
    Exception = exception;
    Operation = operation;
  }
}

public sealed class FileOperationResult<T> : OperationResult<T>
{
  public string Path { get; }
  
  private FileOperationResult(bool success, T value, Exception exception, string operation, string path )
    : base(success, value, exception, operation)
  {
    Path = path;
  }
  
  public static FileOperationResult<T> AsSuccess(T value, string operation, string path)
    => new FileOperationResult<T>(true, value, null, operation, path);
    
  public static FileOperationResult<T> AsFailure(Exception ex, string operation, string path)
    => new FileOperationResult<T>(false, default, ex, operation, path);
}

public sealed class StreamOperationResult<T> : OperationResult<T>
{
  public long BytesTransferred  { get; }
  public long? FinalPosition    { get; }

  private StreamOperationResult(bool success, T value, Exception exception, string operation, long bytesTransferred, long? finalPosition)
    : base(success, value, exception, operation)
  {
    BytesTransferred  = bytesTransferred;
    FinalPosition     = finalPosition;
  }

  public static StreamOperationResult<T> AsSuccess(T value, string operation, long bytesTransferred, long? finalPosition = null)
    => new StreamOperationResult<T>(true, value, null, operation, bytesTransferred, finalPosition);

  public static StreamOperationResult<T> AsFailure(Exception ex, string operation, long bytesTransferred = 0, long? finalPosition = null)
    => new StreamOperationResult<T>(false, default, ex, operation, bytesTransferred, finalPosition);
}

public static void LogOperationResult<T>(OperationResult<T> result, Action<LogEntry> logger)
{
  if (result == null) throw new ArgumentNullException(nameof(result));

  var level = result.IsSuccess ? LogLevelEnum.INFORMATION : LogLevelEnum.ERROR;
  
  var message = result.IsSuccess
      ? $"Operation '{result.Operation}' succeeded."
      : $"Operation '{result.Operation}' failed. Error: {result.Exception?.Message}";

  logger?.Invoke(new LogEntry { Message   = message
                               ,Level     = level
                               ,Timestamp = DateTime.UtcNow });
}

//--------------------------------------------------------------------------------------------
// ENCRYPTION --------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------
public enum EncryptionTypeEnum
{
   NONE
  ,AES
  ,TRIPLEDES
  ,XOR
}

/// <summary>
/// Defines a contract for encrypting and decrypting data.
/// Works for both text and binary content.
/// </summary>
public interface IEncryptionStrategy
{
  string Name { get; }
  bool IsNoOp { get; }
  
  //Byte-array convenience
  byte[] Encrypt(byte[] plaintext);
  byte[] Decrypt(byte[] ciphertext);
  
  //Stream copy (utility owns the CryptoStream lifetime)
  void Encrypt(Stream input, Stream output);
  void Decrypt(Stream input, Stream output);

  //Streaming pipeline (caller owns lifetime)
  //Write-path: returns a stream that encrypts data written into it and forwards to destination
  Stream Encrypt(Stream destination, bool leave_open = true);
  
  //Read-path: returns a stream that decrypts data read from source
  Stream Decrypt(Stream source, bool leave_open = true);
}

public static class EncryptionFactory
{
  public static IEncryptionStrategy Create(EncryptionTypeEnum type)
  {
    return type switch {
       EncryptionTypeEnum.AES 
          => new AesEncryptionStrategy( Encoding.UTF8.GetBytes("ThisIsA16ByteKey")
                                       ,Encoding.UTF8.GetBytes("ThisIsAnInitVect"))
      ,EncryptionTypeEnum.TRIPLEDES
          => new TripleDesEncryptionStrategy( Encoding.UTF8.GetBytes("123456789012345678901234")  //24 bytes
                                             ,Encoding.UTF8.GetBytes("12345678"))                 // 8 bytes
      ,EncryptionTypeEnum.XOR
          => new XorEncryptionStrategy(0x5A)
          
      ,EncryptionTypeEnum.NONE
          => NoEncryptionStrategy.Instance
      ,_  => throw new ArgumentOutOfRangeException(nameof(type), type, "Unsupported encryption type")
    };
  }
}

public sealed class NoEncryptionStrategy : IEncryptionStrategy
{
  private NoEncryptionStrategy() { }
  
  public static NoEncryptionStrategy Instance => new();
  public string Name => "None";
  public bool IsNoOp => true;
  
  // ---- byte[] convenience (delegates to stream form) ----
  public byte[] Encrypt(byte[] plaintext)  => plaintext;
  public byte[] Decrypt(byte[] ciphertext) => ciphertext;
  
  // ---- stream copy (owns lifetime, explicit finalization where needed) ----
  public void Encrypt(Stream input, Stream output)
  {
    if(input  == null)  throw new ArgumentNullException(nameof(input));      
    if(output == null)  throw new ArgumentNullException(nameof(output));
      
    input.CopyTo(output);
  }  
  
  public void Decrypt(Stream input, Stream output)
  {
    if(input  == null)  throw new ArgumentNullException(nameof(input));      
    if(output == null)  throw new ArgumentNullException(nameof(output));
      
    input.CopyTo(output);  
  }
  
  // ---- streaming pipeline (caller owns lifetime) ----
  public Stream Encrypt(Stream destination, bool leave_open = true)
  {
    if(destination == null) throw new ArgumentNullException(nameof(destination));
    
    //Just return the destination; caller writes, it passes through (unchanged)
    return destination;
  }
  
  public Stream Decrypt(Stream source, bool leave_open = true)
  {
    if(source == null) throw new ArgumentNullException(nameof(source));
    
    //Just return the source; caller reads from it (unchanged)
    return source;
  }
}

public sealed class AesEncryptionStrategy : IEncryptionStrategy
{
  private readonly byte[] _key;
  private readonly byte[] _iv;
  
  private readonly byte[] _salt     = null;
  private readonly string _password = null;
  
  public bool UsesPassword => _password != null;
  
  public AesEncryptionStrategy()
    : this(Encoding.UTF8.GetBytes("ThisIsA16ByteKey"), Encoding.UTF8.GetBytes("ThisIsAnInitVect")) { }
  
  public AesEncryptionStrategy(byte[] key, byte[] iv)
  {
    _key = key ?? throw new ArgumentNullException(nameof(key));
    _iv  = iv  ?? throw new ArgumentNullException(nameof(iv));
  }
  
  public AesEncryptionStrategy(string password, byte[] salt = null)
  {
    _password = password ?? throw new ArgumentNullException(nameof(password));
    _salt     = salt     ?? Encoding.UTF8.GetBytes("FixedSaltValue666");  //NOTE: replace with secure salt source
  }
  
  public string Name => "AES";
  public bool IsNoOp => false;
  
  // ---- byte[] convenience (delegates to stream form) ----
  public byte[] Encrypt(byte[] data)
  {
    if(data == null)      throw new ArgumentNullException(nameof(data));    
    if(data.Length == 0)  return Array.Empty<byte>();
    
    using var ms = new MemoryStream();
    using var cs = (CryptoStream)Encrypt(ms, leave_open: true);

    cs.Write(data, 0, data.Length);
    cs.FlushFinalBlock();  //REQUIRED for write-mode encryption
    
    return ms.ToArray();
  }
  
  public byte[] Decrypt(byte[] data)
  {
    if(data == null)      throw new ArgumentNullException(nameof(data));    
    if(data.Length == 0)  return Array.Empty<byte>();
    
    using var ms = new MemoryStream(data);
    using var cs = (CryptoStream)Decrypt(ms, leave_open: true);
    using var ds = new MemoryStream();
    
    cs.CopyTo(ds);  //read-mode; FlushFinalBlock not required or needed
    
    return ds.ToArray();
  }
  
  // ---- stream copy (owns lifetime, explicit finalization where needed) ----
  public void Encrypt(Stream input, Stream output)
  {
    if(input  == null)  throw new ArgumentNullException(nameof(input));
    if(output == null)  throw new ArgumentNullException(nameof(output));
    
    using var cs = (CryptoStream)Encrypt(output, leave_open: true);
    
    input.CopyTo(cs);
    cs.FlushFinalBlock(); //REQUIRED for write-mode encryption
  }
  
  public void Decrypt(Stream input, Stream output)
  {
    if(input  == null)  throw new ArgumentNullException(nameof(input));
    if(output == null)  throw new ArgumentNullException(nameof(output));
    
    using var cs = (CryptoStream)Decrypt(input, leave_open: true);
    cs.CopyTo(output);  //read-mode; FlushFinalBlock not required or needed
  }
  
  // ---- streaming pipeline (caller owns lifetime) ----
  public Stream Encrypt(Stream destination, bool leave_open = true)
  {
    if(destination == null) throw new ArgumentNullException(nameof(destination));
    
    var aes = Aes.Create();
    ConfigureKeyAndIV(aes);

    var encryptor = aes.CreateEncryptor();
    
    //NOTE: DO NOT DISPOSE 'aes' or 'encryptor' here; CryptoStream will dispose transform;
    //      we keep 'aes' alive via closure for the lifetime of CryptoStream.
    return new CryptoStream(destination, encryptor, CryptoStreamMode.Write, leave_open);
  }
  
  public Stream Decrypt(Stream source, bool leave_open = true)
  {
    if(source == null) throw new ArgumentNullException(nameof(source));
    
    var aes = Aes.Create();
    ConfigureKeyAndIV(aes);

    var decryptor = aes.CreateDecryptor();

    //NOTE: DO NOT DISPOSE 'aes' or 'decryptor' here; CryptoStream will dispose transform;
    //      we keep 'aes' alive via closure for the lifetime of CryptoStream.
    return new CryptoStream(source, decryptor, CryptoStreamMode.Read, leave_open);
  }

  // --- internal/private helpers ---
  private void ConfigureKeyAndIV(Aes aes)
  {
    aes.Mode    = CipherMode.CBC;
    aes.Padding = PaddingMode.PKCS7;
    
    if(UsesPassword)
    {
      var key = new Rfc2898DeriveBytes(_password, _salt, 100_000, HashAlgorithmName.SHA256);      
      aes.Key = key.GetBytes(32); // 256-bit Key
      aes.IV  = key.GetBytes(16); // 128-bit IV
    }
    else
    {
      if(_key == null)  throw new InvalidOperationException("No AES Key provided");
      if(_iv  == null)  throw new InvalidOperationException("No AES IV provided");
      
      aes.Key = _key;
      aes.IV  = _iv;
    }
  }
}

public sealed class TripleDesEncryptionStrategy : IEncryptionStrategy
{
  private readonly byte[] _key; //NOTE: must be 24 bytes (3 * 8)
  private readonly byte[] _iv;  //NOTE: must be  8 bytes (block size)
  
  public TripleDesEncryptionStrategy(byte[] key, byte[] iv)
  {
    if(key == null || key.Length != 24)
      throw new ArgumentException("Key must be 24 bytes", nameof(key));
    
    if(iv == null || iv.Length != 8)
      throw new ArgumentException("IV must be 8 bytes", nameof(iv));
    
    _key = key;
    _iv  = iv;
  }
  
  public string Name => "TripleDES";
  public bool IsNoOp => false;
  
  // ---- byte[] convenience ----
  public byte[] Encrypt(byte[] data)
  {
    if(data == null)      throw new ArgumentNullException(nameof(data));    
    if(data.Length == 0)  return Array.Empty<byte>();
    
    using var ms = new MemoryStream();
    using var cs = (CryptoStream)Encrypt(ms, leave_open: true);
    
    cs.Write(data, 0, data.Length);
    cs.FlushFinalBlock(); //REQUIRED for write-mode encryption
    
    return ms.ToArray();
  }
  
  public byte[] Decrypt(byte[] data)
  {
    if(data == null)      throw new ArgumentNullException(nameof(data));    
    if(data.Length == 0)  return Array.Empty<byte>();
    
    using var ms = new MemoryStream(data);
    using var cs = (CryptoStream)Decrypt(ms, leave_open: true);
    using var ds = new MemoryStream();
    
    cs.CopyTo(ds);  //read-mode; FlushFinalBlock not required or needed
    
    return ds.ToArray();
  }
  
  // ---- stream copy ----
  public void Encrypt(Stream input, Stream output)
  {
    if(input  == null)  throw new ArgumentNullException(nameof(input));
    if(output == null)  throw new ArgumentNullException(nameof(output));
    
    using var cs = (CryptoStream)Encrypt(output, leave_open: true);
    
    input.CopyTo(cs);
    cs.FlushFinalBlock();  //REQUIRED for write-mode encryption
  }
  
  public void Decrypt(Stream input, Stream output)
  {
    if(input  == null)  throw new ArgumentNullException(nameof(input));
    if(output == null)  throw new ArgumentNullException(nameof(output));
    
    using var cs = (CryptoStream)Decrypt(input, leave_open: true);
    cs.CopyTo(output); //read-mode; FlushFinalBlock not required or needed
  }
  
  // ---- streaming pipeline (caller owns lifetime) ----
  public Stream Encrypt(Stream destination, bool leave_open = true)
  {
    if(destination == null) throw new ArgumentNullException(nameof(destination));
    
    var tdes = TripleDES.Create();
    
    tdes.Mode    = CipherMode.CBC;
    tdes.Padding = PaddingMode.PKCS7;
    
    tdes.Key = _key;
    tdes.IV  = _iv;
    
    var encryptor = tdes.CreateEncryptor();
    return new CryptoStream(destination, encryptor, CryptoStreamMode.Write, leave_open);
  }
  
  public Stream Decrypt(Stream source, bool leave_open = true)
  {
    if(source == null) throw new ArgumentNullException(nameof(source));
    
    var tdes = TripleDES.Create();
    
    tdes.Mode    = CipherMode.CBC;
    tdes.Padding = PaddingMode.PKCS7;
    
    tdes.Key = _key;
    tdes.IV  = _iv;

    var decryptor = tdes.CreateDecryptor();
    return new CryptoStream(source, decryptor, CryptoStreamMode.Read, leave_open);
  }
}

public sealed class XorEncryptionStrategy : IEncryptionStrategy
{
  private readonly byte _key;
  
  public XorEncryptionStrategy(byte key = 0xAA)
  {
    _key = key;
  }
  
  public string Name => "XOR";
  public bool IsNoOp => false;
  
  // ---- byte[] convenience ----
  public byte[] Encrypt(byte[] plain_text)  => Transform(plain_text);
  
  public byte[] Decrypt(byte[] cipher_text) => Transform(cipher_text);
  
  // ---- stream copy ----
  public void Encrypt(Stream input, Stream output) => Transform(input, output);
  
  public void Decrypt(Stream input, Stream output) => Transform(input, output);
  
  // ---- streaming pipeline (caller owns lifetime) ----
  public Stream Encrypt(Stream destination, bool leave_open = true)
    => new XorTransformStream(destination, _key, writeMode: true, leave_open);

  public Stream Decrypt(Stream source, bool leave_open = true)
    => new XorTransformStream(source, _key, writeMode: false, leave_open);
  
  // --- internal/private helpers ---
  private byte[] Transform(byte[] data) 
  {
    if(data == null)      throw new ArgumentNullException(nameof(data));    
    if(data.Length == 0)  return Array.Empty<byte>();
      
    var result = new byte[data.Length];
    
    for(int i = 0; i < data.Length; i++)
    {
      result[i] = (byte)(data[i] ^ _key);
    }
    
    return result;
  }
  
  private void Transform(Stream input, Stream output)
  {
    if(input  == null)  throw new ArgumentNullException(nameof(input));
    if(output == null)  throw new ArgumentNullException(nameof(output));
    
    int bite;
    
    while((bite = input.ReadByte()) != -1)
    {
      output.WriteByte((byte)(bite ^ _key));
    }
  }
  
  private sealed class XorTransformStream : Stream
  {
    private readonly Stream _inner;
    private readonly byte _key;
    private readonly bool _writeMode;
    private readonly bool _leaveOpen;

    public XorTransformStream(Stream inner, byte key, bool writeMode, bool leaveOpen)
    {
      _inner = inner ?? throw new ArgumentNullException(nameof(inner));
      _key   = key;
      
      _writeMode = writeMode;
      _leaveOpen = leaveOpen;
    }

    public override bool CanRead  => !_writeMode && _inner.CanRead;
    public override bool CanSeek  => false;
    public override bool CanWrite => _writeMode && _inner.CanWrite;
    
    public override long Length => throw new NotSupportedException();
    
    public override long Position
    {
      get => throw new NotSupportedException(); 
      set => throw new NotSupportedException(); 
    }

    public override void Flush() => _inner.Flush();

    public override int Read(byte[] buffer, int offset, int count)
    {
      if(_writeMode)
        throw new NotSupportedException("This XOR stream is in write mode.");
      
      int read = _inner.Read(buffer, offset, count);
      
      for (int i = 0; i < read; i++)
        buffer[offset + i] = (byte)(buffer[offset + i] ^ _key);
      
      return read;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      if(!_writeMode)
        throw new NotSupportedException("This XOR stream is in read mode.");
      
      var temp = new byte[count];
      
      for (int i = 0; i < count; i++)
        temp[i] = (byte)(buffer[offset + i] ^ _key);
      
      _inner.Write(temp, 0, count);
    }

    public override long Seek(long offset, SeekOrigin origin)
      => throw new NotSupportedException();
    
    public override void SetLength(long value)
      => throw new NotSupportedException();

    protected override void Dispose(bool disposing)
    {
      if(disposing && !_leaveOpen)
        _inner.Dispose();
      
      base.Dispose(disposing);
    }
  }
}

//--------------------------------------------------------------------------------------------
// IMPERSONATION -----------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------
public enum ImpersonationLogonTypeEnum
{
   PROVIDER_DEFAULT       = 0
  ,PROVIDER_WINNT50       = 3
  
  ,LOGON_INTERACTIVE      = 2
  ,LOGON_NEW_CREDENTIALS  = 9
}

public interface IImpersonator : IDisposable
{
  void Run(Action action);
  T Run<T>(Func<T> func);
}

/// <summary>
/// Runs code under impersonated Windows Credentials; supports local accounts (domain=".")
/// or domain accounts.
/// </summary>
public sealed class WindowsImpersonator : IImpersonator
{
  private readonly string _domain;
  private readonly string _username;
  private readonly string _password;
  private readonly ImpersonationLogonTypeEnum _logon_type;
  
  // Choose a LOGON type that fits your scenario. 2=INTERACTIVE works for many cases.
  // For network shares under some service contexts, 9=NEW_CREDENTIALS can be appropriate.
  private const int LOGON32_PROVIDER_DEFAULT      = 0;  
  private const int LOGON32_LOGON_INTERACTIVE     = 2;  //INFO: meant for local use/paths...
  private const int LOGON32_PROVIDER_WINNT50      = 3;  
  private const int LOGON32_LOGON_NEW_CREDENTIALS = 9;  //INFO: meant to use for UNC paths...
  
  public WindowsImpersonator(string domain, string username, string password, ImpersonationLogonTypeEnum logon_type = ImpersonationLogonTypeEnum.LOGON_NEW_CREDENTIALS)
  {
    _domain     = domain   ?? throw new ArgumentNullException(nameof(domain));
    _username   = username ?? throw new ArgumentNullException(nameof(username));
    _password   = password ?? throw new ArgumentNullException(nameof(password));
    _logon_type = logon_type;
  }
  
  public void Run(Action action)
  {
    if(action == null)
      throw new ArgumentNullException(nameof(action));
      
    using var identity = GetWindowsIdentity();
    WindowsIdentity.RunImpersonated(identity.AccessToken, action);
  }
  
  public T Run<T>(Func<T> func)
  {
    if(func == null)
      throw new ArgumentNullException(nameof(func));
    
    using var identity = GetWindowsIdentity();    
    return WindowsIdentity.RunImpersonated(identity.AccessToken, func);
  }
  
  public static void RunAs(string domain, string username, string password, Action action)
  {
    using var imp = new WindowsImpersonator(domain, username, password);
    imp.Run(action);
  }
  
  public static T RunAs<T>(string domain, string username, string password, Func<T> func)
  {
    using var imp = new WindowsImpersonator(domain, username, password);
    return imp.Run(func);
  }
  
  public void Dispose()
  {
    // Nothing persistent to clean up here — tokens are disposed with WindowsIdentity.
  }
  
  private WindowsIdentity GetWindowsIdentity()
  {
    if(!LogonUser( _username
                  ,_domain
                  ,_password
                  ,(int)_logon_type
                  ,(int)ImpersonationLogonTypeEnum.PROVIDER_DEFAULT
                  ,out IntPtr token ))
    {
      throw new InvalidOperationException($"LogonUser Failed. Win32Error: {Marshal.GetLastWin32Error()}");
    }
    
    return new WindowsIdentity(token);
  }

  [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
  private static extern bool LogonUser( string lpszUsername
                                       ,string lpszDomain
                                       ,string lpszPassword
                                       ,int dwLogonType
                                       ,int dwLogonProvider
                                       ,out IntPtr phToken );

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  private static extern bool CloseHandle(IntPtr handle);
}

//--------------------------------------------------------------------------------------------
// LOGGING -----------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------
public enum LogLevelEnum
{
   EXCEPTION    = 0 // Unhandled Exception
  ,ERROR        = 1 // Handled but serious failure (e.g., file not found, access denied)
  ,WARNING      = 2 // Recoverable oddity (e.g., overwrite prevented, fallback to defaults)
  ,INFORMATION  = 3 // Normal operations (read/write success)
  ,DEBUG        = 4 // Diagnostic details (raw byte length, impersonator used)
}

public class LogEntry
{
  public DateTime Timestamp   { get; set; } = DateTime.UtcNow;
  public LogLevelEnum Level   { get; set; } = LogLevelEnum.INFORMATION;
                             
  public string Message       { get; set; } = string.Empty;
  public TimeSpan? Duration   { get; set; }
  public int? ThreadId        { get; set; } = Environment.CurrentManagedThreadId;
}

public sealed class FileLogEntry : LogEntry
{
  public string Operation { get; set; } = string.Empty;
  public string Path      { get; set; } = string.Empty;
  
  public DataProcessingModeEnum AtRest    { get; set; }
  public DataProcessingModeEnum InTransit { get; set; }
  
  public string Compression { get; set; } = string.Empty;  
  public string Encryption  { get; set; } = string.Empty;
  
  public bool Success         { get; set; }
  public string Impersonator  { get; set; } = nameof(EncryptionTypeEnum.NONE);
  
  public Exception  Exception { get; set; }
}

public static Action<LogEntry> ConsoleLogger = entry => {
  var prefix = entry.Level.ToString().ToUpper();

  Console.WriteLine($"[{prefix}] {entry.Timestamp} :: {entry.Message}");

  if(entry is FileLogEntry file_entry)
  {
    Console.WriteLine($"  File: {file_entry.Path}");
    Console.WriteLine($"  Operation: {file_entry.Operation}, Success = {file_entry.Success}");

    Console.WriteLine($"  AtRest: {file_entry.AtRest}");
    Console.WriteLine($"  InTransit: {file_entry.InTransit}");

    Console.WriteLine($"  Compression: {file_entry.Compression}");
    Console.WriteLine($"  Encryption: {file_entry.Encryption}");

    if(file_entry.Exception != null)
      Console.WriteLine($"  Exception: {file_entry.Exception.GetType().Name} - {file_entry.Exception.Message}");
  }
};

//--------------------------------------------------------------------------------------------
// COMPRESSION -------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------
public enum CompressionTypeEnum
{
   NONE   = 0
  ,BROTLI = 1
  ,GZIP   = 2
  ,ZIP    = 3
}

public static class CompressionFactory
{
  public static ICompressionStrategy Create(CompressionTypeEnum type)
  {
    return type switch
    {
       CompressionTypeEnum.NONE   => NoCompression.Instance
      ,CompressionTypeEnum.BROTLI => new BrotliCompressionStrategy()
      ,CompressionTypeEnum.GZIP   => new GZipCompressionStrategy()
      ,_ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unsupported compression type")
    };
  }
}

public interface ICompressionStrategy
{
  string Name { get; }
  bool IsNoOp { get; }

  /// <summary>
  /// Compress raw data (text or binary).
  /// </summary>
  byte[] Compress(byte[] data);
  void Compress(Stream input, Stream output);

  //Read-Path: returns a stream that decompresses data read from base_source
  Stream Compress(Stream source, bool leave_open = true);
  
  /// <summary>
  /// Decompress raw data (text or binary).
  /// </summary>
  byte[] Decompress(byte[] data);
  void Decompress(Stream input, Stream output);

  //wrapping streams for true pipelines
  //Write-Path: returns a stream that compresses data written into it and forwards to base_destination
  Stream Decompress(Stream destination, bool leave_open = true);
}

public sealed class NoCompression : ICompressionStrategy
{
  public static readonly NoCompression Instance = new NoCompression();
  private NoCompression() { }
  
  public string Name => nameof(CompressionTypeEnum.NONE);
  public bool IsNoOp => true;
  
  // ---- byte[] convenience (delegates to stream form) ----
  public byte[] Compress(byte[] data)   => data ?? Array.Empty<byte>();
  public byte[] Decompress(byte[] data) => data ?? Array.Empty<byte>();
  
  // ---- stream copy (owns lifetime, explicit finalization where needed) ----
  public void Compress(Stream input, Stream output)
  {
    if(input  == null)  throw new ArgumentNullException(nameof(input));      
    if(output == null)  throw new ArgumentNullException(nameof(output));
      
    input.CopyTo(output);  
  }  
  
  public void Decompress(Stream input, Stream output)
  {
    if(input  == null)  throw new ArgumentNullException(nameof(input));      
    if(output == null)  throw new ArgumentNullException(nameof(output));
      
    input.CopyTo(output);  
  }  
  
  // ---- streaming pipeline (caller owns lifetime) ----
  public Stream Compress(Stream destination, bool leave_open = true)
  {
    if(destination == null) throw new ArgumentNullException(nameof(destination));
    
    //Just return the destination; caller writes to it (unchanged)
    return destination;
  }
  
  public Stream Decompress(Stream source, bool leave_open = true)
  {
    if(source == null) throw new ArgumentNullException(nameof(source));
    
    //Just return the source; caller reads from it (unchanged)
    return source;
  }
}

public sealed class GZipCompressionStrategy : ICompressionStrategy
{
  public string Name => nameof(CompressionTypeEnum.GZIP);
  public bool IsNoOp => false;
  
  // ---- byte[] convenience (delegates to stream form) ----
  public byte[] Compress(byte[] data)
  {
    if(data == null)      throw new ArgumentNullException(nameof(data));    
    if(data.Length == 0)  return Array.Empty<byte>();
      
    using var ms = new MemoryStream();
    using var cs = (GZipStream)Compress(ms, leave_open: true);
    
    cs.Write(data, 0, data.Length);
    cs.Flush(); //REQUIRED for write-mode compression, ensures data is finalized
    
    return ms.ToArray();
  }
  
  public byte[] Decompress(byte[] data)
  {
    if(data == null)      throw new ArgumentNullException(nameof(data));    
    if(data.Length == 0)  return Array.Empty<byte>();

    using var ms = new MemoryStream(data);
    using var cs = (GZipStream)Decompress(ms, leave_open: true);
    using var ds = new MemoryStream();

    cs.CopyTo(ds);  //read-mode; Flush not required or needed
    
    return ds.ToArray();
  }
  
  // ---- stream copy (owns lifetime, explicit finalization where needed) ----
  public void Compress(Stream input, Stream output)
  {
    if(input  == null)  throw new ArgumentNullException(nameof(input));
    if(output == null)  throw new ArgumentNullException(nameof(output));
    
    using var cs = (GZipStream)Compress(output, leave_open: true);
    
    input.CopyTo(cs);
    cs.Flush(); //REQUIRED for write-mode compression, finalizes compressed data
  }
  
  public void Decompress(Stream input, Stream output)
  {
    if(input  == null)  throw new ArgumentNullException(nameof(input));
    if(output == null)  throw new ArgumentNullException(nameof(output));
    
    using var cs = (GZipStream)Decompress(input, leave_open: true);
    
    cs.CopyTo(output);  //read-mode; Flush not required or needed
  }

  // ---- streaming pipeline (caller owns lifetime) ----
  public Stream Compress(Stream destination, bool leave_open = true)
  {
    if(destination == null) throw new ArgumentNullException(nameof(destination));
    
    return new GZipStream(destination, CompressionMode.Compress, leave_open);
  }

  public Stream Decompress(Stream source, bool leave_open = true)
  {
    if(source == null) throw new ArgumentNullException(nameof(source));
    
    return new GZipStream(source, CompressionMode.Decompress, leave_open);
  }
}

public sealed class BrotliCompressionStrategy : ICompressionStrategy
{
  public string Name => nameof(CompressionTypeEnum.BROTLI);
  public bool IsNoOp => false;
  
  // ---- byte[] convenience (delegates to stream form) ----
  public byte[] Compress(byte[] data)
  {
    if(data == null)      throw new ArgumentNullException(nameof(data));    
    if(data.Length == 0)  return Array.Empty<byte>();
         
    using var ms = new MemoryStream();
    using var cs = (BrotliStream)Compress(ms, leave_open: true);
    
    cs.Write(data, 0, data.Length);
    cs.Flush(); //REQUIRED for write-mode compression, ensures data is finalized
    
    return ms.ToArray();
  }
  
  public byte[] Decompress(byte[] data)
  {
    if(data == null)      throw new ArgumentNullException(nameof(data));    
    if(data.Length == 0)  return Array.Empty<byte>();
    
    using var ms = new MemoryStream(data);
    using var cs = (BrotliStream)Decompress(ms, leave_open: true);
    using var ds = new MemoryStream();
    
    cs.CopyTo(ds);  //read-mode; Flush not required or needed
    
    return ds.ToArray();
  }
  
  // ---- stream copy (owns lifetime, explicit finalization where needed) ----
  public void Compress(Stream input, Stream output)
  {
    if(input  == null)  throw new ArgumentNullException(nameof(input));
    if(output == null)  throw new ArgumentNullException(nameof(output));
    
    using var cs = (BrotliStream)Compress(output, leave_open: true);
    
    input.CopyTo(cs);
    cs.Flush(); //read-mode; Flush not required or needed
  }
  
  public void Decompress(Stream input, Stream output)
  {
    if(input  == null)  throw new ArgumentNullException(nameof(input));
    if(output == null)  throw new ArgumentNullException(nameof(output));
    
    using var brotli = (BrotliStream)Decompress(input, leave_open: true);
    
    brotli.CopyTo(output);  //read-mode; Flush not required or needed
  }
  
  // ---- streaming pipeline (caller owns lifetime) ----
  public Stream Compress(Stream destination, bool leave_open = true)
  {
    if(destination == null) throw new ArgumentNullException(nameof(destination));
    
    return new BrotliStream(destination, CompressionMode.Compress, leave_open);
  }
  
  public Stream Decompress(Stream source, bool leave_open = true)
  {
    if(source == null) throw new ArgumentNullException(nameof(source));
    
    return new BrotliStream(source, CompressionMode.Decompress, leave_open);
  }
}

//--------------------------------------------------------------------------------------------
// Unit Tests --------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------
public class mcsFileUtilitiesTests : IDisposable
{
  private readonly string _testRoot;
  //private readonly mcsFileOptions _defaultOptions = new mcsFileOptions();
  private readonly mcsFileOptions _defaultOptions = new mcsFileOptions { Encryption  = EncryptionFactory.Create(EncryptionTypeEnum.AES)
                                                                        ,Compression = CompressionFactory.Create(CompressionTypeEnum.GZIP)
                                                                        ,AtRest      = DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress
                                                                        ,InTransit   = DataProcessingModeEnum.Compress };

  public mcsFileUtilitiesTests()
  {
    _testRoot = Path.Combine(@"C:\temp", "mcsFileUtilitiesTests", Guid.NewGuid().ToString());
    Directory.CreateDirectory(_testRoot);
  }

  public void Dispose()
  {
    if(Directory.Exists(_testRoot))
    {
      try { Directory.Delete(_testRoot, recursive: true); } catch { /* ignore */ }
    }
  }

  #region Helpers
  
  private string CreateTempFile(string name, string content = null)
  {
    var path = Path.Combine(_testRoot, name);
    
    if(content != null)
      File.WriteAllText(path, content, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
    
    return path;
  }

  private string CreateTempFile(string name, byte[] data)
  {
    var path = Path.Combine(_testRoot, name);
    
    File.WriteAllBytes(path, data);
    
    return path;
  }

  private MemoryStream CreateTempStream(string content)
      => new MemoryStream(Encoding.UTF8.GetBytes(content));

  private MemoryStream CreateTempStream(byte[] data)
      => new MemoryStream(data);

  private string ReadFileText(string path)
      => File.ReadAllText(path, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));

  private byte[] ReadFileBytes(string path)
      => File.ReadAllBytes(path);
  
  #endregion
  
  #region 📂 File-based methods -------------------------------------------
  
  [Fact]
  public void ReadJsonFile_ShouldBeValidJson()
  {
    var _testFile = Path.Combine(@"C:\temp", "integration_test_data.txt");

    Assert.True(File.Exists(_testFile), $"Test data file not found: {_testFile}");

    //var json = File.ReadAllText(_testFile, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
    var json = mcsFileUtilities.ReadTextFile(_testFile, new mcsFileOptions());
    using var doc = JsonDocument.Parse(json);

    // ✅ Root properties
    Assert.Equal("integration_001", doc.RootElement.GetProperty("test_id").GetString());
    Assert.Equal("Sample data for File Utility integration testing", doc.RootElement.GetProperty("description").GetString());

    // ✅ Content object
    var content = doc.RootElement.GetProperty("content");
    Assert.Equal("Hello, world! This is a test string with special chars: @#$%^&*()", content.GetProperty("text").GetString());
    Assert.Equal("こんにちは 🌟 Привет!", content.GetProperty("unicode").GetString());

    // ✅ Metadata
    var metadata = content.GetProperty("metadata");
    Assert.Equal("1KB", metadata.GetProperty("size").GetString());
    Assert.Equal("2025-09-19T15:53:00Z", metadata.GetProperty("created").GetString());
    Assert.Contains("file", metadata.GetProperty("tags").EnumerateArray().Select(x => x.GetString()));

    // ✅ Repeated data array
    var repeated = doc.RootElement.GetProperty("repeated_data").EnumerateArray();
    Assert.Contains("Line 1: Lorem ipsum dolor sit amet, consectetur adipiscing elit.", repeated.Select(x => x.GetString()));
  }
  
  //------------------------------------------------------------------
  [Fact]
  public void ReadTextFile_ShouldReturnContents()
  {
    var path = CreateTempFile("text.txt", "hello");
    
    var result = mcsFileUtilities.ReadTextFile(path, _defaultOptions);
    
    Assert.Equal("hello", result);
  }
  
  [Fact]
  public void TryReadTextFile_BoolOut_ShouldReturnTrue()
  {
    var path = CreateTempFile("text.txt", "hello");
    var success = mcsFileUtilities.TryReadTextFile(path, out var value, _defaultOptions);
    Assert.True(success);
    Assert.Equal("hello", value);
  }
  
  [Fact]
  public void TryReadTextFile_Smart_ShouldReturnSuccess()
  {
    var path = CreateTempFile("text.txt", "hello");
    var result = mcsFileUtilities.TryReadTextFile(path, _defaultOptions);
    Assert.True(result.IsSuccess);
    Assert.Equal("hello", result.Value);
  }
  
  [Fact]
  public void WriteTextFile_ShouldCreateFile()
  {
    var path = Path.Combine(_testRoot, "write.txt");
    mcsFileUtilities.WriteTextFile(path, "written", _defaultOptions);
    Assert.Equal("written", ReadFileText(path));
  }
  
  [Fact]
  public void ReadBinaryFile_ShouldReturnBytes()
  {
    var bytes = new byte[] { 1, 2, 3 };
    var path = CreateTempFile("bin.dat", bytes);
    var result = mcsFileUtilities.ReadBinaryFile(path, _defaultOptions);
    Assert.Equal(bytes, result);
  }
  
  [Fact]
  public void TryReadBinaryFile_BoolOut_ShouldReturnTrue()
  {
    var bytes = new byte[] { 9, 8, 7 };
    var path = CreateTempFile("bin.dat", bytes);
    var success = mcsFileUtilities.TryReadBinaryFile(path, out var result, _defaultOptions);
    Assert.True(success);
    Assert.Equal(bytes, result);
  }
  
  [Fact]
  public void TryReadBinaryFile_Smart_ShouldReturnSuccess()
  {
    var bytes = new byte[] { 9, 8, 7 };
    var path = CreateTempFile("bin.dat", bytes);
    var result = mcsFileUtilities.TryReadBinaryFile(path, _defaultOptions);
    Assert.True(result.IsSuccess);
    Assert.Equal(bytes, result.Value);
  }
  
  [Fact]
  public void WriteBinaryFile_ShouldWriteBytes()
  {
    var path = Path.Combine(_testRoot, "binout.dat");
    var data = new byte[] { 4, 5, 6 };
    mcsFileUtilities.WriteBinaryFile(path, data, _defaultOptions);
    Assert.Equal(data, ReadFileBytes(path));
  }
  
  #endregion
  
  #region 🌊 Stream-based methods -----------------------------------------
  
  [Fact]
  public void ReadTextFromStream_ShouldReturnString()
  {
    using var ms = CreateTempStream("stream text");
    var result = mcsFileUtilities.ReadTextFromStream(ms, _defaultOptions);
    Assert.Equal("stream text", result);
  }
  
  [Fact]
  public void TryReadTextFromStream_BoolOut_ShouldReturnTrue()
  {
    using var ms = CreateTempStream("try text");
    var success = mcsFileUtilities.TryReadTextFromStream(ms, _defaultOptions, out var value);
    Assert.True(success);
    Assert.Equal("try text", value);
  }
  
  [Fact]
  public void TryReadTextFromStream_Smart_ShouldReturnSuccess()
  {
    using var ms = CreateTempStream("try smart text");
    var result = mcsFileUtilities.TryReadTextFromStream(ms, _defaultOptions);
    Assert.True(result.IsSuccess);
    Assert.Equal("try smart text", result.Value);
  }
  
  [Fact]
  public void WriteTextToStream_ShouldWriteToMemoryStream()
  {
    using var ms = new MemoryStream();
    mcsFileUtilities.WriteTextToStream(ms, "out text", _defaultOptions);
    var result = Encoding.UTF8.GetString(ms.ToArray());
    Assert.Equal("out text", result);
  }
  
  [Fact]
  public void ReadBinaryFromStream_ShouldReturnBytes()
  {
    using var ms = CreateTempStream(new byte[] { 1, 2, 3 });
    var result = mcsFileUtilities.ReadBinaryFromStream(ms, _defaultOptions);
    Assert.Equal(new byte[] { 1, 2, 3 }, result);
  }
  
  [Fact]
  public void TryReadBinaryFromStream_BoolOut_ShouldReturnTrue()
  {
    using var ms = CreateTempStream(new byte[] { 9, 8, 7 });
    var success = mcsFileUtilities.TryReadBinaryFromStream(ms, _defaultOptions, out var result);
    Assert.True(success);
    Assert.Equal(new byte[] { 9, 8, 7 }, result);
  }
  
  [Fact]
  public void TryReadBinaryFromStream_Smart_ShouldReturnSuccess()
  {
    using var ms = CreateTempStream(new byte[] { 9, 8, 7 });
    var result = mcsFileUtilities.TryReadBinaryFromStream(ms, _defaultOptions);
    Assert.True(result.IsSuccess);
    Assert.Equal(new byte[] { 9, 8, 7 }, result.Value);
  }
  
  [Fact]
  public void WriteBinaryToStream_ShouldWriteBytes()
  {
    using var ms = new MemoryStream();
    var data = new byte[] { 4, 5, 6 };
    mcsFileUtilities.WriteBinaryToStream(ms, data, _defaultOptions);
    Assert.Equal(data, ms.ToArray());
  }
  
  #endregion
  
  #region 🔗 File/Stream bridge methods -----------------------------------
  
  [Fact]
  public void FileFromStream_ShouldWriteStreamToFile()
  {
    var dest = Path.Combine(_testRoot, "stream2file.txt");
    using var input = CreateTempStream("bridge text");
    mcsFileUtilities.FileFromStream(input, dest, _defaultOptions);
    Assert.Equal("bridge text", ReadFileText(dest));
  }
  
  [Fact]
  public void FileToStream_ShouldWriteFileToStream()
  {
    var src = CreateTempFile("file2stream.txt", "bridge back");
    using var ms = new MemoryStream();
    
    mcsFileUtilities.FileToStream(src, ms, _defaultOptions);
    
    Assert.Equal("bridge back", Encoding.UTF8.GetString(ms.ToArray()));
  }
  
  #endregion
}

public class mcsFileUtilitiesTests2 : IDisposable
{
  private readonly string _testRoot;

  public mcsFileUtilitiesTests2()
  {
    _testRoot = Path.Combine(@"C:\temp", "mcsFileUtilitiesTests", Guid.NewGuid().ToString());
    Directory.CreateDirectory(_testRoot);
  }

  public void Dispose()
  {
    if (Directory.Exists(_testRoot))
    {
      try { Directory.Delete(_testRoot, true); } catch { /* ignore */ }
    }
  }

  #region Helpers
  
  private string CreateTempFile(string name, string content, mcsFileOptions options = null)
  {
    var path = Path.Combine(_testRoot, name);
    if (content != null)
      mcsFileUtilities.WriteTextFile(path, content, options ?? new mcsFileOptions());
    return path;
  }

  private string CreateTempFile(string name, byte[] data, mcsFileOptions options = null)
  {
    var path = Path.Combine(_testRoot, name);
    mcsFileUtilities.WriteBinaryFile(path, data, options ?? new mcsFileOptions());
    return path;
  }

  private MemoryStream CreateTempStream(string content)
      => new MemoryStream(Encoding.UTF8.GetBytes(content));

  private MemoryStream CreateTempStream(byte[] data)
      => new MemoryStream(data);

  private string ReadFileText(string path, mcsFileOptions options = null)
      => mcsFileUtilities.ReadTextFile(path, options ?? new mcsFileOptions());

  private byte[] ReadFileBytes(string path, mcsFileOptions options = null)
      => mcsFileUtilities.ReadBinaryFile(path, options ?? new mcsFileOptions());
      
  #endregion

  #region 📂 File-based methods -------------------------------------------
  
  [Fact]
  public void ReadJsonFile_ShouldBeValidJson()
  {
    var _testFile = Path.Combine(@"C:\temp", "integration_test_data.txt");
    Assert.True(File.Exists(_testFile), $"Test data file not found: {_testFile}");

    var json = mcsFileUtilities.ReadTextFile(_testFile, new mcsFileOptions());
    using var doc = JsonDocument.Parse(json);

    Assert.Equal("integration_001", doc.RootElement.GetProperty("test_id").GetString());
    Assert.Equal("Sample data for File Utility integration testing", doc.RootElement.GetProperty("description").GetString());
    var content = doc.RootElement.GetProperty("content");
    Assert.Equal("Hello, world! This is a test string with special chars: @#$%^&*()", content.GetProperty("text").GetString());
    Assert.Equal("こんにちは 🌟 Привет!", content.GetProperty("unicode").GetString());
    var metadata = content.GetProperty("metadata");
    Assert.Equal("1KB", metadata.GetProperty("size").GetString());
    Assert.Equal("2025-09-19T15:53:00Z", metadata.GetProperty("created").GetString());
    Assert.Contains("file", metadata.GetProperty("tags").EnumerateArray().Select(x => x.GetString()));
    var repeated = doc.RootElement.GetProperty("repeated_data").EnumerateArray();
    Assert.Contains("Line 1: Lorem ipsum dolor sit amet, consectetur adipiscing elit.", repeated.Select(x => x.GetString()));
  }

  [Theory]
  [InlineData("text_none_none_none_none.txt", "hello", DataProcessingModeEnum.None, DataProcessingModeEnum.None, EncryptionTypeEnum.NONE, CompressionTypeEnum.NONE)]
  [InlineData("text_enc_enc_none_none.txt", "hello", DataProcessingModeEnum.Encrypt, DataProcessingModeEnum.Encrypt, EncryptionTypeEnum.NONE, CompressionTypeEnum.NONE)]
  [InlineData("text_enc_enc_aes_none.txt", "hello", DataProcessingModeEnum.Encrypt, DataProcessingModeEnum.Encrypt, EncryptionTypeEnum.AES, CompressionTypeEnum.NONE)]
  [InlineData("text_comp_comp_none_none.txt", "hello", DataProcessingModeEnum.Compress, DataProcessingModeEnum.Compress, EncryptionTypeEnum.NONE, CompressionTypeEnum.NONE)]
  [InlineData("text_comp_comp_none_gzip.txt", "hello", DataProcessingModeEnum.Compress, DataProcessingModeEnum.Compress, EncryptionTypeEnum.NONE, CompressionTypeEnum.GZIP)]
  [InlineData("text_enccomp_enc_aes_gzip.txt", "hello", DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, DataProcessingModeEnum.Encrypt, EncryptionTypeEnum.AES, CompressionTypeEnum.GZIP)]
  [InlineData("text_enccomp_comp_aes_gzip.txt", "hello", DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, DataProcessingModeEnum.Compress, EncryptionTypeEnum.AES, CompressionTypeEnum.GZIP)]
  [InlineData("text_enccomp_enccomp_aes_gzip.txt", "hello", DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, EncryptionTypeEnum.AES, CompressionTypeEnum.GZIP)]
  public void TextFile_RoundTrip(string file_name, string content, DataProcessingModeEnum at_rest_mode, DataProcessingModeEnum in_transit_mode, EncryptionTypeEnum encryption_type, CompressionTypeEnum compression_type)
  {
    var path    = Path.Combine(_testRoot, file_name);
    var options = new mcsFileOptions { Encryption  = EncryptionFactory.Create(encryption_type)
                                      ,Compression = CompressionFactory.Create(compression_type)
                                      ,AtRest      = at_rest_mode
                                      ,InTransit   = in_transit_mode
                                      ,Logger      = ConsoleLogger };

    mcsFileUtilities.WriteTextFile(path, content, options);
    var result = mcsFileUtilities.ReadTextFile(path, options);
    Assert.Equal(content, result);

    var tryOutSuccess = mcsFileUtilities.TryReadTextFile(path, out var tryOutValue, options);
    Assert.True(tryOutSuccess);
    Assert.Equal(content, tryOutValue);

    var tryResult = mcsFileUtilities.TryReadTextFile(path, options);
    Assert.True(tryResult.IsSuccess);
    Assert.Equal(content, tryResult.Value);
  }
  
  [Theory]
  [InlineData("bin_none_none_none_none.dat", new byte[] { 1, 2, 3 }, DataProcessingModeEnum.None, DataProcessingModeEnum.None, EncryptionTypeEnum.NONE, CompressionTypeEnum.NONE)]  
  [InlineData("bin_enc_enc_none_none.dat", new byte[] { 1, 2, 3 }, DataProcessingModeEnum.Encrypt, DataProcessingModeEnum.Encrypt, EncryptionTypeEnum.NONE, CompressionTypeEnum.NONE)]
  [InlineData("bin_enc_enc_aes_none.dat", new byte[] { 1, 2, 3 }, DataProcessingModeEnum.Encrypt, DataProcessingModeEnum.Encrypt, EncryptionTypeEnum.AES, CompressionTypeEnum.NONE)]  
  [InlineData("bin_comp_comp_none_none.dat", new byte[] { 1, 2, 3 }, DataProcessingModeEnum.Compress, DataProcessingModeEnum.Compress, EncryptionTypeEnum.NONE, CompressionTypeEnum.NONE)]
  [InlineData("bin_comp_comp_none_gzip.dat", new byte[] { 1, 2, 3 }, DataProcessingModeEnum.Compress, DataProcessingModeEnum.Compress, EncryptionTypeEnum.NONE, CompressionTypeEnum.GZIP)]
  [InlineData("bin_enccomp_enc_aes_gzip.dat", new byte[] { 1, 2, 3 }, DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, DataProcessingModeEnum.Encrypt, EncryptionTypeEnum.AES, CompressionTypeEnum.GZIP)]
  [InlineData("bin_enccomp_comp_aes_gzip.dat", new byte[] { 1, 2, 3 }, DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, DataProcessingModeEnum.Compress, EncryptionTypeEnum.AES, CompressionTypeEnum.GZIP)]
  [InlineData("bin_enccomp_enccomp_aes_gzip.dat", new byte[] { 1, 2, 3 }, DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, EncryptionTypeEnum.AES, CompressionTypeEnum.GZIP)]
  public void BinaryFile_RoundTrip(string file_name, byte[] data, DataProcessingModeEnum at_rest_mode, DataProcessingModeEnum in_transit_mode, EncryptionTypeEnum encryption_type, CompressionTypeEnum compression_type)
  {
    var path    = Path.Combine(_testRoot, file_name);
    var options = new mcsFileOptions { Encryption  = EncryptionFactory.Create(encryption_type)
                                      ,Compression = CompressionFactory.Create(compression_type)
                                      ,AtRest      = at_rest_mode
                                      ,InTransit   = in_transit_mode
                                      ,Logger      = ConsoleLogger
    };

    mcsFileUtilities.WriteBinaryFile(path, data, options);
    var result = mcsFileUtilities.ReadBinaryFile(path, options);
    Assert.Equal(data, result);

    var tryOutSuccess = mcsFileUtilities.TryReadBinaryFile(path, out var tryOutValue, options);
    Assert.True(tryOutSuccess);
    Assert.Equal(data, tryOutValue);

    var tryResult = mcsFileUtilities.TryReadBinaryFile(path, options);
    Assert.True(tryResult.IsSuccess);
    Assert.Equal(data, tryResult.Value);
  }
  
  #endregion
  
  #region COMMENTED OUT:
  
  #region 🌊 Stream-based methods -----------------------------------------
  
  [Theory]
  [InlineData("stream2file_none_none_none_none.txt", "bridge text", DataProcessingModeEnum.None, DataProcessingModeEnum.None, EncryptionTypeEnum.NONE, CompressionTypeEnum.NONE)]
//  [InlineData("stream2file_enc.txt", "bridge text", DataProcessingModeEnum.Encrypt, DataProcessingModeEnum.Encrypt, EncryptionTypeEnum.AES, CompressionTypeEnum.NONE)]
//  [InlineData("stream2file_comp.txt", "bridge text", DataProcessingModeEnum.Compress, DataProcessingModeEnum.Compress, EncryptionTypeEnum.NONE, CompressionTypeEnum.GZIP)]
//  [InlineData("stream2file_enc_comp.txt", "bridge text", DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, EncryptionTypeEnum.AES, CompressionTypeEnum.GZIP)]
  public void FileFromStream_RoundTrip(string file_name, string content, DataProcessingModeEnum at_rest_mode, DataProcessingModeEnum in_transit_mode, EncryptionTypeEnum encryption_type, CompressionTypeEnum compression_type)
  {
    var dest = Path.Combine(_testRoot, file_name);
    var options = new mcsFileOptions { Encryption  = EncryptionFactory.Create(encryption_type)
                                      ,Compression = CompressionFactory.Create(compression_type)
                                      ,AtRest      = at_rest_mode
                                      ,InTransit   = in_transit_mode
                                      ,Logger      = ConsoleLogger };

    using var input = CreateTempStream(content);
    mcsFileUtilities.FileFromStream(input, dest, options);
    Assert.Equal(content, ReadFileText(dest, options));
  }
  
  [Theory]
  [InlineData("file2stream_none_none_none_none.txt", "bridge back", DataProcessingModeEnum.None, DataProcessingModeEnum.None, EncryptionTypeEnum.NONE, CompressionTypeEnum.NONE)]
  [InlineData("file2stream_enc_enc_aes_none.txt", "bridge back", DataProcessingModeEnum.Encrypt, DataProcessingModeEnum.Encrypt, EncryptionTypeEnum.AES, CompressionTypeEnum.NONE)]
  [InlineData("file2stream_comp_comp_none_gzip.txt", "bridge back", DataProcessingModeEnum.Compress, DataProcessingModeEnum.Compress, EncryptionTypeEnum.NONE, CompressionTypeEnum.GZIP)]
  [InlineData("file2stream_enccomp_enc_aes_gzip.txt", "bridge back", DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, DataProcessingModeEnum.Encrypt, EncryptionTypeEnum.AES, CompressionTypeEnum.GZIP)]
  [InlineData("file2stream_enccomp_comp_aes_gzip.txt", "bridge back", DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, DataProcessingModeEnum.Compress, EncryptionTypeEnum.AES, CompressionTypeEnum.GZIP)]
//  [InlineData("file2stream_enccomp_enccomp_aes_gzip.txt", "bridge back", DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, EncryptionTypeEnum.AES, CompressionTypeEnum.GZIP)]
  public void FileToStream_RoundTrip(string file_name, string content, DataProcessingModeEnum at_rest_mode, DataProcessingModeEnum in_transit_mode, EncryptionTypeEnum encryption_type, CompressionTypeEnum compression_type)
  {
    var src = CreateTempFile(file_name, content, new mcsFileOptions { Encryption  = EncryptionFactory.Create(encryption_type)
                                                                     ,Compression = CompressionFactory.Create(compression_type)
                                                                     ,AtRest      = at_rest_mode });

    var options = new mcsFileOptions { Encryption  = EncryptionFactory.Create(encryption_type)
                                      ,Compression = CompressionFactory.Create(compression_type)
                                      ,AtRest      = at_rest_mode
                                      ,InTransit   = in_transit_mode
                                      ,Logger      = ConsoleLogger };

    using var ms = new MemoryStream();
    mcsFileUtilities.FileToStream(src, ms, options);
    ms.Position = 0;
    var result = mcsFileUtilities.ReadTextFromStream(ms, options);
    Assert.Equal(content, result);
  }
  
  #endregion
  
  #region 🔗 File/Stream bridge methods -----------------------------------
  
  //[Fact]
  //public void FileFromStream_ShouldWriteStreamToFile()
  //{
  //  var dest = Path.Combine(_testRoot, "stream2file.txt");
  //  var opts = new FileOptions();
  //  using var input = CreateTempStream("bridge text");
  //  
  //  
  //  mcsFileUtilities.FileFromStream(input, dest, opts);
  //  Assert.Equal("bridge text", ReadFileText(dest));
  //}
  //
  //[Fact]
  //public void FileToStream_ShouldWriteFileToStream()
  //{
  //  var src = CreateTempFile("file2stream.txt", "bridge back");
  //  var opts = new FileOptions();
  //  using var ms = new MemoryStream();
  //  
  //  mcsFileUtilities.FileToStream(src, ms, opts);
  //  
  //  Assert.Equal("bridge back", Encoding.UTF8.GetString(ms.ToArray()));
  //}
  
  #endregion
  
  #region Stream-based methods --------------------------------------------

  [Theory]
  [InlineData("stream text", DataProcessingModeEnum.None, EncryptionTypeEnum.NONE, CompressionTypeEnum.NONE)]
  [InlineData("stream text", DataProcessingModeEnum.Encrypt, EncryptionTypeEnum.AES, CompressionTypeEnum.NONE)]
  [InlineData("stream text", DataProcessingModeEnum.Compress, EncryptionTypeEnum.NONE, CompressionTypeEnum.GZIP)]
//  [InlineData("stream text", DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, EncryptionTypeEnum.AES, CompressionTypeEnum.GZIP)]
  public void StreamText_RoundTrip(string content, DataProcessingModeEnum mode, EncryptionTypeEnum encryption_type, CompressionTypeEnum compression_type)
  {
    var options = new mcsFileOptions { Encryption  = EncryptionFactory.Create(encryption_type)
                                      ,Compression = CompressionFactory.Create(compression_type)
                                      ,InTransit   = mode
                                      ,Logger      = ConsoleLogger };

    using var ms = new MemoryStream();
    
    mcsFileUtilities.WriteTextToStream(ms, content, options);
    ms.Position = 0;
    var result = mcsFileUtilities.ReadTextFromStream(ms, options);
    Assert.Equal(content, result);

    ms.Position = 0;
    var tryOutSuccess = mcsFileUtilities.TryReadTextFromStream(ms, options, out var tryOutValue);
    Assert.True(tryOutSuccess);
    Assert.Equal(content, tryOutValue);

    ms.Position = 0;
    var tryResult = mcsFileUtilities.TryReadTextFromStream(ms, options);
    Assert.True(tryResult.IsSuccess);
    Assert.Equal(content, tryResult.Value);
  }

  [Theory]
  [InlineData(new byte[] { 1, 2, 3 }, DataProcessingModeEnum.None, EncryptionTypeEnum.NONE, CompressionTypeEnum.NONE)]
  [InlineData(new byte[] { 1, 2, 3 }, DataProcessingModeEnum.Encrypt, EncryptionTypeEnum.AES, CompressionTypeEnum.NONE)]
  [InlineData(new byte[] { 1, 2, 3 }, DataProcessingModeEnum.Compress, EncryptionTypeEnum.NONE, CompressionTypeEnum.GZIP)]
//  [InlineData(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, EncryptionTypeEnum.AES, CompressionTypeEnum.GZIP)]
  public void StreamBinary_RoundTrip(byte[] data, DataProcessingModeEnum in_transit_mode, EncryptionTypeEnum encryption_type, CompressionTypeEnum compression_type)
  {
    var options = new mcsFileOptions { Encryption  = EncryptionFactory.Create(encryption_type)
                                      ,Compression = CompressionFactory.Create(compression_type)
                                      ,InTransit   = in_transit_mode
                                      ,Logger      = ConsoleLogger };

    using var ms = new MemoryStream();
    
    mcsFileUtilities.WriteBinaryToStream(ms, data, options);
    ms.Position = 0;
    var result = mcsFileUtilities.ReadBinaryFromStream(ms, options);
    Assert.Equal(data, result);

    ms.Position = 0;
    var tryResult = mcsFileUtilities.TryReadBinaryFromStream(ms, options);
    Assert.True(tryResult.IsSuccess);
    Assert.Equal(data, tryResult.Value);

    ms.Position = 0;
    var tryOutSuccess = mcsFileUtilities.TryReadBinaryFromStream(ms, options, out var tryOutValue);
    Assert.True(tryOutSuccess);
    Assert.Equal(data, tryOutValue);
  }
  
  #endregion
  
  #endregion
}

public class mcsFileUtilitiesPerformanceTests : IDisposable
{
  private readonly string _testRoot;
  //private readonly mcsFileOptions _options = new mcsFileOptions();
  private readonly mcsFileOptions _options = new mcsFileOptions { Encryption     = EncryptionFactory.Create(EncryptionTypeEnum.NONE)
                                                                 ,Compression    = CompressionFactory.Create(CompressionTypeEnum.NONE)
                                                                 ,AtRest         = DataProcessingModeEnum.None
                                                                 ,InTransit      = DataProcessingModeEnum.None
                                                                 ,Logger         = ConsoleLogger
                                                                 ,FileBufferSize = 8192 };

  public mcsFileUtilitiesPerformanceTests()
  {
    _testRoot = Path.Combine(@"C:\temp", "mcsFileUtilitiesPerformanceTests", Guid.NewGuid().ToString());
    Directory.CreateDirectory(_testRoot);
  }

  public void Dispose()
  {
    if(Directory.Exists(_testRoot))
    {
      try 
      {
        Directory.Delete(_testRoot, recursive: true);
        Console.WriteLine($"[DEBUG] Deleted test directory: {_testRoot}");
      }
      catch(Exception ex)
      {
        Console.WriteLine($"[ERROR] Failed to delete {_testRoot}: {ex.Message}");
      }
    }
  }

  [Theory]
  [Trait("Category", "Performance")]
  [InlineData(5)]    //   5 MB
  [InlineData(10)]   //  10 MB
  [InlineData(25)]   //  25 MB
  [InlineData(50)]   //  50 MB
  [InlineData(100)]  // 100 MB
  public void FileRoundTrip_WithBuffer_PerformanceTest(int sizeInMb)
  {
    // Arrange
    var bytes = new byte[sizeInMb * 1024 * 1024];
    new Random(42).NextBytes(bytes); // fill with random but repeatable data

    var srcPath   = Path.Combine(_testRoot, $"source_{sizeInMb}MB.bin");
    var destPath  = Path.Combine(_testRoot, $"roundtrip_{sizeInMb}MB.bin");    
    File.WriteAllBytes(srcPath, bytes);

    // Act
    var sw = Stopwatch.StartNew();
  
    // Step 1: File → Stream
    byte[] buffer;
    using(var ms = new MemoryStream())
    {
      mcsFileUtilities.FileToStream(srcPath, ms, _options);
      buffer = ms.ToArray();
    }
    
    // Step 2: Stream → File
    using(var ms = new MemoryStream(buffer))
    {
      mcsFileUtilities.FileFromStream(ms, destPath, _options); 
      
      //ONLY HERE to force LINQPad to allow collapsing of this using statement
      var x = 0;
    }

    sw.Stop();
    var elapsed = sw.Elapsed;

    // Assert
    var roundTripped = File.ReadAllBytes(destPath);
    Assert.Equal(bytes.Length, roundTripped.Length);

    // Report
    Console.WriteLine($"[Performant Test ({nameof(FileRoundTrip_WithBuffer_PerformanceTest)})] {sizeInMb}MB round-trip completed in {elapsed.TotalSeconds:F2}s "
                    + $"({(bytes.Length / (1024.0 * 1024.0) / elapsed.TotalSeconds):F2} MB/s)");
  }
  
  [Theory]
  [Trait("Category", "Performance")]
  [InlineData(5, DataProcessingModeEnum.None, DataProcessingModeEnum.None, EncryptionTypeEnum.NONE, CompressionTypeEnum.NONE)]
  [InlineData(5, DataProcessingModeEnum.Encrypt, DataProcessingModeEnum.Encrypt, EncryptionTypeEnum.AES, CompressionTypeEnum.NONE)]
  [InlineData(5, DataProcessingModeEnum.Compress, DataProcessingModeEnum.Compress, EncryptionTypeEnum.NONE, CompressionTypeEnum.GZIP)]
  [InlineData(5, DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, EncryptionTypeEnum.AES, CompressionTypeEnum.GZIP)]
  public void FileRoundTrip_WithoutBuffer_PerformanceTest(int file_size_MBs, DataProcessingModeEnum at_rest, DataProcessingModeEnum in_transit, EncryptionTypeEnum encryption, CompressionTypeEnum compression)
  {
    // Arrange
    var srcPath  = Path.Combine(_testRoot, $"perf_src_{file_size_MBs}MB_{at_rest}_{in_transit}.bin");
    var destPath = Path.Combine(_testRoot, $"perf_dest_{file_size_MBs}MB_{at_rest}_{in_transit}.bin");    
    
    var data = new byte[file_size_MBs * 1024 * 1024];
    new Random(42).NextBytes(data); // deterministic fill
    
    var options = new mcsFileOptions { Encryption     = EncryptionFactory.Create(encryption)
                                      ,Compression    = CompressionFactory.Create(compression)
                                      ,AtRest         = at_rest
                                      ,InTransit      = in_transit
                                      ,Logger         = ConsoleLogger
                                      ,FileBufferSize = 8192 };
    
    // Write source file with matching processing ...
    Console.WriteLine($"[DEBUG] Before WriteBinaryFile (size: {file_size_MBs}MB, at_rest: {at_rest}, in_transit: {in_transit})");
    mcsFileUtilities.WriteBinaryFile(srcPath, data, options);
    Console.WriteLine($"[DEBUG] After WriteBinaryFile: srcSize={new FileInfo(srcPath).Length}");
    
    var sw = Stopwatch.StartNew();

    // Act
    using(var ms = new MemoryStream())
    {
      Console.WriteLine($"[DEBUG] Before FileToStream (size: {file_size_MBs}MB, at_rest: {at_rest}, in_transit: {in_transit})");
      // File → Stream
      mcsFileUtilities.FileToStream(srcPath, ms, _options, leave_open: true);      
      Console.WriteLine($"[DEBUG] After FileToStream: ms.Length={ms.Length}, ms.Position={ms.Position}");
      
      ms.Position = 0;
      
      Console.WriteLine($"[DEBUG] Before FileFromStream (size: {file_size_MBs}MB, at_rest: {at_rest}, in_transit: {in_transit})");
      // Stream → File
      mcsFileUtilities.FileFromStream(ms, destPath, _options, leave_open: true);
      Console.WriteLine($"[DEBUG] After FileFromStream: destSize={new FileInfo(destPath).Length}");
      if(new FileInfo(destPath).Length % 16 != 0)
      {
        File.ReadAllBytes(destPath).Dump($"Destination file bytes (mode={at_rest})", 0);
      }
    }

    sw.Stop();
    var elapsed = sw.Elapsed;

    // Assert
    Console.WriteLine($"[DEBUG] Before ReadBinaryFile: destPath={destPath}");
    var round_trip = mcsFileUtilities.ReadBinaryFile(destPath, options);
    Assert.Equal(data.Length, round_trip.Length);
    Assert.Equal(data, round_trip);
    
    // Hash Check
    using var srcFs  = File.OpenRead(srcPath);
    using var destFs = File.OpenRead(destPath);
    
    using var sha = System.Security.Cryptography.SHA256.Create();
    
    var srcHash  = BitConverter.ToString(sha.ComputeHash(srcFs));
    var destHash = BitConverter.ToString(sha.ComputeHash(destFs));
    
    Assert.Equal(srcHash, destHash);

    // Debug output for verification
    Console.WriteLine( $"[Performance Test] Roundtrip {file_size_MBs}MB (at_rest: {at_rest}, in_transit: {in_transit}): " 
                     + $"expected={data.Length}, actual={round_trip.Length}, hashMatch={(srcHash == destHash)}, "
                     + $"time={elapsed.TotalSeconds:F2}s ({(data.Length / (1024.0 * 1024.0) / elapsed.TotalSeconds):F2} MB/s)");
  }

  [Theory]
  [InlineData(DataProcessingModeEnum.None, EncryptionTypeEnum.NONE, CompressionTypeEnum.NONE)]
  [InlineData(DataProcessingModeEnum.Encrypt, EncryptionTypeEnum.AES, CompressionTypeEnum.NONE)]
  [InlineData(DataProcessingModeEnum.Compress, EncryptionTypeEnum.NONE, CompressionTypeEnum.GZIP)]
  [InlineData(DataProcessingModeEnum.Encrypt | DataProcessingModeEnum.Compress, EncryptionTypeEnum.AES, CompressionTypeEnum.GZIP)]
  public void FileRoundTrip_ZeroLength(DataProcessingModeEnum mode, EncryptionTypeEnum encryption, CompressionTypeEnum compression)
  {
    var src  = Path.Combine(_testRoot, $"zero_{mode}.bin");
    var dest = Path.Combine(_testRoot, $"zero_dest_{mode}.bin");
    
    var options = new mcsFileOptions { Encryption     = EncryptionFactory.Create(encryption)
                                      ,Compression    = CompressionFactory.Create(compression)
                                      ,AtRest         = mode
                                      ,InTransit      = mode
                                      ,Logger         = ConsoleLogger
                                      ,FileBufferSize = 8192 };

    Console.WriteLine($"[DEBUG] ZeroLength (mode={mode}): Before WriteBinaryFile");
    
    mcsFileUtilities.WriteBinaryFile(src, Array.Empty<byte>(), options);
    var srcSize = new FileInfo(src).Length;
    
    Console.WriteLine($"[DEBUG] ZeroLength (mode={mode}): After WriteBinaryFile, srcSize={new FileInfo(src).Length}");    
    if(srcSize > 0)
    {
      File.ReadAllBytes(src).Dump($"Source file bytes (mode={mode}, encryption={encryption}, compression={compression})");
    }    
    
    using var ms = new MemoryStream();
    
    Console.WriteLine($"[DEBUG] ZeroLength (mode={mode}, encryption={encryption}, compression={compression}): Before FileToStream");
    
    mcsFileUtilities.FileToStream(src, ms, options, leave_open: true);
    
    Console.WriteLine($"[DEBUG] ZeroLength (mode={mode}, encryption={encryption}, compression={compression}): After FileToStream, ms.Length={ms.Length}, ms.Position={ms.Position}");
    
    ms.Position = 0;
    
    Console.WriteLine($"[DEBUG] ZeroLength (mode={mode}, encryption={encryption}, compression={compression}): Before FileFromStream");
    
    mcsFileUtilities.FileFromStream(ms, dest, options, leave_open: true);
    var destSize = new FileInfo(dest).Length;    
    
    Console.WriteLine($"[DEBUG] ZeroLength (mode={mode}, encryption={encryption}, compression={compression}): After FileFromStream, destSize={destSize}");
    if(destSize > 0)
    {
      File.ReadAllBytes(dest).Dump($"Destination file bytes (mode={mode}, encryption={encryption}, compression={compression})");
    }
    
    var result = mcsFileUtilities.ReadBinaryFile(dest, options);
    
    Assert.Empty(result);   
    Console.WriteLine($"[DEBUG] ZeroLength (mode={mode}, encryption={encryption}, compression={compression}): srcSize={srcSize}, destSize={destSize}, resultLength={result.Length}");
  }
  
  // Cleanup verification
  [Fact]
  public void FileToStream_LeaveOpenBehavior()
  {
    var src = Path.Combine(_testRoot, "test.bin");
    var data = new byte[1024];
    new Random(42).NextBytes(data);
    mcsFileUtilities.WriteBinaryFile(src, data, _options);

    using var ms = new MemoryStream();
    mcsFileUtilities.FileToStream(src, ms, _options, leave_open: true);
    Assert.True(ms.CanSeek, "Stream should remain open with leave_open: true");

    using var ms2 = new MemoryStream();
    mcsFileUtilities.FileToStream(src, ms2, _options, leave_open: false);
    Assert.False(ms2.CanSeek, "Stream should be closed with leave_open: false");
  }

  [Fact(Skip = "LINQPad test runner holds references")]
  public void FileToStream_MemoryCleanup()
  {
    var src = Path.Combine(_testRoot, "test.bin");
    var data = new byte[1024 * 1024]; // 1MB
    
    new Random(42).NextBytes(data);
    mcsFileUtilities.WriteBinaryFile(src, data, _options);

    WeakReference msRef;
    
    using(var ms = new MemoryStream())
    {
      msRef = new WeakReference(ms);
      mcsFileUtilities.FileToStream(src, ms, _options, leave_open: true);
      ms.Flush();
      ms.Position = 0;  //Reset to ensure no pending operations
    }
    
    GC.Collect(2, GCCollectionMode.Forced, true);
    GC.WaitForPendingFinalizers();
    GC.Collect(2, GCCollectionMode.Forced, true); // Double GC for LINQPad
    
    Assert.False(msRef.IsAlive, "MemoryStream should be collectible");
  }
}




