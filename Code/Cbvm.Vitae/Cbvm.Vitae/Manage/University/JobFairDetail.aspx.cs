using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentation.UIView.University;
using LkDataContext;
using Presentation.UIView.College.View;
using Presentation.UIView.Enterprise.View;

namespace Cbvm.Vitae.Manage.University
{
    public partial class JobFairDetail : BaseUniversityDetailPage
    {
        private int jobFairId
        {
            get
            {
                int id = 0;
                int.TryParse(Request.QueryString["ID"], out id);

                return id;
            }
        }

        private IList<JobFareCollegePresentation> Colleges
        {
            get
            {
                var _coll = this.ViewState["Colleges"] as IList<JobFareCollegePresentation>;
                if (_coll == null)
                {
                    _coll = new List<JobFareCollegePresentation>();
                }

                return _coll;
            }
            set
            {
                this.grdCollege.DataSource = value;
                grdCollege.VirtualItemCount = value.Count();
                grdCollege.DataBind();

                this.ViewState["Colleges"] = value;
            }
        }

        //private IList<JobFareEnterprisePresentation> Enterprises
        //{
        //    get
        //    {
        //        var _coll = this.ViewState["Enterprises"] as IList<JobFareEnterprisePresentation>;
        //        if (_coll == null)
        //        {
        //            _coll = new List<JobFareEnterprisePresentation>();
        //        }

        //        return _coll;
        //    }
        //    set
        //    {
        //        this.grdEnterprise.DataSource = value;
        //        grdEnterprise.VirtualItemCount = value.Count();
        //        grdEnterprise.DataBind();

        //        this.ViewState["Enterprises"] = value;
        //    }
        //}

        protected override void InitData()
        {
            var data = DataContext.JobFairManage.Where(it=>it.ID==jobFairId).Select(it => new JobFarePresentation()
            {
                ID = it.ID,
                Name = it.Name,
                UniversityCode = it.UniversityCode,
                IsOnline = it.IsOnline,
                BeginTime = it.BeginTime,
                EndTime = it.EndTime,
                Description = it.Description,
                Address = it.Address,
                Colleges = it.JobFairManageCollege.Select(ix => new JobFareCollegePresentation
                {
                    Code = ix.CollegeCode,
                    Name = ix.College.Name
                }).ToList(),
                //Enterprises = it.JobFairManageEnterprise.Select(ix => new JobFareEnterprisePresentation
                //{
                //    Code = ix.EnterpriseCode,
                //    Name = ix.Enterprise.Name
                //}).ToList(),

            }).FirstOrDefault();

            if (data != null)
            {
                txt_Name_.Text = data.Name;
                dtp_BeginTime_.SelectedDate = data.BeginTime;
                //dtp_EndTime_.SelectedDate = data.EndTime;

                //chk_IsOnline_.Checked = data.IsOnline;
                txt_Content_.LoadData(data.Description);
                txt_Address_.Text = data.Address;
                this.Colleges = data.Colleges;
                //this.Enterprises = data.Enterprises;
            }

            base.InitData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var jobFare = DataContext.JobFairManage.Where(it => it.ID == jobFairId).FirstOrDefault();

            if (jobFare == null)
            {
                jobFare = new LkDataContext.JobFairManage()
                {
                    UniversityCode = this.UniversityCode
                };

                DataContext.JobFairManage.InsertOnSubmit(jobFare);
            }

            jobFare.Name = txt_Name_.Text;
            jobFare.BeginTime = dtp_BeginTime_.SelectedDate.Value;
            //jobFare.EndTime = dtp_EndTime_.SelectedDate.Value;
            jobFare.EndTime = jobFare.BeginTime.AddYears(1);
            jobFare.Description = txt_Content_.SaveData();
            jobFare.IsOnline = true;// chk_IsOnline_.Checked;
            jobFare.Address = txt_Address_.Text;
            jobFare.CreatedTime = DateTime.Now;
            jobFare.LastUpdateTime = DateTime.Now;

            foreach (var ent in jobFare.JobFairManageEnterprise)
            {
                DataContext.JobFairManageEnterprise.DeleteOnSubmit(ent);
            }
            jobFare.JobFairManageEnterprise.Clear();

            //foreach (var ent in Enterprises)
            //{
            //    if (!jobFare.JobFairManageEnterprise.Any(ix => ix.EnterpriseCode == ent.Code))
            //    {
            //        jobFare.JobFairManageEnterprise.Add(new JobFairManageEnterprise
            //        {
            //            CreateTime = DateTime.Now,
            //            EnterpriseCode = ent.Code
            //        });
            //    }
            //}

            foreach (var col in jobFare.JobFairManageCollege)
            {
                if (!this.Colleges.Any(ix => ix.Code == col.CollegeCode))
                {
                    DataContext.JobFairManageCollege.DeleteOnSubmit(col);
                }
            }
            //jobFare.JobFairManageCollege.Clear();

            foreach (var col in this.Colleges)
            {
                if (!jobFare.JobFairManageCollege.Any(ix => ix.CollegeCode == col.Code))
                {
                    jobFare.JobFairManageCollege.Add(new JobFairManageCollege
                    {
                        CreateTime = DateTime.Now,
                        CollegeCode = col.Code,
                        IsSentMail=false
                    });
                }
            }
            DataContext.SubmitChanges();

            //ShowMsg(true, "保存成功!");

            Response.Redirect("JobFairManage.aspx");
        }

        protected void btnClearCollege_Click(object sender, EventArgs e)
        {
            this.Colleges = new List<JobFareCollegePresentation>();
            Session.ClearEntityFromSession<JobFareCollegePresentation>();
        }

        //protected void btnClearEnterprise_Click(object sender, EventArgs e)
        //{
        //    this.Enterprises = new List<JobFareEnterprisePresentation>();

        //    Session.ClearEntityFromSession<JobFareEnterprisePresentation>();
        //}

        protected void btnRefreshSession_Click(object sender, EventArgs e)
        {
            var _colleges = this.Colleges;
            foreach (var col in Session.GetEntityFromSession<CollegeCommonPresentation>().Select(it => new JobFareCollegePresentation
           {
               Code = it.Code,
               Name = it.Name
           }).ToList())
            {
                if (!_colleges.Any(ix => ix.Code == col.Code))
                {
                    _colleges.Add(col);
                }                
            }

            this.Colleges = _colleges;

            //var _enterprises = this.Enterprises;

            //foreach(var ent in Session.GetEntityFromSession<EnterpriseCommonPresentation>().Select(it => new JobFareEnterprisePresentation
            //{
            //    Code = it.Code,
            //    Name = it.Name
            //}).ToList())
            //{
            //    if (!_enterprises.Any(ix => ix.Code == ent.Code))
            //    {
            //        _enterprises.Add(ent);
            //    }                
            //}
            //this.Enterprises = _enterprises;
        }

        protected override void InitBindData()
        {
            this.grdCollege.AllowCustomPaging = false;
            this.grdCollege.AllowPaging = false;
            this.grdCollege.ClientSettings.Scrolling.AllowScroll = false;
            this.grdCollege.PagerStyle.AlwaysVisible = false;

            //this.grdEnterprise.AllowCustomPaging = false;
            //this.grdEnterprise.AllowPaging = false;
            //this.grdEnterprise.ClientSettings.Scrolling.AllowScroll = false;
            //this.grdEnterprise.PagerStyle.AlwaysVisible = false;


            //Session.ClearEntityFromSession<EnterpriseCommonPresentation>();
            Session.ClearEntityFromSession<CollegeCommonPresentation>();
        }

        //protected void grdEnterprise_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        //{
        //    var enterpriseCode = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Code"].ToString();
        //    if (!String.IsNullOrEmpty(enterpriseCode))
        //    {
        //        var ent = this.Enterprises.Where(it => it.Code == enterpriseCode).FirstOrDefault();
        //        if (ent != null)
        //        {
        //            var _ents = this.Enterprises;
        //            _ents.Remove(ent);

        //            this.Enterprises = _ents;
        //        }
        //    }
        //}

        protected void grdCollege_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var collegeCode = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Code"].ToString();
            if (!String.IsNullOrEmpty(collegeCode))
            {
                var col = this.Colleges.Where(it => it.Code == collegeCode).FirstOrDefault();
                if (col != null)
                {
                    var _cols = this.Colleges;
                    _cols.Remove(col);

                    this.Colleges = _cols;
                }
            }
        }
    }
}