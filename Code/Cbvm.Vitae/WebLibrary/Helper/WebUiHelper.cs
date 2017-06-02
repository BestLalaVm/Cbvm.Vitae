using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using LkDataContext;
using Presentation.Cache;
using Presentation.Criteria;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView;
using System.Text.RegularExpressions;
using Telerik.Web.UI;
using Presentation.Criteria.Base;

namespace WebLibrary.Helper
{
    public static partial class WebUiHelper
    {
        private static readonly string PROPERTY_NAME = "PropertyName";
        private static readonly Regex PropertyExtractor = new Regex(String.Format(@"prm_(?<{0}>\w+)_", PROPERTY_NAME));

        public static TCriteria ExtractCriteriaFromPanel<TCriteria>(this Panel pnlCondition)
            where TCriteria : BaseCriteria, new()
        {
            var criteria = new TCriteria()
            {
                CurrentUser = HaddockContext.Current.CurrentUser
            };
            var criteriaProperties = criteria.GetType().GetProperties();
            for (var index = 0; index < pnlCondition.Controls.Count; index++)
            {
                var control = pnlCondition.Controls[index];
                if (String.IsNullOrEmpty(control.ID)) continue;
                var propertyMatch = PropertyExtractor.Match(control.ID).Groups[PROPERTY_NAME];
                if (propertyMatch.Success)
                {
                    var property =
                        criteriaProperties.FirstOrDefault(
                            it => it.Name.Equals(propertyMatch.Value, StringComparison.CurrentCultureIgnoreCase));
                    if (property != null)
                    {
                        property.SetValue(criteria, control.ExtractControlValue(), null);
                    }
                }
            }
            criteria.CurrentUser = HaddockContext.Current.CurrentUser;

            return criteria;
        }

        public static void BindSource(this Control control,BindingSourceType sourceType,bool isCondition,BaseCriteria criteria=null)
        {
            switch (sourceType)
            {
                case BindingSourceType.ActivityTypeInfo:
                    BindActivityTypeInfo(isCondition, control);
                    break;
                case BindingSourceType.CourseInfo:
                    BindCourseInfo(isCondition, control, criteria);
                    break;
                case BindingSourceType.CourseTypeInfo:
                    BindCourseTypeInfo(isCondition, control, criteria);
                    break;
                case BindingSourceType.DepartInfo:
                    BindDepartInfo(isCondition, control);
                    break;
                case BindingSourceType.EducationCodeInfo:
                    BindEducationCodeInfo(isCondition, control, criteria);
                    break;
                case BindingSourceType.EducationRequiredInfo:
                    BindEducationRequiredInfo(isCondition, control);
                    break;
                case BindingSourceType.EnterpriseScopeCodeInfo:
                    BindEnterpriseScopeCodeInfo(isCondition, control, criteria);
                    break;
                case BindingSourceType.EnterpriseTypeCodeInfo:
                    BindEnterpriseTypeCodeInfo(isCondition, control, criteria);
                    break;
                case BindingSourceType.IndustryCodeInfo:
                    BindIndustryCodeInfo(isCondition, control, criteria);
                    break;
                case BindingSourceType.MarjorInfo:
                    BindMarjorInfo(isCondition, control);
                    break;
                case BindingSourceType.MarriedInfo:
                    BindMarriedInfo(isCondition, control);
                    break;
                case BindingSourceType.PeriodInfo:
                    BindPeriodInfo(isCondition, control);
                    break;
                case BindingSourceType.PoliticsInfo:
                    BindPoliticsInfo(isCondition, control);
                    break;
                case BindingSourceType.PositionNatureInfo:
                    BindPositionNatureInfo(isCondition, control);
                    break;
                case BindingSourceType.RecruitFlowSettedInfo:
                    BindRecruitFlowSettedInfo(isCondition, control, criteria);
                    break;
                case BindingSourceType.ReferralStateInfo:
                    BindReferralStateInfo(isCondition, control);
                    break;
                case BindingSourceType.RegionCodeInfo:
                    BindRegionCodeInfo(isCondition, control, criteria);
                    break;
                case BindingSourceType.RequiredTargeterInfo:
                    BindRequiredTargeterInfo(isCondition, control);
                    break;
                case BindingSourceType.SalaryInfo:
                    BindSalaryInfo(isCondition, control, criteria);
                    break;
                case BindingSourceType.SexInfo:
                    BindSexInfo(isCondition, control);
                    break;
                case BindingSourceType.StudentOpenTypeInfo:
                    BindStudentOpenTypeInfo(isCondition, control);
                    break;
                case BindingSourceType.TeacherGroupCodeInfo:
                    BindTeacherGroupCodeInfo(isCondition, control, criteria);
                    break;
                case BindingSourceType.TeacherNumInfo:
                    BindTeacherNumInfo(isCondition, control, criteria);
                    break;
                case BindingSourceType.VerifyStatusInfo:
                    BindVerifyStatusInfo(isCondition, control);
                    break;
                case BindingSourceType.WorkYearsInfo:
                    BindWorkYearsInfo(isCondition, control,criteria);
                    break;
                case BindingSourceType.College:
                    BindCollegesInfo(isCondition, control, criteria);
                    break;
                case BindingSourceType.ProvinceInfo:
                    BindProvinceInfo(isCondition, control);
                    break;
                case BindingSourceType.CityInfo:
                    BindCityInfo(isCondition, control, criteria as AddressCriteria);
                    break;
                case BindingSourceType.DistrictInfo:
                    BindDistrictInfo(isCondition, control, criteria as AddressCriteria);
                    break;
            }
        }

        public static void Reset(this Panel pnlConditionControl, RadGrid radGrid)
        {
            for (var index = 0; index < pnlConditionControl.Controls.Count; index++)
            {
                var controlItem = pnlConditionControl.Controls[index];
                var txt = controlItem as TextBox;
                if (txt != null)
                {
                    txt.Text = "";
                    continue;
                }
                var drp = controlItem as DropDownList;
                if (drp != null && drp.Items.Count > 0)
                {
                    drp.SelectedIndex = 0;
                    continue;
                }
                var cmb = controlItem as RadComboBox;
                if (cmb != null && cmb.Items.Count > 0)
                {
                    cmb.SelectedIndex = 0;
                    continue;
                }
                var dtp = controlItem as RadDatePicker;
                if (dtp != null)
                {
                    dtp.Clear();
                }
            }
            radGrid.Rebind();
        }

        public static void ReflashFrame(this Control control)
        {
            var script = new StringBuilder();
            script.Append("window.parent.ReflashFramePage();");
            control.Page.ClientScript.RegisterClientScriptBlock(control.Page.GetType(), "FrameReflash", script.ToString(), true);
        }

        public static bool PresentationEmptyCheck(this PlaceHolder control,BasePresentation presentation)
        {
            if (presentation == null)
            {
                control.Controls.Clear();
                control.Controls.Add(new Literal()
                {
                    Text = "找不到任何数据!"
                });
                return false;
            }
            return true;
        }
    }

    partial class WebUiHelper
    {
        public static object ExtractControlValue(this Control control)
        {
            var txt = control as TextBox;
            if (txt != null)
            {
                return txt.Text.Trim();
            }

            var drpDownList = control as DropDownList;
            if (drpDownList != null)
            {
                return drpDownList.SelectedValue;
            }
            var cmb = control as RadComboBox;
            if (cmb != null)
            {
                return cmb.SelectedValue;
            }

            var tlDate = control as RadDatePicker;
            if (tlDate != null)
            {
                return tlDate.SelectedDate;
            }

            return null;
        }

        private static CVAcademicianDataContext dataContext = new CVAcademicianDataContext();

        private static void BindSexInfo(bool isCondition, Control control)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "全部",
                    Value = ""
                });
            }
            GlobalBaseDataCache.SexInfoList.ForEach(it =>
            {
                list.Add(new DataItemPresentation
                {
                    Value = it.Value,
                    Text = it.Text
                });
            });
            var radio = control as RadioButtonList;
            if (radio != null)
            {
                radio.DataTextField = "Text";
                radio.DataValueField = "Value";
                radio.DataSource = list;
                radio.DataBind();
                radio.SelectedIndex = 0;
            }
        }

        private static void BindMarriedInfo(bool isCondition, Control control)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "全部",
                    Value = ""
                });
            }
            GlobalBaseDataCache.MarriedList.ForEach(it =>
            {
                list.Add(new DataItemPresentation
                {
                    Text = it.Text,
                    Value = it.Value
                });
            });
            var radio = control as RadioButtonList;
            if (radio != null)
            {
                radio.DataTextField = "Text";
                radio.DataValueField = "Value";
                radio.DataSource = list;
                radio.DataBind();
            }
        }

        private static void BindReferralStateInfo(bool isCondition, Control control)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "全部",
                    Value = ""
                });
            }
            GlobalBaseDataCache.ReferralStateList.ForEach(it =>
            {
                list.Add(new DataItemPresentation
                {
                    Text = it.Text,
                    Value = it.Value
                });
            });
            var drp = control as DropDownList;
            if (drp != null)
            {
                drp.DataTextField = "Text";
                drp.DataValueField = "Value";
                drp.DataSource = list;
                drp.DataBind();
            }
        }

        private static void BindPeriodInfo(bool isCondition, Control control)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "全部",
                    Value = ""
                });
            }
            list.Add(new DataItemPresentation
            {
                Text = "2000",
                Value = "2000"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2001",
                Value = "2001"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2002",
                Value = "2002"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2003",
                Value = "2003"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2004",
                Value = "2004"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2005",
                Value = "2005"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2006",
                Value = "2006"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2007",
                Value = "2007"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2008",
                Value = "2008"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2009",
                Value = "2009"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2010",
                Value = "2010"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2011",
                Value = "2011"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2012",
                Value = "2012"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2013",
                Value = "2013"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2014",
                Value = "2014"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2015",
                Value = "2015"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2016",
                Value = "2016"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2017",
                Value = "2017"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2018",
                Value = "2018"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2018",
                Value = "2018"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2019",
                Value = "2019"
            });
            list.Add(new DataItemPresentation
            {
                Text = "2020",
                Value = "2020"
            });
            var drp = control as DropDownList;
            if (drp != null)
            {
                drp.DataTextField = "Text";
                drp.DataValueField = "Value";
                drp.DataSource = list;
                drp.DataBind();
            }
            else
            {
                var cmb = control as RadComboBox;
                if (cmb != null)
                {
                    cmb.DataTextField = "Text";
                    cmb.DataValueField = "Value";
                    cmb.DataSource = list;
                    cmb.DataBind();
                }
            }
        }

        private static void BindPoliticsInfo(bool isCondition, Control control)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "全部",
                    Value = ""
                });
            }
            GlobalBaseDataCache.PoliticsList.ForEach(it =>
            {
                list.Add(new DataItemPresentation
                {
                    Text = it.Text,
                    Value = it.Value
                });
            });

            var drp = control as DropDownList;
            if (drp != null)
            {
                drp.DataTextField = "Text";
                drp.DataValueField = "Value";
                drp.DataSource = list;
                drp.DataBind();
            }
            else
            {
                var cmb = control as RadComboBox;
                if (cmb != null)
                {
                    cmb.DataTextField = "Text";
                    cmb.DataValueField = "Value";
                    cmb.DataSource = list;
                    cmb.DataBind();
                }
            }
        }

        private static void BindDepartInfo(bool isCondition, Control control)
        {
            var list = GlobalBaseDataCache.DepartList.Select(it => new DataItemPresentation
            {
                Text = it.Name,
                Value = it.Code
            }).ToList();
            if (isCondition)
            {
                list.Insert(0,new DataItemPresentation
                {
                    Text = "全部",
                    Value = ""
                });
            }

            var drp = control as DropDownList;
            if (drp != null)
            {
                drp.DataTextField = "Text";
                drp.DataValueField = "Value";
                drp.DataSource = list;
                drp.DataBind();
            }
            else
            {
                var cmb = control as RadComboBox;
                if (cmb != null)
                {
                    cmb.DataTextField = "Text";
                    cmb.DataValueField = "Value";
                    cmb.DataSource = list;
                    cmb.DataBind();
                }
            }
        }

        private static void BindMarjorInfo(bool isCondition, Control control)
        {
            var list = GlobalBaseDataCache.MarjorList.Select(it => new DataItemPresentation
            {
                Text = it.Name,
                Value = it.Code
            }).ToList();
            var drp = control as DropDownList;
            if (isCondition)
            {
                list.Insert(0, new DataItemPresentation()
                {
                    Text = "全部",
                    Value = ""
                });
            }
            if (drp != null)
            {
                drp.DataTextField = "Text";
                drp.DataValueField = "Value";
                drp.DataSource = list;
                drp.DataBind();
            }
            else
            {
                var cmb = control as RadComboBox;
                if (cmb != null)
                {
                    cmb.DataTextField = "Text";
                    cmb.DataValueField = "Value";
                    cmb.DataSource = list;
                    cmb.DataBind();
                }
            }


        }

        private static void BindVerifyStatusInfo(bool isCondition, Control control)
        {
            var list = new List<DataItemPresentation>(); ;
            if (isCondition)
            {
                list.Insert(0, new DataItemPresentation
                {
                    Text = "全部",
                    Value = ""
                });
            }
            GlobalBaseDataCache.VerifyStatusList.ForEach(it => list.Add(new DataItemPresentation
            {
                Text = it.Text,
                Value = it.Value
            }));
            var drp = control as DropDownList;
            if (drp != null)
            {
                drp.DataTextField = "Text";
                drp.DataValueField = "Value";
                drp.DataSource = list;
                drp.DataBind();
            }
            else
            {
                var cmb = control as RadComboBox;
                if (cmb != null)
                {
                    cmb.DataTextField = "Text";
                    cmb.DataValueField = "Value";
                    cmb.DataSource = list;
                    cmb.DataBind();
                }
            }
        }

        private static void BindActivityTypeInfo(bool isCondition, Control control)
        {
            var list =
                GlobalBaseDataCache.ActivityTypeList.Where(it => it.ActivityDataType != ActivityDataType.Exercitation)
                           .Select(it => new DataItemPresentation()
                           {
                               Text = it.Name,
                               Value = it.Code
                           }).ToList();
            if (isCondition)
            {
                list.Insert(0, new DataItemPresentation()
                {
                    Text = "全部",
                    Value = ""
                });
            }
            var drp = control as DropDownList;
            if (drp != null)
            {
                drp.DataTextField = "Text";
                drp.DataValueField = "Value";
                drp.DataSource = list;
                drp.DataBind();
            }
            else
            {
                var cmb = control as RadComboBox;
                if (cmb != null)
                {
                    cmb.DataTextField = "Text";
                    cmb.DataValueField = "Value";
                    cmb.DataSource = list;
                    cmb.DataBind();
                }
            }
        }

        private static void BindPositionNatureInfo(bool isCondition, Control control)
        {
            var list = GlobalBaseDataCache.PositionNatureList;
            var drp = control as DropDownList;
            if (drp != null)
            {
                drp.DataTextField = "Text";
                drp.DataValueField = "Text";
                drp.DataSource = list;
                drp.DataBind();
            }
            else
            {
                var cmb = control as RadComboBox;
                if (cmb != null)
                {
                    cmb.DataTextField = "Text";
                    cmb.DataValueField = "Value";
                    cmb.DataSource = list;
                    cmb.DataBind();
                }
                else
                {
                    var listC = control as ListBox;
                    if (listC != null)
                    {
                        listC.DataTextField = "Text";
                        listC.DataValueField = "Value";
                        listC.DataSource = list;
                        listC.DataBind();
                    }
                }
            }
        }

        private static void BindRequiredTargeterInfo(bool isCondition, Control control)
        {
            var list = GlobalBaseDataCache.RequiredTargeterList;
            var drp = control as DropDownList;
            if (drp != null)
            {
                drp.DataTextField = "Text";
                drp.DataValueField = "Text";
                drp.DataSource = list;
                drp.DataBind();
            }
            else
            {
                var cmb = control as RadComboBox;
                if (cmb != null)
                {
                    cmb.DataTextField = "Text";
                    cmb.DataValueField = "Value";
                    cmb.DataSource = list;
                    cmb.DataBind();
                }
            }
        }

        private static void BindEducationRequiredInfo(bool isCondition, Control control)
        {
            var list = GlobalBaseDataCache.EducationRequiredList;
            var drp = control as DropDownList;
            if (drp != null)
            {
                drp.DataTextField = "Text";
                drp.DataValueField = "Text";
                drp.DataSource = list;
                drp.DataBind();
            }
            else
            {
                var cmb = control as RadComboBox;
                if (cmb != null)
                {
                    cmb.DataTextField = "Text";
                    cmb.DataValueField = "Value";
                    cmb.DataSource = list;
                    cmb.DataBind();
                }
            }
        }

        private static void BindTeacherNumInfo(bool isCondition, Control control,BaseCriteria criteria)
        {
            var query = dataContext.Teacher.Where(it => !it.IsDelete && it.IsOnline);

            //This is the Student criteria
            if (criteria.CurrentUser != null)
            {
                query = query.Where(it => it.CollegeCode == criteria.CurrentUser.Identity);
            }

            var list = query.Select(it => new DataItemPresentation()
                    {
                        Text = it.NameZh,
                        Value = it.TeacherNum
                    }).ToList();

            
            var drp = control as DropDownList;
            if (drp != null)
            {
                drp.DataTextField = "Text";
                drp.DataValueField = "Value";
                drp.DataSource = list;
                drp.DataBind();
            }
            else
            {
                var cmb = control as RadComboBox;
                if (cmb != null)
                {
                    cmb.DataTextField = "Text";
                    cmb.DataValueField = "Value";
                    cmb.DataSource = list;
                    cmb.DataBind();
                }
            }
        }

        private static void BindCourseInfo(bool isCondition, Control control, BaseCriteria criteria)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "全部",
                    Value = ""
                });
            }

            var query = GlobalBaseDataCache.CourseList;
            StudentCourseCriteria courseCriteria = criteria as StudentCourseCriteria;
            if (courseCriteria != null)
            {
                if (!String.IsNullOrEmpty(courseCriteria.CourseTypeCode))
                {
                    query = query.Where(ix => ix.TypeCode.Trim() == courseCriteria.CourseTypeCode.Trim()).ToList();
                }

                if (!String.IsNullOrEmpty(courseCriteria.MarjorCode))
                {
                    query = query.Where(ix => ix.MarjorCode.Trim() == courseCriteria.MarjorCode.Trim()).ToList();
                }
            }

            query.ForEach(it =>
            {
                list.Add(new DataItemPresentation
                {
                    Text = it.Name,
                    Value = it.Code
                });

            });
            var drp = control as DropDownList;
            if (drp != null)
            {
                drp.DataTextField = "Text";
                drp.DataValueField = "Value";
                drp.DataSource = list;
                drp.DataBind();
            }
            else
            {
                var cmb = control as RadComboBox;
                if (cmb != null)
                {
                    cmb.DataTextField = "Text";
                    cmb.DataValueField = "Value";
                    cmb.DataSource = list;
                    cmb.DataBind();
                }
            }
        }

        private static void BindCourseTypeInfo(bool isCondition, Control control, BaseCriteria criteria)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "全部",
                    Value = ""
                });
            }
            GlobalBaseDataCache.CourseTypeList.ForEach(it =>
            {
                list.Add(new DataItemPresentation
                {
                    Text = it.Name,
                    Value = it.Code
                });
            });
            var drp = control as DropDownList;
            if (drp != null)
            {
                drp.DataTextField = "Text";
                drp.DataValueField = "Value";
                drp.DataSource = list;
                drp.DataBind();
            }
            else
            {
                var cmb = control as RadComboBox;
                if (cmb != null)
                {
                    cmb.DataTextField = "Text";
                    cmb.DataValueField = "Value";
                    cmb.DataSource = list;
                    cmb.DataBind();
                }
            }
        }

        private static void BindEducationCodeInfo(bool isCondition, Control control, BaseCriteria criteria)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "全部",
                    Value = ""
                });
            }
            GlobalBaseDataCache.EducationList.ForEach(it =>
            {
                list.Add(new DataItemPresentation
                {
                    Text = it.Name,
                    Value = it.Code
                });
            });
            Bind2Control(list, control);
        }

        private static void BindRegionCodeInfo(bool isCondition, Control control, BaseCriteria criteria)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "全部",
                    Value = ""
                });
            }
            GlobalBaseDataCache.CdRegionList.ForEach(it =>
            {
                list.Add(new DataItemPresentation
                {
                    Text = it.Name,
                    Value = it.Code
                });
            });
            Bind2Control(list, control);
        }

        private static void BindIndustryCodeInfo(bool isCondition, Control control, BaseCriteria criteria)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "全部",
                    Value = ""
                });
            }
            GlobalBaseDataCache.CdIndustryList.ForEach(it =>
            {
                list.Add(new DataItemPresentation
                {
                    Text = it.Name,
                    Value = it.Code
                });
            });
            Bind2Control(list, control);
        }

        private static void BindEnterpriseTypeCodeInfo(bool isCondition, Control control, BaseCriteria criteria)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "全部",
                    Value = ""
                });
            }
            GlobalBaseDataCache.EnterpriseTypeList.ForEach(it =>
            {
                list.Add(new DataItemPresentation
                {
                    Text = it.Name,
                    Value = it.Code
                });
            });
            Bind2Control(list, control);
        }

        private static void BindEnterpriseScopeCodeInfo(bool isCondition, Control control, BaseCriteria criteria)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "全部",
                    Value = ""
                });
            }
            GlobalBaseDataCache.EnterpriseScopeList.ForEach(it =>
            {
                list.Add(new DataItemPresentation
                {
                    Text = it.Name,
                    Value = it.Code
                });
            });
            Bind2Control(list, control);
        }

        private static void BindTeacherGroupCodeInfo(bool isCondition, Control control, BaseCriteria criteria)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "全部",
                    Value = ""
                });
            }
            GlobalBaseDataCache.TeacherGroupList.ForEach(it =>
            {
                list.Add(new DataItemPresentation
                {
                    Text = it.Name,
                    Value = it.Code
                });
            });
            Bind2Control(list, control);
        }

        private static void BindRecruitFlowSettedInfo(bool isCondition, Control control, BaseCriteria criteria)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "全部",
                    Value = ""
                });
            }
            GlobalBaseDataCache.RecruitFlowSettedList.ForEach(it =>
            {
                list.Add(new DataItemPresentation
                {
                    Text = it.Name,
                    Value = it.Id.ToString()
                });
            });
            Bind2Control(list, control);
        }

        private static void BindWorkYearsInfo(bool isCondition, Control control, BaseCriteria criteria)
        {

        }

        private static void BindCollegesInfo(bool isCondition, Control control, BaseCriteria criteria)
        {
            var collegeCriteria = criteria as CollegeCriteriaBase;

             var datacontext = new CVAcademicianDataContext();
             var query = datacontext.College.AsQueryable();
             if (collegeCriteria != null)
             {
                 query = query.Where(it => it.UniversityCode == collegeCriteria.UniversityCode);
             }


             var list = query.Select(it => new DataItemPresentation()
            {
                Text = it.Name,
                Value = it.Code
            }).ToList();

             if (isCondition)
             {
                 list.Insert(0,new DataItemPresentation
                 {
                     Text = "全部",
                     Value = ""
                 });
             }

            Bind2Control(list, control);
        }

        private static void BindSalaryInfo(bool isCondition, Control control, BaseCriteria criteria)
        {
            var list = GlobalBaseDataCache.SalaryList;
            var drp = control as DropDownList;
            if (drp != null)
            {
                drp.DataTextField = "Text";
                drp.DataValueField = "Value";
                drp.DataSource = list;
                drp.DataBind();
            }
        }

        private static void BindStudentOpenTypeInfo(bool isCondition, Control control)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "全部",
                    Value = ""
                });
            }
            GlobalBaseDataCache.OpenTypeInfoList.ForEach(it =>
            {
                list.Add(new DataItemPresentation
                {
                    Value = it.Value,
                    Text = it.Text
                });
            });
            var chkList = control as CheckBoxList;
            if (chkList != null)
            {
                chkList.DataTextField = "Text";
                chkList.DataValueField = "Value";
                chkList.DataSource = list;
                chkList.DataBind();
            }
        }

        private static void Bind2Control(List<DataItemPresentation> list, Control control)
        {
            var drp = control as ListControl;
            if (drp != null)
            {
                drp.DataTextField = "Text";
                drp.DataValueField = "Value";
                drp.DataSource = list;
                drp.DataBind();
            }
            else
            {
                var cmb = control as RadComboBox;
                if (cmb != null)
                {
                    cmb.DataTextField = "Text";
                    cmb.DataValueField = "Value";
                    cmb.DataSource = list;
                    cmb.DataBind();
                }
            }
        }

        private static void BindProvinceInfo(bool isCondition, Control control)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "",
                    Value = ""
                });
            }
            GlobalBaseDataCache.AddressPresentationList.Where(it=>it.Type==LkDataContext.AddressType.Province).ToList().ForEach(it =>
            {
                list.Add(new DataItemPresentation
                {
                    Value = it.Code,
                    Text = it.Name
                });
            });

            var chkList = control as DropDownList;
            if (chkList != null)
            {
                chkList.DataTextField = "Text";
                chkList.DataValueField = "Value";
                chkList.DataSource = list;
                chkList.DataBind();
            }
        }

        private static void BindCityInfo(bool isCondition, Control control,AddressCriteria criteria)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "",
                    Value = ""
                });
            }
            GlobalBaseDataCache.AddressPresentationList.Where(it => it.Type == LkDataContext.AddressType.City && it.ParentCode == criteria.Code).ToList().ForEach(it =>
            {
                list.Add(new DataItemPresentation
                {
                    Value = it.Code,
                    Text = it.Name
                });
            });

            var chkList = control as DropDownList;
            if (chkList != null)
            {
                chkList.DataTextField = "Text";
                chkList.DataValueField = "Value";
                chkList.DataSource = list;
                chkList.DataBind();
            }
        }

        private static void BindDistrictInfo(bool isCondition, Control control, AddressCriteria criteria)
        {
            var list = new List<DataItemPresentation>();
            if (isCondition)
            {
                list.Add(new DataItemPresentation
                {
                    Text = "",
                    Value = ""
                });
            }
            GlobalBaseDataCache.AddressPresentationList.Where(it => it.Type == LkDataContext.AddressType.District && it.ParentCode == criteria.Code).ToList().ForEach(it =>
            {
                list.Add(new DataItemPresentation
                {
                    Value = it.Code,
                    Text = it.Name
                });
            });

            var chkList = control as DropDownList;
            if (chkList != null)
            {
                chkList.DataTextField = "Text";
                chkList.DataValueField = "Value";
                chkList.DataSource = list;
                chkList.DataBind();
            }
        }
    }

    public enum BindingSourceType
    {
        SexInfo,
        MarriedInfo,
        ReferralStateInfo,
        PeriodInfo,
        PoliticsInfo,
        DepartInfo,
        MarjorInfo,
        VerifyStatusInfo,
        ActivityTypeInfo,
        PositionNatureInfo,
        RequiredTargeterInfo,
        EducationRequiredInfo,
        TeacherNumInfo,
        CourseInfo,
        CourseTypeInfo,
        EducationCodeInfo,
        RegionCodeInfo,
        IndustryCodeInfo,
        EnterpriseTypeCodeInfo,
        EnterpriseScopeCodeInfo,
        TeacherGroupCodeInfo,
        RecruitFlowSettedInfo,
        WorkYearsInfo,
        SalaryInfo,
        StudentOpenTypeInfo,
        College,
        ProvinceInfo,
        CityInfo,
        DistrictInfo
    }
}
