using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Inpinke.Model.CustomClass;
using Inpinke.Model;
using Inpinke.Model.Enum;
using Helper.UI;

namespace Inpinke.BLL
{
    public class DBMessageBLL
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(DBMessageBLL));
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BaseResponse SendMessage(Inpinke_Message model)
        {
            BaseResponse br = new BaseResponse();
            try
            {
                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;
                model.Status = (int)RecordStatus.Nomral;
                model.MsgStatus = (int)MsgStatus.NoRead;
                model.InsertWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                br.IsSuccess = true;
                br.ResponseObj = model;

            }
            catch (Exception ex)
            {
                br.IsSuccess = false;
                br.Message = "发送消息失败，请稍后再试";
                Logger.Error(string.Format("SendMessage FromUser:{0},ToUser:{1},Error:{2}", model.FromUser, model.ToUser, ex.ToString()));
            }
            return br;
        }
        /// <summary>
        /// 获取消息详情，包括相关回复
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IList<Inpinke_Message> GetMsgDetail(int id)
        {
            try
            {
                var q = from m in InpinkeDataContext.Instance.Inpinke_Messages
                        where m.Status == (int)RecordStatus.Nomral && (m.ID == id || m.ReplyID == id)
                        select m;
                return q.ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetMsgDetail MsgID:{0},Error:{1}", id, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 获取用户相关的信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static IList<Inpinke_Message> GetMsgDetail(int id, int userid)
        {
            try
            {
                var q = from m in InpinkeDataContext.Instance.Inpinke_Messages
                        where m.Status == (int)RecordStatus.Nomral && (m.ID == id || m.ReplyID == id) && (m.ToUser == userid || m.FromUser == userid)
                        select m;
                return q.ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetMsgDetail MsgID:{0},Error:{1}", id, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 获取用户消息列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static IList<Inpinke_Message> GetUserMessage(int userid)
        {
            try
            {
                var q = from m in InpinkeDataContext.Instance.Inpinke_Messages
                        where m.Status == (int)RecordStatus.Nomral && (m.ToUser == userid || m.FromUser == userid) && m.ReplyID == 0
                        select m;
                return q.ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserMessage UserID:{0},Error:{1}", userid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 分页获取用户消息
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static IList<Inpinke_Message> GetUserMessage(PageInfo pInfo, int userid)
        {
            try
            {
                var q = from m in InpinkeDataContext.Instance.Inpinke_Messages
                        where m.Status == (int)RecordStatus.Nomral && (m.ToUser == userid || m.FromUser == userid) && m.ReplyID == 0
                        orderby m.CreateTime descending
                        select m;
                pInfo.Total = InpinkeDataContext.Instance.ExecuteQuery<int>(string.Format("select isnull(count(*),0) from Inpinke_Message where status=1 and (touser={0} or fromuser={0}) and replyid=0", userid)).FirstOrDefault();

                return q.Skip(pInfo.Skip).Take(pInfo.PageSize).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserMessage UserID:{0},Error:{1}", userid, ex.ToString()));
                return null;
            }
        }
    }
}
