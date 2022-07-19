using System;
using SpotifyAPI;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;

namespace Songify.Spotify
{
    public class Auth
    {

        private EmbedIOAuthServer server;

        public Auth()
        {
        }

        public async Task Connect()
        {
            server = new EmbedIOAuthServer(new Uri("http://localhost:4200"), 4200);
            server.AuthorizationCodeReceived += OnAuthorizationCodeReceived;
            server.ErrorReceived += OnError;

            LoginRequest request = new LoginRequest(server.BaseUri, "Client ID HERE", LoginRequest.ResponseType.Code)
            {
                Scope = new List<string>
                {
                    Scopes.UserModifyPlaybackState,
                    Scopes.UserReadCurrentlyPlaying,
                    Scopes.UserReadPlaybackState
                }
            };

            BrowserUtil.Open(request.ToUri());
        }

        private async Task OnAuthorizationCodeReceived(object sender, AuthorizationCodeResponse response)
        {
            await server.Stop();
            SpotifyClientConfig config = SpotifyClientConfig.CreateDefault();
            IToken tokenResponse = await new OAuthClient(config).RequestToken(
                new AuthorizationCodeTokenRequest(
                    "Client ID HERE", "Client Secret HERE", response.Code, new Uri("http://localhost:4200/callback")
                )
            );

            SpotifyClient spotify = new SpotifyClient(tokenResponse.AccessToken);
            // now do something with spotifyclient and save access token
        }

        private async Task OnError(object sender, string error, string state)
        {
            Console.WriteLine($"Aborting auth, error received: {error}");
            await server.Stop();
        }
    }
}

