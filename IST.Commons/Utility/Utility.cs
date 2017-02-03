using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using IST.Models.Common.DropDown;

namespace IST.Commons.Utility
{
    public static class Utility
    {
        public static string FormatDate(this DateTime date)
        {
            return date.ToString("dd-MM-yyy");
        }
        public static DateTime ConvertTimeByOffset(this DateTime dateTime, int offsetMinutes)
        {
            if (offsetMinutes < 0)
                offsetMinutes *= (-1);

            var clientDateTime = dateTime.AddMinutes(offsetMinutes);
            return clientDateTime;
        }
        public static DateTime ConvertToUtc(this DateTime dateTime, int offsetMinutes)
        {
            var clientDateTime = dateTime.AddMinutes(offsetMinutes);
            return clientDateTime;
        }
        public static TimeSpan ConvertToUtc(this TimeSpan time, int offsetMinutes)
        {
            //if (offsetMinutes < 0)
            //    offsetMinutes *= (1);
            //var clientDateTime = time.Add(new TimeSpan(0, 0, offsetMinutes, 0));
            //return clientDateTime.Duration();
            return time;
        }
        public static TimeSpan ConvertTimeByOffset(this TimeSpan time, int offsetMinutes)
        {
            //if (offsetMinutes < 0)
            //    offsetMinutes *= (-1);
            //var clientDateTime = time.Add(new TimeSpan(0, 0, offsetMinutes, 0));
            //return clientDateTime.Duration();
            return time;
        }

        /// <summary>
        /// Extension method for getting week of month
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <returns>Week of month as int</returns>
        public static int GetWeekOfMonth(this DateTime date)
        {
            int week = 1;
            if (date.Day <= 7)
                week = 1;
            if (date.Day > 7 && date.Day <= 14)
                week = 2;
            if (date.Day > 14 && date.Day <= 21)
                week = 3;
            if (date.Day > 21)
                week = 4;
            return week;
        }

        public static string FormatTime(this TimeSpan time)
        {
            return time.ToString(@"hh\:mm");
        }

        public static IEnumerable<DropDownModel> GetDaysDropDown(Type myEnum)
        {
            return Enum.GetValues(myEnum)
                .Cast<DayOfWeek>()
                .Select(x => new DropDownModel { DisplayName = x.ToString(), Id = (int)x });
        }

        public static int CompareWithoutSecond(this TimeSpan time, TimeSpan time2)
        {
            var t1 = new TimeSpan(time.Hours, time.Minutes, 0);
            var t2 = new TimeSpan(time2.Hours, time2.Minutes, 0);

            if (t1 > t2)
                return 1;

            if (t1 < t2)
                return -1;

            return 0;
        }

        /// <summary>
        /// Calculates Age from given DateTime
        /// </summary>
        /// <param name="dob"></param>
        /// <returns>string i.e "{year} y, {month} m, {day} d"</returns>
        public static string ToAgeString(this DateTime dob)
        {
            DateTime today = DateTime.Today;

            int months = today.Month - dob.Month;
            int years = today.Year - dob.Year;

            if (today.Day < dob.Day)
            {
                months--;
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            int days = (today - dob.AddMonths((years * 12) + months)).Days;

            return string.Format("{0}y, {1}m, {2}d",
                                 years,
                                 months,
                                 days);
        }

        /// <summary>
        /// Scales Image to given size
        /// </summary>
        /// <param name="imageBase64"></param>
        /// <param name="size"></param>
        /// <returns>base64 String of image</returns>
        public static string ScaleImage(string imageBase64, Size size)
        {
            //Get Byte[] from base64 string
            var imageBytes = Convert.FromBase64String(imageBase64);
            Image image;

            //Create an Image from the byte[]
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                image = Image.FromStream(ms);
            }

            //Check if image is already smaller than or equal to provided size
            if (image.Height <= size.Height || image.Width <= size.Width)
            {
                return imageBase64;
            }

            //Calculate ratio of the image
            var ratioX = (double)size.Width / image.Width;
            var ratioY = (double)size.Height / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            //Calculate new size of image according to aspect ratio
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            //Instantiate new Image
            var newImage = new Bitmap(newWidth, newHeight);

            //Re-Draw Image using new size and Quality Options
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            }

            //Convert Image back to base64 string
            using (MemoryStream ms = new MemoryStream())
            {
                newImage.Save(ms, ImageFormat.Jpeg);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static bool SendEmailAsync(string email, string body)
        {
            //Get Configuration from Web Config
            string fromAddress = ConfigurationManager.AppSettings["FromAddress"];
            string fromPwd = ConfigurationManager.AppSettings["FromPassword"];
            string fromDisplayName = ConfigurationManager.AppSettings["FromDisplayNameA"];
            
            //Getting the file from config, to send
            MailMessage oEmail = new MailMessage
            {
                From = new MailAddress(fromAddress, fromDisplayName),
                Subject = "Check it out",
                IsBodyHtml = true,
                Body = body,
                Priority = MailPriority.High
            };
            oEmail.To.Add(email);
            string smtpServer = ConfigurationManager.AppSettings["SMTPServer"];
            string smtpPort = ConfigurationManager.AppSettings["SMTPPort"];
            string enableSsl = ConfigurationManager.AppSettings["EnableSSL"];
            SmtpClient client = new SmtpClient(smtpServer, Convert.ToInt32(smtpPort))
            {
                EnableSsl = enableSsl == "1",
                Credentials = new NetworkCredential(fromAddress, fromPwd)
            };
            client.SendMailAsync(oEmail);
            return true;
        }
    }
}