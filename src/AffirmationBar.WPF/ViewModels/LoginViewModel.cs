﻿using SusiAPIClient;
using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffirmationBar.WPF.ViewModels
{
	class LoginViewModel
	{
		private SusiClient susiClient = new SusiClient();

		public async Task<StudentInfo> GetStudentInfoAsync()
		{
			StudentInfo studentInfo = null;

			if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
			{
				IsLoading = true;
				studentInfo = await susiClient.GetStudentInfoAsync(username, password);
				IsLoading = false;
			}

			return studentInfo;
		}

		private bool isLoading;
		public bool IsLoading
		{
			get
			{
				return isLoading;
			}
			set
			{
				isLoading = value;
				OnPropertyChanged();
			}
		}

		private string username;
		public string Username
		{
			get
			{
				return username;
			}
			set
			{
				username = value;
				OnPropertyChanged();
			}
		}

		private string password;
		public string Password
		{
			get
			{
				return password;
			}
			set
			{
				password = value;
				OnPropertyChanged();
			}
		}
	}
}