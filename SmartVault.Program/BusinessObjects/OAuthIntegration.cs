using System;

namespace SmartVault.Program.BusinessObjects
{
    public class OAuthIntegration
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public OAuthIntegration(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public void Authenticate()
        {
            throw new NotImplementedException();
        }

        public void PerformAction()
        {
            throw new NotImplementedException();
        }
    }
}
