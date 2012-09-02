using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Collections;

/// <summary>
/// Summary description for TimeZones
/// </summary>
public class TimeZones
{
	public TimeZones()
	{

	}
    public string GetCurrentDateTimeMySqlFormat(Hashtable State)
    {
        DateTime now = GetCurrentDateTime(State);
        return now.ToString("s").Replace("T", " ");
    }
    public DateTime GetCurrentDateTime(Hashtable State)
    {
        double time_zone_delta_hours = 0.0;
        if (State["TimeZoneDeltaHours"] != null)
            time_zone_delta_hours = Convert.ToDouble(State["TimeZoneDeltaHours"].ToString());
        return DateTime.Now.ToUniversalTime().AddHours(time_zone_delta_hours);
    }
    public string GetDefaultTimeZone(Hashtable State)
    {
        DB db = new DB();
        string sql = "SELECT default_time_zone_delta_hours FROM customers WHERE customer_id='" + State["CustomerID"].ToString() + "'";
        string default_time_zone_delta_hours = db.ViziAppsExecuteScalar(State,sql);
        db.CloseViziAppsDatabase(State);
        State["TimeZoneDeltaHours"] = default_time_zone_delta_hours;
        return default_time_zone_delta_hours;
    }
    public string GetTimeZoneStringFromDouble(double TimeZoneOffset, string TimeZoneName)
    {
        int hour = Convert.ToInt32(Math.Floor(TimeZoneOffset));
        int minute = Convert.ToInt32((TimeZoneOffset - hour) * 60.0);
        TimeSpan span = new TimeSpan(hour, minute, 0);
        string timezone_string = span.ToString().Remove(span.ToString().Length - 3) + ") " + TimeZoneName;
        if (hour >= 0)
            return "(GMT+" + timezone_string;
        else
            return "(GMT" + timezone_string;
    }
    public double GetDaylightSavingsTimeOffset(DateTime dateTime)
    {
        TimeZone localZone = TimeZone.CurrentTimeZone;
        DaylightTime daylightyear = localZone.GetDaylightChanges(dateTime.Year);
        double offset = TimeZone.IsDaylightSavingTime(DateTime.Now.ToUniversalTime(), daylightyear) ? 1.0 : 0.0;
        return offset;
    }
    public void SetTimeZoneList(DropDownList TimeZoneList, string selected_value, string text, string value)
    {
        ListItem item = new ListItem(text, value);
        TimeZoneList.Items.Add(item);
        if ((TimeZoneList.SelectedIndex == -1 || TimeZoneList.SelectedIndex == 0) && value == selected_value)
        {
            item.Selected = true;
            TimeZoneList.SelectedIndex = TimeZoneList.Items.Count - 1;
        }
    }

    public void InitTimeZones(Hashtable State, DateTime dateTime, DropDownList TimeZoneList, string selected_value)
    {
        double daylight_savings_offset = GetDaylightSavingsTimeOffset(dateTime);
        TimeZoneList.Items.Clear();
        TimeZoneList.SelectedIndex = -1;

        double GMT = -11.0 + daylight_savings_offset;
        string timezone = GetTimeZoneStringFromDouble(GMT, "Midway Island, Samoa");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = -10.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Hawaii");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = -9.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Alaska");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = -8.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Pacific Time (US and Canada), Tijuana");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = -7.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Mountain Time (US and Canada)");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = -7.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Chihuahua, La Paz, Mazatlan");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = -7.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Arizona");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = -6.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Central Time (US and Canada");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = -6.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Saskatchewan");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = -6.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Guadalajara, Mexico City, Monterrey");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = -6.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Central America");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = -5.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Eastern Time (US and Canada)");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = -5.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Indiana (East)");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = -5.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Bogota, Lima, Quito");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = -4.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Atlantic Time (Canada)");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = -4.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Caracas, La Paz");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = -4.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Santiago");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = -3.5 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Newfoundland and Labrador");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = -3.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Brasilia");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = -3.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Buenos Aires, Georgetown");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = -3.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Greenland");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = -2.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Mid-Atlantic");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = -1.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Azores");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = -1.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Cape Verde Islands");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Dublin, Edinburgh, Lisbon, London");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Casablanca, Monrovia");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = 1.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Belgrade, Bratislava, Budapest, Ljubljana, Prague");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = 1.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Sarajevo, Skopje, Warsaw, Zagreb");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 1.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Brussels, Copenhagen, Madrid, Paris");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 1.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 1.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "West Central Africa");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        
        GMT = 2.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Bucharest");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = 2.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Cairo");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = 2.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Helsinki, Kiev, Riga, Sofia, Tallinn, Vilnius");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 2.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Athens, Istanbul, Minsk");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 2.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Jerusalem");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 2.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Harare, Pretoria");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = 3.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Moscow, St. Petersburg, Volgograd");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = 3.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Kuwait, Riyadh");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 3.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Nairobi");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 3.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Baghdad");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = 3.5 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Tehran");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 4.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Abu Dhabi, Muscat");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = 4.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Baku, Tbilisi, Yerevan");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = 4.5 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Kabul");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 5.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Ekaterinburg");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = 5.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Islamabad, Karachi, Tashkent");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = 5.5 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Chennai, Kolkata, Mumbai, New Delhi");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 5.75 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Kathmandu");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 6.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Astana, Dhaka");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = 6.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Sri Jayawardenepura");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 6.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Almaty, Novosibirsk");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = 6.5 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Yangon Rangoon");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 7.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Bangkok, Hanoi, Jakarta");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = 7.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Krasnoyarsk");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = 8.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Beijing, Chongqing, Hong Kong SAR, Urumqi");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = 8.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Kuala Lumpur, Singapore");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 8.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Taipei");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 8.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Perth");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 8.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Irkutsk, Ulaanbaatar");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        
        GMT = 9.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Seoul");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = 9.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Osaka, Sapporo, Tokyo");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = 9.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Yakutsk");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = 9.5 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Darwin");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = 9.5 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Adelaide");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = 10.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Canberra, Melbourne, Sydney");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = 10.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Brisbane");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 10.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Hobart");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 10.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Vladivostok");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 10.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Guam, Port Moresby");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = 11.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Magadan, Solomon Islands, New Caledonia");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        GMT = 12.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Fiji Islands, Kamchatka, Marshall Islands");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        /*GMT = 12.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Auckland, Wellington");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());
        */
        GMT = 13.0 + daylight_savings_offset;
        timezone = GetTimeZoneStringFromDouble(GMT, "Nuku’alofa");
        SetTimeZoneList(TimeZoneList, selected_value, timezone, GMT.ToString());

        State[TimeZoneList.ID] = true;

    }
}
