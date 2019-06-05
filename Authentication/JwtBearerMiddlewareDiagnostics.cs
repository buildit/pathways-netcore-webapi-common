namespace pathways_common.Authentication
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.JwtBearer;

    /// <summary>
    /// Diagnostics for the JwtBearer middleware (used in Web APIs)
    /// </summary>
    public class JwtBearerMiddlewareDiagnostics
    {
        /// <summary>
        /// Invoked if exceptions are thrown during request processing. The exceptions will be re-thrown after this event unless suppressed.
        /// </summary>
        static Func<AuthenticationFailedContext, Task> onAuthenticationFailed;

        /// <summary>
        /// Invoked when a protocol message is first received.
        /// </summary>
        static Func<MessageReceivedContext, Task> onMessageReceived;

        /// <summary>
        /// Invoked after the security token has passed validation and a ClaimsIdentity has been generated.
        /// </summary>
        static Func<TokenValidatedContext, Task> onTokenValidated;

        /// <summary>
        /// Invoked before a challenge is sent back to the caller.
        /// </summary>
        static Func<JwtBearerChallengeContext, Task> onChallenge;

        /// <summary>
        /// Subscribes to all the JwtBearer events, to help debugging, while
        /// preserving the previous handlers (which are called)
        /// </summary>
        /// <param name="events">Events to subscribe to</param>
        public static JwtBearerEvents Subscribe(JwtBearerEvents events)
        {
            if (events == null)
            {
                events = new JwtBearerEvents();
            }

            JwtBearerMiddlewareDiagnostics.onAuthenticationFailed = events.OnAuthenticationFailed;
            events.OnAuthenticationFailed = JwtBearerMiddlewareDiagnostics.OnAuthenticationFailed;

            JwtBearerMiddlewareDiagnostics.onMessageReceived = events.OnMessageReceived;
            events.OnMessageReceived = JwtBearerMiddlewareDiagnostics.OnMessageReceived;

            JwtBearerMiddlewareDiagnostics.onTokenValidated = events.OnTokenValidated;
            events.OnTokenValidated = JwtBearerMiddlewareDiagnostics.OnTokenValidated;

            JwtBearerMiddlewareDiagnostics.onChallenge = events.OnChallenge;
            events.OnChallenge = JwtBearerMiddlewareDiagnostics.OnChallenge;

            return events;
        }

        static async Task OnMessageReceived(MessageReceivedContext context)
        {
            Debug.WriteLine($"1. Begin {nameof(JwtBearerMiddlewareDiagnostics.OnMessageReceived)}");
            // Place a breakpoint here and examine the bearer token (context.Request.Headers.HeaderAuthorization / context.Request.Headers["Authorization"])
            // Use https://jwt.ms to decode the token and observe claims
            await JwtBearerMiddlewareDiagnostics.onMessageReceived(context);
            Debug.WriteLine($"1. End - {nameof(JwtBearerMiddlewareDiagnostics.OnMessageReceived)}");
        }

        static async Task OnAuthenticationFailed(AuthenticationFailedContext context)
        {
            Debug.WriteLine($"99. Begin {nameof(JwtBearerMiddlewareDiagnostics.OnAuthenticationFailed)}");
            // Place a breakpoint here and examine context.Exception
            await JwtBearerMiddlewareDiagnostics.onAuthenticationFailed(context);
            Debug.WriteLine($"99. End - {nameof(JwtBearerMiddlewareDiagnostics.OnAuthenticationFailed)}");
        }

        static async Task OnTokenValidated(TokenValidatedContext context)
        {
            Debug.WriteLine($"2. Begin {nameof(JwtBearerMiddlewareDiagnostics.OnTokenValidated)}");
            await JwtBearerMiddlewareDiagnostics.onTokenValidated(context);
            Debug.WriteLine($"2. End - {nameof(JwtBearerMiddlewareDiagnostics.OnTokenValidated)}");
        }

        static async Task OnChallenge(JwtBearerChallengeContext context)
        {
            Debug.WriteLine($"55. Begin {nameof(JwtBearerMiddlewareDiagnostics.OnChallenge)}");
            await JwtBearerMiddlewareDiagnostics.onChallenge(context);
            Debug.WriteLine($"55. End - {nameof(JwtBearerMiddlewareDiagnostics.OnChallenge)}");
        }
    }
}