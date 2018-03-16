using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using VecturaServer.Interfaces;
using VecturaServer.Models;

namespace VecturaServer.Controllers
{
    public class VisitsController : ApiController
    {
        private IVisitsRepository<VisitModel> _VisitsRepository;

        public VisitsController(IVisitsRepository<VisitModel> repository)
        {
            _VisitsRepository = repository;
        }


        [HttpGet]
        [Route("api/Visits/GetUserVisits")]
        public async Task<IHttpActionResult> GetUserVisits([FromUri]string userName)
        {
            IEnumerable<VisitModel> result = null;
            try
            {
                result = await Task.Run(() =>
                {
                    return _VisitsRepository.Find(t => t.User == userName);
                });
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(
        Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message));
            }
            if (result != null)
            {
                return Ok(result);
            }
            
            return NotFound();
        }


        [HttpPost]
        [Route("api/Visits/Add")]
        public async Task<IHttpActionResult> AddVisit([FromBody]VisitModel Visit)
        {

            bool result = await Task.Run(() =>
            {
                try
                {
                    var identityClaims = (ClaimsIdentity)User.Identity;
                    var newVisit = new VisitModel()
                    {
                        User = identityClaims.FindFirstValue("UserName"),
                        Town = Visit.Town,
                        Country = Visit.Country,
                        Comment = Visit.Comment,
                        Raiting = Visit.Raiting
                    };

                    _VisitsRepository.Create(newVisit);
                    if (_VisitsRepository.Get(newVisit.Id) == null)
                        return false;
                    return true;
                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                }
            });

            if (result)
            {
                return Ok("Added");
            }
            else
            {
                return InternalServerError(new Exception("Can not add Visit to DB"));
            }
        }


        [HttpDelete]
        [Route("api/Visits/Delete")]
        public async Task<IHttpActionResult> DeleteUserVisit(int id)
        {

            if (_VisitsRepository.Get(id) == null)
            {
                return NotFound();
            }

            bool result = await Task.Run(() =>
            {
                try
                {
                    _VisitsRepository.Delete(id);
                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                }


                if (_VisitsRepository.Get(id) != null)
                    return false;
                else return true;
            });

            if (result)
            {
                return Ok("Deleted");
            }
            else
            {
                return InternalServerError(new Exception("Can not delete Visit from DB"));
            }
        }

        [HttpPut]
        [Route("api/Visits/UpdateComment")]
        public async Task<IHttpActionResult> UpdateComment(int id, string comment)
        {

            if (string.IsNullOrEmpty(comment) || string.IsNullOrWhiteSpace(comment))
            {
                return BadRequest("Empty string");
            }

            if (_VisitsRepository.Get(id) == null)
            {
                return NotFound();
            }

            bool result = await Task.Run(() =>
            {
                try
                {
                    var Visit = _VisitsRepository.Get(id);
                    Visit.Comment = comment;
                    _VisitsRepository.Update(Visit);
                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                }

                if (_VisitsRepository.Get(id) != null)
                    return true;
                return false;
            });

            if (result)
            {
                return Ok("Updated");
            }
            else
            {
                return InternalServerError(new Exception("Can not update the Visit"));
            }
        }
    
}
}
