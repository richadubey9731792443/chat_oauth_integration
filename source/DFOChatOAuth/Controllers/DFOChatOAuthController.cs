using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using NLog;

namespace DFOChatOAuth.Controllers
{
    public class DFOChatOAuthController : ApiController
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        [HttpPost]
        [Route("authorize")]
        public IHttpActionResult Authorize(AuthorizationRequest request)
        {
            Log.Trace("Start Authorize");
            if (request != null)
            {
                Log.Debug("grant_type: " + request.grant_type);
                Log.Debug("client_id: " + request.client_id);
                Log.Debug("client_secret: " + request.client_secret);
                Log.Debug("code: " + request.code);
                Log.Debug("redirect_uri: " + request.redirect_uri);
            }

            if (request == null || request.grant_type != "authorization_code")
            {
                Log.Info("Invalid request.");
                return BadRequest("Invalid request.");
            }

            // Generate access and refresh tokens.
            string accessToken = "ea7afba053836fa62f49a40c0c08f5580cf61057";
            string refreshToken = "727e2187147b1fa614a29079c30b3883119f4598";
            int expiresIn = 60; //1 minute in seconds

            var response = new
            {
                access_token = accessToken,
                token_type = "bearer",
                refresh_token = refreshToken,
                expires_in = expiresIn
            };

            Log.Debug("Json response: " + JsonConvert.SerializeObject(response));
            Log.Trace("End Authorize");
            return Ok(response);
        }


        [HttpPost]
        [Route("refresh")]
        public IHttpActionResult Refresh(RefreshRequest request)
        {
            Log.Trace("Start Refresh");

            if (request != null)
            {
                Log.Debug("grant_type: " + request.grant_type);
                Log.Debug("client_id: " + request.client_id);
                Log.Debug("client_secret: " + request.client_secret);
                Log.Debug("refresh_token: " + request.refresh_token);
            }

            if (request == null || request.grant_type != "refresh_token")
            {
                Log.Info("Invalid request.");
              
                return BadRequest("Invalid request.");
            }

            string newRefreshToken = "1c7d477f8dc37c37915d2b52a0efa723d27caf87";
            int expiresIn = 1800; // 30 minutes in seconds

            var response = new
            {
                access_token = request.refresh_token,
                token_type = "Bearer",
                refresh_token = newRefreshToken,
                expires_in = expiresIn
            };

            Log.Debug("Json response: " + JsonConvert.SerializeObject(response));
            Log.Trace("End Refresh");
            return Ok(response);
        }

        [HttpGet]
        [Route("me")]
        public IHttpActionResult GetMe()
        {
            Log.Trace("Start Me");

            // Check if the authorization header is present in the request.
            if (!Request.Headers.Contains("Authorization"))
            {
                Log.Info("Request does not contain authorization header.");
                return Unauthorized();
            }

            // Get the authorization header value.
            string authorizationHeader = Request.Headers.GetValues("Authorization").FirstOrDefault();
            Log.Debug("authorization header value: " + authorizationHeader);

            // Create the JSON response.
            var response = new
            {
                //user = new
                //{
                //    id = "110",
                //    firstName = "Luke",
                //    lastName = "Johnson"
                //},
                //consumerCustomFields = new
                //{
                //    consumerCustomFieldIdent = "subject",
                //    other = "field"
                //},
                //consumerContactCustomFields = new
                //{
                //    consumerContactCustomFieldIdent = "thread",
                //    other = "field"
                //}
                customerId = "12345",
                customerFirstName = "John",
                customerLastName = "Doe",
                customerSubject = "Hello from PSE"
            };

            Log.Debug("Json response: " + JsonConvert.SerializeObject(response));
            Log.Trace("End Me");
            return Json(response);
        }

        [HttpGet]
        [Route("test")]
        public IHttpActionResult TestPath()
        {
            Log.Trace("Start TestPath");
            Log.Trace("End TestPath");
            return Ok();
        }
    }
}
