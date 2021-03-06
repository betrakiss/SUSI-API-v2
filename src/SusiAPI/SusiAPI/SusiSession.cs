﻿using SusiAPI.Parser;
using SusiAPI.Responses;
using SusiAPICommon.Models;
using System;
using System.Threading.Tasks;

namespace SusiAPI
{
    public class SusiSession
    {
        private ISusiParser parser;
        public SusiSession(ISusiParser parser = null)
        {
            this.parser = parser ?? new ClassicSusiParser();
        }
        
        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            return await parser.LoginAsync(username, password);
        }
        
        public bool IsCurrentlyAStudent()
        {
            if (!parser.IsAuthenticated)
                throw new Exception("Use login method first.");

            return parser.IsCurrentlyAStudent;
        }

        public async Task<StudentInfo> GetStudentInfoAsync(string number = null)
        {
            if (!parser.IsAuthenticated)
                throw new Exception("Use login method first.");

            if (!String.IsNullOrEmpty(number))
                await parser.SelectRoleAsync(number);
            
            return await parser.GetStudentInfoAsync();
        }
    }
}
