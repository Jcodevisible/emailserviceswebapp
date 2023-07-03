using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Azure;
using Azure.Communication.Email;
using System.Threading.Tasks;
using System.Configuration;

namespace emailserviceswebapp.Email
{
    public static class EmailSender
    {

        #region sendEmail
        //Add send email method content here 
        public static async Task sendEmail()
        {

            var connectionString = ConfigurationManager.AppSettings["connectionString"].ToString();
            var emailClient = new EmailClient(connectionString);

            var sender = ConfigurationManager.AppSettings["sender"].ToString();
            var recipient = "<your email recipient>";
            var subject = "Send email plain text sample";

            var emailContent = new EmailContent(subject)
            {
                PlainText = "This is plain text mail send test body \n Best Wishes!!",
            };

            var emailMessage = new EmailMessage(sender, recipient, emailContent);

            try
            {
                var emailSendOperation = await emailClient.SendAsync(
                    wait: WaitUntil.Completed,
                    message: emailMessage);

                Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");

                /// Get the OperationId so that it can be used for tracking the message for troubleshooting
                string operationId = emailSendOperation.Id;
                Console.WriteLine($"Email operation id = {operationId}");
            }
            catch (RequestFailedException ex)
            {
                /// OperationID is contained in the exception message and can be used for troubleshooting purposes
                Console.WriteLine($"Email send operation failed with error code: {ex.ErrorCode}, message: {ex.Message}");
            }


        }

        #endregion


    }
}