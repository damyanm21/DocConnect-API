using DocConnect.Business.Abstraction.TokenManager;
using DocConnect.Business.Models.Token;
using static DocConnect.Data.Models.Utilities.Constants.InMemoryTokenManagerConstants;

namespace DocConnect.Business.TokenManager
{
    public class InMemoryTokenManager : ITokenManager
    {
        /// <summary>
        /// Collection which holds users tokens.
        /// </summary>
        private readonly List<InMemoryToken> _tokens;

        /// <summary>
        /// Lock mechanism to ensure the code is being used by a single thread.
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// Timer that cleans the in-memory collection of tokens at given time.
        /// </summary>
        private Timer _cleanMemoryTimer;

        public InMemoryTokenManager() : this(TimeSpan.FromMinutes(TriggerTimerInMinutes))
        {

        }

        public InMemoryTokenManager(TimeSpan cleanupInterval)
        {
            _tokens = new List<InMemoryToken>();

            // Sets the Timer to start immediately and clean the expired tokens every 1 minute / 60 seconds. 
            _cleanMemoryTimer = new Timer(CleanupExpiredTokens, null, TimeSpan.FromSeconds(StartTimerInSeconds), cleanupInterval);
        }

        public bool IsActiveTokenExisting(string userId, string tokenType)
        {
            lock (_lock)
            {
                var token = _tokens.FirstOrDefault(t => t.UserId == userId && t.Type == tokenType);

                if (token == null || token.Expiry <= DateTime.UtcNow)
                {
                    return false;
                }

                return true;
            }
        }
       
        public void AddToken(InMemoryToken token)
        {
            lock (_lock)
            {
                _tokens.Add(token);
            }
        }

        public void DeleteToken(string userId, string tokenValue)
        {
            lock (_lock)
            {
                var token = _tokens.FirstOrDefault(t => t.UserId == userId && t.Value == tokenValue);

                if (token != null)
                {
                    _tokens.Remove(token);
                }
            }
        }

        /// <summary>
        /// Method that is triggered on given time to delete all expired tokens.
        /// </summary>
        /// <param name="state">State passed by the Timer.</param>
        private void CleanupExpiredTokens(object state)
        {
            lock (_lock)
            {
                var expiredTokens = _tokens
                    .Where(t => t.Expiry <= DateTime.UtcNow)
                    .ToList();

                foreach (var token in expiredTokens)
                {
                    _tokens.Remove(token);
                }
            }
        }
    }
}

