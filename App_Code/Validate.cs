using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Validate
/// </summary>
public static class Check
{
    public static bool ValidateField(Label Message, string type, string value)
    {
        string T = type.ToLower();

        if (T == "telephone" || T == "phone" || T == "us_phone" || T=="usphone")
        {
            return ValidatePhone(Message, value);
        }
        else if (T == "number")
        {
            return ValidateNumber(Message, value);
        }
        else if (T == "quantity")
        {
            return ValidateQuantity(Message, value);
        }
        else if (T == "currency")
        {
            return ValidateCurrency(Message, value);
        }
        else if (T == "integer")
        {
            return ValidateInteger(Message, value);
        }
        else if (T == "name")
        {
            return ValidateName(Message, value);
        }
        else if (T == "email")
        {
            return ValidateEmail(Message, value);
        }
        else if (T == "date")
        {
            return ValidateDate(Message, value);
        }
        else if (T == "future_date")
        {
            return ValidateFutureDate(Message, value);
        }
        else if (T == "time")
        {
            return ValidateTime(Message, value);
        }
        else if (T == "date_time" || T == "datetime")
        {
            return ValidateDateTime(Message, value);
        }
        else if (T == "future_date_time" || T == "future_datetime")
        {
            return ValidateFutureDateTime(Message, value);
        }
        else if (T == "ip_domain" || T == "ip_address" || T == "ipaddress")
        {
            return ValidateIPAddress(Message, value);
        }
        else if (T == "text")
        {
            return ValidateText(Message, value);
        }
        else if (T == "string")
        {
            return ValidateString(Message, value);
        }
        else if (T == "zipcode" || T == "zip_code")
        {
            return ValidateZipcode(Message, value);
        }
        else
        {
            Message.Text = "Unsupported or missing field type.";
            return false;
        }
    }
    public static bool ValidateZipcode(Label Message, string zipcode)
    {
        if (zipcode == null || (zipcode.Length != 5 && zipcode.Length != 9 && zipcode.Length != 10))
        {
            Message.Text = "Enter a zip code.";
            return false;
        }
        else if (zipcode.Length == 5)
        {
            if (!ValidateInteger(Message, zipcode))
            {
                Message.Text = "Enter a zip code.";
                return false;
            }
        }
        else if (zipcode.Length == 9)
        {
            if (!ValidateInteger(Message, zipcode.Substring(0, 5)) || !ValidateInteger(Message, zipcode.Substring(5, 4)))
            {
                Message.Text = "Enter a zip code.";
                return false;
            }
        }
        else if (zipcode.Length == 10)
        {
            if (!ValidateInteger(Message, zipcode.Substring(0, 5)) || !ValidateInteger(Message, zipcode.Substring(6, 4)))
            {
                Message.Text = "Enter a zip code.";
                return false;
            }
        }
        return true;
    }
    public static bool ValidateName(Label Message, string name)
    {
         if (name == null || name.Length == 0 || name.Length > 100)
        {
            Message.Text = "Name field must be between 1 to 100 characters";
            return false;
        }
        bool first = true;
        foreach (char c in name)
        {
            string err = "The name must start with a letter and thereafter can only contain either a letter, number,('),(.),(,) or space. It can be 1 to 100 characters long";
            if (first)
            {
                if (!Char.IsLetter(c))
                {
                    Message.Text = err;
                    return false;
                }
                first = false;
            }
            if (!Char.IsLetter(c) && !Char.IsNumber(c) && c != ' ' && c != '\'' && c != '.' && c != ',')
            {
                Message.Text = err;
                return false;
            }
        }
        return true;
    }   
    public static bool ValidateObjectName(Label Message, string name)
    {
        Util util = new Util();
        if (!util.IsGoodApplicationName(name))
        {
            Message.Text = "The name must start with a letter and thereafter can only contain either a letter, number, space or '_'. It can be 1 to 100 characters long";
            return false;
        }
        return true;
    }
    public static bool ValidateObjectNameNoSpace(Label Message, string name)
    {
        Util util = new Util();
        if (!util.IsGoodObjectNameNoSpace(name))
        {
            Message.Text = "The name must start with a letter and thereafter can only contain either a letter, number or '_'. It can be 1 to 100 characters long";
            return false;
        }
        return true;
    }
    public static bool ValidateDateTime(Label Message, string dateTime)
    {
        string error = "Enter the date-time with the format ‘MM/dd/yyyy HH:mm AM/PM’.";
        DateTime in_date_time = DateTime.MinValue;
        if (dateTime==null || !DateTime.TryParse(dateTime, out in_date_time))
        {
            Message.Text = error;
            return false;
        }
        if (dateTime.Length < 11 || dateTime.IndexOf(":") < 0)
        {
            Message.Text = error;
            return false;
        }
        return true;
    }
    public static bool ValidateFutureDateTime(Label Message, string dateTime)
    {
        string error = "Enter a future date-time with the format ‘MM/dd/yyyy HH:mm AM or PM’.";
        DateTime in_date_time = DateTime.MinValue;
        if (dateTime == null || !DateTime.TryParse(dateTime, out in_date_time))
        {
            Message.Text = error;
            return false;
        }
        if (in_date_time <= DateTime.Now.ToUniversalTime())
        {
            Message.Text = error;
            return false;
        }
        if (dateTime.Length < 11 || dateTime.IndexOf(":") < 0)
        {
            Message.Text = error;
            return false;
        }
        return true;
    }
    public static bool ValidateDate(Label Message, string date)
    {
        string error = "Enter the date with the format ‘MM/dd/yyyy’.";
        DateTime in_date = DateTime.MinValue;
        if (date==null || !DateTime.TryParse(date, out in_date))
        {
            Message.Text = error;
            return false;
        }
        if (date.IndexOf(":") >= 0)
        {
            Message.Text = error;
            return false;
        }
        return true;
    }
    public static bool ValidateFutureDate(Label Message, string date)
    {
        string error = "Enter a future date with the format ‘MM/dd/yyyy’.";
        DateTime in_date = DateTime.MinValue;
        if (date == null || !DateTime.TryParse(date, out in_date))
        {
            Message.Text = error;
            return false;
        }
        if (in_date <= DateTime.Now.ToUniversalTime())
        {
            Message.Text = error;
            return false;
        }
        if (date.IndexOf(":") >= 0)
        {
            Message.Text = error;
            return false;
        } 
        return true;
    }
    public static bool ValidateTime(Label Message, string time)
    {
        string error = "Enter the time with the format ‘HH:mm AM/PM'.";
        DateTime in_time = DateTime.MinValue;
        if (time == null || !DateTime.TryParse(time, out in_time))
        {
            Message.Text = error;
            return false;
        }
        if (time.Length > 10 || time.IndexOf(":") < 0)
        {
            Message.Text = error;
            return false;
        } 
        return true;
    }
    public static bool ValidateText(Label Message, string text)
    {
        if (text == null || text.Length  == 0 || text.Length > 32000)
        {
            Message.Text ="Enter a text field between 1 to 32,000 characters";
            return false;
        } 
        return true;
    }
    public static bool ValidateString(Label Message, string value)
    {
        if (value == null || value.Length == 0 || value.Length > 256)
        {
            Message.Text = "Enter a string field between 1 to 256 characters";
            return false;
        }
        return true;
    }
    public static bool ValidateIPAddress(Label Message, string IPAddress)
    {
        if (IPAddress == null || IPAddress.Length==0)
        {
            Message.Text = "IP Address field must be an Internet number";
            return false;
        }
        string error = "IP address must be domain name or Internet number.";
        string[] tokens = IPAddress.Split('.');
        if (tokens.Length == 4)
        {
            string number = IPAddress.Replace(".", "");
            try
            {
                Int64 x = Convert.ToInt64(number);
            }
            catch
            {
                Message.Text = error;
                return false;
            }
        }
        else if (tokens.Length < 2 || tokens.Length > 4)
        {
            Message.Text = error;
            return false;
        }
        return true;
    }
    public static bool ValidateEmail(Label Message, string email)
    {
        if (email == null || email.Length==0 ||
            email.IndexOf("@") < 0 || email.IndexOf(".") < 0 || email.LastIndexOf("@") > email.LastIndexOf("."))
        {
            Message.Text = "Enter a valid email address";
            return false;
        }
        return true;
    }
    public static bool ValidatePhone(Label Message, string phone)
    {
        Util util = new Util();
        //check for any letters except for x for extension
        foreach (char x in phone)
        {
            if (Char.IsLetter(x))
            {
                Message.Text = "Enter a phone number with 10 digits without a leading '1'.";
                return false;
            }
        }
        string filtered_phone = util.GetPhoneDigits(phone);
        if (filtered_phone.Length < 10 || filtered_phone.Length > 30)
        {
            Message.Text = "Enter a phone number with 10 to 30 digits without a leading '1'.";
            return false;
        }
        return true;
    }
    public static bool ValidatePassword(Label Message, string password)
    {
        if (password==null || password.Length < 6 || password.Length > 30)
        {
            Message.Text = "Enter a password that is 6 to 30 characters which is case sensitive";
            return false;
        }
        return true;
    }
    public static bool ValidateCurrency(Label Message, string currency)
    {
        string error = "Enter a valid currency value.";
        if (currency == null || currency.Length==0)
        {
            Message.Text = error;
            return false;
        }
        else if (currency.StartsWith("$"))
        {
            try
            {
                if (Convert.ToDouble(currency.Substring(1)) < 0.0)
                {
                    Message.Text = error;
                    return false;
                }
            }
            catch
            {
                Message.Text = error;
                return false;
            }
        }
        else
        {
            try
            {
                if (Convert.ToDouble(currency) < 0.0)
                {
                    Message.Text = error;
                    return false;
                }
            }
            catch
            {
                Message.Text = error;
                return false;
            }
        }
        //check number of decimal places
        int point = currency.IndexOf(".");
        if (point >= 0 && point != currency.Length-1 && currency.Substring(point + 1).Length != 2)
        {
            Message.Text = error;
            return false;
        }
        return true;
    }
    public static bool ValidateCreditCardSecurityCode(Label Message, string CreditCard)
    {
        string invalid_error = "Credit card security code is invalid.";
        try
        {
            Util util = new Util();
            string number = util.GetDigits(CreditCard);
            if (number.Length < 3 || number.Length > 4)
            {
                Message.Text = invalid_error;
                return false;
            }
        }
        catch
        {
            Message.Text = invalid_error;
            return false;
        }
        return true;
    }

    public static bool ValidateCreditCard(Label Message, string CreditCard)
    {
        string invalid_error = "Credit card number is invalid.";
        try
        {
            Util util = new Util();
            string number = util.GetDigits(CreditCard);
            if (number.Length < 12|| number.Length > 22)
            {
                Message.Text = invalid_error;
                return false;
            }
            switch (number[0]) //check on number of digits based on credit card type
            {
                case '3':
                    if (number.Length != 15)
                    {
                        Message.Text = invalid_error;
                        return false;
                    }
                    break;
                case '4':
                case '5':
                    if (number.Length != 16)
                    {
                        Message.Text = invalid_error;
                        return false;
                    }
                    break;
                default:
                    break;
            }
        }
        catch
        {
            Message.Text = invalid_error;
            return false;
        }
        return true;
    }
    public static bool ValidateNumber(Label Message, string number)
    {
        try
        {
            double x = Convert.ToDouble(number);
        }
        catch
        {
            Message.Text = "Enter a valid number";
            return false;
        }
        return true;
    }
    public static bool ValidateInteger(Label Message, string number)
    {
        try
        {
            if (number.IndexOf(".") >= 0)
            {
                Message.Text = "Enter a valid integer";
                return false;
            }
            int x = Convert.ToInt32(number);
        }
        catch
        {
            Message.Text = "Enter a valid integer";
            return false;
        }
        return true;
    }
    public static bool ValidateQuantity(Label Message, string quantity)
    {
        try
        {
            if (Convert.ToDouble(quantity) < 0.0)
            {
                Message.Text = "Enter a quantity equal or greater than 0.";
                return false;
            }
        }
        catch
        {
            Message.Text = "Enter a quantity equal or greater than 0.";
            return false;
        }
        return true;
    }
    public static bool ValidateUsername(Label Message, string username)
    {
        bool first = true;
        string error = "Enter the Username to be 2 to 24 alphanumeric characters starting with a letter ";
        if (username == null || username.Length < 2 || username.Length > 24)
        {
            Message.Text = error;
            return false;
        }
        foreach (char c in username)
        {
            if (first)
            {
                if (!Char.IsLetter(c))
                {
                    Message.Text = error;
                    return false;
                }
                first = false;
            }
            if (!Char.IsLetter(c) && !Char.IsNumber(c))
            {
                Message.Text = error;
                return false;
            }
        }
        return true;
    }
}
