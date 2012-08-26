using Nancy;
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using EventLibrary.ServiceBrokers;
using EventLibrary.Entities;
using Nancy.ModelBinding;
/*
 * Dave: Slight modifications to integrate with Nancy
 * Credits to rafek for making my life easier
 * Project Source: https://github.com/rafek/SimpleSocialAuth
 */
using SimpleSocialAuth.MVC3;
using Nancy.Cookies;

namespace EventWebService
{
    /// <summary>
    /// The Person REST Module
    /// </summary>
    public class PersonModule : NancyModule
    {
        public PersonModule(PersonSvcBroker personBroker) :base("/Person")
        {
            //Now a get request for testing purposes, need to convert to form post
            Get["/signin/{authType}"] = x =>
            {
                int authRequest=0;
                AuthType authType;
                try
                {
                    authRequest = Convert.ToInt32(x.authType.ToString());
                    authType = this.DetermineAuthType(authRequest);
                    if (authType == AuthType.Unknown)
                        return HttpStatusCode.BadRequest;
                }
                catch 
                {
                    return HttpStatusCode.BadRequest;
                }
                   /*
                 * Create abstract auth handler and specify the authentication api
                 * Note that api keys are stored in appSettings section of the web.config
                 */
                var auth_handler = AuthHandlerFactory.CreateAuthHandler(authType);
                /*
                 * Builds the API request url and callback url. Also specifies API scope. In this case the scope is limited to the user info. 
                 * Can be expanded at will
                 */
                string redirect_url =
                     auth_handler.PrepareAuthRequest(this.Request, string.Format(@"person/doauth?authType={0}",authRequest));
                
                //Send api request.
                return this.Response.AsRedirect(redirect_url);
            };

            Get["/doauth"] = x =>
            {
                //Parse query string in order to retrieve the AuthType
                if (Request.Query.authType.HasValue)
                {  
                    string authRequest = Request.Query.authType.ToString();
                    AuthType authType = this.DetermineAuthType(Convert.ToInt32(authRequest));
                    
                    if (authType == AuthType.Unknown)
                        return HttpStatusCode.BadRequest;
                    
                    //Retrieve auth handler abstract object and specify the api platform   
                    var authHandler = AuthHandlerFactory.CreateAuthHandler(authType);
                    //Request user data via api call. Note, user data object can be modified to get more data
                    var userData = authHandler.ProcessAuthRequest(this.Request);
                    //Validate user record
                    personBroker.ValidateUser(userData);
                    
                    After += context =>
                    {
                        //Store the platform under which the user logged in for subsequent api calls
                        if (userData != null)
                            context.Response.AddCookie(new NancyCookie("sessionId", authType.ToString()) { Expires = DateTime.Now.AddDays(1) });
                    };
                    //Return JSON representation of user data to consumer
                    return this.Response.AsJson(userData);
                }

                return HttpStatusCode.NotFound; 
            };

            Get["/isauthenticated"] = x =>
            {
                return this.Response.AsJson(new { IsAuthenticated = this.Request.Cookies.ContainsKey("sessionId")});
            };

            //Temporary hack. Need to find out how to log out from social networks
            Get["/logout"] = x =>
            {
                After += ctx =>
                {
                    ctx.Response.AddCookie(new NancyCookie("sessionId",string.Empty) { Expires = DateTime.Now.AddDays(-1) });
                };
                return HttpStatusCode.OK;
            };
        }
    }
}