using System;
using System.Linq;
using System.ServiceModel;

namespace UserStorageApp
{
    public static class ServiceHostExtensions
    {
        public static void SmartOpen(this ServiceHost serviceHost)
        {
            try
            {
                serviceHost.Open();
            }
            catch (AddressAccessDeniedException exception)
            {
                var message = string.Format("Run this command under an administrator:\nnetsh http add urlacl url={0} user={1}\n\n--------------\n\nException message: {2}", serviceHost.BaseAddresses.First(), Environment.UserName, exception.Message);
                throw new Exception(message, exception);
            }
        }
    }
}
