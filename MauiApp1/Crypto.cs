using System.Security.Cryptography;
using System.Text;

namespace MauiApp1 {
    public static class Crypto
    {
        /*
            NOTE: The key and initialization vector (IV) used in this example are hardcoded.
            A real world application would replace the key and initialization vector (IV) with its 
            own values. The key and IV must be of a certain length. For AES, the key can be 128 bits 
            (16 bytes), 192 bits (24 bytes), or 256 bits (32 bytes) long, and the IV is always 128 
            bits (16 bytes) long.
        */
        
        /*
            Cypher Key - In the context of cryptography, a cipher key (often just called a "key") is 
            a piece of information used in combination with an algorithm (a cipher) to transform 
            plaintext into ciphertext (encryption) and vice versa (decryption). The key is what makes 
            the transformation secure, because even if someone knows the algorithm used, they can't 
            decrypt the ciphertext back into plaintext without the key. The key is meant to be a secret, 
            known only to the sender and the receiver of the encrypted data.
        */
        private readonly static string key = "abcdefghijklmnop";
        
        /*
            Initialization Vector (IV) - In cryptography, an Initialization Vector (IV) is an arbitrary 
            number that can be used along with a secret key for data encryption. This number, also called 
            a nonce, is used only once in any session. The use of an IV prevents repetition during the data 
            encryption process, making it impossible for hackers to hack the encrypted data. Essentially, 
            an IV is an added input to a cryptographic primitive, making it more random and thereby more 
            secure.
        */
        private readonly static string iv = "abcdefghijklmnop";
        
        /*
            The DecryptString method takes a string of encrypted text as a parameter and returns a string 
            of plain text. The method first converts the cipher text from a Base64 string to a byte array. 
            It then converts the encryption key and initialization vector (IV) to byte arrays. The method 
            creates a new AES instance and sets the key and IV for the instance. Next, the method creates a 
            decrypter that uses the key and IV. It creates a memory stream to hold the cipher text bytes, a 
            crypto stream that reads from the memory stream and decrypts the data, and a stream reader that 
            reads from the crypto stream. The method reads all decrypted data from the stream reader and 
            returns it.
        */
        public static string DecryptString(string textToDecrypt)
        {
            // Convert the cipher text from a Base64 string to a byte array
            byte[] cipherTextBytes = Convert.FromBase64String(textToDecrypt);
            
            // Convert the encryption key and initialization vector (IV) to byte arrays
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

            // Create a new AES instance
            using (Aes aes = Aes.Create())
            {
                // Set the key and IV for the AES instance
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                // Create a decrypter that uses the key and IV
                ICryptoTransform decrypter = aes.CreateDecryptor(aes.Key, aes.IV);

                // Create a memory stream that holds the cipher text bytes
                using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
                {
                    // Create a crypto stream that reads from the memory stream and decrypts the data
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decrypter, CryptoStreamMode.Read))
                    {
                        // Create a stream reader that reads from the crypto stream
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read all decrypted data from the stream reader and return it
                            String decryptedText = srDecrypt.ReadToEnd();
                            return decryptedText.ToString();
                        }
                    }
                }
            }
        }

        /*
            The EncryptString method takes a string of plain text as a parameter and returns a string of 
            encrypted text. The method first converts the plain text, encryption key, and initialization 
            vector (IV) to byte arrays. It then creates a new AES instance and sets the key and IV for the 
            instance. Next, the method creates an encrypter that uses the key and IV. It creates a memory 
            stream to hold the encrypted data, a crypto stream that writes to the memory stream and encrypts 
            the data, and a stream writer that writes to the crypto stream. The method writes the plain text 
            bytes to the stream writer, converts the encrypted data from a byte array to a Base64 string, and 
            returns the string.
        */
        public static string EncryptString(string plainText)
        {
            // Convert the plain text, encryption key, and initialization vector (IV) from strings to byte arrays
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

            // Create a new AES instance
            using (Aes aes = Aes.Create())
            {
                // Set the key and IV for the AES instance
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                // Create an encrypt3r that uses the key and IV
                ICryptoTransform encrypt3r = aes.CreateEncryptor(aes.Key, aes.IV);

                // Create a memory stream that will hold the encrypted data
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // Create a crypto stream that can write to the memory stream and encrypts the data
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encrypt3r, CryptoStreamMode.Write))
                    {
                        // Create a stream writer that can write to the crypto stream
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            // Write the plain text bytes to the stream writer
                            swEncrypt.Write(plainText);
                        }
                        // Convert the encrypted data from a byte array to a Base64 string and return it
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }
    }
}
