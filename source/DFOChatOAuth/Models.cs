﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DFOChatOAuth
{
    public class AuthorizationRequest
    {
        public string grant_type { get; set; }
        public string code { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string redirect_uri { get; set; }
    }

    public class RefreshRequest
    {
        public string grant_type { get; set; }
        public string refresh_token { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
    }
}