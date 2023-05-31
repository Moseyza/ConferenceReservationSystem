using ConferenceReservationSystem.Entity;
using ConferenceReservationSystem.Entity.TotalSystem;
using ConferenceReservationSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceReservationSystem.Infrastructure
{
    public class SmsSender : ISmsSender
    {
        private readonly SmsDbContext _dbContext;
        public SmsSender(SmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        

        public async Task SendSecurityCodeAsync(string phoneNumber, string code)
        {
            var lastSms = await _dbContext.Smsdata.OrderByDescending(x => x.Serialcreatesms).FirstOrDefaultAsync();
            var serial = lastSms.Serialcreatesms.ToString();
            var convertedSerial = Convert.ToInt64(serial);
            convertedSerial++;
            var smsSerialNumber = "";
            serial = convertedSerial.ToString();
            var serialCharList = serial.ToArray().ToList();
            var count = 10 - serialCharList.Count;
            for (var i = 0; i < count; i++)
            {
                serialCharList.Insert(0, '0');
            }
            serial = new String(serialCharList.ToArray());
            smsSerialNumber = serial;
            var newSms = new SmsData()
            {
                Serialcreatesms = smsSerialNumber,
                Receivernumber = phoneNumber,
                Smstext = $"با سلام؛ کد تایید \n {code} \n سیستم مدیریت همایش - سازمان بازنشستگی",
                Startsendtime = DateTime.Now,
            };
            _dbContext.Add(newSms);
            await _dbContext.SaveChangesAsync();

        }


    }
}
