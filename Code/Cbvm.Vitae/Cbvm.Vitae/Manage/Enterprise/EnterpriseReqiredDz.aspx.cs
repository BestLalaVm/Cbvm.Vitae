using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cbvm.Vitae.Manage.Enterprise
{
    public partial class EnterpriseReqiredDz : BaseEnterpriseDetailPage
    {
        protected override void InitData()
        {
            var data = DataContext.EnterpriseRequiredDz.Where(it => it.EnterpriseCode == this.EnterpriseCode).FirstOrDefault();

            if (data != null)
            {
                txt_Keyword_.Text = data.Keyword;
                txt_Grade_.Text = data.Grade;
                drp_Sex_.SelectedValue = data.Sex.ToString();
                drp_Frquence_.SelectedValue = data.Frequence;
            }

            base.InitData();
        }

        protected void btnSave1_Click(object sender, EventArgs e)
        {
            var data = DataContext.EnterpriseRequiredDz.Where(it => it.EnterpriseCode == this.EnterpriseCode).FirstOrDefault();
            if (data == null)
            {
                data = new LkDataContext.EnterpriseRequiredDz
                {
                    EnterpriseCode = this.EnterpriseCode
                };

                DataContext.EnterpriseRequiredDz.InsertOnSubmit(data);
            }

            data.Keyword = txt_Keyword_.Text;
            data.Grade = txt_Grade_.Text;
            data.Sex = int.Parse(drp_Sex_.SelectedValue);
            data.Frequence = drp_Frquence_.SelectedValue;

            data.UpdateTime = DateTime.Now;
            data.CountLeft = 10;

            DataContext.SubmitChanges();

            this.ShowMsg(true, "保存成功!");
        }
    }
}