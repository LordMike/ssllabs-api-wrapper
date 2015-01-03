﻿using System.Net;
using SSLLWrapper.Models;

namespace SSLLWrapper.Interfaces
{
	interface IApiProvider
	{
		HttpWebResponse MakeGetRequest(RequestModel requestModel);
	}
}
