﻿using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class HashComputer
    {
        public string GetPasswordHashAndSalt(string message)
        {
            // Let us use SHA256 algorithm to 
            // generate the hash from this salted password
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] dataBytes = Utility.GetBytes(message);
            byte[] resultBytes = sha.ComputeHash(dataBytes);

            // return the hash string to the caller
            return Utility.GetString(resultBytes);
        }
    }

    public class MockUserRepository
    {
        // Function to add the user to im memory dummy DB
        //public int AddUser(PB_USER user)
        //{
        //    int returnValue = 0;
        //    try
        //    {
        //        using (var context = new PASTEBOOKEntities())
        //        {
        //            context.PB_USER.Add(user);
        //            returnValue = context.SaveChanges();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return returnValue;
        //}

        // Function to retrieve the user based on user id
        public PB_USER GetUser(string userName)
        {
            var results = new PB_USER();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    results = context.PB_USER.Where(x => x.USER_NAME == userName).SingleOrDefault();

                    return results;
                }
            }
            catch (Exception)
            {
            }
            return results;
        }
    }

    public class PasswordManager
    {
        HashComputer m_hashComputer = new HashComputer();

        public string GeneratePasswordHash(string plainTextPassword, out string salt)
        {
            salt = SaltGenerator.GetSaltString();

            string finalString = plainTextPassword + salt;

            return m_hashComputer.GetPasswordHashAndSalt(finalString);
        }

        public bool IsPasswordMatch(string password, string salt, string hash)
        {
            string finalString = password + salt;
            return hash == m_hashComputer.GetPasswordHashAndSalt(finalString);
        }

    }

    public static class SaltGenerator
    {
        private static RNGCryptoServiceProvider m_cryptoServiceProvider = null;
        private const int SALT_SIZE = 24;

        static SaltGenerator()
        {
            m_cryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        public static string GetSaltString()
        {
            // Lets create a byte array to store the salt bytes
            byte[] saltBytes = new byte[SALT_SIZE];

            // lets generate the salt in the byte array
            m_cryptoServiceProvider.GetNonZeroBytes(saltBytes);

            // Let us get some string representation for this salt
            string saltString = Utility.GetString(saltBytes);

            // Now we have our salt string ready lets return it to the caller
            return saltString;
        }
    }

    public class Utility
    {
        public static byte[] GetBytes(string message)
        {
            return Encoding.ASCII.GetBytes(message);
        }

        public static string GetString(byte[] resultBytes)
        {
            return Encoding.ASCII.GetString(resultBytes);
        }


    }
}
