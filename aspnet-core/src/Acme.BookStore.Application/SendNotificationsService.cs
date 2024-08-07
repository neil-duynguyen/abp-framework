using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System;

using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp;
using Firebase.Database;
using System.Net.Http;
using Firebase.Database.Query;

namespace Acme.BookStore
{
    [RemoteService(IsEnabled = false)]
    public class SendNotificationsService : ApplicationService
    {
        private FirebaseClient _firebaseClient;
        private readonly string Timestamp = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")).ToString();
        public async Task InitializeFirebase()
        {
            try
            {
                //để đường dẫn trong appsettings
                string pathToServiceAccountKey = "C:\\Users\\DuyNguyen\\Downloads\\testsendnoti-f5a2c-firebase-adminsdk-oyuje-410bc0077a.json";

                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(pathToServiceAccountKey),
                });

                _firebaseClient = new FirebaseClient("https://testsendnoti-f5a2c-default-rtdb.asia-southeast1.firebasedatabase.app/");

                Console.WriteLine("Firebase initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error initializing Firebase: " + ex.Message);
            }
        }


        public async Task SendNoti(string token, string title, string mess)
        {
            await InitializeFirebase();

            var registrationToken = token;

            var message = new Message()
            {
                Notification = new Notification()
                {
                    Title = title,
                    Body = mess,
                },
                Token = registrationToken,
            };

            try
            {
                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                Console.WriteLine("Successfully sent message: " + response);

                await SaveNotificationAsync(token, title, mess);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending message: " + ex.Message);
            }
        }

        private async Task SaveNotificationAsync(string token, string title, string mess)
        {
            var notification = new
            {
                Token = token,
                Title = title,
                Message = mess,
                Status = "unread",
                Timestamp
            };

            string notificationJson = System.Text.Json.JsonSerializer.Serialize(notification);

            try
            {
                // Save the notification to the database
                await _firebaseClient
                    .Child("notifications")
                    .PostAsync(notificationJson);

                Console.WriteLine("Notification saved to database successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving notification to database: " + ex.Message);
            }
        }

        public async Task UpdateNotificationStatusAsync(string notificationId, string newStatus)
        {
            try
            {

                var updateStatus = new { Status = newStatus + " " + Timestamp };

                _firebaseClient = new FirebaseClient("https://testsendnoti-f5a2c-default-rtdb.asia-southeast1.firebasedatabase.app/");

                await _firebaseClient.Child("notifications").Child(notificationId).PatchAsync(updateStatus);

                Console.WriteLine("Notification status updated successfully.");
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error updating notification status: " + ex.Message);
            }
        }
    }

}
