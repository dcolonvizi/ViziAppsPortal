using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using MySql.Data.MySqlClient;
using Telerik.Web.UI;
using System.Xml;

public partial class Dialogs_EventMappingStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../../Default.aspx');", true); return; }

        LoadData();
    }
    protected void PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        LoadData();
    }

    private void LoadData()
    {
        DataTable myDataTable = new DataTable();
        DataColumn event_column = new DataColumn("Event");
        myDataTable.Columns.Add(event_column);

        DataColumn inputs_mapped = new DataColumn("Inputs Mapped");
        myDataTable.Columns.Add(inputs_mapped);

        DataColumn outputs_mapped = new DataColumn("Outputs Mapped");
        myDataTable.Columns.Add(outputs_mapped);

        Util util = new Util();
        XmlDocument doc = util.GetStagingAppXml((Hashtable)HttpRuntime.Cache[Session.SessionID]);

        RadComboBox WebServiceEvents  = (RadComboBox)  ((Hashtable)HttpRuntime.Cache[Session.SessionID])["WebServiceEvents"]  ;
        String Yes = "<img src='../../images/check.gif'/>";
        String No = "<img src='../../images/delete.gif'/>";
       
        foreach (RadComboBoxItem item in WebServiceEvents.Items)
        {
            if (item.Index == 0) //skip the select label item
                continue;

            DataRow row = myDataTable.NewRow();
            row.SetField<String>(event_column,item.Text);

            XmlNode mapped_node = doc.SelectSingleNode("//phone_data_request/event_field[.='" + item.Text + "']");
            if (mapped_node != null)
            {
                row.SetField<String>(inputs_mapped, Yes);
                mapped_node = mapped_node.ParentNode.SelectSingleNode("output_mapping");
                 if (mapped_node != null)
                 {
                     row.SetField<String>(outputs_mapped, Yes);
                 }
                 else
                 {
                     row.SetField<String>(outputs_mapped, No);
                 }
            }
            else
            {
                row.SetField<String>(inputs_mapped, No);
                row.SetField<String>(outputs_mapped, No);
            }

            myDataTable.Rows.Add(row);
        }
        
        MappingStatus.DataSource = myDataTable;
        MappingStatus.DataBind();
    }
     private byte[] GetByteArray(String strFileName)
        {
            System.IO.FileStream fs = new System.IO.FileStream(strFileName, System.IO.FileMode.Open);
            // initialise the binary reader from file streamobject
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            // define the byte array of filelength

            byte[] imgbyte = new byte[fs.Length + 1];
            // read the bytes from the binary reader

            imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
            // add the image in bytearray
            
            br.Close();
            // close the binary reader

            fs.Close();
            // close the file stream
            return imgbyte;
        }
}