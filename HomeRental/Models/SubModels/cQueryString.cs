using System;
using System.Text;

namespace HomeRental.Models.SubModels
{
    /// <summary>
    /// Class that encapsulates the query GET string from the URL of the search page.
    /// </summary>
    public class cQueryString
    {
        public string address { get; set; }
        public DateTime? checkin { get; set; }
        public DateTime? checkout { get; set; }
        public int? guests { get; set; } 

        /// <summary>
        /// Get the short date from checkin if valid
        /// </summary>
        public String getShortDateCheckin()
        {
            if(checkin.HasValue)
            {
                int result = DateTime.Compare(checkin.Value, DateTime.Now.Date);
                return result > 0 ? rightShortDateFormatToDatePicker(checkin) : "";
            }
            return "";
        }

        /// <summary>
        /// Get the short date from checkout if valid
        /// </summary>
        public String getShortDateCheckout()
        {
            if(getShortDateCheckin()!="" && checkout.HasValue)
            {
                int result = DateTime.Compare(checkout.Value, checkin.Value);
                return result > 0 ? rightShortDateFormatToDatePicker(checkout) : "";
            }
            return "";
        }

        /// <summary>
        /// Get right format for the datepicker element
        /// </summary>
        private String rightShortDateFormatToDatePicker(DateTime? date)
        { 
            string strDate = date.Value.Date.ToShortDateString();
            return date.Value.Date.Day < 10 ? "0" + strDate : strDate; 
        }
    }
}