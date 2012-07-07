using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Controls_DatabaseToPhoneField : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }

        XmlDocument doc = util.GetStagingAppXml(State);
        XmlNode database_config = doc.SelectSingleNode("//application/data_sources/data_source"); //new config
        if (database_config == null)
        {
            database_config = doc.SelectSingleNode("//mobiflex_project/database_config"); //old config
            if (database_config == null)
            {
                return;
            }
        }

        string table =  State["SelectedDatabaseTable"].ToString();
        XmlNode table_name_node = database_config.SelectSingleNode("//tables/table/table_name[.='" + table + "'] | //tables/table/table_name[.='" + table.ToLower() + "']");
        if (table_name_node == null)
        {
            State["SpreadsheetError"] = "Query Error: A saved worksheet or table: " + table + " does not exist. All queries for this event have been deleted to avoid runtime errors.";
            State["SelectedDatabaseTable"] = null;
            return;
        }
        XmlNodeList field_list = table_name_node.ParentNode.SelectNodes("fields/field/name");

        // init database fields
         database_field.Items.Clear();
        ArrayList fields = new ArrayList();
        foreach (XmlNode field_node in field_list)
        {
            fields.Add(field_node.InnerText);
        }
        fields.Sort();

        foreach (string name in fields)
        {
            database_field.Items.Add(new Telerik.Web.UI.RadComboBoxItem(name, name));
        }

        //init phone_fields
        fields.Clear();
        RadComboBox DatabaseEvents = null;
        if ( State["ManageDataType"].ToString() == "database")
        {
            DatabaseEvents = (RadComboBox) State["DatabaseEvents"];
        }
        else
        {
            DatabaseEvents = (RadComboBox) State["SpreadSheetEvents"];
        }
        phone_field.Items.Clear();
        ArrayList phone_fields = new ArrayList();


        string[] page_names = DatabaseEvents.SelectedItem.Value.Split(":".ToCharArray());
        string from_page = page_names[0];
        string to_page = page_names[1];

        XmlNodeList pages = doc.SelectNodes("//pages/page");
        foreach (XmlNode page in pages)
        {
            XmlNode page_name_node = page.SelectSingleNode("name");
            string page_name = page_name_node.InnerText;

            //if (page_name != from_page && page_name != to_page)
            //    continue;

            //do inputs from phone 
            XmlNodeList input_nodes = page.SelectNodes(
                 "fields/text_area | fields/label | fields/text_field | fields/hidden_field | fields/alert | fields/web_view | fields/image | fields/photo | fields/switch | fields/checkbox | fields/audio | fields/slider");
            foreach (XmlNode input_node in input_nodes)
            {
                // phone input field
                XmlNode id_node = input_node.SelectSingleNode("id");
                fields.Add(id_node.InnerText.Trim());
            }

            XmlNodeList gps_fields = page.SelectNodes("fields/gps");
            foreach (XmlNode gps_field in gps_fields)
            {
                XmlNode id_node = gps_field.SelectSingleNode("id");
                string input_field = id_node.InnerText.Trim();

                XmlNode part_node = gps_field.SelectSingleNode("latitude");
                fields.Add(part_node.InnerText);
                part_node = gps_field.SelectSingleNode("longitude");
                fields.Add(part_node.InnerText);
            }

            XmlNodeList pickers = page.SelectNodes("fields/picker");
            foreach (XmlNode picker in pickers)
            {
                XmlNode id_node = picker.SelectSingleNode("id");
                XmlNode picker_fields = picker.SelectSingleNode("picker_fields");
                if (picker_fields != null)
                {
                    //picker fields are both inputs and outputs

                    XmlNodeList picker_field_list = picker_fields.SelectNodes("picker_field");
                    foreach (XmlNode field_item in picker_field_list)
                    {
                        XmlNode field_name = field_item.SelectSingleNode("name");

                        //table fields can be inputs too
                        fields.Add(field_name.InnerText.Trim());
                    }
                }
                else//for date and time picker types
                {
                    //picker fields are both inputs and outputs
                    fields.Add(id_node.InnerText.Trim());
                }
            }


            //do responses to phone 
            XmlNodeList tables = page.SelectNodes("fields/table");
            foreach (XmlNode table_node in tables)
            {
                XmlNode id_node = table_node.SelectSingleNode("id");
                XmlNode table_fields = table_node.SelectSingleNode("table_fields");
                XmlNodeList table_field_list = table_fields.SelectNodes("table_field");

                //table fields are both inputs and outputs
                foreach (XmlNode table_field_item in table_field_list)
                {
                    XmlNode table_field_name = table_field_item.SelectSingleNode("name");

                    //table fields can be inputs too
                    fields.Add(table_field_name.InnerText.Trim());
                }
            }
        }
        fields.Sort();

        foreach (string name in fields)
        {
            phone_field.Items.Add(new Telerik.Web.UI.RadComboBoxItem(name, name));
        }
        phone_field.SelectedIndex = 0;
    }
}