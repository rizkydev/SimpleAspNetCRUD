using SimpleAspNetCRUD.Controllers;
using SimpleAspNetCRUD.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimpleAspNetCRUD.View
{
    public partial class Contact : System.Web.UI.Page
    {

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnDelete.Enabled = false;
                FillGridView();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            hfContactID.Value = "";
            txtName.Text = txtMobile.Text = txtAddress.Text = "";
            lblSuccessMessage.Text = lblErrorMessage.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var inputDat = GetInputData();
            var ContactCtrl = new ContactController();
            var isValid = ContactCtrl.SaveData(inputDat);
            if (isValid)
            {
                Clear();
                if (hfContactID.Value == "")
                    lblSuccessMessage.Text = "Create Succeed";
                else
                    lblSuccessMessage.Text = "Update Succeed";
            }
            else
            {
                lblErrorMessage.Text = "Process Failed";
            }
            FillGridView();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var inputDat = GetInputData();
            var ContactCtrl = new ContactController();
            var isValid = ContactCtrl.DeleteData(hfContactID.Value == "" ? 0 : Convert.ToInt32(hfContactID.Value));
            if (isValid)
            {
                Clear();
                lblSuccessMessage.Text = "Delete Successfully";
            }
            else
            {
                lblErrorMessage.Text = "Delete Failed";
            }
            FillGridView();
        }

        private ContactEntity GetInputData()
        {
            var dat = new ContactEntity();
            dat.ContactID = hfContactID.Value == "" ? 0 : Convert.ToInt32(hfContactID.Value);
            dat.Name = txtName.Text.Trim();
            dat.Mobile = txtMobile.Text.Trim();
            dat.Address = txtAddress.Text.Trim();
            return dat;
        }

        void FillGridView()
        {
            var ContactCtrl = new ContactController();
            var dtbl = ContactCtrl.GetListData();
            gvContact.DataSource = dtbl;
            gvContact.DataBind();        
        }

        protected void lnk_OnClick(object sender, EventArgs e)
        {
            var ContactCtrl = new ContactController();
            int contactID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            var dtbl = ContactCtrl.GetListData(contactID);
            hfContactID.Value = contactID.ToString();
            txtName.Text = dtbl.Rows[0]["Name"].ToString();
            txtMobile.Text = dtbl.Rows[0]["Mobile"].ToString();
            txtAddress.Text = dtbl.Rows[0]["Address"].ToString();
            btnSave.Text = "Update";
            btnDelete.Enabled = true;
        }
    }
}