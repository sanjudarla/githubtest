using HospitalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HospitalManagement.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DoctorController : ApiController
    {
        private readonly HMSystemEntities db = new HMSystemEntities();

        [HttpGet]
        public IEnumerable<Doctor> List()
        {
            return db.Doctors.ToList();
        }

        [HttpPost]
        public HttpResponseMessage AddDoctor(Doctor obj)
        {
            try
            {
                db.Doctors.Add(obj);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                return response;

            }
            catch (Exception)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }
        [HttpPut]
        public HttpResponseMessage Update(int id, Doctor obj)
        {
            try
            {
                if (id == obj.Doctor_Id)
                {
                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotModified);
                    return response;
                }

            }
            catch (Exception)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            Doctor obj = db.Doctors.Find(id);
            if (obj != null)
            {
                db.Doctors.Remove(obj);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }
        [HttpGet]
        public Doctor Search(int id)
        {
            Doctor obj = db.Doctors.Find(id);
            return obj;
        }
    }
}

