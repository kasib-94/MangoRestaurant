﻿namespace Mango.Web.Models;
using static Mango.Web.SD;

public class ApiRequest
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string Url { get; set; }
    public object Data { get; set; }
    public string AccessToken { get; set; }
}