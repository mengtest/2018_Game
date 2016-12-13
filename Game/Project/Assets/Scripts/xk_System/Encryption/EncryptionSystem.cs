﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using xk_System.Debug;
using System.IO;
using System.Text;
using System;

namespace xk_System.Crypto
{
    public abstract class EncryptionSystem
    {
        private static EncryptionSystem single;
        public EncryptionSystem()
        {

        }

        public static T getSingle<T>() where T : EncryptionSystem, new()
        {
            if (single == null)
            {
                single = new T();
            }
            return (T)single;
        }
    }
    /// <summary>
    /// md5是一个不可逆的过程（单向性）
    /// 作用：验证数据的完整性，正确性。判断数据是否被修改过了
    /// </summary>
    public class EncryptionSystem_md5 : EncryptionSystem
    {
        /// <summary>
        /// 对字符串进行md5签名
        /// </summary>
        /// <param name="s"></param>
        public string Encryption(string s)
        {
            MD5 md = new MD5CryptoServiceProvider();
            byte[] bytes = md.ComputeHash(System.Text.Encoding.UTF8.GetBytes(s));
            string MD5Str = "";
            foreach (byte b in bytes)
            {
                MD5Str += b.ToString();
            }
            DebugSystem.Log("md5:" + MD5Str);

            return MD5Str;
        }
        /// <summary>
        /// 对文件流进行md5签名
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public string Encryption(Stream stream)
        {

            MD5 md5serv = MD5CryptoServiceProvider.Create();

            byte[] buffer = md5serv.ComputeHash(stream);

            StringBuilder sb = new StringBuilder();

            foreach (byte var in buffer)
            {
                sb.Append(var.ToString());
            }
            return sb.ToString();

        }

    }

    internal class Encryption_DES : EncryptionSystem
    {
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="data">加密数据</param>
        /// <param name="key">8位字符的密钥字符串</param>
        /// <param name="iv">8位字符的初始化向量字符串</param>
        /// <returns></returns>
        public string Encryption(string data, string key, string iv)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(iv);
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            string str = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            DebugSystem.Log("DES:" + str);
            return str;
        }



        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="data">解密数据</param>
        /// <param name="key">8位字符的密钥字符串(需要和加密时相同)</param>
        /// <param name="iv">8位字符的初始化向量字符串(需要和加密时相同)</param>
        /// <returns></returns>
        public string Decryption(string data, string key, string iv)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(iv);
            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

            MemoryStream ms = new MemoryStream(byEnc);

            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);

            StreamReader sr = new StreamReader(cst);

            string str = sr.ReadToEnd();

            DebugSystem.Log("DES_Decryption:" + str);
            return str;

        }


    }
    /// <summary>
    /// 本类无法解析Linux上的AES密文,现在可以解析了
    /// </summary>
    internal class Encryption_AES : EncryptionSystem
    {
        /// <summary>  
        /// AES加密  
        /// </summary>  
        /// <param name="Data">被加密的明文</param>  
        /// <param name="Key">密钥:it should be 16, 24 or 32 bytes long</param>  
        /// <param name="Vector">向量:it should be 16 bytes long</param>  
        /// <returns>密文</returns>  
        public byte[] Encryption(Byte[] Data, string Key, string Vector)
        {
            Byte[] bKey = Encoding.UTF8.GetBytes(Key);
            Byte[] bVector = Encoding.UTF8.GetBytes(Vector);
            Byte[] Cryptograph = null; // 加密后的密文  

            Rijndael Aes = Rijndael.Create();
            Aes.Mode = CipherMode.CBC;
            Aes.Key = bKey;
            Aes.IV = bVector;
            //printAesInfo(Aes);
            ICryptoTransform cTransform = Aes.CreateEncryptor();
            try
            {
                Cryptograph = cTransform.TransformFinalBlock(Data, 0, Data.Length);
            }catch(Exception e)
            {
                DebugSystem.LogError("Encryption_AES: "+e.Message);
            }
            if (Cryptograph == null)
            {
                DebugSystem.LogError("Encryption_AES: value is null");
            }
            Aes.Clear();
            return Cryptograph;
        }
        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="Data">被解密的密文</param>  
        /// <param name="Key">密钥:it should be 16, 24 or 32 bytes long.</param>  
        /// <param name="Vector">向量:it should be 16 bytes long</param>  
        /// <returns>明文</returns>        
        public byte[] Decryption(Byte[] Data, string Key, string Vector)
        {
            DebugSystem.Log("Data Length:" + Data.Length);
            Byte[] bKey = Encoding.UTF8.GetBytes(Key);
            Byte[] bVector = Encoding.UTF8.GetBytes(Vector);
            Byte[] original = null; // 解密后的明文  

            Rijndael Aes = Rijndael.Create();
           // Aes.Padding = PaddingMode.Zeros;
            Aes.Mode = CipherMode.CBC;
            Aes.Key = bKey;
            Aes.IV = bVector;
            //printAesInfo(Aes);
            ICryptoTransform cTransform = Aes.CreateDecryptor();
            try
            {
                original = cTransform.TransformFinalBlock(Data, 0, Data.Length);
            }
            catch (Exception e)
            {
                DebugSystem.Log("Decryption_AES:" + e.Message);
            }
            Aes.Clear();

            if (original == null)
            {
                DebugSystem.LogError("Decryption_AES: value is null");
            }
            return original;
        }

        private void printAesInfo(Rijndael Aes)
        {
            DebugSystem.Log("Mode:"+Aes.Mode);
            DebugSystem.Log("KeySize:" + Aes.KeySize);
            DebugSystem.Log("BlockSize:" + Aes.BlockSize);
            DebugSystem.Log("FeedBackSize:" + Aes.FeedbackSize);
            DebugSystem.Log("Padding:" + Aes.Padding);

        }
}


    internal class Encryption_DSA : EncryptionSystem
    {
        public string Encryption()
        {
            return "";
        }

        public string Decryption()
        {
            return "";
        }
    }
    /// <summary>
    /// 数字签名，单向性
    /// </summary>
    internal class Encryption_ECDSA : EncryptionSystem
    {
        /*private Key myPrivateKey;
        public byte[] myPublicKey;

        public void CreateKeys()
        {
            MyPrivateKey = CngKey.Create(CngAlgorithm.ECDsaP256);
            myPublicKey = MyPrivateKey.Export(CngKeyBlobFormat.GenericPublicBlob);
        }

        public byte[] CreateSignature(byte[] data, CngKey key)
        {
            byte[] signature;
            using (ECDsaCng signingAlg = new ECDsaCng(key))
            {
                signature = signingAlg.SignData(data);
                signingAlg.Clear();
            }

            return signature;
        }

        public bool VerifySignature(byte[] data, byte[] signature, byte[] pubKey)
        {
            bool retValue = false;
            using (CngKey key = CngKey.Import(pubKey, CngKeyBlobFormat.GenericPublicBlob))
            {
                using (ECDsaCng signingAlg = new ECDsaCng(key))
                {
                    retValue = signingAlg.VerifyData(data, signature);
                    signingAlg.Clear();
                }
            }
            return retValue;
        }*/
    }

    internal class Encryption_DiffieHellman : EncryptionSystem
    {
        /*public CngKey myPrivateKey;
        public byte[] myPublicKey;

        public void CreateKeys()
        {
            myPrivateKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
            myPublicKey = myPrivateKey.Export(CngKeyBlobFormat.EccPublicBlob);
        }

        public byte[] CreateSignature(byte[] data, CngKey key)
        {
            byte[] signature;
            using (ECDsaCng signingAlg = new ECDsaCng(key))
            {
                signature = signingAlg.SignData(data);
                signingAlg.Clear();
            }

            return signature;
        }*/


    }

}