using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaPath.Constants{
    public class WebConstants
    {
        public const string ModeTooltip = "Development env: 1, Localhost: 2, No-Internet: 3";
        public const string EnvCloudUrl = "CLOUD_URL";
        public const string EnvLocalUrl = "LOCAL_URL";
        public const string EnvSampleData = "SAMPLE_DATA";
        public const string EnvOauthUrl = "OAUTH_URL";
        public const string Authorization = "Authorization";
        public const string Bearer = "Bearer ";
        public const string NoInternetMessage = "Switching to No-Internet Env";
        public const string EmptyUrl = "";
        public const string Basic = "Basic ";
        public const string EnvClientCredetials = "CLIENT_CREDENTIALS";
        public const string ContentType = "Content-Type";
        public const string MimeType = "application/x-www-form-urlencoded";
        public const string AccessToken = "access_token";
        public const string GrantType = "grant_type=client_credentials";
        public const int ModeDevelopmentEnv = 1;
        public const int ModeLocalEnv = 2;
        public const int ModeNoInternet = 3;
    } 
}
