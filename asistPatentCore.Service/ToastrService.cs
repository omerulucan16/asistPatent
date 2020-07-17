using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using asistPatentCore.Model.Enums;
using asistPatentCore.Service;

namespace asistPatentCore.Service
{
    public static class ToastrService
    {
        private static readonly List<Toastr> _toastrs = new List<Toastr>();
        
        //private static string GetSessionId()
        //{

        //    return HttpContent.Current.Session.SessionID;
        //}

        public static void AddToUserQueue(Toastr toastr)
        {
            _toastrs.Add(new Toastr()
            {
                Message = toastr.Message,
                Title = toastr.Title,
                Type = toastr.Type
            });
        }

        public static void AddToUserQueue(string message, string title, ToastrType type)
        {
            AddToUserQueue(new Toastr(message, title, type));
        }



        public static List<Toastr> ReadUserQueue()
        {
            return _toastrs.ToList();
        }

        public static List<Toastr> ReadAndRemoveUserQueue()
        {
            var list = ReadUserQueue();
            RemoveUserQueue();
            return list;
        }
        public static void RemoveUserQueue()
        {
            _toastrs.Clear();
        }

    }
}
